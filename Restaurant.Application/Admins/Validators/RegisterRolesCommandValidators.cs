using FluentValidation;
using Restaurant.Application.Admins.Dto;

namespace Restaurant.Application.Admins.Validators
{
    public class RegisterRolesCommandValidators : AbstractValidator<RegisterRoleDto>
    {
        public RegisterRolesCommandValidators()
        {
            RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .WithMessage("First name is required")
                    .MaximumLength(50)
                    .MinimumLength(3);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is required")
                .WithMessage("Email is not valid");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .WithMessage("Password must be at least 6 characters")
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^\d{10,15}$")
                .WithMessage("Phone number is not valid");
        }
    }
}
