using AIYTVideoSummarizer.Api.Common.Extensions;
using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
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
    public class SummariesController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SummariesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSummariesQuery();

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetSummaryByIdQuery { SummaryId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<SummaryDetailsDto>.SuccessResponse(result));
        }

        [HttpGet("videoId/{id:guid}")]
        public async Task<IActionResult> GetByVideoId([FromRoute] Guid id)
        {
            var query = new GetSummariesByVideoIdQuery { VideoId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.GetUserIdOrThrow();

            var query = new GetSummariesByUserIdQuery { UserId = userId };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result));
        }

        [HttpGet("promptId/{id:guid}")]
        public async Task<IActionResult> GetByPromptId([FromRoute] Guid id)
        {
            var query = new GetSummariesByPromptIdQuery { PromptId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result));
        }
    }
}
