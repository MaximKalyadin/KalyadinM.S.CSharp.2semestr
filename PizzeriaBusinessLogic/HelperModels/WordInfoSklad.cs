using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.HelperModels
{
    public class WordInfoSklad
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<SkladViewModel> Sklads { get; set; }
    }
}
