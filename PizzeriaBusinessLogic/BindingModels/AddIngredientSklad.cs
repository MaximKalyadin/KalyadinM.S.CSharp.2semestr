using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class AddIngredientSklad
    {
        public int SkladId { set; get; }
        public int IngredientId { set; get; }
        public int Count { set; get; }
    }
}
