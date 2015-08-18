using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealService
{
    class Program
    {
        private static readonly IWaiter _waiter;

        static Program()
        {
            _waiter = new Waiter();
        }

        static void Main(string[] args)
        {
        }
    }
}
