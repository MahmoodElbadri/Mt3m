using FluentValidation;
using Restaurant.Application.Dishes.Commands.CreateDish;

namespace Restaurant.Application.Dishes.Commands.Validators;

public class CreateDishCommandValidator: AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(tmp=>tmp.KillCalories)
            .GreaterThan(0)
            .WithMessage("The killCalories field must be greater than 0")
            .LessThan(1000)
            .WithMessage("The killCalories field must be less than 1000");

        RuleFor(tmp=>tmp.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The price field must be greater than or equal to 0");

        RuleFor(tmp => tmp.Name)
            .NotEmpty()
            .WithMessage("The name field is required");

        RuleFor(tmp => tmp.Description)
            .NotEmpty()
            .WithMessage("The description field is required");
    }
}

