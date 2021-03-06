﻿using System;

namespace MealService
{
    /// <summary>
    /// This is a data transfer object to store updateable meta data about dishes.
    /// Public modifiers get with set are required by the .net serializer.
    /// </summary>
    public class DishDto : IDishDto
    {
        public int DishType { get; set; }
        public DishDescripionDto morning { get; set; }
        public DishDescripionDto night { get; set; }
    }
}
