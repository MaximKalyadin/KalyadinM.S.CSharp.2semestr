using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaDatabaseImplement.Models
{
    public class SkladIngredient
    {
        public int Id { set; get; }
        public int SkladId { set; get; }
        public int Ingredientid { set; get; }
        [Required]
        public int Count { set; get; }
        public virtual Sklad Sklad { set; get; }
        public virtual Ingredient Ingredient { set; get; }
    }
}
