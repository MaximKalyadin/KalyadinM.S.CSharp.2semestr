using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriyListImplement.Models;

namespace PizzeriyListImplement.Implements
{
    public class PizzaLogic : IPizzaLogic
    {
        private readonly DataListSingleton source;
        public PizzaLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            Pizza tempProduct = model.Id.HasValue ? null : new Pizza { Id = 1 };
            foreach (var product in source.Pizza)
            {
                if (product.PizzaName == model.PizzaName && product.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
                else if (model.Id.HasValue && product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (model.Id.HasValue)
            {
            if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Pizza.Add(CreateModel(model, tempProduct));
            }
        }
        public void Delete(PizzaBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == model.Id)
                {
                    source.PizzaIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Pizza.Count; ++i)
            {
                if (source.Pizza[i].Id == model.Id)
                {
                    source.Pizza.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Pizza CreateModel(PizzaBindingModel model, Pizza product)
        {
            product.PizzaName = model.PizzaName;
            product.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredients[i].Id;
                }
                if (source.PizzaIngredients[i].PizzaId == product.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.PizzaIngredients.ContainsKey(source.PizzaIngredients[i].IngredientId))
                    {
                        // обновляем количество
                        source.PizzaIngredients[i].Count =
                        model.PizzaIngredients[source.PizzaIngredients[i].IngredientId].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные
                    
model.PizzaIngredients.Remove(source.PizzaIngredients[i].IngredientId);
                    }
                    else
                    {
                        source.PizzaIngredients.RemoveAt(i--);
                     }
                }
            }
            // новые записи
            foreach (var pc in model.PizzaIngredients)
            {
                source.PizzaIngredients.Add(new PizzaIngredient
                {
                    Id = ++maxPCId,
                    PizzaId = product.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return product;
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var component in source.Pizza)
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
        private PizzaViewModel CreateViewModel(Pizza product)
        {
            // требуется дополнительно получить список компонентов для изделия с  названиями и их количество
        Dictionary<int, (string, int)> pizzaIngredients = new Dictionary<int,
(string, int)>();
            foreach (var pc in source.PizzaIngredients)
            {
                if (pc.PizzaId == product.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Ingredients)
                    {
                        if (pc.IngredientId == component.Id)
                        {
                            componentName = component.IngredientName;
                            break;
                        }
                    }
                    pizzaIngredients.Add(pc.IngredientId, (componentName, pc.Count));
                }
            }
            return new PizzaViewModel
            {
                Id = product.Id,
                PizzaName = product.PizzaName,
                Price = product.Price,
                PizzaIngredients = pizzaIngredients
            };
        }
    }
}
