
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class SummarizationRequestRepository : GenericRepository<SummarizationRequest, Guid>, ISummarizationRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public SummarizationRequestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SummarizationRequest>> GetByPromptIdAsync(Guid promptId)
        {
            return await _context.SummarizationRequests
                .Where(sr => sr.PromptId == promptId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<SummarizationRequest>> GetByStatusAsync(RequestStatus status)
        {
            return await _context.SummarizationRequests
                .Where(sr => sr.RequestStatus == status)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<SummarizationRequest>> GetByUserIdAsync(Guid userId)
        {
            return await _context.SummarizationRequests
                .Where(sr => sr.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
