using System.Collections.Generic;

namespace MealService
{
    public interface IReferenceData
    {
        void Load(string xmlPath);
        IDictionary<int, IDictionary<string, string>> Dishes { get; }
    }
}
