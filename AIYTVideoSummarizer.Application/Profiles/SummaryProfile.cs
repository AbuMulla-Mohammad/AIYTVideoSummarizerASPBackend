
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class SummaryProfile:Profile
    {
        public SummaryProfile()
        {
            CreateMap<Summary, SummaryDetailsDto>();
            CreateMap<Summary, VideoSummaryResponseDto>()
                .ForMember(dest => dest.SummarySections, opt => opt.MapFrom(src => src.SummarySections))
                .ForMember(dest => dest.FormattedTranscripts, opt => opt.MapFrom(src => src.Video.FormattedTranscripts));
            CreateMap<Summary, UserSummaryDto>()
                .ForMember(dest => dest.VideoTitle, opt => opt.MapFrom(src => src.Video.Title))
                .ForMember(dest => dest.PromptUsed, opt => opt.MapFrom(src => src.Prompt.Name))
                .ForMember(dest => dest.SummarySectionsCount, opt => opt.MapFrom(src => src.SummarySections.Count));
            CreateMap<VideoSummaryResponseDto, Summary>()
                .ForMember(dest => dest.SummarySections, opt => opt.MapFrom(src => src.SummarySections))
                .ForMember(dest => dest.Video, opt => opt.Ignore()) 
                .ForMember(dest => dest.Prompt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
