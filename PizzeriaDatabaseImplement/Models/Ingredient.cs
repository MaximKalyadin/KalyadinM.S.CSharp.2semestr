﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaDatabaseImplement.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [ForeignKey("IngredientId")]
        public virtual List<PizzaIngredient> PizzaIngredients { get; set; }
        [ForeignKey("IngredientId")]
        public virtual List<SkladIngredient> SkladMaterials { get; set; }
    }
}
