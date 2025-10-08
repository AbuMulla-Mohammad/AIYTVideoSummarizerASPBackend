
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class GetSummarizedVideosByUserIdQueryHandler : IRequestHandler<GetSummarizedVideosByUserIdQuery, List<VideoDto>>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public GetSummarizedVideosByUserIdQueryHandler(
            IVideoRepository videoRepository,
            IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<List<VideoDto>> Handle(GetSummarizedVideosByUserIdQuery request, CancellationToken cancellationToken)
        {
            var videos = await _videoRepository.GetByUserId(request.UserId);

            return _mapper.Map<List<VideoDto>>(videos);
        }
    }
}
