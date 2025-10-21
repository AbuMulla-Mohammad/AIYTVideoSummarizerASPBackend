using AIYTVideoSummarizer.Api.Common.Extensions;
using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy= "MustBeAdminOrSuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<List<UserInfoDto>>.SuccessResponse(result));
        }

        [Authorize(Policy = "MustBeAdminOrSuperAdmin")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetUserByIdQuery { UserId = id };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<UserInfoDto>.SuccessResponse(result));
        }

        [HttpGet("user/profile")]
        public async Task<IActionResult> GetUserProfileById()
        {
            var userId = User.GetUserIdOrThrow();

            var query = new GetUserProfileQuery { UserId = userId };

            var result = await _mediator.Send(query);

            return Ok(ApiResponse<UserProfileDto>.SuccessResponse(result));
        }
    }
}
