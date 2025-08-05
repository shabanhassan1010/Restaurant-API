using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.Commands.RegisterUser;
using Restaurant.Application.Users.DTOS;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region mediator
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        [HttpPost]
        [EndpointSummary("Register For User")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var command = new RegisterUserCommand(dto);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered successfully.");
        }
    }
}
