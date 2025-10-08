using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("userId/{id:guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid id)
        {
            var query = new GetSummariesByUserIdQuery { UserId = id };

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
