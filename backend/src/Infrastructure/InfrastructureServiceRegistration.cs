using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.ImageManagement;
using Ecommerce.Application.Models.Token;
using Ecommerce.Application.Persistence;
using Infrastructure.MessageImplementation;
using Infrastructure.Repositories;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddTransient<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings")); //matchear la clase JwtSettings con JwtSettings de appsetings.json
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));


            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}