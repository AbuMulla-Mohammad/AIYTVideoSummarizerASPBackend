using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
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
        Task<PaginatedList<Summary>> GetByUserIdAsync(
            int pageNumber,
            int pageSize,
            Guid userId,
            Expression<Func<Summary, bool>>? filter = null,
            params Expression<Func<Summary, object>>[] includes);
        Task<PaginatedList<Summary>> GetByVideoIdAsync(
            int pageNumber,
            int pageSize,
            Guid videoId,
            Expression<Func<Summary, bool>>? filter = null,
            params Expression<Func<Summary, object>>[] includes);
        Task<PaginatedList<Summary>> GetByPromptIdAsync(
            int pageNumber,
            int pageSize,
            Guid promptId,
            Expression<Func<Summary, bool>>? filter = null,
            params Expression<Func<Summary, object>>[] includes);
        Task<Summary?> GetByYtVideoIdAndPromptIdAsync(string ytId, Guid promptId);
    }
}
