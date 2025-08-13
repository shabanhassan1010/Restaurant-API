using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Users.Commands.ResetPassword;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Users.Handlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IdentityResult>
    {
        #region 
        private readonly UserManager<User> _userManager;
        public ResetPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        #endregion
        public async Task<IdentityResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.resetPasswordDto.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "No user associated with this email."
                });
            }

            // use url code instead of apaces
            var token = request.resetPasswordDto.Token ?? string.Empty;
            if (token.Contains(" "))
            {
                token = token.Replace(" ", "+");
            }

            return await _userManager.ResetPasswordAsync(user, token, request.resetPasswordDto.NewPassword);
        }
    }
}
