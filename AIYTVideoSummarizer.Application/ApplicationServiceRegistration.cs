using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AIYTVideoSummarizer.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(configuration=>configuration.RegisterServicesFromAssembly(assembly));
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
