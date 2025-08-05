using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Users.DTOS;

namespace Restaurant.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        public RegisterUserDto Dto { get; set; }
        public RegisterUserCommand(RegisterUserDto dto)
        {
            Dto = dto;
        }
    }
}
