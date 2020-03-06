using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PizzeriaDatabaseImplement.Models;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaDatabaseImplement.Implements
{
    public class PizzaLogic
    {
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Pizza element = context.Pizzas.FirstOrDefault(rec => rec.PizzaName == model.PizzaName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                    {
                            element = new Pizza();
                            context.Pizzas.Add(element);
                        }
                        element.PizzaName = model.PizzaName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var productComponents = context.PizzaIngredients.Where(rec => rec.PizzaId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.PizzaIngredients.RemoveRange(productComponents.Where(rec => !model.PizzaIngredients.ContainsKey(rec.IngredientId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateComponent in productComponents)
                            {
                                updateComponent.Count = model.PizzaIngredients[updateComponent.IngredientId].Item2;
                                model.PizzaIngredients.Remove(updateComponent.IngredientId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.PizzaIngredients)
                        {
                            context.PizzaIngredients.Add(new PizzaIngredient
                            {
                                PizzaId = element.Id,
                                IngredientId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(PizzaBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.PizzaIngredients.RemoveRange(context.PizzaIngredients.Where(rec => rec.PizzaId == model.Id));
                        Pizza element = context.Pizzas.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Pizzas.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Pizzas
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new PizzaViewModel
                {
                    Id = rec.Id,
                    PizzaName = rec.PizzaName,
                    Price = rec.Price,
                    PizzaIngredients = context.PizzaIngredients
                .Include(recPC => recPC.Ingredient)
                .Where(recPC => recPC.PizzaId == rec.Id)
                .ToDictionary(recPC => recPC.IngredientId, recPC =>
                (recPC.Ingredient?.IngredientName, recPC.Count))
                })
                .ToList();
            }
        }
    }
}
