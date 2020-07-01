using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaRestApi.Model
{
    public class PizzaModel
    {
        public int Id { set; get; }
        public string PizzaName { set; get; }
        public decimal Price { set; get; }
    }
}
