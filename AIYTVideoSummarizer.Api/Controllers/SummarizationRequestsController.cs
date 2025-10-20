using AIYTVideoSummarizer.Api.Common.Extensions;
using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands;
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries;
using AIYTVideoSummarizer.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var userId = User.GetUserIdOrThrow();

            var command = _mapper.Map<CreateSummarizationRequestCommand>(createSummarizationRequestDto);
            command.UserId = userId;

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

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.GetUserIdOrThrow();

            var query = new GetSummarizationRequestsByUserIdQuery { UserId = userId };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummarizationRequestDto>>.SuccessResponse(result));
        }
    }
}
