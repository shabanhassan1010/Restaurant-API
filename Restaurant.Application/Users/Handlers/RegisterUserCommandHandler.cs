using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users.Commands.RegisterUser;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Entities.Roles;
using System.Security.Claims;

namespace Restaurant.Application.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        #region Context
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager
            , ILogger<RegisterUserCommandHandler>  logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        #endregion
        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // check if the user is register before or not
            var IsExistuser = await _userManager.FindByEmailAsync(dto.Email);
            if (IsExistuser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "Email is already registered."
                });
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Passwords do not match" });
            }

            // create User
            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Email.Split('@')[0],
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = true,  // Assuming email confirmation is not required for registration
            };

            // Create user account
            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded)  // if my data match validation
            {
                _logger.LogWarning("User creation failed: {Errors}", string.Join(", ", result.Errors));
                return result;
            }

            // this will be changed by each controller
            string role = Roles.User.ToString();

            // Always assign role "User"
            var roleResult = await AssignUserRole(newUser, Roles.User.ToString());
            if (!roleResult.Succeeded)
                return roleResult;

            // Add user claims
            var claimResult = await AddUserClaims(newUser, Roles.User.ToString());
            if (!claimResult.Succeeded)
                return claimResult;

            return result;
        }


        #region Private Helpers
        private async Task<IdentityResult> AssignUserRole(User user, string role)
        {
            // Validate requested role
            if (role != Roles.Owner.ToString() && role != Roles.User.ToString() && role != Roles.Admin.ToString() )
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidRole",
                    Description = "Invalid role specified"
                });
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                var roleCreateResult = await _roleManager.CreateAsync(new IdentityRole(role));
                if (!roleCreateResult.Succeeded)
                {
                    _logger.LogWarning("Failed to create role: {Errors}", string.Join(", ", roleCreateResult.Errors.Select(e => e.Description)));
                    return roleCreateResult;
                }
            }

            return await _userManager.AddToRoleAsync(user, role);
            // Assign role
        }
        private async Task<IdentityResult> AddUserClaims(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var result = await _userManager.AddClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Failed to add claims: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return result;
        }
        #endregion
    }
}
