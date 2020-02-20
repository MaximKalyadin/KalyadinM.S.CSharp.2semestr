using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.Enums;

namespace PizzeriaFileImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime? TimeImplement { get; set; }
    }
}
