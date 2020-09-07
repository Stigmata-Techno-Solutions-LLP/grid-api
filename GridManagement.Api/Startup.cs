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
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //DI Services and Repos
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IHeroAppService, HeroAppService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPageAccessService, PageAccessService>();
            services.AddScoped<IPageAccessRepository, PageAccessRepository>();
            services.AddScoped<IGridRepository, GridRepository>();
            services.AddScoped<IGridService, GridService>();
            

          // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<GridManagement.Model.Dto.AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<GridManagement.Model.Dto.AppSettings>();
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

        services.AddCors(  
            options => options.AddPolicy("AllowCors",  
                builder => {  
                    builder  
                    //.WithOrigins("http://localhost:4456") //AllowSpecificOrigins;  
                    //.WithOrigins("http://localhost:4456", "http://localhost:4457") //AllowMultipleOrigins;  
                        .AllowAnyOrigin() //AllowAllOrigins;  
  
                    //.WithMethods("GET") //AllowSpecificMethods;  
                    //.WithMethods("GET", "PUT") //AllowSpecificMethods;  
                    //.WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;  
                    .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;  
                        //.AllowAnyMethod() //AllowAllMethods;  
  
                    //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header"); //AllowSpecificHeaders;  
                    .AllowAnyHeader(); //AllowAllHeaders;  
                })  
        );  
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
            app.UseMiddleware<JwtMiddleware>();

            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseAuthorization();
           // app.UseCors(options => options.AllowAnyOrigin());  
app.UseCors("AllowCors");  

        }
    }
}
