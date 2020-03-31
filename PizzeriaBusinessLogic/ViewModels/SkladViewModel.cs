using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class SkladViewModel
    {
        public int Id { set; get; }
        [DisplayName("Склад")]
        public string SkladName { set; get; }
        public Dictionary<int, (string, int)> SkladIngredients { get; set; }
    }
}
