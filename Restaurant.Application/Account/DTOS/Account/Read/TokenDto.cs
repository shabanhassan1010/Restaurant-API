using Restaurant.Domain.Entities.Roles;

namespace Restaurant.Application.Account.DTOS.Account.Read
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public string UserName { get; set; }
    }
}
