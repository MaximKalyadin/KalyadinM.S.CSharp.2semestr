using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PizzeriaBusinessLogic.Enums;
using System.Runtime.Serialization;
using PizzeriaBusinessLogic.Attributes;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        [DataMember]
        public int PizzaId { get; set; }
        [DataMember]
        [Column(title: "Пицца", width: 150)]
        public string PizzaName { get; set; }
        [DataMember]
        [Column(title: "Количество", width: 100)]
        public int Count { get; set; }
        [DataMember]
        [Column(title: "Сумма", width: 50)]
        public decimal Sum { get; set; }
        [Column(title: "Исполнитель", width: 150)]
        public string ImplementerFIO { set; get; }
        [DataMember]
        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }
        [DataMember]
        [Column(title: "Время создания", width: 100)]
        public DateTime TimeCreate { get; set; }
        [DataMember]
        [Column(title: "Время выполнения", width: 100)]
        public DateTime? TimeImplement { get; set; }
        [DataMember]
        public int? ClientId { set; get; }
        [DataMember]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { set; get; }

        public int? ImplementerId { set; get; }
        public override List<string> Properties() => new List<string> { "Id", "ClientFIO", "PizzaName", "ImplementerFIO", "Count", "Sum", "Status", "TimeCreate", "TimeImplement" };
    }
}
