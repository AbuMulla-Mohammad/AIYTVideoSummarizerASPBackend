
namespace AIYTVideoSummarizer.Application.Interfaces.Common
{
    public interface IUserNameGenerator
    {
        Task<string> GenerateUniqueUserNameAsync(string email);
    }
}
