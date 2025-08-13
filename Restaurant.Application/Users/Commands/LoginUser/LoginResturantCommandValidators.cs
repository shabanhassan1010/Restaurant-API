using FluentValidation;
using Restaurant.Application.Users.DTOS;


namespace Restaurant.Application.Users.Commands.LoginUser
{
    public class LoginResturantCommandValidators : AbstractValidator<LoginUserDto>
    {
        public LoginResturantCommandValidators()
        {
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

        }
    }
}
