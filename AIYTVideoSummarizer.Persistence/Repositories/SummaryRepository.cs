
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class SummaryRepository : GenericRepository<Summary, Guid>, ISummaryRepository
    {
        private readonly ApplicationDbContext _context;

        public SummaryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Summary>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Summaries
                .Where(s => s.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Summary>> GetByVideoIdAsync(Guid videoId)
        {
            return await _context.Summaries
                .Where(s => s.VideoId == videoId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Summary?> GetByYtVideoIdAndPromptIdAsync(string ytId, Guid promptId)
        {
            return await _context.Summaries
                .Include(s => s.Video)
                .ThenInclude(v=>v.FormattedTranscripts)
                .Include(s=>s.SummarySections)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Video.YouTubeVideoID == ytId && s.PromptId == promptId);
        }
    }
}
