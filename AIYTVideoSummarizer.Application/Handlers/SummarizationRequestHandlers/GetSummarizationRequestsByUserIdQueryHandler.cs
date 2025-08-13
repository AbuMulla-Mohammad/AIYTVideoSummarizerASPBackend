
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class GetSummarizationRequestsByUserIdQueryHandler : IRequestHandler<GetSummarizationRequestsByUserIdQuery, List<SummarizationRequestDto>>
    {
        private readonly ISummarizationRequestRepository _summarizationRequestRepository;
        private readonly IMapper _mapper;

        public GetSummarizationRequestsByUserIdQueryHandler(
            ISummarizationRequestRepository summarizationRequestRepository,
            IMapper mapper)
        {
            _summarizationRequestRepository = summarizationRequestRepository;
            _mapper = mapper;
        }
        public async Task<List<SummarizationRequestDto>> Handle(GetSummarizationRequestsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var summarizationRequests = await _summarizationRequestRepository.GetByUserIdAsync(request.UserId);
            return _mapper.Map<List<SummarizationRequestDto>>(summarizationRequests);
        }
    }
}
