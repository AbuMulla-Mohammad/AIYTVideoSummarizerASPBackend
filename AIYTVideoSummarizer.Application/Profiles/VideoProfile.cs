

using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;

namespace AIYTVideoSummarizer.Application.Profiles
{
    public class VideoProfile:Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoDto>();
            CreateMap<Video, CreateVideoDto>();
            CreateMap<CreateVideoCommand, Video>();
        }
    }
}
