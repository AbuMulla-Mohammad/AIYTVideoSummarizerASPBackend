
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.UserExternalLoginCommands
{
    public class CreateExternalLoginCommand:IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = string.Empty;
        public string ProviderKey { get; set; } = string.Empty;
    }
}
