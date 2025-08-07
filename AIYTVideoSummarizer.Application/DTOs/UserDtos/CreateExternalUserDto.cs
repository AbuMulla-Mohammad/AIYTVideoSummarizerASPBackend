
namespace AIYTVideoSummarizer.Application.DTOs.UserDtos
{
    public class CreateExternalUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string LoginProvider { get; set; } = string.Empty;
        public string ProviderKey { get; set; } = string.Empty;
    }
}
