
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Persistence.Context;

namespace AIYTVideoSummarizer.Persistence.Repositories
{
    public class PromptRepository : GenericRepository<Prompt, Guid>, IPromptRepository
    {
        public PromptRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
