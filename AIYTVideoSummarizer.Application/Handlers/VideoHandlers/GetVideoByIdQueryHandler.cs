
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, VideoDetailsDto?>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public GetVideoByIdQueryHandler(IVideoRepository videoRepository,IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }
        public async Task<VideoDetailsDto?> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var video = await _videoRepository.GetByIdAsync(request.VideoId,
                v => v.Summaries,
                v => v.Summaries.Select(s => s.SummarySections),
                v => v.FormattedTranscripts
                )
                ?? throw new NotFoundException(nameof(Video), request.VideoId);
            return _mapper.Map<VideoDetailsDto>(video);
        }
    }
}
