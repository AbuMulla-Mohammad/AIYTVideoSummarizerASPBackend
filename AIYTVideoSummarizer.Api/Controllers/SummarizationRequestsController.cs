using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummarizationRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SummarizationRequestsController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSummarizationRequestsQuery();

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummarizationRequestDto>>.SuccessResponse(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSummarizationRequestDto createSummarizationRequestDto)
        {
            var command = _mapper.Map<CreateSummarizationRequestCommand>(createSummarizationRequestDto);
            command.UserId = Guid.Parse("5b50ffa9-ba39-4c3a-810f-49e8a5e467be");

            var result = await _mediator.Send(command);

            return Ok(ApiResponse<VideoSummaryResponseDto>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetSummarizationRequestByIdQuery { SummarizationRequestId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<SummarizationRequestDetailsDto>.SuccessResponse(result));
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> GetByStatus([FromRoute]RequestStatus status)
        {
            var query = new GetSummarizationRequestsByStatusQuery { RequestStatus = status };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummarizationRequestDto>>.SuccessResponse(result));
        }

        [HttpGet("userId/{id:guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid id)
        {
            var query = new GetSummarizationRequestsByUserIdQuery { UserId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummarizationRequestDto>>.SuccessResponse(result));
        }
    }
}
