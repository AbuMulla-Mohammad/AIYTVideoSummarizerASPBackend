using AIYTVideoSummarizer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }

        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<Summary> Summaries { get; set; } = new List<Summary>();
        public ICollection<UserExternalLogin> ExternalLogins { get; set; } = new List<UserExternalLogin>();
        public ICollection<SummarizationRequest> SummarizationRequests { get; set; } = new List<SummarizationRequest>();

    }
}
