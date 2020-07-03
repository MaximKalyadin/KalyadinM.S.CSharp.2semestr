using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaBusinessLogic.Enums;

namespace PizzeriyListImplement.Models
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
        public int? ClientId { set; get; }
        public int? ImplementerId { set; get; }

    }
}
