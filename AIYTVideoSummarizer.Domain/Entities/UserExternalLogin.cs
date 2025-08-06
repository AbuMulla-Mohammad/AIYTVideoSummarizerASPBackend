using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class UserExternalLogin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LoginProvider { get; set; } = string.Empty;
        public string ProviderKey { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
