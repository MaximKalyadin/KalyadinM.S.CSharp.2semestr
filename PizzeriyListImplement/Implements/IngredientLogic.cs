using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriyListImplement.Models;

namespace PizzeriyListImplement.Implements
{
    public class IngredientLogic : IIngredientLogic
    {
        private readonly DataListSingleton source;
        public IngredientLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(IngredientBindingModel model)
        {
            Ingredient tempComponent = model.Id.HasValue ? null : new Ingredient
            {
                Id = 1
            };
            foreach (var component in source.Ingredients)
            {
                if (component.IngredientName == model.IngredientName && component.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть ингредиент с таким названием");
                }
                if (!model.Id.HasValue && component.Id >= tempComponent.Id)
                {
                    tempComponent.Id = component.Id + 1;
                }
                else if (model.Id.HasValue && component.Id == model.Id)
            {
                    tempComponent = component;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempComponent == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempComponent);
            }
            else
            {
                source.Ingredients.Add(CreateModel(model, tempComponent));
            }
        }
        public void Delete(IngredientBindingModel model)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == model.Id.Value)
                {
                    source.Ingredients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<IngredientViewModel> Read(IngredientBindingModel model)
        {
            List<IngredientViewModel> result = new List<IngredientViewModel>();
            foreach (var component in source.Ingredients)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(component));
            }
            return result;
        }
        private Ingredient CreateModel(IngredientBindingModel model, Ingredient component)
        {
            component.IngredientName = model.IngredientName;
            return component;
        }
        private IngredientViewModel CreateViewModel(Ingredient component)
        {
            return new IngredientViewModel
            {
                Id = component.Id,
                IngredientName = component.IngredientName
            };
        }
    }
}
