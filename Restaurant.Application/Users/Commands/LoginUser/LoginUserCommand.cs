using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Account.DTOS.Account.Read;
using Restaurant.Application.Account.DTOS.Account.Write;
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
