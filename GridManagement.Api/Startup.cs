using GridManagement.Api.Extensions;
using GridManagement.repository;
using GridManagement.Model.Dto;
using GridManagement.service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using GridManagement.Api.Helper;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using GridManagement.Api.Helper;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using OwaspHeaders.Core.Extensions;
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

          // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<GridManagement.Model.Dto.AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<GridManagement.Model.Dto.AppSettings>();
            //Extension method for less clutter in startup
            services.AddApplicationDbContext(appSettings);
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //DI Services and Repos
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPageAccessService, PageAccessService>();
            services.AddScoped<IPageAccessRepository, PageAccessRepository>();
            services.AddScoped<IGridRepository, GridRepository>();
            services.AddScoped<IGridService, GridService>();
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");


            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<ISubContService, SubContService>();
            services.AddScoped<ISubContractorRepository, SubContractorRepository>();
          
            services.Configure<GridManagement.Model.Dto.AppSettings>(Configuration.GetSection("AppSettings"));


           // services.AddCors();
            //app.UseCors(options => options.AllowAnyOrigin());  

        // services.AddCors(  
        //     options => options.AddPolicy("AllowCors",  
        //         builder => {  
        //             builder  
        //             //.WithOrigins("http://localhost:4456") //AllowSpecificOrigins;  
        //             //.WithOrigins("http://localhost:4456", "http://localhost:4457") //AllowMultipleOrigins;  
        //                 .AllowAnyOrigin() //AllowAllOrigins;  
  
        //             //.WithMethods("GET") //AllowSpecificMethods;  
        //             //.WithMethods("GET", "PUT") //AllowSpecificMethods;  
        //             //.WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;  
        //             .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;  
        //                 //.AllowAnyMethod() //AllowAllMethods;  
  
        //             //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header"); //AllowSpecificHeaders;  
        //             .AllowAnyHeader(); //AllowAllHeaders;  
        //         })  
        // );  

        services.AddCors(options => {
            options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });




 services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)  

  .ConfigureApiBehaviorOptions(options => {  


   options.InvalidModelStateResponseFactory = actionContext => {  
ValidateModelAttribute val = new ValidateModelAttribute();
    return val.CustomErrorResponse(actionContext);  
   };  
  });  
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

            
 services.AddControllersWithViews();
    services.AddDirectoryBrowser();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCors("AllowCors");  
           // app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    
           // app.UseCustomSerilogRequestLogging();
            app.UseCustomSerilogRequestLogging();

            app.UseRouting();
            app.UseStaticFiles();
       if (!Directory.Exists("./Images"))    Directory.CreateDirectory("./Images");
           app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "Images")),
        RequestPath = "/Images"
    });

    app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "Images")),
        RequestPath = "/Images"
    });

            app.UseCors("AllowAll");

            app.UseApiDoc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //added request logging


            app.UseHttpsRedirection();
            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<ApiLoggingMiddleware>();

            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSecureHeadersMiddleware(SecureHeadersMiddlewareExtensions.BuildDefaultConfiguration());
            app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
           // app.UseCors(options => options.AllowAnyOrigin());  


        
        
}
    }
}