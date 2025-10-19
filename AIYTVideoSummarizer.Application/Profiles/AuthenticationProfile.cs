
using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.DTOs.AuthenticationDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<RegisterDto, RegisterCommand>();
            CreateMap<LoginDto, LoginCommand>();
            CreateMap<ChangePasswordDto, ChangePasswordCommand>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<ForgotPasswordDto, ForgotPasswordCommand>();
        }
    }
}
