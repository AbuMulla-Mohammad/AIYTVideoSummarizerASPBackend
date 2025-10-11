using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<UserInfoDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetUserByIdQuery { UserId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<UserInfoDto>.SuccessResponse(result));
        }

        [HttpGet("profile/{id:guid}")]
        public async Task<IActionResult> GetUserProfileById([FromRoute] Guid id)
        {
            var query = new GetUserProfileQuery { UserId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<UserProfileDto>.SuccessResponse(result));
        }
    }
}
