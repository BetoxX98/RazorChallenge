using Common.Constants;
using DataAcces.Context;
using Ioc;

namespace TechChallengeApi.BuilderExtensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString(GenericSettings.ApiDatabase);
            DependencyInjector.AddDbContext<ApiContext>(services, connection);

            services.AddHttpContextAccessor();
            services.AddHttpClient();

            DependencyInjector.RegisterServices(services, configuration);

            return services;
        }
    }
}
