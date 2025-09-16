using MediatR;

namespace Restaurant.Application.Dishes.Commands.CreateDish;

public class CreateDishCommand:IRequest<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? KillCalories { get; set; }
    public int RestaurantId { get; set; }
}
