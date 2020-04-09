using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class ReportSkladViewModel
    {
        public string SkladName { set; get; }
        public Dictionary<int, (string, int)> Ingredients { set; get; }
    }
}
