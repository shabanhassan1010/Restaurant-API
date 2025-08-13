using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Users.Commands.LoginUser;
using Restaurant.Application.Users.DTOS;
using Restaurant.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Application.Users.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenDto>
    {
        #region
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        private readonly IConfiguration _config;

        public LoginUserCommandHandler(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<LoginUserCommandHandler> logger,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _config = config;
        }
        #endregion
        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Dto.Email);
            if (user == null)
            {
                return new TokenDto
                {
                    Token = string.Empty,
                    Message = "User not Found",
                    Success = false
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Dto.Password))
            {
                return new TokenDto
                {
                    Token = string.Empty,
                    Message = "Password or email is not correct",
                    Success = false
                };
            }
            // Ensure user is in "User" role
            await CheckRoleAddAddAsync(user, "User");

            var claimsList = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            if (!string.IsNullOrEmpty(role))
            {
                claimsList.Add(new Claim(ClaimTypes.Role, role));
            }
            claimsList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty));

            var token = GenerateJwtToken(user, claimsList);

            return new TokenDto
            {
                Token = token.Token,
                Message = "Login successful",
                Success = true,
                UserName = user.UserName!
            };
        }

        private TokenDto GenerateJwtToken(User user, IList<Claim> claimsList)
        {
            var secretKey = _config["JwtSettings:Key"];
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];
            var duration = double.Parse(_config["JwtSettings:DurationInDay"] ?? "1");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(duration);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claimsList,
                expires: expiration,
                signingCredentials: creds
            );

            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
        private async Task<IdentityUser> CheckRoleAddAddAsync(User user, string role)
        {
            // Ensure the "User" role exists
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Add user to "User" role if not already in it
            if (!await _userManager.IsInRoleAsync(user, "User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return user;
        }
    }
}
