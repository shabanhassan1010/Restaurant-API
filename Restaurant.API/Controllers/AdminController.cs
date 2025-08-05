using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Admins.Command;
using Restaurant.Application.Users.Commands.RegisterUser;
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

        [HttpPost]
        [EndpointSummary("Register For Admin and Owner")]
        public async Task<IActionResult> Register([FromBody] AdminAndOwnerCommand dto)
        {
            var result = await _mediator.Send(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Admin/Owner registered successfully");
        }
    }
}
