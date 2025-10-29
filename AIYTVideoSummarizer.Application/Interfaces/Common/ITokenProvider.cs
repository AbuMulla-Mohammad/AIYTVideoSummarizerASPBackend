using System.Security.Claims;

namespace AIYTVideoSummarizer.Application.Interfaces.Common
{
    public interface ITokenProvider
    {
        Task<string> GenerateToken(List<Claim> claims);
    }
}
