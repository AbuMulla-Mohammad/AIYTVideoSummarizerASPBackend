using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand,CreateVideoDto>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public CreateVideoCommandHandler(IVideoRepository videoRepository,IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<CreateVideoDto> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var video = _mapper.Map<Video>(request);
            video.Id = Guid.NewGuid();
            var addedVideo = await _videoRepository.AddAsync(video);
            return _mapper.Map<CreateVideoDto>(addedVideo);
        }
    }
}
