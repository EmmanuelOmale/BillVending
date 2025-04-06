using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interceptors;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnectionString");

            services.AddSingleton<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var interceptor = sp.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
                options.UseNpgsql(connStr);
                options.AddInterceptors(interceptor); // This should now work after the upgrade
            });

            return services;
        }
    }
}
