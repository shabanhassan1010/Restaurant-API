using MediatR;
using Microsoft.AspNetCore.Identity;
namespace Restaurant.Application.Admins.Command
{
    public class AdminAndOwnerCommand : IRequest<IdentityResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }    // Admin or Owner
    }
}
