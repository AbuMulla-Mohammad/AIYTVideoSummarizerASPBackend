using AIYTVideoSummarizer.Application.Interfaces.ExternalServices;
using AIYTVideoSummarizer.Infrastructure.ExternalServices.AIService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIYTVideoSummarizer.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            services.AddOptions<AISummarizerOptions>()
                .Bind(config.GetSection(AISummarizerOptions.SectionName))
                .ValidateDataAnnotations()
                .Validate(o => Uri.IsWellFormedUriString(o.BaseUrl, UriKind.Absolute),
                        "AIService:BaseUrl must be a valid absolute URI")
                .ValidateOnStart();

            return services;

        }
    }
}
