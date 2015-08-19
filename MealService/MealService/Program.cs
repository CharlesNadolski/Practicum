using System;

namespace MealService
{
    class Program
    {
        private static readonly IWaiter _waiter;

        static Program()
        {
            _waiter = new Waiter(new ReferenceData(new DishFactory()), new OrderFactory());
        }

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Enter meal time and dish types.  Type 'exit' when done.");
            var command = Console.ReadLine();
            while (command != "exit")
            {
                Console.WriteLine(_waiter.Serve(command));
                command = Console.ReadLine();
            }
        }
    }
}
