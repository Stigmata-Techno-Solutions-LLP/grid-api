using GridManagement.domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GridManagement.Api.Extensions
{

    public static class DatabaseExtension
    {
    

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {

            services.AddDbContextPool<gridManagementContext>(o =>
            {

                o.UseSqlServer("Server=landt.ctxkj3vcelr3.ap-southeast-1.rds.amazonaws.com;Database=gridManagement;User Id=admin;Password=PlH34cwug3tqupePJcAp;");
               // o.UseInMemoryDatabase(databaseName: "heroesdb");
            });

            return services;
        }
    }
}
