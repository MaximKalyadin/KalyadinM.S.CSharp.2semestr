using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PizzeriaBusinessLogic.Attributes;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class PizzaIngredientViewModel : BaseViewModel
    {
        public int PizzaId { get; set; }
        public int IngredientId { get; set; }
        [Column(title: "Ингредиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IngredientName { get; set; }
        [Column(title: "Количество", width: 100)]
        public int Count { get; set; }
        public override List<string> Properties() => new List<string> { "Id", "IngredientName", "Count" };
    }
}
