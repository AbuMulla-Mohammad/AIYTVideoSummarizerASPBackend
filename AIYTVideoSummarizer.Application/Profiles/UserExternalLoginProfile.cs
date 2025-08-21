
using AIYTVideoSummarizer.Application.Commands.UserExternalLoginCommands;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class UserExternalLoginProfile:Profile
    {
        public UserExternalLoginProfile()
        {
            CreateMap<CreateExternalLoginCommand, UserExternalLogin>();
        }
    }
}
