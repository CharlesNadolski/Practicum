using System.Collections.Generic;
using System.Linq;

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
            var meals = new List<string>();
            var parsedOrder = _orderFactory.Parse(inputOrder);
            foreach(var dishType in parsedOrder.DishTypes)
            {
                IDish matchingDish;
                string dishName;
                //First check if the dish type actually exists, and is valid for this time of day
                if (_referenceData.Dishes.TryGetValue(dishType, out matchingDish) &&
                    matchingDish.NameAtMealTime.TryGetValue(parsedOrder.TimeOfDay, out dishName))
                {
                    //Now check if duplicates are supported
                    //Heuristic: we can assume that since the name is valid, the duplicate boolean is also valid
                    var allowMultiple = matchingDish.MultipleAtMealTime[parsedOrder.TimeOfDay];
                    if (allowMultiple || !meals.Contains(dishName))
                    {
                        meals.Add(dishName);
                    }
                    else
                    {
                        meals.Add(Constants.InvalidDish);
                        break;
                    }
                }
                else
                {
                    meals.Add(Constants.InvalidDish);
                    break;
                }
            }
            meals = MergeDuplicates(meals);
            return string.Join(Constants.DishSeparator, meals);
        }

        /// <summary>
        /// Takes duplicates in the list and merges them together with a suffix indicating how often it was duplicated.
        /// </summary>
        private static List<string> MergeDuplicates(List<string> meals)
        {
            var mergedMeals = new List<string>();
            //Start by finding the unique meals
            var distinctMeals = meals.Distinct().ToArray();
            //Now we can how how many appearred in the original list and add the suffix.
            foreach(string distinctMeal in distinctMeals)
            {
                var numberOfEntries = meals.Count(distinctMeal.Equals);
                if (numberOfEntries == 1)
                {
                    mergedMeals.Add(distinctMeal);
                }
                else
                {
                    mergedMeals.Add(string.Format("{0}(x{1})", distinctMeal, numberOfEntries));
                }
            }
            return mergedMeals;
        }
    }
}
