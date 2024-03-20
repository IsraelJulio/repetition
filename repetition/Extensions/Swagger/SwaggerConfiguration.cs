
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using repetition.Extensions.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace repetition.Extensions.DependencyInjection
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = config["SwaggerConfig:Title"],
                    Version = config["SwaggerConfig:Version"],
                    Description = config["SwaggerConfig:Description"]
                });

                c.DocumentFilter<SwaggerODataControllerDocumentFilter>();
            });
        }

        public static void EnableSwagger(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("./swagger/v1/swagger.json", config["SwaggerConfig:Title"]);
                opt.RoutePrefix = string.Empty;
            });
        }
    }
}