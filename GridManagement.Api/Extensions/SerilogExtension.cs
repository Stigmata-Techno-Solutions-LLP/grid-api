using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using System;

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridManagement.Api.Extensions
{
    public static class SerilogExtension
    {
        public static Logger CreateLogger()
        {
            var configuration = LoadAppConfiguration();
            return new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .ReadFrom.Configuration(configuration)
                .Destructure.AsScalar<JObject>()
                .Destructure.AsScalar<JArray>()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Console(new RenderedCompactJsonFormatter())       
                .WriteTo.Debug(outputTemplate:DateTime.Now.ToString())          
                .WriteTo.File("logs/log.txt",rollingInterval:RollingInterval.Day)  
                .CreateLogger();
        }

        public static IApplicationBuilder UseCustomSerilogRequestLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(c =>
            {
                c.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
string data = ReadRequestBody(httpContext.Request);
                   // Log.Logger.Information(data);
                    //Add your useful information here
                };
            });

            return app;
        }

           private static string ReadRequestBody(HttpRequest request)
        {
            // request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
           // request.Body.Seek(0, SeekOrigin.Begin);
            return bodyAsText;
        }

        private static IConfigurationRoot LoadAppConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build();
        }
    }
}
