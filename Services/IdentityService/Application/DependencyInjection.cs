using System.Text;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<IdentityServiceDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IIdentityAppService, IdentityAppService>();

            var secretKey = jwtSettings["Secret"];
            var keyBytes = Convert.FromBase64String(secretKey);

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = jwtSettings["Issuer"],
                            ValidAudience = jwtSettings["Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true
                        };
                    });

            return services;
        }
    }
}
