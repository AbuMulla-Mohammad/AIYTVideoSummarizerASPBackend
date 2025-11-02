
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Application.Handlers.SummaryHandlers
{
    public class GetAllSummariesQueryHandler : IRequestHandler<GetAllSummariesQuery, PaginatedList<SummaryDto>>
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IMapper _mapper;

        public GetAllSummariesQueryHandler(
            ISummaryRepository summaryRepository,
            IMapper mapper)
        {
            _summaryRepository = summaryRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SummaryDto>> Handle(GetAllSummariesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Summary, bool>>? filter = null;
            if (!String.IsNullOrWhiteSpace(request.SearchQuery))
            {
                var searchQuery = request.SearchQuery.Trim();
                filter = s => s.Prompt.Name.Contains(searchQuery);
            }
            var summaries = await _summaryRepository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                s=>s.Video,
                s=>s.Prompt,
                s=>s.SummarySections);

            return new PaginatedList<SummaryDto>(
                _mapper.Map<List<SummaryDto>>(summaries.Items),
                summaries.PageData);
        }
    }
}
