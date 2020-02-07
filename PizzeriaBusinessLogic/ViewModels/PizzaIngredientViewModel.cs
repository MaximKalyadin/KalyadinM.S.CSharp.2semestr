using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class PizzaIngredientViewModel
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngredientId { get; set; }
        [DisplayName("Ингредиент")]
        public string IngredientName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
