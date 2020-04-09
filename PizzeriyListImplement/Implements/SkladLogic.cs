using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.Enums;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriyListImplement.Models;
using PizzeriaBusinessLogic.ViewModels;
using System.Linq;

namespace PizzeriyListImplement.Implements
{
    public class SkladLogic : ISkladLogic
    {
        private readonly DataListSingleton source;
        public SkladLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(SkladBindingModel sklad)
        {
            Sklad tempSklad = sklad.Id.HasValue ? null : new Sklad
            {
                Id = 1
            };
            foreach (var s in source.Sklads)
            {
                if (s.SkladName == sklad.SkladName && s.Id != sklad.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                if (!sklad.Id.HasValue && s.Id >= tempSklad.Id)
                {
                    tempSklad.Id = s.Id + 1;
                }
                else if (sklad.Id.HasValue && s.Id == sklad.Id)
                {
                    tempSklad = s;
                }
            }
            if (sklad.Id.HasValue)
            {
                if (tempSklad == null)
                {
                    throw new Exception("Элемент не найден");
                }
                tempSklad.SkladName = sklad.SkladName;
            }
            else
            {
                source.Sklads.Add(new Sklad()
                {
                    Id = tempSklad.Id,
                    SkladName = sklad.SkladName
                });
            }
        }
        public void Delete(SkladBindingModel model)
        {
            for (int i = 0; i < source.SkladIngredients.Count; ++i)
            {
                if (source.SkladIngredients[i].SkladId == model.Id)
                {
                    source.SkladIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Sklads.Count; ++i)
            {
                if (source.Sklads[i].Id == model.Id)
                {
                    source.Sklads.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<SkladViewModel> Read(SkladBindingModel model)
        {
            List<SkladViewModel> result = new List<SkladViewModel>();
            foreach (var sklad in source.Sklads)
            {
                if (model != null)
                {
                    if (sklad.Id == model.Id)
                    {
                        result.Add(new SkladViewModel()
                        {
                            Id = sklad.Id,
                            SkladName = sklad.SkladName,
                            SkladIngredients = source.SkladIngredients.Where(sm => sm.SkladId == sklad.Id)
                            .ToDictionary(sm => source.Ingredients.FirstOrDefault(c => c.Id == sm.IngredientId).IngredientName, sm => sm.Count)
                        });
                        break;
                    }
                    continue;
                }
                result.Add(new SkladViewModel()
                {
                    Id = sklad.Id,
                    SkladName = sklad.SkladName,
                    SkladIngredients = source.SkladIngredients.Where(sm => sm.SkladId == sklad.Id)
                    .ToDictionary(sm => source.Ingredients.FirstOrDefault(c => c.Id == sm.IngredientId).IngredientName, sm => sm.Count)
                });
            }
            return result;
        }
        
        public void AddIngredientToSklad(AddIngredientInSkladBindingModel model)
        {
            if (source.SkladIngredients.Count == 0)
            {
                source.SkladIngredients.Add(new SkladIngredients()
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
                    source.SkladIngredients.Add(new SkladIngredients()
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
    }
}
