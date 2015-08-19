using System.Collections.Generic;

namespace MealService
{
    /// <summary>
    /// A transposed and more user-friendly version of <see cref="DishDto"/>.
    /// It is intended to be immutable.
    /// </summary>
    internal class Dish : IDish
    {
        /// <summary>
        /// Maps the name of the dish to a specific meal time.
        /// If the meal time is not mapped, that means the dish is not available.
        /// </summary>
        public IDictionary<string, string> NameAtMealTime { get; private set; }
        /// <summary>
        /// Indicates that this dish can be ordered multiple times during a meal.
        /// </summary>
        public IDictionary<string, bool> MultipleAtMealTime { get; private set; }

        public Dish(IDictionary<string, string> nameAtMealTime, IDictionary<string, bool> multipleAtMealTime)
        {
            NameAtMealTime = nameAtMealTime;
            MultipleAtMealTime = multipleAtMealTime;
        }
    }
}
