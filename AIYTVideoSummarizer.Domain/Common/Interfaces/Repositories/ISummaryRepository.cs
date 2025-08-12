using AIYTVideoSummarizer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories
{
    public interface ISummaryRepository:IGenericRepository<Summary,Guid>
    {
        Task<IEnumerable<Summary>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Summary>> GetByVideoIdAsync(Guid videoId);
        Task<Summary?> GetByYtVideoIdAndPromptIdAsync(string ytId, Guid promptId);
    }
}
