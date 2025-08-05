using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.Commands.UpdateUser;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        #region Constructor
        private readonly IMediator mediator;
        public IdentityController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        #endregion

        [HttpPut("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserDetails)
        {
            await mediator.Send(updateUserDetails);
            return NoContent();
        }
    }
}
