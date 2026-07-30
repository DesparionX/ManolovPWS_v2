using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Modules.Contact.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContactModule(this IServiceCollection services)
        {
            services.AddHandlers();

            return services;
        }
    }
}
