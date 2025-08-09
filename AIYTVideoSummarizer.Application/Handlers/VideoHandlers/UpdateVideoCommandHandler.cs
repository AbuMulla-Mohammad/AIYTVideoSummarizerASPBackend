using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;


namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, UpdateVideoDto>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public UpdateVideoCommandHandler(IVideoRepository videoRepository,IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<UpdateVideoDto> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            var existingVideo = await _videoRepository.GetByIdAsync(request.Id);
            if (existingVideo == null) 
                throw new KeyNotFoundException($"Video with Id {request.Id} not found.");

            _mapper.Map<UpdateVideoCommand, Video>(request, existingVideo);
            
            await _videoRepository.UpdateAsync(existingVideo);
            return _mapper.Map < UpdateVideoDto > (existingVideo);
            _mapper.Map<UpdateVideoDto,>
        }
    }
}
