using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories
{
    public interface ISummarizationRequestRepository:IGenericRepository<SummarizationRequest,Guid>
    {
        Task<PaginatedList<SummarizationRequest>> GetByUserIdAsync(
            int pageNumber,
            int pageSize,
            Guid userId);
        Task<PaginatedList<SummarizationRequest>> GetByStatusAsync(
            int pageNumber,
            int pageSize,
            RequestStatus status);
        Task<IEnumerable<SummarizationRequest>> GetByPromptIdAsync(Guid promptId);
    }
}
