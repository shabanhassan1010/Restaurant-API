﻿using Restaurant.Domain.Entities.Roles;

namespace Restaurant.Application.Account.DTOS.Account.Write
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? Role { get; set; }
    }
}
