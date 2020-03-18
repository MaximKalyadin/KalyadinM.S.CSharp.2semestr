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
                CreateModel(sklad, tempSklad);
            }
            else
            {
                source.Sklads.Add(CreateModel(sklad, tempSklad));
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
            foreach (var storage in source.Sklads)
            {
                if (model != null)
                {
                    if (storage.Id == model.Id)
                    {
                        result.Add(CreateViewModel(storage));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(storage));
            }
            return result;
        }
        private Sklad CreateModel(SkladBindingModel model, Sklad storage)
        {
            storage.SkladName = model.SkladName;
            int maxSMId = 0;
            for (int i = 0; i < source.SkladIngredients.Count; ++i)
            {
                if (source.SkladIngredients[i].Id > maxSMId)
                {
                    maxSMId = source.SkladIngredients[i].Id;
                }
                if (source.SkladIngredients[i].SkladId == storage.Id)
                {
                    if (model.SkladIngredients.ContainsKey(source.SkladIngredients[i].IngredientId))
                    {
                        source.SkladIngredients[i].Count = model.SkladIngredients[source.SkladIngredients[i].IngredientId].Item2;
                        model.SkladIngredients.Remove(source.SkladIngredients[i].IngredientId);
                    }
                    else
                    {
                        source.SkladIngredients.RemoveAt(i--);
                    }
                }
            }
            foreach (var sm in model.SkladIngredients)
            {
                source.SkladIngredients.Add(new SkladIngredients
                {
                    Id = ++maxSMId,
                    SkladId = storage.Id,
                    IngredientId = sm.Key,
                    Count = sm.Value.Item2
                });
            }
            return storage;
        }
        private SkladViewModel CreateViewModel(Sklad storage)
        {
            Dictionary<int, (string, int)> storageMaterials = new Dictionary<int, (string, int)>();
            foreach (var sm in source.SkladIngredients)
            {
                if (sm.SkladId == storage.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Ingredients)
                    {
                        if (sm.IngredientId == component.Id)
                        {
                            componentName = component.IngredientName;
                            break;
                        }
                    }
                    storageMaterials.Add(sm.IngredientId, (componentName, sm.Count));
                }
            }
            return new SkladViewModel
            {
                Id = storage.Id,
                SkladName = storage.SkladName,
                SkladIngredients = storageMaterials
            };
        }
    }
}
