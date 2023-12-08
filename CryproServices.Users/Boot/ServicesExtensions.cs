using CryproServices.Users.Infrastructure;
using CryproServices.Users.Infrastructure.Repositories;
using CryproServices.Users.Infrastructure.Services;
using CryproServices.Users.Infrastructure.Shared.Repositories;
using CryproServices.Users.Infrastructure.Shared.Services;
using CryptoServices.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CryproServices.Users.API.Boot
{
    public static class ServicesExtensions
    {
        public static IServiceCollection RegisterRabbitMq(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    var mqSection = configuration.GetSection("RabbitMQ");
                    cfg.Host(new Uri(mqSection["RootUri"]), h =>
                    {
                        h.Username(mqSection["UserName"]);
                        h.Password(mqSection["Password"]);
                    });
                });
            });

            return services;
        }

        public static IServiceCollection RegisterDB(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<DBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"),
                    x => x.MigrationsHistoryTable("__efmigrationshistory", "public")));

            return services;
        }

        public static IServiceCollection RegisterDBServices(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();

            services.AddTransient<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
