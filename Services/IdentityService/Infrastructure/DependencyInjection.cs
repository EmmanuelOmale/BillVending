using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Common.Interceptors;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnectionString");

            services.AddSingleton<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<IdentityServiceDbContext>((sp, options) => 
            {
                var interceptor = sp.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
                options.UseNpgsql(connStr);
                options.AddInterceptors(interceptor);
            });

            return services;
        } 
    }
}