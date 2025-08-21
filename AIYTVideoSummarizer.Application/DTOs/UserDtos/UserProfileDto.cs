﻿
namespace AIYTVideoSummarizer.Application.DTOs.UserDtos
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public string Role { get; set; } = "User";
        public List<string> ExternalProviders { get; set; } = new();
    }
}
