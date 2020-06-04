using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaDatabaseImplement.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required]
        public string PizzaName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("PizzaId")]
        public virtual List<PizzaIngredient> PizzaIngredients { get; set; }
        [ForeignKey("PizzaId")]
        public virtual List<Order> Order { get; set; }
    }
}

