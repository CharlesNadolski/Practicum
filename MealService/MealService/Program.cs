﻿
namespace MealService
{
    class Program
    {
        private static readonly IWaiter _waiter;

        static Program()
        {
            _waiter = new Waiter(new ReferenceData());
        }

        static void Main(string[] args)
        {
        }
    }
}
