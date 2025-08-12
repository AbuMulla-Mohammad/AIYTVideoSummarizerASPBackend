
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class SummarizationRequestProfile:Profile
    {
        public SummarizationRequestProfile()
        {

            CreateMap<CreateSummarizationRequestCommand, SummarizationRequest>();
        }
    }
}
