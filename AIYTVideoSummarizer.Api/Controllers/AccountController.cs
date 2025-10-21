using AIYTVideoSummarizer.Api.Common.Extensions;
using AIYTVideoSummarizer.Api.Common.Responses;
using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.DTOs.AuthenticationDtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var command = _mapper.Map<RegisterCommand>(registerDto);

            await _mediator.Send(command);

            return Created();
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var command = new VerifyEmailCommand { Token = token };

            await _mediator.Send(command);

            return Ok(ApiResponse<string>.SuccessResponse("Your email has been successfully verified."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = _mapper.Map<LoginCommand>(loginDto);

            var result = await _mediator.Send(command);

            return Ok(ApiResponse<string>.SuccessResponse(result));
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = User.GetUserIdOrThrow();

            var command = _mapper.Map<ChangePasswordCommand>(changePasswordDto);
            command.UserId = userId;

            await _mediator.Send(command);
            
            return Ok(ApiResponse<string>.SuccessResponse("Password changed successfully"));

        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var command = _mapper.Map<ForgotPasswordCommand>(forgotPasswordDto);

            await _mediator.Send(command);

            return Ok(ApiResponse<string>.SuccessResponse("A password reset link has been sent to your email."));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto, [FromQuery] string token)
        {
            var command = _mapper.Map<ResetPasswordCommand>(resetPasswordDto);
            command.Token=token;

            await _mediator.Send(command);

            return Ok(ApiResponse<string>.SuccessResponse("Your password has been reset successfully"));
        }
    }
}
