using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.Commands.ForgetPassword;
using Restaurant.Application.Users.Commands.LoginUser;
using Restaurant.Application.Users.Commands.RegisterUser;
using Restaurant.Application.Users.Commands.ResetPassword;
using Restaurant.Application.Users.Commands.UpdateUser;
using Restaurant.Application.Users.DTOS;
using System.Security.Claims;

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

        protected string? GetUserId() =>  User.FindFirstValue(ClaimTypes.NameIdentifier);

        #region register
        [HttpPost("register")]
        [EndpointSummary("Register For User")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var command = new RegisterUserCommand(dto);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered successfully.");
        }
        #endregion

        # region login
        [HttpPost("login")]
        [EndpointSummary("Login For User")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var command = new LoginUserCommand(dto);
            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }
        #endregion

        #region Forgot Password
        [HttpPost("forgot-password")]
        [EndpointSummary("Forgot Password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var command = new forgetPasswordCommand(forgotPasswordDto);
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest(result);
            return Ok(new { message = "Password reset token sent to your email." });
        }
        #endregion

        #region Reset Password
        [HttpPost("reset-password")]
        [EndpointSummary("Reset Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var command = new ResetPasswordCommand(resetPasswordDto);
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok("Password reset successfully.");
        }
        #endregion

    }
}
