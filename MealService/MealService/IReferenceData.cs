using System.Collections.Generic;

namespace MealService
{
    public interface IReferenceData
    {
        void Load(string xmlPath);
        IDictionary<int, IDish> Dishes { get; }
    }
}
