using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PizzeriaBusinessLogic.Enums;
using System.Runtime.Serialization;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PizzaId { get; set; }
        [DataMember]
        [DisplayName("Пицца")]
        public string PizzaName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
        [DataMember]
        [DisplayName("Время создания")]
        public DateTime TimeCreate { get; set; }
        [DataMember]
        [DisplayName("Время выполнения")]
        public DateTime? TimeImplement { get; set; }
        [DataMember]
        public int ClientId { set; get; }
        [DataMember]
        [DisplayName("ФИО клиента")]
        public string ClientFIO { set; get; }
    }
}
