
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Admins.Command.Register;
using Restaurant.Application.Admins.Dto;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Entities.Roles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Application.Admins.Handlers
{
    public class LoginRolesCommandHandler : IRequestHandler<LoginRolesCommand, TokenDto>
    {
        #region
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<LoginRolesCommandHandler> _logger;
        private readonly IConfiguration _config;

        public LoginRolesCommandHandler(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<LoginRolesCommandHandler> logger,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _config = config;
        }
        #endregion
        public async Task<TokenDto> Handle(LoginRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.loginRoleDto.Email);
            if (user == null)
            {
                return new TokenDto
                {
                    Token = string.Empty,
                    Message = "User not Found",
                    Success = false
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, request.loginRoleDto.Password))
            {
                return new TokenDto
                {
                    Token = string.Empty,
                    Message = "Password or email is not correct",
                    Success = false
                };
            }

            // check for roles first
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any(r => r == Roles.Admin.ToString() || r == Roles.Owner.ToString()))
            {
                return new TokenDto
                {
                    Token = string.Empty,
                    Message = "Access denied. Only Admin or Owner can log in.",
                    Success = false
                };
            }

            // After get roles i will add claims for him
            var claimsList = await _userManager.GetClaimsAsync(user);
            foreach (var role in roles)
            {
                claimsList.Add(new Claim(ClaimTypes.Role, role));
            }

            claimsList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty));

            // Generate JWT token
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
    }
}
