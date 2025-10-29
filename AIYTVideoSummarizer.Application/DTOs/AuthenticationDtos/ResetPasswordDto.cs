namespace AIYTVideoSummarizer.Application.DTOs.AuthenticationDtos
{
    public class ResetPasswordDto
    {
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
