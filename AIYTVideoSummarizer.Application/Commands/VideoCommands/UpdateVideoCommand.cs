using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Application.Commands.VideoCommands
{
    class UpdateVideoCommand:IRequest<UpdateVideoDto>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? ChannelName { get; set; }
    }
}
