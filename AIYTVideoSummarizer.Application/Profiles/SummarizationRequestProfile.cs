
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class SummarizationRequestProfile:Profile
    {
        public SummarizationRequestProfile()
        {
            CreateMap<SummarizationRequest, SummarizationRequestDto>();

            CreateMap<CreateSummarizationRequestCommand, SummarizationRequest>();
        }
    }
}
