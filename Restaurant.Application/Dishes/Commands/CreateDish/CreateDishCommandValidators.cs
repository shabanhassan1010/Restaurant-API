using FluentValidation;


namespace Restaurant.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidators : AbstractValidator<CreateDishCommand>
    { 
        public CreateDishCommandValidators()
        {
            RuleFor(dto => dto.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is Requried")
               .Length(10, 300).WithMessage("Description must be between 10 and 300 characters.");

            RuleFor(dto => dto.Price)
                .NotEmpty().WithMessage("Price  is Requried");

            RuleFor(dto => dto.RestaurantId)
                .NotEmpty().WithMessage("RestaurantId  is Requried");
        }
    }
}
