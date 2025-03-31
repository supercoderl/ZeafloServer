using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ZeafloServer.Domain.Settings;
using ZeafloServer.Presentation.Swagger;

namespace ZeafloServer.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Zeaflo Api",
                    Version = "v1",
                    Description = "Api for testing functions in server."
                });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. " +
                              "Use the /api/v1/user/login endpoint to generate a token",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.ParameterFilter<SortableFieldsAttributeFilter>();

                c.SupportNonNullableReferenceTypes();

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = CreateTokenValidationParameters(configuration);
                jwtOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Get token from query string or header Authorization
                        var accessToken = context.Request.Query["access_token"];

                        // Check if token is in header Authorization
                        if (string.IsNullOrEmpty(accessToken) &&
                            context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                        {
                            accessToken = context.HttpContext.Request.Headers["Authorization"]
                                .ToString()
                                .Replace("Bearer ", "");
                        }

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddOptions<TokenSettings>().Bind(configuration.GetSection("Auth")).ValidateOnStart();
            services.AddOptions<MailSettings>().Bind(configuration.GetSection("Mail")).ValidateOnStart();
            services.AddOptions<DeepseekSettings>().Bind(configuration.GetSection("Deepseek")).ValidateOnStart();

            return services;
        }

        public static TokenValidationParameters CreateTokenValidationParameters(IConfiguration configuration)
        {
            var result = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Auth:Issuer"],
                ValidAudience = configuration["Auth:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Secret"]!)),
                RequireSignedTokens = false
            };

            return result;
        }
    }
}
