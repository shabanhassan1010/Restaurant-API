using FluentValidation;
using Restaurant.Application.Admins.Dto;

namespace Restaurant.Application.Admins.Validators
{
    public class LoginRolesCommandValidators : AbstractValidator<LoginRoleDto>
    {
        public LoginRolesCommandValidators()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
