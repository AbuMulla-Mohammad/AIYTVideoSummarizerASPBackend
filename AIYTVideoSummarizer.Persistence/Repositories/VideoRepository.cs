
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class VideoRepository : GenericRepository<Video, Guid>, IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Video>?> GetByUserId(
            int pageNumber,
            int pageSize,
            Guid userId,
            Expression<Func<Video, bool>>? filter = null)
        {
            IQueryable<Video> query = _context.Videos
                .AsNoTracking()
                .Where(v => v.SummarizationRequest.Any(sr => sr.UserId == userId));

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

            return new PaginatedList<Video>(items, pageData);
        }

        public async Task<Video?> GetByYtIdAndPromptNameAsync(string videoYtId, string promptName)
        {
            return await _context.Videos
                .Include(v => v.Summaries)
                  .ThenInclude(s => s.Prompt)
                .FirstOrDefaultAsync(v => v.YouTubeVideoID == videoYtId
                   && v.Summaries.Any(s => s.Prompt != null && s.Prompt.Name == promptName));
        }

        public async Task<Video?> GetByYtIdAsync(string videoYtId)
        {
            return await _context.Videos
                .Include(v=>v.FormattedTranscripts)
                .FirstOrDefaultAsync(v => v.YouTubeVideoID == videoYtId);
        }
    }
}
