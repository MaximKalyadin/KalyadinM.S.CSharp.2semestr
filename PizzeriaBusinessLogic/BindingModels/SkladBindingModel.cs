using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class SkladBindingModel
    {
        public int? Id { set; get; }
        public string SkladName { set; get; }
        public Dictionary<int, (string, int)> SkladIngredients { get; set; }
    }
}
