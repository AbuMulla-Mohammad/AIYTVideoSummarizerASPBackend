using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.DTOs.AuthenticationDtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            var result = await _mediator.Send(command);

            return Created();
        }
    }
}
