using FluentValidation;
using Restaurant.Application.Users.DTOS;

namespace Restaurant.Application.Users.Validators
{
    public class forgetPasswordCommandValidator : AbstractValidator<ForgotPasswordDto>
    {
        public forgetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .WithMessage("Email is required")
                    .WithMessage("Email is not valid");
        }
    }
}
