using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaDatabaseImplement.Models;
using System.Linq;

namespace PizzeriaDatabaseImplement.Implements
{
    public class IngredientLogic : IIngredientLogic
    {
        public void CreateOrUpdate(IngredientBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                Ingredient element = context.Ingredients.FirstOrDefault(rec => rec.IngredientName == model.IngredientName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Ingredient();
                    context.Ingredients.Add(element);
                }
                element.IngredientName = model.IngredientName;
                context.SaveChanges();
            }
        }
        public void Delete(IngredientBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                Ingredient element = context.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Ingredients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<IngredientViewModel> Read(IngredientBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Ingredients
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new IngredientViewModel
                {
                    Id = rec.Id,
                    IngredientName = rec.IngredientName
                })
                .ToList();
            }
        }
    }
}
