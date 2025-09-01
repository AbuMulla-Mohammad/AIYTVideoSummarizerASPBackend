
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class SummarySectionRepository : GenericRepository<SummarySection, int>, ISummarySectionRepository
    {
        private readonly ApplicationDbContext _context;

        public SummarySectionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SummarySection>> GetBySummaryIdAsync(Guid summaryId)
        {
            return await _context.SummarySections
                .Where(ss => ss.SummaryId == summaryId)
                .OrderBy(ss => ss.StartTime)
                .ToListAsync();
        }
    }
}
