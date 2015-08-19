using System.Collections.Generic;

namespace MealService
{
    public interface IOrder
    {
        string TimeOfDay { get; }
        IEnumerable<int> DishTypes { get; }
    }
}
