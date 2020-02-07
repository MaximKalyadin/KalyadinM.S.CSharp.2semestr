using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface IPizzaLogic
    {
        List<PizzaViewModel> GetList();
        PizzaViewModel GetElement(int id);
        void AddElement(PizzaBindingModel model);
        void UpdElement(PizzaBindingModel model);
        void DelElement(int id);
    }
}
