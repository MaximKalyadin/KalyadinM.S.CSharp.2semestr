using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using PizzeriaBusinessLogic.Attributes;

namespace PizzeriaBusinessLogic.ViewModels
{
    [DataContract]
    public class PizzaViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "Пицца", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string PizzaName { get; set; }
        [DataMember]
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> PizzaIngredients { get; set; }
        public override List<string> Properties() => new List<string> { "Id", "PizzaName", "Price" };
    }
}
