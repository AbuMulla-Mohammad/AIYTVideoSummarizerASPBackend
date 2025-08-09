
using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, VideoDto>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public DeleteVideoCommandHandler(IVideoRepository videoRepository,IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<VideoDto> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {

            var video = await _videoRepository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Video with ID {request.Id} was not found.");
            await _videoRepository.DeleteAsync(request.Id);
            return _mapper.Map<VideoDto>(video);
        }
    }
}
