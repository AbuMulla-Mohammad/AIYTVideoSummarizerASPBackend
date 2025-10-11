using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class PromptProfile:Profile
    {
        public PromptProfile()
        {
            CreateMap<Prompt, PromptDto>();
            CreateMap<Prompt, PromptListDto>();
            CreateMap<Prompt, PromptSummariesDto>();
            CreateMap<CreatePromptCommand, Prompt>();
            CreateMap<CreatePromptDto, CreatePromptCommand>();
            CreateMap<UpdatePromptDto, UpdatePromptCommand>();
        }
    }
}
