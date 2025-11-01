
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
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

        public async Task<PaginatedList<SummarizationRequest>> GetByStatusAsync(
            int pageNumber,
            int pageSize,
            RequestStatus status)
        {
            IQueryable<SummarizationRequest> query = _context.SummarizationRequests
                .AsNoTracking()
                .Where(s=>s.RequestStatus==status);

            var totalItems = query.Count();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageData = new PageData(totalItems, pageSize, pageNumber);

            return new PaginatedList<SummarizationRequest>(items, pageData);
        }

        public async Task<PaginatedList<SummarizationRequest>> GetByUserIdAsync(
            int pageNumber,
            int pageSize,
            Guid userId)
        {

            IQueryable<SummarizationRequest> query = _context.SummarizationRequests
                 .AsNoTracking()
                 .Where(s => s.UserId == userId);

            var totalItems = query.Count();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageData = new PageData(totalItems, pageSize, pageNumber);

            return new PaginatedList<SummarizationRequest>(items, pageData);
        }
    }
}
