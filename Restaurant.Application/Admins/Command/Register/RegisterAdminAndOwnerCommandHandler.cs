using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Entities.Roles;
using System.Data;
using System.Security.Claims;
namespace Restaurant.Application.Admins.Command.Login
{
    public class RegisterAdminAndOwnerCommandHandler : IRequestHandler<RegisterAdminAndOwnerCommand, IdentityResult>
    {
        #region Context
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterAdminAndOwnerCommandHandler(UserManager<User> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
        }
        #endregion
        public async Task<IdentityResult> Handle(RegisterAdminAndOwnerCommand request, CancellationToken cancellationToken)
        {
             // check if the user is register before or not
            var IsExistuser = await _userManager.FindByEmailAsync(request.registerRoleDto.Email);
            if (IsExistuser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "Duplicate Email",
                    Description = "Email is already registered."
                });
            }

            // check if password == Confirm Password
            if (request.registerRoleDto.Password != request.registerRoleDto.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Passwords do not match" });
            }

            // Check if this role is exist or not
            if (request.registerRoleDto.Role != Roles.Admin.ToString() && request.registerRoleDto.Role != Roles.Owner.ToString())
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "Invalid Role",
                    Description = "Role must be either Admin or Owner"
                });
            }

            // Create User
            var user = new User
            {
                FirstName = request.registerRoleDto.FirstName,
                LastName = request.registerRoleDto.LastName,
                UserName = request.registerRoleDto.Email.Split('@')[0],
                Email = request.registerRoleDto.Email,
                PhoneNumber = request.registerRoleDto.PhoneNumber
            };

            var CreatednewUser = await _userManager.CreateAsync(user , request.registerRoleDto.Password);
            if (!CreatednewUser.Succeeded)
                return CreatednewUser;

            // Assign role
            var roleResult = await AssignUserRole(user, request.registerRoleDto.Role);
            if (!roleResult.Succeeded)
                return roleResult;

            // Add claims
            var claimResult = await AddUserClaims(user , request.registerRoleDto.Role);
            return claimResult;
        }
        private async Task<IdentityResult> AssignUserRole(User user, string role)
        {
            // validate the role
            if (role != Roles.Owner.ToString() && role != Roles.Admin.ToString())
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "Invalid Role",
                    Description = "Invalid role specified"
                });
            }

            // Ensure role exists
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            return await _userManager.AddToRoleAsync(user, role);
        }
        private async Task<IdentityResult> AddUserClaims(User user , string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role) 

            };

            return await _userManager.AddClaimsAsync(user, claims);
        }
    }
}
