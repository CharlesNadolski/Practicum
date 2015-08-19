using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MealService
{
    internal class DishFactory : MealService.IDishFactory
    {
        /// <summary>
        /// The text fields of DishDto represent the times of day that we have meals
        /// </summary>
        private static readonly PropertyInfo[] TimeOfDayProperties =
            typeof(DishDto).GetProperties().Where(property => property.PropertyType == typeof(DishDescripionDto)).ToArray();

        public IDish Transform(IDishDto dishDto)
        {
            var namesAtMealTime = new Dictionary<string, string>();
            var multipleAtMealTime = new Dictionary<string, bool>();
            foreach (var propertyInfo in TimeOfDayProperties)
            {
                var dishTimeValue = (DishDescripionDto)propertyInfo.GetValue(dishDto);
                if (dishTimeValue != null)
                {
                    namesAtMealTime.Add(propertyInfo.Name, dishTimeValue.Name);
                    multipleAtMealTime.Add(propertyInfo.Name, dishTimeValue.AllowMultiple);
                }
            }
            return new Dish(namesAtMealTime, multipleAtMealTime);
        }
    }
}
