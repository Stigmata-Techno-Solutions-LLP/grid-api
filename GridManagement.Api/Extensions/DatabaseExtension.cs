using GridManagement.domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GridManagement.Model.Dto;


namespace GridManagement.Api.Extensions
{

    public static class DatabaseExtension
    {
    
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, AppSettings app )
        {

            services.AddDbContextPool<gridManagementContext>(o =>
            {
                o.UseSqlServer(app.DBConn);
               // o.UseInMemoryDatabase(databaseName: "heroesdb");
            });

            return services;
        }
    }
}
