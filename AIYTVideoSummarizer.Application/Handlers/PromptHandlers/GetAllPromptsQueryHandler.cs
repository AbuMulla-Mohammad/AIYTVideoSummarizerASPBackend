
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Application.Queries.PromptQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Application.Handlers.PromptHandlers
{
    public class GetAllPromptsQueryHandler : IRequestHandler<GetAllPromptsQuery, PaginatedList<PromptListDto>>
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IMapper _mapper;

        public GetAllPromptsQueryHandler(
            IPromptRepository promptRepository,
            IMapper mapper)
        {
            _promptRepository = promptRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PromptListDto>> Handle(GetAllPromptsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Prompt, bool>>? filter = null;

            if (!String.IsNullOrWhiteSpace(request.SearchQuery))
            {
                var searchQuery = request.SearchQuery.Trim();
                filter = p =>
                p.Name.Contains(searchQuery) ||
                p.Description.Contains(searchQuery);
            }

            var prompts = await _promptRepository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                filter:filter);

            return new PaginatedList<PromptListDto>( 
                _mapper.Map<List<PromptListDto>>(prompts.Items),
               prompts.PageData);
        }
    }
}
