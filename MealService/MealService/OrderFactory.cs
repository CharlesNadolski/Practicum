using System;
using System.Linq;

namespace MealService
{
    internal class OrderFactory : MealService.IOrderFactory
    {
        public IOrder Parse(string inputOrder)
        {
            var orderArguments = inputOrder.Split(new[] { Constants.DishSeparator }, StringSplitOptions.None);
            return new Order(
                orderArguments[0],
                //Dish types must always be ascending order.
                orderArguments.Skip(1).Select(arg => int.Parse(arg)).OrderBy(key => key));
        }
    }
}
