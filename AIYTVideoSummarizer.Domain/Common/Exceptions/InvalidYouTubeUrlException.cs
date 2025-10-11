
namespace AIYTVideoSummarizer.Domain.Common.Exceptions
{
    public class InvalidYouTubeUrlException : Exception
    {
        public InvalidYouTubeUrlException(string? message) : base(message)
        {
        }
    }
}
