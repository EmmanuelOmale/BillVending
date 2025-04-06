using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<IdentityServiceDbContext>((sp, options) => 
            {
                var interceptor = sp.GeteRequiredService<AuditableEntitySaveChangesInterceptor>();
                options.UseNpgsql(connStr);
                options.AddInterceptors(interceptor);
            });

            return services;
        } 
    }
}