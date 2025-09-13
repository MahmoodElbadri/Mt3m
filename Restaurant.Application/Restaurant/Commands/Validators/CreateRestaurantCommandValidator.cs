using FluentValidation;
using Restaurant.Application.Restaurant.Commands.CreateRestaurant;
using Restaurant.Application.Restaurant.Dtos;

namespace Restaurant.Application.Restaurant.Commands.Validators;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> allowedCategories = ["Fast Food", "Italian", "Chinese", "Indian", "Mexican"];
    public CreateRestaurantCommandValidator()
    {

        RuleFor(tmp => tmp.Category)
        .Must(x => allowedCategories.Any(allowedCategory => x.Contains(allowedCategory)))
        .WithMessage("Category must be one of the following: " + string.Join(", ", allowedCategories));

        RuleFor(tmp => tmp.Name)
            .Length(3, 100);
        RuleFor(tmp => tmp.Category)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(tmp => tmp.Description)
            .NotEmpty()
            .WithMessage("Description is required");

        RuleFor(tmp => tmp.ContactEmail)
            .EmailAddress()
            .WithMessage("Contact email is not valid");

        RuleFor(tmp => tmp.ContactNumber)
            .NotEmpty()
            .WithMessage("Contact number is required");
    }
}
