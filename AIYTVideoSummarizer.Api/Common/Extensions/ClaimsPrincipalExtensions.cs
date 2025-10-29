using System.Security.Claims;

namespace AIYTVideoSummarizer.Api.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserIdOrThrow(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("Unauthorized access.");

            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("Unauthorized access.");

            return userId;

        }
    }
}
