using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaFileImplement.Models;
using PizzeriaBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class IngredientLogic : IIngredientLogic
    {
        private readonly FileDataListSingleton source;
        public IngredientLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(IngredientBindingModel model)
        {
            Ingredient element = source.Ingredients.FirstOrDefault(rec => rec.IngredientName
           == model.IngredientName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть ингредиент с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Ingredients.Count > 0 ? source.Ingredients.Max(rec =>
               rec.Id) : 0;
                element = new Ingredient { Id = maxId + 1 };
                source.Ingredients.Add(element);
            }
            element.IngredientName = model.IngredientName;
        }
        public void Delete(IngredientBindingModel model)
        {
            Ingredient element = source.Ingredients.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Ingredients.Remove(element);
            }
            else
            {
            throw new Exception("Элемент не найден");
            }
        }
        public List<IngredientViewModel> Read(IngredientBindingModel model)
        {
            return source.Ingredients
            .Where(rec => model == null || (model.Id.HasValue && rec.Id == model.Id))
            .Select(rec => new IngredientViewModel
            {
                Id = rec.Id,
                IngredientName = rec.IngredientName
            })
            .ToList();
        }

    }
}
