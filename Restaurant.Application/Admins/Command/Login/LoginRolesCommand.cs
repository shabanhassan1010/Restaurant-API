using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Admins.Dto;

namespace Restaurant.Application.Admins.Command.Register
{
    public class LoginRolesCommand : IRequest<TokenDto>
    {
        public LoginRoleDto loginRoleDto;

        public LoginRolesCommand(LoginRoleDto loginRoleDto)
        {
            this.loginRoleDto = loginRoleDto;
        }
    }
}
