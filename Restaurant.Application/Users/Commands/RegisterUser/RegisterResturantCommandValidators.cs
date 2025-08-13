using FluentValidation;
namespace Restaurant.Application.Users.Commands.RegisterUser
{
    public class RegisterResturantCommandValidators : AbstractValidator<RegisterUserCommand>
    {
        public RegisterResturantCommandValidators()
        {
            RuleFor(x => x.Dto.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .MinimumLength(3);

            RuleFor(x => x.Dto.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(x => x.Dto.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is required")
                .WithMessage("Email is not valid");

            RuleFor(x => x.Dto.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .WithMessage("Password must be at least 6 characters")
                .MinimumLength(6);

            RuleFor(x=>x.Dto.ConfirmPassword)
                .Equal(x=>x.Dto.Password)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.Dto.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^\d{10,15}$")
                .WithMessage("Phone number is not valid");
        }
    }
}
