﻿using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.AuthenticationCommands
{
    public class ResetPasswordCommand:IRequest<Unit>
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
