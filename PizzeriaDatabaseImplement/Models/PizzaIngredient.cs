using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace PizzeriaDatabaseImplement.Models
{
    public class PizzaIngredient
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngredientId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
