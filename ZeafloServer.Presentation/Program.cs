
using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using RabbitMQ.Client;
using ZeafloServer.Application.Extensions;
using ZeafloServer.Application.Hubs;
using ZeafloServer.Domain.Consumers;
using ZeafloServer.Domain.Extensions;
using ZeafloServer.Infrastructure.Database;
using ZeafloServer.Infrastructure.Extensions;
using ZeafloServer.Presentation.Extensions;
using ZeafloServer.Presentation.Middlewares;


namespace ZeafloServer.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var rabbitConfiguration = builder.Configuration.GetRabbitMqConfiguration();

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services
                .AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>()
                .AddApplicationStatus()
                .AddNpgSql(builder.Configuration["ConnectionStrings:DefaultConnection"]!)
                .AddRedis(builder.Configuration["RedisHostName"]!, "Redis")
                .AddRabbitMQ(
                    async _ =>
                    {
                        var factory = new ConnectionFactory
                        {
                            Uri = new Uri(rabbitConfiguration.ConnectionString)
                        };

                        return await factory.CreateConnectionAsync();
                    },
                    name: "RabbitMQ"
                );

            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseLazyLoadingProxies();
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("ZeafloServer.Infrastructure"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("policy", x =>
                    x.SetIsOriginAllowed(x => _ = true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });

            builder.Services.AddHttpClient();
            builder.Services.AddSwagger();
            builder.Services.AddAuth(builder.Configuration);
            builder.Services.AddCommandHandlers();
            builder.Services.AddInfrastructure(builder.Configuration, "ZeafloServer.Infrastructure");
            builder.Services.AddNotificationHandlers();
            builder.Services.AddApiUser();
            builder.Services.AddHelpers();
            builder.Services.AddServices();
            builder.Services.AddAccounts();
            builder.Services.AddQueryHandlers();
            builder.Services.AddSortProviders();
            builder.Services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly); });

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<FanoutEventConsumer>();
                x.AddConsumer<GenerateQRCodeConsumer>();
                x.AddConsumer<UploadConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureNewtonsoftJsonSerializer(settings =>
                    {
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        settings.NullValueHandling = NullValueHandling.Ignore;
                        return settings;
                    });
                    cfg.UseNewtonsoftJsonSerializer();
                    cfg.ConfigureNewtonsoftJsonDeserializer(settings =>
                    {
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        settings.NullValueHandling = NullValueHandling.Ignore;
                        return settings;
                    });

                    cfg.Host(rabbitConfiguration.Host, (ushort)rabbitConfiguration.Port, "/", h => {
                        h.Username(rabbitConfiguration.Username);
                        h.Password(rabbitConfiguration.Password);
                    });

                    // Every instance of the service will receive the message
                    cfg.ReceiveEndpoint("zeaflo-fanout-event-" + Guid.NewGuid(), e =>
                    {
                        e.Durable = false;
                        e.AutoDelete = true;
                        e.ConfigureConsumer<FanoutEventConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint("zeaflo-qr-code-queue", e =>
                    {
                        e.ConfigureConsumer<GenerateQRCodeConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    cfg.ReceiveEndpoint("zeaflo-upload-queue", e =>
                    {
                        e.ConfigureConsumer<UploadConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            builder.Services.AddLogging(x => x.AddSimpleConsole(console =>
            {
                console.TimestampFormat = "[yyyy-MM-ddTHH:mm:ss.fff]";
                console.IncludeScopes = true;
            }));
            builder.Services.AddDistributedMemoryCache();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var appDbContext = services.GetRequiredService<ApplicationDbContext>();
                var storeDbContext = services.GetRequiredService<EventStoreDbContext>();
                var domainStoreDbContext = services.GetRequiredService<DomainNotificationStoreDbContext>();

                appDbContext.EnsureMigrationsApplied();
                appDbContext.SeedDatabase();
                storeDbContext.EnsureMigrationsApplied();
                domainStoreDbContext.EnsureMigrationsApplied();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapGrpcReflectionService();
            }

            app.UseCors("policy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllers();
            app.MapHub<ChatHub>("/tracker");
            app.UseMiddleware<ExceptionMiddleware>();

            app.Run();
        }
    }
}
