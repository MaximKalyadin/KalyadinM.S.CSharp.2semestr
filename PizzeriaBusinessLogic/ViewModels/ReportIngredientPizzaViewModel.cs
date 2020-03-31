using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class ReportIngredientPizzaViewModel
    {
        public string IngredientName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Pizzas { get; set; }
    }
}
