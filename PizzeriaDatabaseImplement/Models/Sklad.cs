using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaDatabaseImplement.Models
{
    public class Sklad
    {
        public int Id { set; get; }
        [Required]
        public string SkladName { set; get; }
        [ForeignKey("SkaldId")]
        public virtual List<SkladIngredient> SkladIngredients { set; get; }
    }
}
