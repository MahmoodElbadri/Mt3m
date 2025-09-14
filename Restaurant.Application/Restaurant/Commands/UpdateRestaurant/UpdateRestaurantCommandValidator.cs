using FluentValidation;

namespace Restaurant.Application.Restaurant.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        /*public string? Name { get; set; }
    public string? Description { get; set; }
    public bool HasDelivery { get; set; }*/
        RuleFor(tmp=>tmp.Name)
            .NotEmpty().WithMessage("Name is required from FLUENT VALIDATION")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters");
        RuleFor(tmp=>tmp.Description)
            .NotEmpty().WithMessage("Description is required from FLUENT VALIDATION")
            .MaximumLength(200).WithMessage("Description must be less than 200 characters");
    }
}
