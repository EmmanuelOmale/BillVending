using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection; 
using Application.UserWallet.Repository;
using System.Reflection;
using MediatR;
using Application.Services.Interfaces;
using Application.Services.Implementation;
using Application.Services;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // services.AddMediatR(cfg =>
            // {
            //     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // });
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Repository registrations
            services.AddScoped<IWalletRepository, WalletRepository>();

            //Services
            services.AddScoped<Application.UserWallet.Interfaces.IWalletService, Application.Services.WalletService>();

            services.AddSingleton<IWalletLockProvider, WalletLockProvider>();

            services.AddScoped<IBillVendorService, BillVendorService>();

            return services;
        }
    }
}