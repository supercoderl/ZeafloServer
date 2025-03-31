﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ZeafloServer.Presentation.Swagger
{
    public sealed class SortableFieldsAttributeFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (context.ParameterInfo is null)
            {
                return;
            }

            var attribute = context.ParameterInfo
                .GetCustomAttributes<SwaggerSortableFieldsAttribute>()
                .SingleOrDefault();

            if (attribute is null)
            {
                return;
            }

            var description = string.Join("<br/>", attribute.GetFields().Order());

            parameter.Description = $"{parameter.Description}<br><br/>" + $"**Allowd values:**<br/>{description}";
        }
    }
}
