using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealService
{
    public interface IWaiter
    {
        string Serve(string order);
    }
}
