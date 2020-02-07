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
        public List<PizzaViewModel> GetList()
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            for (int i = 0; i < source.Pizza.Count; ++i)
            {
                List<PizzaIngredientViewModel> productComponents = new List<PizzaIngredientViewModel>();
                for (int j = 0; j < source.PizzaIngredients.Count; ++j)
                {
                    if (source.PizzaIngredients[j].PizzaId == source.Pizza[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.PizzaIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                componentName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        productComponents.Add(new PizzaIngredientViewModel
                        {
                            Id = source.PizzaIngredients[j].Id,
                            PizzaId = source.PizzaIngredients[j].PizzaId,
                            IngredientId = source.PizzaIngredients[j].IngredientId,
                            IngredientName = componentName,
                            Count = source.PizzaIngredients[j].Count
                        });
                    }
                }
                result.Add(new PizzaViewModel
                {
                    Id = source.Pizza[i].Id,
                    PizzaName = source.Pizza[i].PizzaName,
                    Price = source.Pizza[i].Price,
                    PizzaIngredients = productComponents
                });
            }
            return result;
        }
        public PizzaViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Pizza.Count; ++i)
            {
                List<PizzaIngredientViewModel> productComponents = new List<PizzaIngredientViewModel>();
                for (int j = 0; j < source.PizzaIngredients.Count; ++j)
                {
                    if (source.PizzaIngredients[j].PizzaId == source.Pizza[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.PizzaIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                componentName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        productComponents.Add(new PizzaIngredientViewModel
                        {
                            Id = source.PizzaIngredients[j].Id,
                            PizzaId = source.PizzaIngredients[j].PizzaId,
                            IngredientId = source.PizzaIngredients[j].IngredientId,
                            IngredientName = componentName,
                            Count = source.PizzaIngredients[j].Count
                        });
                    }
                }
                if (source.Pizza[i].Id == id)
                {
                    return new PizzaViewModel
                    {
                        Id = source.Pizza[i].Id,
                        PizzaName = source.Pizza[i].PizzaName,
                        Price = source.Pizza[i].Price,
                        PizzaIngredients = productComponents
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(PizzaBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Pizza.Count; ++i)
            {
                if (source.Pizza[i].Id > maxId)
                {
                    maxId = source.Pizza[i].Id;
                }
                if (source.Pizza[i].PizzaName == model.PizzaName)
                {
                    throw new Exception("Уже есть пицца с таким названием");
                }
            }
            source.Pizza.Add(new Pizza
            {
                Id = maxId + 1,
                PizzaName = model.PizzaName,
                Price = model.Price
            });
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredients[i].Id;
                }
            }
            for (int i = 0; i < model.PizzaIngredients.Count; ++i)
            {
                for (int j = 1; j < model.PizzaIngredients.Count; ++j)
                {
                    if (model.PizzaIngredients[i].IngredientId ==
                    model.PizzaIngredients[j].IngredientId)
                    {
                        model.PizzaIngredients[i].Count +=
                        model.PizzaIngredients[j].Count;
                        model.PizzaIngredients.RemoveAt(j--);
                    }
                }
            }
            for (int i = 0; i < model.PizzaIngredients.Count; ++i)
            {
                source.PizzaIngredients.Add(new PizzaIngredient
                {
                    Id = ++maxPCId,
                    PizzaId = maxId + 1,
                    IngredientId = model.PizzaIngredients[i].IngredientId,
                    Count = model.PizzaIngredients[i].Count
                });
            }
        }
        public void UpdElement(PizzaBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Pizza.Count; ++i)
            {
                if (source.Pizza[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Pizza[i].PizzaName == model.PizzaName &&
                source.Pizza[i].Id != model.Id)
                {
                    throw new Exception("Уже есть пицца с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Pizza[index].PizzaName = model.PizzaName;
            source.Pizza[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIngredients[i].Id;
                }
            }
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.PizzaIngredients.Count; ++j)
                    {
                        if (source.PizzaIngredients[i].Id == model.PizzaIngredients[j].Id)
                        {
                            source.PizzaIngredients[i].Count = model.PizzaIngredients[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        source.PizzaIngredients.RemoveAt(i--);
                    }
                }
            }
            for (int i = 0; i < model.PizzaIngredients.Count; ++i)
            {
                if (model.PizzaIngredients[i].Id == 0)
                {
                    for (int j = 0; j < source.PizzaIngredients.Count; ++j)
                    {
                        if (source.PizzaIngredients[j].PizzaId == model.Id &&
                        source.PizzaIngredients[j].IngredientId == model.PizzaIngredients[i].IngredientId)
                        {
                            source.PizzaIngredients[j].Count += model.PizzaIngredients[i].Count;
                            model.PizzaIngredients[i].Id = source.PizzaIngredients[j].Id;
                            break;
                        }
                    }
                    if (model.PizzaIngredients[i].Id == 0)
                    {
                        source.PizzaIngredients.Add(new PizzaIngredient
                        {
                            Id = ++maxPCId,
                            PizzaId = model.Id,
                            IngredientId = model.PizzaIngredients[i].IngredientId,
                            Count = model.PizzaIngredients[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.PizzaIngredients.Count; ++i)
            {
                if (source.PizzaIngredients[i].PizzaId == id)
                {
                    source.PizzaIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Pizza.Count; ++i)
            {
                if (source.Pizza[i].Id == id)
                {
                    source.Pizza.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
