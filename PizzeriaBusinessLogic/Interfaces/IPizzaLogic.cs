using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface IPizzaLogic
    {
        List<PizzaViewModel> Read(PizzaBindingModel model);
        void CreateOrUpdate(PizzaBindingModel model);
        void Delete(PizzaBindingModel model);
    }
}
