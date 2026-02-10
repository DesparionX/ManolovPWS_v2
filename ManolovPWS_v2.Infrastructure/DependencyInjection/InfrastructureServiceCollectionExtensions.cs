using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure
            (
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString)
        {
            services.AddDatabase(configuration, connectionString);
            services.AddUserIdentity();
            
            return services;
        }
    }
}
