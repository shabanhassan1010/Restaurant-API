namespace Restaurant.Application.Users.DTOS
{
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
