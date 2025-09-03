
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class VideoRepository : GenericRepository<Video, Guid>, IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>?> GetByUserId(Guid userId)
        {
            return await _context.Videos
                .Where(v => v.SummarizationRequest.Any(sr => sr.UserId == userId))
                .ToListAsync();
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
                .FirstOrDefaultAsync(v => v.YouTubeVideoID == videoYtId);
        }
    }
}
