using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Entities.Roles;
using System.Security.Claims;
namespace Restaurant.Application.Admins.Command
{
    public class AdminAndOwnerCommandHandler : IRequestHandler<AdminAndOwnerCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        public AdminAndOwnerCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> Handle(AdminAndOwnerCommand request, CancellationToken cancellationToken)
        {
             // check if the user is register before or not
            var IsExistuser = await _userManager.FindByEmailAsync(request.Email);
            if (IsExistuser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "Email is already registered."
                });
            }

            // check if password == Confirm Password
            if (request.Password != request.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Passwords do not match" });
            }

            // Check if this role is exist or not
            if (request.Role != Roles.Admin.ToString() && request.Role != Roles.Owner.ToString())
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRole",
                    Description = "Role must be either Admin or Owner"
                });
            }

            // Create User
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email.Split('@')[0],
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var CreatednewUser = await _userManager.CreateAsync(user , request.Password);
            if (!CreatednewUser.Succeeded)
                return CreatednewUser;

            // Assign role
            var roleResult = await AssignUserRole(user, request.Role);
            if (!roleResult.Succeeded)
                return roleResult;

            // Add claims
            var claimResult = await AddUserClaims(user);
            return claimResult;
        }
        private async Task<IdentityResult> AssignUserRole(User user, string role)
        {
            if (role != Roles.Owner.ToString() && role != Roles.User.ToString() && role != Roles.Admin.ToString())
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRole",
                    Description = "Invalid role specified"
                });
            }

            return await _userManager.AddToRoleAsync(user, role);
        }

        private async Task<IdentityResult> AddUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            return await _userManager.AddClaimsAsync(user, claims);
        }
    }
}
