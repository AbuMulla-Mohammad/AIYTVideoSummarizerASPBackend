using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.PromptHandlers
{
    public class DeletePromptCommandHandler : IRequestHandler<DeletePromptCommand, Unit>
    {
        private readonly IPromptRepository _promptRepository;

        public DeletePromptCommandHandler(
            IPromptRepository promptRepository)
        {
            _promptRepository = promptRepository;
        }

        public async Task<Unit> Handle(DeletePromptCommand request, CancellationToken cancellationToken)
        {
            var prompt = await _promptRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Prompt), request.Id);

            await _promptRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
