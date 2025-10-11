
namespace AIYTVideoSummarizer.Domain.Common.Exceptions
{
    public class ExternalAIServiceException : Exception
    {
        public ExternalAIServiceException(string? message) : base(message)
        {
        }
    }
}
