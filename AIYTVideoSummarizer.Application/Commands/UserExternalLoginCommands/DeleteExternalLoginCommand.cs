
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.UserExternalLoginCommands
{
    public class DeleteExternalLoginCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
