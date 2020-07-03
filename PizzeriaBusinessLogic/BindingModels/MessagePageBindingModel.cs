using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class MessagePageBindingModel
    {
        public int? ClientId { set; get; }
        public int pageNumber { set; get; }
    }
}
