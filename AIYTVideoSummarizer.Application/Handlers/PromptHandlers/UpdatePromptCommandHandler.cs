using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.PromptHandlers
{
    public class UpdatePromptCommandHandler : IRequestHandler<UpdatePromptCommand, PromptDto>
    {
        private readonly IPromptRepository _promptRepository;
        private readonly IMapper _mapper;

        public UpdatePromptCommandHandler(
            IPromptRepository promptRepository,
            IMapper mapper)
        {
            _promptRepository = promptRepository;
            _mapper = mapper;
        }

        public async Task<PromptDto> Handle(UpdatePromptCommand request, CancellationToken cancellationToken)
        {
            var prompt = await _promptRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Prompt), request.Id);
            if (!string.IsNullOrWhiteSpace(request.Name))
                prompt.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Text))
                prompt.Text = request.Text;

            if (!string.IsNullOrWhiteSpace(request.Description))
                prompt.Description = request.Description;

            await _promptRepository.UpdateAsync(prompt);
            return _mapper.Map<PromptDto>(prompt);

        }
    }
}
