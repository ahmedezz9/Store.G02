using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //builder.Services.AddAutoMapper(typeof(AsssemblyRefrence).Assembly);
            services.AddAutoMapper(cfg => { }, typeof(AsssemblyRefrence).Assembly);
            services.AddScoped<IServiceManger, ServiceManger>();
            return services;
        }
    }
}
