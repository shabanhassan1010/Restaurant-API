using FluentValidation;
using Restaurant.Application.Restaurants.Commands.CreateResturant;

namespace Restaurant.Application.Restaurants.Validators
{
    public class CreateResturantCommandValidators : AbstractValidator<CreateResturantCommand>
    {
        public CreateResturantCommandValidators()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is Requried")
               .Length(10, 300).WithMessage("Description must be between 10 and 300 characters.");

            RuleFor(dto => dto.Category)
                .NotEmpty().WithMessage("Category  is Requried");

            RuleFor(dto => dto.ContactEmail)
                .NotEmpty().WithMessage("Contact email is required.")
                .EmailAddress().WithMessage("Contact email is not valid.");

            RuleFor(dto => dto.ContactNumber)
                .Matches(@"^01[0-2,5]{1}[0-9]{8}$") // Egyptian phone number format
                .When(dto => !string.IsNullOrWhiteSpace(dto.ContactNumber))
                .WithMessage("Contact number is not a valid Egyptian phone number.");

            RuleFor(dto => dto.Address)
                .NotNull().WithMessage("Address is required.");

            RuleFor(dto => dto.Address.PostalCode)
                .NotEmpty().WithMessage("PostalCode is required.")
                .Matches(@"^\d{5}$").WithMessage("PostalCode must be exactly 5 digits.")
                .When(dto => dto.Address != null);
        }
    }
}
