
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
        }
    }
}
