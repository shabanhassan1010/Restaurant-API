using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Users.DTOS;

namespace Restaurant.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<IdentityResult>  // this command will return IdentityResult
    {
        public ResetPasswordDto resetPasswordDto;
        public ResetPasswordCommand(ResetPasswordDto resetPasswordDto)
        {
            this.resetPasswordDto = resetPasswordDto;
        }
    }
}
