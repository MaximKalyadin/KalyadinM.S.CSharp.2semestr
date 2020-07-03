using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class PizzaIngredientBindingModel
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int PizzaId { get; set; }
        public int Count { get; set; }
    }
}
