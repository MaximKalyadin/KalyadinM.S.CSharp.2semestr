﻿using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface ISkladLogic
    {
        List<SkladViewModel> Read(SkladBindingModel model);
        void CreateOrUpdate(SkladBindingModel model);
        void Delete(SkladBindingModel model);
        void AddIngredients(AddIngredientSklad model);
        bool RemoveIngredients(OrderViewModel order);
    }
}
