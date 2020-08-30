using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace GridManagement.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddApiDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "GridManagement.Api",
                        Version = "v1",
                        Description = "GridManagement de API",
                     
                    });
                c.DescribeAllParametersInCamelCase();
                c.OrderActionsBy(x => x.RelativePath);

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlPath);

            });
            return services;
        }

        public static IApplicationBuilder UseApiDoc(this IApplicationBuilder app)
        {
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.RoutePrefix = "api-docs";
                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
                   c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
               });
            return app;
        }
    }
}
