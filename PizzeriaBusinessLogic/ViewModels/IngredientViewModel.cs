using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PizzeriaBusinessLogic.Attributes;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class IngredientViewModel : BaseViewModel
    {
        [Column(title: "Ингредиет", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IngredientName { get; set; }
        public override List<string> Properties() => new List<string> {"Id", "IngredientName" };
    }
}
