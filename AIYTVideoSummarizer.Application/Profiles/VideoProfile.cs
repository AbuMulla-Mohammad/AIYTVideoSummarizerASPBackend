

using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class VideoProfile:Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoDto>();
            CreateMap<Video, CreateVideoDto>();
            CreateMap<Video, UpdateVideoDto>();
            CreateMap<Video, VideoSummaryResponseDto>()
                .ForMember(dest => dest.SummarySections, opt => opt.MapFrom(src => src.Summaries.SelectMany(s => s.SummarySections)))
                .ForMember(dest => dest.FormattedTranscripts, opt => opt.MapFrom(src => src.FormattedTranscripts));


            CreateMap<CreateVideoCommand, Video>();
            CreateMap<UpdateVideoCommand, Video>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
