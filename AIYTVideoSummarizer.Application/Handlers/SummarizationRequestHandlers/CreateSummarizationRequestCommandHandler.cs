
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos;
using AIYTVideoSummarizer.Application.DTOs.TranscriptDtos;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Interfaces.ExternalServices;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class CreateSummarizationRequestCommandHandler : IRequestHandler<CreateSummarizationRequestCommand, VideoSummaryResponseDto>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ISummarizationRequestRepository _summarizationRequestRepository;
        private readonly ISummaryRepository _summaryRepository;
        private readonly IAIService _aIService;
        private readonly IMapper _mapper;

        public CreateSummarizationRequestCommandHandler(
            IVideoRepository videoRepository,
            ISummarizationRequestRepository summarizationRequestRepository,
            ISummaryRepository summaryRepository,
            IAIService aIService,
            IMapper mapper)
        {
            _videoRepository = videoRepository;
            _summarizationRequestRepository = summarizationRequestRepository;
            _summaryRepository = summaryRepository;
            _aIService = aIService;
            _mapper = mapper;
        }
        public async Task<VideoSummaryResponseDto> Handle(CreateSummarizationRequestCommand request, CancellationToken cancellationToken)
        {
            var summarizationRequest = _mapper.Map<SummarizationRequest>(request);
            var ytId = await _aIService.GetYTVideoId(request.YouTubeUrl);

            var video = await _videoRepository.GetByYtIdAsync(ytId);

            if(video is not null)
            {
                var existingSummary = await _summaryRepository.GetByYtVideoIdAndPromptIdAsync(ytId, request.PromptId);
                if (existingSummary is not null)
                {

                    return await ReturnExistingSummaryAsync(existingSummary, summarizationRequest, video.Id);

                }
                return await SummarizeVideoAsync(request, summarizationRequest, video);
            }
            video = await CreateNewVideo(request.YouTubeUrl, ytId);

            return await SummarizeVideoAsync(request,summarizationRequest,video);

        }

        private async Task<VideoSummaryResponseDto> ReturnExistingSummaryAsync(
            Summary existingSummary,
            SummarizationRequest summarizationRequest,
            Guid videoId)
        {
            summarizationRequest.RequestStatus = RequestStatus.Completed;
            summarizationRequest.VideoId = videoId;

            await _summarizationRequestRepository.AddAsync(summarizationRequest);

            return new VideoSummaryResponseDto
            {
                SummarySections = _mapper.Map<List<SummarySectionDto>>(existingSummary.SummarySections),
                FormattedTranscripts = _mapper.Map<List<FormattedTranscriptDto>>(existingSummary.Video.FormattedTranscripts),
            };
        }

        private async Task<VideoSummaryResponseDto> SummarizeVideoAsync(
            CreateSummarizationRequestCommand request,
            SummarizationRequest summarizationRequest,
            Video video)
        {
            try
            {
                var summaryResponse = await _aIService.SummarizeVideo(request.YouTubeUrl, request.PromptId);

                if (summaryResponse == null || summaryResponse.SummarySections == null || !summaryResponse.SummarySections.Any())
                {
                    throw new SummarizationFailedException("The AI service returned no meaningful summary. Please try again with another video or prompt.");
                }

                var summaryEntity = _mapper.Map<Summary>(summaryResponse);
                _mapper.Map(summarizationRequest, summaryEntity);
                summaryEntity.VideoId = video.Id;

                await _summaryRepository.AddAsync(summaryEntity);

                if (!video.FormattedTranscripts.Any())
                {
                    video.FormattedTranscripts = summaryResponse.FormattedTranscripts
                        .Select(dto => _mapper.Map<FormattedTranscript>(dto))
                        .ToList();
                    await _videoRepository.UpdateAsync(video);
                }

                summarizationRequest.VideoId = video.Id;
                summarizationRequest.RequestStatus = RequestStatus.Completed;
                await _summarizationRequestRepository.AddAsync(summarizationRequest);

                return summaryResponse;
            }
            catch(Exception ex)
            {
                summarizationRequest.RequestStatus = RequestStatus.Failed;
                summarizationRequest.ErrorMessage = ex.Message;
                await _summarizationRequestRepository.AddAsync(summarizationRequest);
                throw;
            }
        }
        
        private async Task<Video> CreateNewVideo(
            string youTubeUrl,
            string youTubeVideoId)
        {
            Video video = new Video
            {
                YouTubeUrl = youTubeUrl,
                YouTubeVideoID = youTubeVideoId
            };

            await _videoRepository.AddAsync(video);

            return video;
        }
    }
}
