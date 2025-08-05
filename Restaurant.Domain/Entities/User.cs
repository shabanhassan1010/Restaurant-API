using Microsoft.AspNetCore.Identity;

namespace Restaurant.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}