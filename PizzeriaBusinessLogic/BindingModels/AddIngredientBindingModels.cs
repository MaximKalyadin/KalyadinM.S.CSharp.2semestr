using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class AddIngredientBindingModels
    {
        [DataMember]
        public int SkladId { set; get; }
        [DataMember]
        public int IngredientId { set; get; }
        [DataMember]
        public int Count { set; get; }
    }
}
