using AIYTVideoSummarizer.Api.Common.Extensions;
using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.SummaryQueries;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

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

        [Authorize(Policy = "MustBeAdminOrSuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSummariesQuery getAllSummariesQuery)
        {
            var result = await _mediator.Send(getAllSummariesQuery);

            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(result.PageData)
            );

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result.Items));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetSummaryByIdQuery { SummaryId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<SummaryDetailsDto>.SuccessResponse(result));
        }

        [Authorize(Policy = "MustBeAdminOrSuperAdmin")]
        [HttpGet("videoId/{id:guid}")]
        public async Task<IActionResult> GetByVideoId(
            [FromRoute] Guid id,
            [FromQuery] string? searchQuery,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1)
        {
            var query = new GetSummariesByVideoIdQuery
            {
                VideoId = id,
                PageSize = pageSize,
                PageNumber = pageNumber,
                SearchQuery = searchQuery,
            };

            var result = await _mediator.Send(query);

            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(result.PageData)
            );

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result.Items));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1)
        {
            var userId = User.GetUserIdOrThrow();

            var query = new GetSummariesByUserIdQuery {
                UserId = userId,
                PageSize = pageSize,
                PageNumber = pageNumber,
                SearchQuery = searchQuery,
            };

            var result = await _mediator.Send(query);

            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(result.PageData)
            );

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result.Items));
        }

        [Authorize(Policy = "MustBeAdminOrSuperAdmin")]
        [HttpGet("promptId/{id:guid}")]
        public async Task<IActionResult> GetByPromptId
            ([FromRoute] Guid id,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1)
        {
            var query = new GetSummariesByPromptIdQuery { 
                PromptId = id,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);

            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(result.PageData)
            );

            return Ok(ApiResponse<List<SummaryDto>>.SuccessResponse(result.Items));
        }
    }
}
