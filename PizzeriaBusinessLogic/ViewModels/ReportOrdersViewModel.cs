using PizzeriaBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public string PizzaName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
    }
}
