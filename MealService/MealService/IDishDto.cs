using System;

namespace MealService
{
    /// <summary>
    /// This is a data transfer object to store updateable meta data about dishes.
    /// Public modifiers get with set are required by the .net serializer.
    /// </summary>
    public interface IDishDto
    {
        int DishType { get; }
        DishDescripionDto morning { get; }
        DishDescripionDto night { get; }
    }
}
