using AIYTVideoSummarizer.Application.DTOs.UserExternalLoginDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class ExternalLoginProfile:Profile
    {
        public ExternalLoginProfile()
        {
            CreateMap<UserExternalLogin, UserExternalLoginDto>();
        }
    }
}
