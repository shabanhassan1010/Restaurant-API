using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Application.Admins.Dto;
namespace Restaurant.Application.Admins.Command.Login
{
    public class RegisterAdminAndOwnerCommand : IRequest<IdentityResult>
    {
        public RegisterRoleDto registerRoleDto;

        public RegisterAdminAndOwnerCommand(RegisterRoleDto registerRoleDto)
        {
            this.registerRoleDto = registerRoleDto;
        }
    }
}
