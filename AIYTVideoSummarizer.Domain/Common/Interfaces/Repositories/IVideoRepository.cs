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
    public interface IVideoRepository:IGenericRepository<Video,Guid>
    {
        Task<Video?> GetByYtIdAndPromptNameAsync(string videoYtId, string promptName);
        Task<Video?> GetByYtIdAsync(string videoYtId);
        Task<PaginatedList<Video>?> GetByUserId(
            int pageNumber,
            int pageSize,
            Guid userId,
            Expression<Func<Video, bool>>? filter = null);
    }
}
