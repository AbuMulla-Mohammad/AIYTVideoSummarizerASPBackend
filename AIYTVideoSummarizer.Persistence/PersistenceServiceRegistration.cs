
using AIYTVideoSummarizer.Persistence.Context;
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
            return services;
        }
    }
}
