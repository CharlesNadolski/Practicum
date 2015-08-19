
namespace MealService
{
    public interface IWaiter
    {
        /// <summary>
        /// Waiter takes an order then serves a meal.
        /// </summary>
        /// <param name="inputOrder">String representation of an order.</param>
        /// <returns>String representation of a meal.</returns>
        string Serve(string inputOrder);
    }
}
