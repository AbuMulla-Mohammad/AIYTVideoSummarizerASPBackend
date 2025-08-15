
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Application.Queries.PromptQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.PromptHandlers
{
    public class GetAllPromptsQueryHandler : IRequestHandler<GetAllPromptsQuery, List<PromptListDto>>
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

        public async Task<List<PromptListDto>> Handle(GetAllPromptsQuery request, CancellationToken cancellationToken)
        {
            var prompts = await _promptRepository.GetAllAsync();
            return _mapper.Map<List<PromptListDto>>(prompts);
        }
    }
}
