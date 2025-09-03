using AIYTVideoSummarizer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories
{
    public interface IFormattedTranscriptRepository:IGenericRepository<FormattedTranscript,int>
    {
        Task<IEnumerable<FormattedTranscript>> GetByVideoIdAsync(Guid videoId);
    }
}
