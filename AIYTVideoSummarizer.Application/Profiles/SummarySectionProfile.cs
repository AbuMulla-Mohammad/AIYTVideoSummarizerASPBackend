
using AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class SummarySectionProfile:Profile
    {
        public SummarySectionProfile()
        {
            CreateMap<SummarySection, SummarySectionDto>()
                .ForMember(dest => dest.StartTime,
                           opt => opt.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.TotalSeconds : 0))
                .ForMember(dest => dest.EndTime,
                           opt => opt.MapFrom(src => src.EndTime.HasValue ? src.EndTime.Value.TotalSeconds : 0));

            CreateMap<SummarySectionDto, SummarySection>()
                .ForMember(dest => dest.StartTime,
                           opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.StartTime)))
                .ForMember(dest => dest.EndTime,
                           opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.EndTime)));

        }
    }
}
