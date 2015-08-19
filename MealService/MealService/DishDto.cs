using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealService
{
    /// <summary>
    /// This is a data transfer object to store updateable meta data about dishes.
    /// </summary>
    public class DishDto : MealService.IDishDto
    {
        public int DishType { get; set; }
        public string MorningDish { get; set; }
        public string EveningDish { get; set; }
    }
}
