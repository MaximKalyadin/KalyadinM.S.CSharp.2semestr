using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.HelperModels
{
    public class PdfInfoIngredientSklad
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportIngredientSkladViewModel> IngredientSklads { get; set; }
    }
}
