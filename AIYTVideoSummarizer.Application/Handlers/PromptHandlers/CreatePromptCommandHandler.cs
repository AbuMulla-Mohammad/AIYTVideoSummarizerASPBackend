
using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.PromptHandlers
{
    public class CreatePromptCommandHandler : IRequestHandler<CreatePromptCommand, PromptDto>
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IMapper _mapper;

        public CreatePromptCommandHandler(
            IPromptRepository promptRepository,
            IMapper mapper)
        {
            _promptRepository = promptRepository;
            _mapper = mapper;
        }

        public async Task<PromptDto> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
        {
            var prompt = await _promptRepository.AddAsync(_mapper.Map<Prompt>(request));
            return _mapper.Map<PromptDto>(prompt);
        }
    }
}
