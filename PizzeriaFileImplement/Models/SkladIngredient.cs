using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaFileImplement.Models
{
    public class SkladIngredient
    {
        public int Id { set; get; }
        public int SkladId { set; get; }
        public int IngredientId { set; get; }
        public int Count { set; get; }
    }
}
