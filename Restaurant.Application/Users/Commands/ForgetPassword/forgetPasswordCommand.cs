using MediatR;
using Restaurant.Application.Users.DTOS;

namespace Restaurant.Application.Users.Commands.ForgetPassword
{
    public class forgetPasswordCommand : IRequest<bool>  // this command will return a boolean indicating success or failure
    {
        public ForgotPasswordDto forgotPasswordDto;

        public forgetPasswordCommand(ForgotPasswordDto forgotPasswordDto)
        {
            this.forgotPasswordDto = forgotPasswordDto;
        }
    }
}
