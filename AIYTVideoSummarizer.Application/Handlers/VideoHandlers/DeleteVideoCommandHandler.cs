
using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, Unit>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public DeleteVideoCommandHandler(IVideoRepository videoRepository,IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {

            var video = await _videoRepository.GetByIdAsync(request.Id) 
                ?? throw new NotFoundException(nameof(Video),request.Id);
            await _videoRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
