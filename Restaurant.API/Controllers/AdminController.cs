using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Admins.Command.Login;
using Restaurant.Application.Admins.Command.Register;
using Restaurant.Application.Admins.Dto;
using Restaurant.Application.Users.Commands.ForgetPassword;
using Restaurant.Application.Users.Commands.LoginUser;
using Restaurant.Application.Users.Commands.RegisterUser;
using Restaurant.Application.Users.Commands.ResetPassword;
using Restaurant.Application.Users.DTOS;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region Context
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Register
        [HttpPost]
        [EndpointSummary("Register For Admin and Owner")]
        public async Task<IActionResult> Register([FromBody] RegisterRoleDto dto)
        {
            var command = new RegisterAdminAndOwnerCommand(dto);
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Account registered successfully");
        }
        #endregion

        #region Login
        [HttpPost("login")]
        [EndpointSummary("Login For Admin and Owner")]
        public async Task<IActionResult> Login([FromBody] LoginRoleDto dto)
        {
            var command = new LoginRolesCommand(dto);
            var result = await _mediator.Send(command);
            if (result == null)
                return Unauthorized("Invalid credentials");
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
