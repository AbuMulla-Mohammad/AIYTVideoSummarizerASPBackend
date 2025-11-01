
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class GetSummarizationRequestsByStatusQueryHandler : IRequestHandler<GetSummarizationRequestsByStatusQuery, PaginatedList<SummarizationRequestDto>>
    {
        private readonly ISummarizationRequestRepository _summarizationRequestRepository;
        private readonly IMapper _mapper;

        public GetSummarizationRequestsByStatusQueryHandler(
            ISummarizationRequestRepository summarizationRequestRepository,
            IMapper mapper)
        {
            _summarizationRequestRepository = summarizationRequestRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SummarizationRequestDto>> Handle(GetSummarizationRequestsByStatusQuery request, CancellationToken cancellationToken)
        {
            var summarizationRequests = await _summarizationRequestRepository.GetByStatusAsync(
                request.PageNumber,
                request.PageSize,
                request.RequestStatus);

            return new PaginatedList<SummarizationRequestDto>(
                _mapper.Map<List<SummarizationRequestDto>>(summarizationRequests.Items),
                summarizationRequests.PageData);
        }
    }
}
