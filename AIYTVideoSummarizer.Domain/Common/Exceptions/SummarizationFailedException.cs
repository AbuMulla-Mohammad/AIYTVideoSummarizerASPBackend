

namespace AIYTVideoSummarizer.Domain.Common.Exceptions
{
    public class SummarizationFailedException : Exception
    {
        public SummarizationFailedException(string? message) : base(message)
        {
        }
    }
}
