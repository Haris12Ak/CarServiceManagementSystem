using Application.Authorization;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Configuration
{
    public static class ServiceRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IKeycloakAuthService, KeycloakAuthService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICurrentSystemUserService, CurrentSystemUserService>();
            services.AddScoped<ICompanyAuthorizationService, CompanyAuthorizationService>();

            return services;
        }
    }
}
