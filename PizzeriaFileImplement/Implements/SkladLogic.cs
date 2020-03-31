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
                var skladIngredients = source.SkladIngredients.Where(sm => sm.SkladId == element.Id).ToList();
                for (int i = 0; i < skladIngredients.Count; i++)
                {
                    if (model.SkladIngredients.ContainsKey(skladIngredients[i].IngredientId))
                    {
                        skladIngredients[i].Count = model.SkladIngredients[skladIngredients[i].IngredientId].Item2;
                    }
                    else
                    {
                        skladIngredients.RemoveAt(i);

                    }
                }
                var keysIngredients = model.SkladIngredients.Keys;
                int maxId = source.SkladIngredients.Count > 0 ? source.SkladIngredients.Count : 0;
                foreach (var k in keysIngredients)
                {
                    if (!source.SkladIngredients.Where(sm => sm.SkladId == element.Id).Select(sm => sm.IngredientId).Contains(k)) source.SkladIngredients.Add(new SkladIngredient()
                        {
                            Id = ++maxId,
                            IngredientId = k,
                            SkladId = element.Id,
                            Count = model.SkladIngredients[k].Item2
                        });
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
                    .Select(sm => (source.Ingredients.FirstOrDefault(m => m.Id == sm.IngredientId).IngredientName, sm.Count))
                    .ToDictionary(k => source.Ingredients.FirstOrDefault(m => m.IngredientName == k.IngredientName).Id)
            })
            .ToList();
        }
    }
}
