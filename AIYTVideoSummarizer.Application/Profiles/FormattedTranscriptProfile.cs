
using AIYTVideoSummarizer.Application.DTOs.TranscriptDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class FormattedTranscriptProfile:Profile
    {
        public FormattedTranscriptProfile()
        {
            CreateMap<FormattedTranscript, FormattedTranscriptDto>()
                .ForMember(dest => dest.StartTime,
                           opt => opt.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.TotalSeconds : 0))
                .ForMember(dest => dest.EndTime,
                           opt => opt.MapFrom(src => src.EndTime.HasValue ? src.EndTime.Value.TotalSeconds : 0));

            CreateMap<FormattedTranscriptDto, FormattedTranscript>()
                .ForMember(dest => dest.StartTime,
                            opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.StartTime)))
                .ForMember(dest => dest.EndTime,
                            opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.EndTime)));

             
        }
    }
}
