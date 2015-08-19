using System;

namespace MealService
{
    public interface IOrderFactory
    {
        IOrder Parse(string inputOrder);
    }
}
