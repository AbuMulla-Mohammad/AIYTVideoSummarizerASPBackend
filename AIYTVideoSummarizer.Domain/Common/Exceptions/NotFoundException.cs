namespace AIYTVideoSummarizer.Domain.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(
            string entityName,
            object? Id = null) :base(Id!=null
            ?$"{entityName} with '{Id}' was not found."
            :$"{entityName} was not found.")
        {
        }
    }
}
