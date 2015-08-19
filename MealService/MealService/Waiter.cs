using System;

namespace MealService
{
    internal class Waiter : IWaiter
    {
        private IReferenceData _referenceData;
        private IOrderFactory _orderFactory;

        public Waiter(IReferenceData referenceData, IOrderFactory orderFactory)
        {
            _referenceData = referenceData;
            _referenceData.Load("ReferenceData.xml");
            _orderFactory = orderFactory;
        }

        /// <summary>
        /// Waiter takes an order then serves a meal.
        /// It acts as a facade and encapsulates all the underlying business logic in a simple
        /// to use interface.
        /// </summary>
        /// <param name="inputOrder">String representation of an order.</param>
        /// <returns>String representation of a meal.</returns>
        public string Serve(string inputOrder)
        {
            var parsedOrder = _orderFactory.Parse(inputOrder);
            throw new NotImplementedException();
        }
    }
}
