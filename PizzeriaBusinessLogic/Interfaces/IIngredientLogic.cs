using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel> GetList();
        IngredientViewModel GetElement(int id);
        void AddElement(IngredientBindingModel model);
        void UpdElement(IngredientBindingModel model);
        void DelElement(int id);
    }
}
