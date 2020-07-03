using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.ViewModels;

namespace PizzeriaBusinessLogic.HelperModels
{
    public class ExcelInfoSklad
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportSkladViewModel> Sklads { get; set; }
    }
}
