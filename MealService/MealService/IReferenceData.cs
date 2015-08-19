using System;
using System.Collections.Generic;

namespace MealService
{
    public interface IReferenceData
    {
        void Load(string xmlPath);
        IReadOnlyCollection<IDishDto> Dishes { get; }
    }
}
