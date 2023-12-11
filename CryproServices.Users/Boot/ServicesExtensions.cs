using CryproServices.Users.Infrastructure;
using CryproServices.Users.Infrastructure.Repositories;
using CryproServices.Users.Infrastructure.Services;
using CryproServices.Users.Infrastructure.Shared.Repositories;
using CryproServices.Users.Infrastructure.Shared.Services;
using CryptoServices.Data;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.AddTransient<IHashService, HashService>();
            services.AddTransient<IJWTService, JWTService>();

            return services;
        }

        public static IServiceCollection AddJWTAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JWT")["secretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
