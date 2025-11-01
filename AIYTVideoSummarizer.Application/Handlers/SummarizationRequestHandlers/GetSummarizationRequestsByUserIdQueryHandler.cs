
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class GetSummarizationRequestsByUserIdQueryHandler : IRequestHandler<GetSummarizationRequestsByUserIdQuery, PaginatedList<SummarizationRequestDto>>
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
        public async Task<PaginatedList<SummarizationRequestDto>> Handle(GetSummarizationRequestsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var summarizationRequests = await _summarizationRequestRepository.GetByUserIdAsync(
                request.PageNumber,
                request.PageSize,
                request.UserId);

            return new PaginatedList<SummarizationRequestDto>(
                _mapper.Map<List<SummarizationRequestDto>>(summarizationRequests.Items),
                summarizationRequests.PageData);
        }
    }
}
