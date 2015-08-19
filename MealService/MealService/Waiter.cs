using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealService
{
    internal class Waiter : IWaiter
    {
        private IReferenceData _referenceData;

        public Waiter(IReferenceData referenceData)
        {
            _referenceData = referenceData;
            _referenceData.Load("ReferenceData.xml");
        }

        /// <summary>
        /// Waiter takes an order then serves a meal.
        /// It acts as a facade and encapsulates all the underlying business logic in a simple
        /// to use interface.
        /// </summary>
        /// <param name="order">String representation of an order.</param>
        /// <returns>String representation of a meal.</returns>
        public string Serve(string order)
        {
            throw new NotImplementedException();
        }
    }
}
