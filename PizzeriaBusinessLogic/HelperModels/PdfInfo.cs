using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportPizzaIngredientViewModel> PizzaIngredients { get; set; }
    }
}
