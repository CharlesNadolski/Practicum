
namespace MealService
{
    /// <summary>
    /// This is a data transfer object to store updateable meta data about dishes.
    /// Public modifiers get with set are required by the .net serializer.
    /// </summary>
    public class DishDto
    {
        public int DishType { get; set; }
        public string morning { get; set; }
        public string night { get; set; }
    }
}
