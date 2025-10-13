
using AIYTVideoSummarizer.Application.Interfaces.Common;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Infrastructure.Services
{
    public class UserNameGenerator : IUserNameGenerator
    {
        private readonly ApplicationDbContext _context;

        public UserNameGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateUniqueUserNameAsync(string email)
        {
            var baseName = email.Split('@')[0];
            var userName = baseName;
            int counter = 1;

            while(await _context.Users.AnyAsync(u => u.UserName == userName))
            {
                userName = $"{baseName}{counter}";
                counter++;
            }

            return userName;
        }
    }
}
