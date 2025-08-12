
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos;
using AIYTVideoSummarizer.Application.DTOs.TranscriptDtos;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Interfaces.ExternalServices;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;
using AutoMapper;
using MediatR;
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
            var ytId = await _aIService.GetYTVideoId(request.YouTubeUrl);
            var existingSummary = await _summaryRepository.GetByYtVideoIdAndPromptIdAsync(ytId, request.PromptId);
           
            if (existingSummary is not null)
            {
                return _mapper.Map<VideoSummaryResponseDto>(existingSummary);

            }
            var video = await _videoRepository.GetByYtIdAsync(ytId);
            if(video is null)
            {
                video = new Video
                {
                    YouTubeUrl = request.YouTubeUrl,
                    YouTubeVideoID = ytId,
                };
                await _videoRepository.AddAsync(video);
            }

            var summarizationRequest = _mapper.Map<SummarizationRequest>(request);
            summarizationRequest.VideoId = video.Id;
            summarizationRequest.RequestStatus = RequestStatus.Pending;
            await _summarizationRequestRepository.AddAsync(summarizationRequest);
            VideoSummaryResponseDto summarizeResponse = new VideoSummaryResponseDto();
            try
            {
                 summarizeResponse = await _aIService.SummarizeVideo(request.YouTubeUrl, request.PromptId);
                var summaryEntity = _mapper.Map<Summary>(summarizeResponse);
                summaryEntity.VideoId = video.Id;
                summaryEntity.PromptId = request.PromptId;
                summaryEntity.UserId = request.UserId;
                await _summaryRepository.AddAsync(summaryEntity);

                summarizationRequest.RequestStatus = RequestStatus.Completed;
            }
            catch (Exception ex)
            {
                summarizationRequest.RequestStatus = RequestStatus.Failed;
                summarizationRequest.ErrorMessage = ex.Message;
            }
            await _summarizationRequestRepository.UpdateAsync(summarizationRequest);

            return summarizeResponse;

        }
    }
}
