using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PizzeriaBusinessLogic.Enums;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        [DisplayName("Пицца")]
        public string PizzaName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
        [DisplayName("Время создания")]
        public DateTime TimeCreate { get; set; }
        [DisplayName("Время выполнения")]
        public DateTime? TimeImplement { get; set; }

    }
}
