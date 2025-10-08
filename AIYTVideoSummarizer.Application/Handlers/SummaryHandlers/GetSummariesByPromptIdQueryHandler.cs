
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummaryHandlers
{
    public class GetSummariesByPromptIdQueryHandler : IRequestHandler<GetSummariesByPromptIdQuery, List<SummaryDto>>
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
        public async Task<List<SummaryDto>> Handle(GetSummariesByPromptIdQuery request, CancellationToken cancellationToken)
        {
            var summaries = await _summaryRepository.GetByPromptIdAsync(request.PromptId, s => s.Video, s => s.Prompt, s => s.SummarySections);

            return _mapper.Map<List<SummaryDto>>(summaries);
        }
    }
}
