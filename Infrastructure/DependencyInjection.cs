using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnectionString");

            services.AddDbContext<ApplicationDbContext>((services, options) => 
            {
                options.UseNpgsql(connStr);
            });

            return services;
        }
    }
}