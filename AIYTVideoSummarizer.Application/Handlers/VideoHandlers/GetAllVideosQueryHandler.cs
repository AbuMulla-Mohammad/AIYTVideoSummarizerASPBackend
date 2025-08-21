
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, List<VideoDto>>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public GetAllVideosQueryHandler(
            IVideoRepository videoRepository,
            IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<List<VideoDto>> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
        {
            var videos = await _videoRepository.GetAllAsync();
            return _mapper.Map<List<VideoDto>>(videos);
        }
    }
}
