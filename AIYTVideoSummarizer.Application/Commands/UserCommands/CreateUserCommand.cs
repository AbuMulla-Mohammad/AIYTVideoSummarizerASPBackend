using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.UserCommands
{
    public class CreateUserCommand:IRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
