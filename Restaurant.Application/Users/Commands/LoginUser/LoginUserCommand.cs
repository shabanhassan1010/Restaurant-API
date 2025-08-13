using MediatR;
using Restaurant.Application.Users.DTOS;
namespace Restaurant.Application.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<TokenDto>
    {
        public LoginUserDto Dto { get; set; }
        public LoginUserCommand(LoginUserDto dto)
        {
            Dto = dto;
        }
    }
}
