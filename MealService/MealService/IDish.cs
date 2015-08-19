using System.Collections.Generic;

namespace MealService
{
    /// <summary>
    /// A transposed and more user-friendly version of <see cref="DishDto"/>.
    /// It is intended to be immutable.
    /// </summary>
    public interface IDish
    {
        /// <summary>
        /// Maps the name of the dish to a specific meal time.
        /// If the meal time is not mapped, that means the dish is not available.
        /// </summary>
        IDictionary<string, string> NameAtMealTime { get; }
        /// <summary>
        /// Indicates that this dish can be ordered multiple times during a meal.
        /// </summary>
        IDictionary<string, bool> MultipleAtMealTime { get; }
    }
}
