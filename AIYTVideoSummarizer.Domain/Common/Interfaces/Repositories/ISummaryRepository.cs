using AIYTVideoSummarizer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories
{
    public interface ISummaryRepository:IGenericRepository<Summary,Guid>
    {
        Task<IEnumerable<Summary>> GetByUserIdAsync(Guid userId, params Expression<Func<Summary, object>>[] includes);
        Task<IEnumerable<Summary>> GetByVideoIdAsync(Guid videoId, params Expression<Func<Summary, object>>[] includes);
        Task<IEnumerable<Summary>> GetByPromptIdAsync(Guid promptId, params Expression<Func<Summary, object>>[] includes);
        Task<Summary?> GetByYtVideoIdAndPromptIdAsync(string ytId, Guid promptId);
    }
}
