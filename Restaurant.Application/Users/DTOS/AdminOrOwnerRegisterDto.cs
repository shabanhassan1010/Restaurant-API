using Restaurant.Application.Account.DTOS.Account.Write;
namespace Restaurant.Application.Users.DTOS
{
    public class AdminOrOwnerRegisterDto : RegisterDto
    {
        public string Role { get; set; } // Admin or Owner 
    }
}
