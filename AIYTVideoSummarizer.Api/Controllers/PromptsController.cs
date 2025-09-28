using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.Commands.PromptCommands;
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Application.Queries.PromptQueries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PromptsController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllPromptsQuery();

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<PromptListDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var query = new GetPromptByIdQuery { Id = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<PromptDto>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}/summaries")]
        public async Task<IActionResult> GetSummariesByPromptId([FromRoute] Guid id)
        {
            var query = new GetPromptSummariesQuery { Id = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<PromptSummariesDto>>.SuccessResponse(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromptDto promptDto)
        {
            var command = _mapper.Map<CreatePromptCommand>(promptDto);
            
            var result = await _mediator.Send(command);
            
            return Ok(ApiResponse<PromptDto>.SuccessResponse(result));

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody] UpdatePromptDto promptDto)
        {
            var command = _mapper.Map<UpdatePromptCommand>(promptDto);
            command.Id = id;

            var result = await _mediator.Send(command);

            return Ok(ApiResponse<PromptDto>.SuccessResponse(result));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult>Delete(Guid id)
        {
            var command = new DeletePromptCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
