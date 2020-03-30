using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel> Read(IngredientBindingModel model);
        void CreateOrUpdate(IngredientBindingModel model);
        void Delete(IngredientBindingModel model);
    }
}
