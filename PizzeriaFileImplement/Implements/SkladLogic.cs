using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaFileImplement.Models;

namespace PizzeriaFileImplement.Implements
{
    public class SkladLogic : ISkladLogic
    {
        private readonly FileDataListSingleton source;
        public SkladLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(SkladBindingModel model)
        {
            Sklad element = source.Sklads.FirstOrDefault(s => s.SkladName == model.SkladName && s.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть ингредиент с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Sklads.FirstOrDefault(s => s.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Sklads.Count > 0 ? source.Sklads.Max(s => s.Id) : 0;
                element = new Sklad { Id = maxId + 1 };
                source.Sklads.Add(element);
            }
            element.SkladName = model.SkladName;
        }
        public void Delete(SkladBindingModel model)
        {
            Sklad sklad = source.Sklads.FirstOrDefault(s => s.Id == model.Id);
            if (sklad != null)
            {
                source.Sklads.Remove(sklad);
            }
            else
            { 
                throw new Exception("Склад не найден");
            }
        }
        public List<SkladViewModel> Read(SkladBindingModel model)
        {
            return source.Sklads
            .Where(s => model == null || s.Id == model.Id)
            .Select(s => new SkladViewModel
            {
                Id = s.Id,
                SkladName = s.SkladName,
                SkladIngredients = source.SkladIngredients
                    .Where(sm => sm.SkladId == s.Id)
                    .ToDictionary(k => source.Ingredients.FirstOrDefault(m => m.Id == k.IngredientId).IngredientName, k => k.Count)
            })
            .ToList();
        }

        public void AddIngredientToSklad(AddIngredientBindingModels model)
        {
            if (source.SkladIngredients.Count == 0)
            {
                source.SkladIngredients.Add(new SkladIngredient()
                {
                    Id = 1,
                    IngredientId = model.IngredientId,
                    SkladId = model.SkladId,
                    Count = model.Count
                });
            }
            else
            {
                var ingredient = source.SkladIngredients.FirstOrDefault(sm => sm.SkladId == model.SkladId && sm.IngredientId == model.IngredientId);
                if (ingredient == null)
                {
                    source.SkladIngredients.Add(new SkladIngredient()
                    {
                        Id = source.SkladIngredients.Max(sm => sm.Id) + 1,
                        IngredientId = model.IngredientId,
                        SkladId = model.SkladId,
                        Count = model.Count
                    });
                }
                else
                    ingredient.Count += model.Count;
            }
        }

        private bool CheckingStoragedMaterials(OrderViewModel order)
        {
            var pizzaIngredient = source.PizzaIngredients.Where(dm => dm.PizzaId == order.PizzaId);
            var ingredientSklad = new Dictionary<int, int>();
            foreach (var sm in source.SkladIngredients)
            {
                if (ingredientSklad.ContainsKey(sm.IngredientId))
                    ingredientSklad[sm.IngredientId] += sm.Count;
                else
                    ingredientSklad.Add(sm.IngredientId, sm.Count);
            }

            foreach (var dm in pizzaIngredient)
            {
                if (!ingredientSklad.ContainsKey(dm.IngredientId) || ingredientSklad[dm.IngredientId] < dm.Count * order.Count)
                    return false;
            }

            return true;
        }

        public bool RemoveIngredients(OrderViewModel order)
        {
            if (CheckingStoragedMaterials(order))
            {
                var pizzaIngredient = source.PizzaIngredients.Where(dm => dm.PizzaId == order.PizzaId);
                foreach (var dm in pizzaIngredient)
                {
                    int IngrCount = dm.Count * order.Count;
                    foreach (var sm in source.SkladIngredients)
                    {
                        if (sm.IngredientId == dm.IngredientId && sm.Count >= IngrCount)
                        {
                            sm.Count -= IngrCount;
                            break;
                        }
                        else if (sm.IngredientId == dm.IngredientId && sm.Count < IngrCount)
                        {
                            IngrCount -= sm.Count;
                            sm.Count = 0;
                        }
                    }
                }
                return true;
            }
            else
                return false;
        }
    }
}
