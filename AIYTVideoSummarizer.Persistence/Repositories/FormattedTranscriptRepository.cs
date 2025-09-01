
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class FormattedTranscriptRepository : GenericRepository<FormattedTranscript, int>, IFormattedTranscriptRepository
    {
        private readonly ApplicationDbContext _context;

        public FormattedTranscriptRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FormattedTranscript>> GetByVideoIdAsync(Guid videoId)
        {
            return await _context.FormattedTranscripts
                .Where(ft => ft.VideoId == videoId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
