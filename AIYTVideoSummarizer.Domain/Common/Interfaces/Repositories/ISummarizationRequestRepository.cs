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
        Task<IEnumerable<SummarizationRequest>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<SummarizationRequest>> GetByStatusAsync(RequestStatus status);
        Task<IEnumerable<SummarizationRequest>> GetByPromptIdAsync(Guid promptId);
    }
}
