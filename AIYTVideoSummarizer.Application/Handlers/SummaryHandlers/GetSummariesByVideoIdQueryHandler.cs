
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummaryHandlers
{
    public class GetSummariesByVideoIdQueryHandler:IRequestHandler<GetSummariesByVideoIdQuery,List<SummaryDto>>
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IMapper _mapper;

        public GetSummariesByVideoIdQueryHandler(
            ISummaryRepository summaryRepository,
            IMapper mapper)
        {
            _summaryRepository = summaryRepository;
            _mapper = mapper;
        }

        public async Task<List<SummaryDto>> Handle(GetSummariesByVideoIdQuery request, CancellationToken cancellationToken)
        {
            var summaries = await _summaryRepository.GetByVideoIdAsync(request.VideoId, s => s.Video, s => s.Prompt, s => s.SummarySections);

            return _mapper.Map<List<SummaryDto>>(summaries);
        }
    }
}
