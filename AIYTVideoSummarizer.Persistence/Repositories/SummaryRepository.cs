
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
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

        public async Task<PaginatedList<Summary>> GetByUserIdAsync(
            int pageNumber,
            int pageSize,
            Guid userId,
            Expression<Func<Summary, bool>>? filter = null,
            params Expression<Func<Summary, object>>[] includes)
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
            
            if(filter is not null)
            {
                query = query
                    .Where(filter);
            }
            var totalItems = query.Count();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageData = new PageData(totalItems, pageSize, pageNumber);

            return new PaginatedList<Summary>(items, pageData);
        }

        public async Task<PaginatedList<Summary>> GetByVideoIdAsync(
            int pageNumber,
            int pageSize,
            Guid videoId,
            Expression<Func<Summary, bool>>? filter = null,
            params Expression<Func<Summary, object>>[] includes)
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
            if(filter is not null)
            {
                query = query
                    .Where(filter);
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageData = new PageData(totalItems, pageSize, pageNumber);

            return new PaginatedList<Summary>(items, pageData);
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

        public async Task<PaginatedList<Summary>> GetByPromptIdAsync(
            int pageNumber,
            int pageSize,
            Guid promptId,
            Expression<Func<Summary, bool>>? filter = null,
            params Expression<Func<Summary, object>>[] includes)
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

            if(filter is not null)
            {
                query = query
                    .Where(filter);
            }

            var totalItems = query.Count();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageData = new PageData(totalItems, pageSize, pageNumber);

            return new PaginatedList<Summary>(items, pageData);
        }
    }
}
