using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime TimeCreate { get; set; }
        [Required]
        public int? ClientId { set; get; }
        [Required]
        public string ClientFIO { set; get; }
        public int? ImplementerId { set; get; }
        public string ImplementerFIO { set; get; }
        public DateTime? TimeImplement { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Client Client { set; get; }
        public virtual Implementer Implementer { set; get; }
    }
}
