using FluentValidation;
using Restaurant.Application.Users.DTOS;

namespace Restaurant.Application.Users.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
