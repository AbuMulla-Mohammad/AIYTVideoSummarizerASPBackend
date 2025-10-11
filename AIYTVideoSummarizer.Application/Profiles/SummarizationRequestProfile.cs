
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
            CreateMap<SummarizationRequest, SummarizationRequestDetailsDto>()
                .ForMember(dest => dest.PromptName,
                           opt => opt.MapFrom(src => src.Prompt.Name))
                .ForMember(dest => dest.PromptDescription,
                           opt => opt.MapFrom(src => src.Prompt.Description))
                .ForMember(dest => dest.YouTubeUrl,
                           opt => opt.MapFrom(src => src.Video.YouTubeUrl));
            CreateMap<SummarizationRequest, UserSummarizationRequestDto>()
                .ForMember(dest => dest.PromptUsed, opt => opt.MapFrom(src => src.Prompt.Name))
                .ForMember(dest => dest.VideoTitle, opt => opt.MapFrom(src => src.Video.Title))
                .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => src.RequestStatus.ToString()));
            CreateMap<CreateSummarizationRequestCommand, SummarizationRequest>();
            CreateMap<CreateSummarizationRequestDto, CreateSummarizationRequestCommand>();
        }
    }
}
