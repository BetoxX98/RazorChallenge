using DataAcces.Context;
using DataAcces.Repositories;
using DataAcces.Repositories.Base;
using Domain.Entities;
using Infrastructure.Interfaces.Context;
using Infrastructure.Interfaces.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Repositories.Base;
using Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;

namespace Ioc
{
    public static class DependencyInjector
    {
        private static IServiceCollection Services { get; set; }

        public static void AddDbContext<T>(IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseSqlServer(connectionString, x => x.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(15), errorNumbersToAdd: null)));
        }

        public static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;

            RegisterAuthoritzationServiceLayer(services);
            RegisterRepositoryLayer(services);
            RegisterServiceLayer(services);
            RegisterConfigurations(services, configuration);

            return Services;
        }

        private static void RegisterAuthoritzationServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IUser, User>();
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IJwtFactory, JwtFactory>();
        }

        private static void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
        }

        private static void RegisterRepositoryLayer(IServiceCollection services)
        {
            services.AddScoped<IApiContext, ApiContext>();
            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        }

        public static void RegisterConfigurations(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
        }
    }
}
