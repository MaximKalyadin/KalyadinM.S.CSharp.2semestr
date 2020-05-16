using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriyListImplement.Models
{
    public class SkladIngredients
    {
        public int Id { set; get; }
        public int SkladId { set; get; }
        public int Ingredientid { set; get; }
        public int Count { set; get; }
    }
}
