using AIYTVideoSummarizer.Api.Common.Extensions;
using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.Commands.VideoCommands;
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Queries.VideoQueries;
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
    public class VideosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "MustBeAdminOrSuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllVideosQuery getAllVideosQuery)
        {
            var result = await _mediator.Send(getAllVideosQuery);

            Response.Headers.Append(
                "X-Pagination",
                JsonSerializer.Serialize(result.PageData)
            );

            return Ok(ApiResponse<List<VideoDto>>.SuccessResponse(result.Items));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetVideoByIdQuery { VideoId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<VideoDetailsDto>.SuccessResponse(result));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.GetUserIdOrThrow();

            var query = new GetSummarizedVideosByUserIdQuery { UserId = userId };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<VideoDto>>.SuccessResponse(result));
        }

        [Authorize(Policy = "MustBeAdminOrSuperAdmin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteVideoCommand { Id = id };

            var result = await _mediator.Send(command);

            return NoContent();
        }
    }
}
