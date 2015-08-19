using System;
using System.Linq;

namespace MealService
{
    internal class OrderFactory : MealService.IOrderFactory
    {
        public IOrder Parse(string inputOrder)
        {
            var orderArguments = inputOrder.Split(new[] { ", " }, StringSplitOptions.None);
            return new Order(
                orderArguments[0],
                orderArguments.Skip(1).Select(arg => int.Parse(arg)));
        }
    }
}
