using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using PizzeriaBusinessLogic.Attributes;

namespace PizzeriaBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel : BaseViewModel
    {
        [DataMember]
        [Column(title: "Фио Клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { set; get; }
        [DataMember]
        public string Login { set; get; }
        [DataMember]
        public string Password { set; get; }

        public override List<string> Properties() => new List<string> { "Id", "ClientFIO" };
    }
}
