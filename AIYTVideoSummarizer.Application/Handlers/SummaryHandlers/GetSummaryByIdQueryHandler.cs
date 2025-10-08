
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummaryHandlers
{
    public class GetSummaryByIdQueryHandler : IRequestHandler<GetSummaryByIdQuery, SummaryDetailsDto>
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IMapper _mapper;

        public GetSummaryByIdQueryHandler(
            ISummaryRepository summaryRepository,
            IMapper mapper)
        {
            _summaryRepository = summaryRepository;
            _mapper = mapper;
        }
        public async Task<SummaryDetailsDto> Handle(GetSummaryByIdQuery request, CancellationToken cancellationToken)
        {
            var summary= await _summaryRepository.GetByIdAsync(request.SummaryId,s=>s.SummarySections,s=>s.Prompt,s=>s.Video)
                ?? throw new NotFoundException(nameof(Summary), request.SummaryId);

            return _mapper.Map<SummaryDetailsDto>(summary);
        }
    }
}
