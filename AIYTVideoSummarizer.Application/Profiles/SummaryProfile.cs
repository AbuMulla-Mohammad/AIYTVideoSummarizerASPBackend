
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class SummaryProfile:Profile
    {
        public SummaryProfile()
        {
            CreateMap<Summary, SummaryDetailsDto>();
        }
    }
}
