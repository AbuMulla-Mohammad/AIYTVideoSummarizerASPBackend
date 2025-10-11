
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class SummaryRepository : GenericRepository<Summary, Guid>, ISummaryRepository
    {
        private readonly ApplicationDbContext _context;

        public SummaryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Summary>> GetByUserIdAsync(Guid userId, params Expression<Func<Summary, object>>[] includes)
        {
            IQueryable<Summary> query = _context.Summaries;

            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query
                        .Include(include);
                }
            }
            return await query
                .Where(s => s.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Summary>> GetByVideoIdAsync(Guid videoId, params Expression<Func<Summary, object>>[] includes)
        {
            IQueryable<Summary> query = _context.Summaries;

            if(includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query
                        .Include(include);
                }
            }
            return await query
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

        public async Task<IEnumerable<Summary>> GetByPromptIdAsync(Guid promptId, params Expression<Func<Summary, object>>[] includes)
        {
            IQueryable<Summary> query= _context.Summaries;

            if(includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query
                        .Include(include);
                }
            }

            return await query
                .Where(s => s.PromptId == promptId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
