
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Application.Handlers.VideoHandlers
{
    public class GetSummarizedVideosByUserIdQueryHandler : IRequestHandler<GetSummarizedVideosByUserIdQuery, PaginatedList<VideoDto>>
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

        public async Task<PaginatedList<VideoDto>> Handle(GetSummarizedVideosByUserIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Video, bool>>? filter = null;
            if (!String.IsNullOrWhiteSpace(request.SearchQuery))
            {
                var searchQuery = request.SearchQuery.Trim();
                filter = v => v.Title.Contains(searchQuery);
            }
            var videos = await _videoRepository.GetByUserId(
                request.PageNumber,
                request.PageSize,
                request.UserId,
                filter);

            return new PaginatedList<VideoDto>(
                _mapper.Map<List<VideoDto>>(videos.Items),
                videos.PageData);
        }
    }
}
