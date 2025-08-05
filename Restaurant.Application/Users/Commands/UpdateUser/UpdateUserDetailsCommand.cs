using MediatR;

namespace Restaurant.Application.Users.Commands.UpdateUser
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
