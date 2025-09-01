
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Persistence.Context;
using AIYTVideoSummarizer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIYTVideoSummarizer.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    config.GetConnectionString("DefaultConnection"),
                    npg => npg.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    );

            });

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserExternalLoginRepository, UserExternalLoginRepository>();
            services.AddScoped<ISummarySectionRepository, SummarySectionRepository>();
            services.AddScoped<ISummaryRepository, SummaryRepository>();
            services.AddScoped<ISummarizationRequestRepository, SummarizationRequestRepository>();
            return services;
        }
    }
}
