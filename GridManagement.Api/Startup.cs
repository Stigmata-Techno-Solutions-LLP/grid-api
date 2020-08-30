using GridManagement.Api.Extensions;
using GridManagement.Api.Helper;
using GridManagement.repository;
using GridManagement.service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace GridManagement.Api
{
    public class Startup
    {


        public IConfiguration Configuration { get; }
        // public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public  void ConfigureServices(IServiceCollection services)
        {
            //Extension method for less clutter in startup
            services.AddApplicationDbContext();

            //DI Services and Repos
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IHeroAppService, HeroAppService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IGridRepository, GridRepository>();
            services.AddScoped<IGridService, GridService>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors();
            // WebApi Configuration
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // for enum as strings
            });

            // AutoMapper settings
            services.AddAutoMapperSetup();

            // HttpContext for log enrichment 
            services.AddHttpContextAccessor();

            // Swagger settings
            services.AddApiDoc();
            // GZip compression
            services.AddCompression();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCustomSerilogRequestLogging();
            app.UseRouting();
            app.UseApiDoc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //added request logging


            app.UseHttpsRedirection();


            app.UseResponseCompression();

        }
    }
}
