using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class PizzaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название пиццы")]
        public string PizzaName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<PizzaIngredientViewModel> PizzaIngredients { get; set; }
    }
}
