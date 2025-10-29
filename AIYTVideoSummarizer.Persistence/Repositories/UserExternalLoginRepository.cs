
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class UserExternalLoginRepository : GenericRepository<UserExternalLogin, Guid>, IUserExternalLoginRepository
    {
        private readonly ApplicationDbContext _context;

        public UserExternalLoginRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserExternalLogin?> GetByProviderAsync(string providerName, string subject)
        {
            return await _context.UserExternalLogins
                 .Include(ex => ex.User)
                 .FirstOrDefaultAsync(ex => ex.LoginProvider == providerName && ex.ProviderKey == subject);
        }

        public async Task<IEnumerable<UserExternalLogin>?> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserExternalLogins
                    .Where(ex => ex.UserId == userId)
                    .ToListAsync();
        }
    }
}
