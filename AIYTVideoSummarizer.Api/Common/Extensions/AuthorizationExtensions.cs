using AIYTVideoSummarizer.Domain.Enums;

namespace AIYTVideoSummarizer.Api.Common.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeSuperAdmin", policy =>
                policy.RequireAuthenticatedUser().RequireRole(UserRole.SuperAdmin.ToString()));

                options.AddPolicy("MustBeAdmin", policy =>
                policy.RequireAuthenticatedUser().RequireRole(UserRole.Admin.ToString()));

                options.AddPolicy("MustBeAdminOrSuperAdmin", policy =>
                policy.RequireAuthenticatedUser().RequireRole(UserRole.Admin.ToString(),UserRole.SuperAdmin.ToString()));

                options.AddPolicy("MustBeUser", policy =>
                policy.RequireAuthenticatedUser().RequireRole(UserRole.User.ToString()));
            });

            return services;
        }
    }
}
