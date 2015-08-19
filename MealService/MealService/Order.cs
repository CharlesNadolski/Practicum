using System.Collections.Generic;

namespace MealService
{
    internal class Order : IOrder
    {
        public string TimeOfDay { get; private set; }
        public IEnumerable<int> DishTypes { get; private set; }

        public Order(string timeOfDay, IEnumerable<int> dishTypes)
        {
            TimeOfDay = timeOfDay;
            DishTypes = dishTypes;
        }
    }
}
