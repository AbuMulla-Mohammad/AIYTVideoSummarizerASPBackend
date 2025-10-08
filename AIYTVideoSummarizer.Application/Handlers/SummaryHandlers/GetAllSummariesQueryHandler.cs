
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummaryHandlers
{
    public class GetAllSummariesQueryHandler : IRequestHandler<GetAllSummariesQuery, List<SummaryDto>>
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

        public async Task<List<SummaryDto>> Handle(GetAllSummariesQuery request, CancellationToken cancellationToken)
        {
            var summaries = await _summaryRepository.GetAllAsync(s=>s.Video,s=>s.Prompt,s=>s.SummarySections);

            return _mapper.Map<List<SummaryDto>>(summaries);
        }
    }
}
