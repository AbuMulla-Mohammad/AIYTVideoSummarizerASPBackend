using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User,Guid>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailConfirmationTokenAsync(string token)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
        }

        public async Task<User?> GetWithExternalLoginsAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.ExternalLogins)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetWithSummariesAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Summaries)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
