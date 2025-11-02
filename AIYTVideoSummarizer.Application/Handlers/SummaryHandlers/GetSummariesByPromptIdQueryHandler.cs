
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
    public class GetSummariesByPromptIdQueryHandler : IRequestHandler<GetSummariesByPromptIdQuery, PaginatedList<SummaryDto>>
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IMapper _mapper;

        public GetSummariesByPromptIdQueryHandler(
            ISummaryRepository summaryRepository,
            IMapper mapper)
        {
            _summaryRepository = summaryRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<SummaryDto>> Handle(GetSummariesByPromptIdQuery request, CancellationToken cancellationToken)
        {
            var summaries = await _summaryRepository.GetByPromptIdAsync(
                request.PageNumber,
                request.PageSize,
                request.PromptId,
                filter:null,
                s => s.Video,
                s => s.Prompt,
                s => s.SummarySections);

            return new PaginatedList<SummaryDto>(
                _mapper.Map<List<SummaryDto>>(summaries.Items),
                summaries.PageData);
        }
    }
}
