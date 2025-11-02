
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Application.Handlers.SummarizationRequestHandlers
{
    public class GetAllSummarizationRequestsQueryHandler : IRequestHandler<GetAllSummarizationRequestsQuery, PaginatedList<SummarizationRequestDto>>
    {
        private readonly ISummarizationRequestRepository _summarizationRequestRepository;
        private readonly IMapper _mapper;

        public GetAllSummarizationRequestsQueryHandler(
            ISummarizationRequestRepository summarizationRequestRepository,
            IMapper mapper)
        {
            _summarizationRequestRepository = summarizationRequestRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SummarizationRequestDto>> Handle(GetAllSummarizationRequestsQuery request, CancellationToken cancellationToken)
        {
            var summarizationRequests = await _summarizationRequestRepository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                filter:null);

            return new PaginatedList<SummarizationRequestDto>(
                _mapper.Map<List<SummarizationRequestDto>>(summarizationRequests.Items),
                summarizationRequests.PageData);
        }
    }
}
