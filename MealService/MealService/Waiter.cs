using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealService
{
    internal class Waiter : IWaiter
    {
        /// <summary>
        /// Waiter takes an order then serves a meal.
        /// </summary>
        /// <param name="order">String representation of an order.</param>
        /// <returns>String representation of a meal.</returns>
        public string Serve(string order)
        {
            throw new NotImplementedException();
        }
    }
}
