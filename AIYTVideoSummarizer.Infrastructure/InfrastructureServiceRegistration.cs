using AIYTVideoSummarizer.Application.Interfaces.Common;
using AIYTVideoSummarizer.Application.Interfaces.ExternalServices;
using AIYTVideoSummarizer.Application.Interfaces.Security;
using AIYTVideoSummarizer.Application.Models.Email;
using AIYTVideoSummarizer.Infrastructure.ExternalServices.AIService;
using AIYTVideoSummarizer.Infrastructure.Security;
using AIYTVideoSummarizer.Infrastructure.Services;
using AIYTVideoSummarizer.Infrastructure.Services.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.AddHttpClient<IAIService, AISummarizerClient>((provider, client) =>
            {
            var options = provider.GetRequiredService<IOptions<AISummarizerOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

                if (options.DefaultHeaders != null)
                {
                    foreach (var header in options.DefaultHeaders)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            });

            services.AddSingleton<IPasswordHasher, Argon2PasswordHasher>();
            services.AddScoped<IUserNameGenerator, UserNameGenerator>();
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            services.Configure<EmailConfiguration>(config.GetSection("EmailSettings"));

            return services;

        }
    }
}
