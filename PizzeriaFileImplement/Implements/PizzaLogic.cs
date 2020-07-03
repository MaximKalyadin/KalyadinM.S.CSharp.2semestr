using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.Interfaces;
using System.Linq;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaFileImplement.Models;

namespace PizzeriaFileImplement.Implements
{
    public class PizzaLogic : IPizzaLogic
    {
        private readonly FileDataListSingleton source;
        public PizzaLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.PizzaName ==
           model.PizzaName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть пицца с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Pizzas.Count > 0 ? source.Ingredients.Max(rec =>
               rec.Id) : 0;
                element = new Pizza { Id = maxId + 1 };
                source.Pizzas.Add(element);
            }
            element.PizzaName = model.PizzaName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.PizzaIngredients.RemoveAll(rec => rec.PizzaId == model.Id &&
           !model.PizzaIngredients.ContainsKey(rec.IngredientId));
            // обновили количество у существующих записей
            var updateComponents = source.PizzaIngredients.Where(rec => rec.IngredientId ==
           model.Id && model.PizzaIngredients.ContainsKey(rec.IngredientId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count =
               model.PizzaIngredients[updateComponent.IngredientId].Item2;
                model.PizzaIngredients.Remove(updateComponent.IngredientId);
            }
            // добавили новые
            int maxPCId = source.PizzaIngredients.Count > 0 ?
           source.PizzaIngredients.Max(rec => rec.Id) : 0;
        foreach (var pc in model.PizzaIngredients)
            {
                source.PizzaIngredients.Add(new PizzaIngredient
                {
                    Id = ++maxPCId,
                    PizzaId = element.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(PizzaBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.PizzaIngredients.RemoveAll(rec => rec.PizzaId == model.Id);
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Pizzas.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            return source.Pizzas
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new PizzaViewModel
            {
                Id = rec.Id,
                PizzaName = rec.PizzaName,
                Price = rec.Price,
                PizzaIngredients = source.PizzaIngredients
            .Where(recPC => recPC.PizzaId == rec.Id)
           .ToDictionary(recPC => recPC.IngredientId, recPC =>
            (source.Ingredients.FirstOrDefault(recC => recC.Id ==
           recPC.IngredientId)?.IngredientName, recPC.Count))
            })
            .ToList();
        }

    }
}
