using AIYTVideoSummarizer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories
{
    public interface IUserRepository:IGenericRepository<User,Guid>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetWithSummariesAsync(Guid userId);
        Task<User?> GetWithExternalLoginsAsync(Guid userId);
    }
}
