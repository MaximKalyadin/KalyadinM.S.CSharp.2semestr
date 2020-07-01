using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class SkladBindingModel
    {
        [DataMember]
        public int? Id { set; get; }
        [DataMember]
        public string SkladName { set; get; }
    }
}
