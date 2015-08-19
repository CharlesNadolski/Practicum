using System;

namespace MealService
{
    public interface IDishDto
    {
        int DishType { get; }
        string EveningDish { get; }
        string MorningDish { get; }
    }
}
