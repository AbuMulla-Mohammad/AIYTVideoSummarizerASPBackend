using AIYTVideoSummarizer.Application.Commands.UserCommands;
using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<CreateUserDto, CreateUserCommand>();
            CreateMap<CreateExternalUserCommand, User>();
            CreateMap<CreateExternalUserDto, CreateExternalUserCommand>();
        }
    }
}
