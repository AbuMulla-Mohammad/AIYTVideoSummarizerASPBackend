using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.AuthenticationCommands
{
    public class RegisterCommand:IRequest<Unit>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
    }
}
