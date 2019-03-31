using eshop.Logger;
using eshop.Models;
using eshop.Repositories.Core;
using eshop.Services;
using eshop.Services.Core;
using eshop.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Extension
{
    public static class ServicesExtension
    {
        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
        }

        public static void AddSqlServerContext(this IServiceCollection services, IConfiguration Configuration)
        {
            var connection = Configuration["ConnectionString"];
            services.AddDbContext<eshopContext>(options =>
            {
                options.UseSqlServer(connection);
            });
        }

        public static void ConfigureNLog(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void AddScopedServices(this IServiceCollection services)
        {
            // Injected containers
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IServicesWrapper, ServicesWrapper>();

            // Injected services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IArticlesService, ArticlesService>();
        }

        // Configuration de l'authentication par jwt pour décoder le jwt reçu par les api avec l'attribut Authorize
        public static void AddAuthenticationPolicy(this IServiceCollection services, IConfiguration Configuration)
        {
            /********Method : https://github.com/cornflourblue/aspnet-core-jwt-authentication-api *******/
            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
