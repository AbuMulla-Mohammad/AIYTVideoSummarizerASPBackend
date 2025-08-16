
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Application.Queries.PromptQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.PromptHandlers
{
    public class GetPromptSummariesQueryHandler : IRequestHandler<GetPromptSummariesQuery, List<PromptSummariesDto>>
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IMapper _mapper;

        public GetPromptSummariesQueryHandler(
            IPromptRepository promptRepository,
            IMapper mapper)
        {
            _promptRepository = promptRepository;
            _mapper = mapper;
        }

        public async Task<List<PromptSummariesDto>> Handle(GetPromptSummariesQuery request, CancellationToken cancellationToken)
        {
            var prompt = await _promptRepository.GetByIdAsync(request.Id, p => p.Summaries)
                ?? throw new NotFoundException(nameof(Prompt), request.Id);
            return _mapper.Map<List<PromptSummariesDto>>(prompt.Summaries);
        }
    }
}
