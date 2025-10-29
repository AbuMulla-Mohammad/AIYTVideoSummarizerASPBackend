using AIYTVideoSummarizer.Infrastructure.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AIYTVideoSummarizer.Api.Common.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationServices(
            this IServiceCollection services,
            JwtOptions jwtOptions)
        {

            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SecretForKey)),
                    };
                });
            return services;
        }
    }
}
