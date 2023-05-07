using bioTekno.OrderProject.DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bioTekno.OrderProject.DataAccess.Uow;
using bioTekno.OrderProject.Business.Interfaces;
using bioTekno.OrderProject.Business.Services;
using StackExchange.Redis;

namespace bioTekno.OrderProject.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {

        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            


            services.AddDbContextPool<bioTeknoContext>(opt =>
            {
                opt.UseMySql(configuration.GetConnectionString("Local"), new MySqlServerVersion(new Version()));
            });



            var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));

            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSingleton<ICacheService, RedisCacheService>();

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IOrderService, OrderService>();

        }
    }
}
