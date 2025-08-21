using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Interfaces.ExternalServices;
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class GetVideoSummaryByUrlPromptTypeQueryHandler : IRequestHandler<GetVideoSummaryByUrlPromptTypeQuery, VideoSummaryResponseDto?>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IAIService _aIService;
        private readonly IMapper _mapper;

        public GetVideoSummaryByUrlPromptTypeQueryHandler(IVideoRepository videoRepository,IAIService aIService,IMapper mapper)
        {
            _videoRepository = videoRepository;
            _aIService = aIService;
            _mapper = mapper;
        }
        public async Task<VideoSummaryResponseDto?> Handle(GetVideoSummaryByUrlPromptTypeQuery request, CancellationToken cancellationToken)
        {
            var ytId = await _aIService.GetYTVideoId(request.VideoUrl);
            var video = await _videoRepository.GetByYtIdAndPromptNameAsync(ytId, request.PromptName);
            if (video is null)
                return null;
            return _mapper.Map<VideoSummaryResponseDto>(video);
        }
    }
}
