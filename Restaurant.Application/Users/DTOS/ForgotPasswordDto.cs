using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Users.DTOS
{
    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
