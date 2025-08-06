using AIYTVideoSummarizer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories
{
    public interface IVideoRepository:IGenericRepository<Video,Guid>
    {
    }
}
