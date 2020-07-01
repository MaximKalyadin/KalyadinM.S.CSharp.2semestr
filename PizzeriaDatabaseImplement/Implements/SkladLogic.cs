using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaDatabaseImplement.Implements
{
    public class SkladLogic : ISkladLogic
    {
        public void CreateOrUpdate(SkladBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                Sklad element = context.Sklads.FirstOrDefault(rec => rec.SkladName == model.SkladName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Sklads.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Sklad();
                    context.Sklads.Add(element);
                }
                element.SkladName = model.SkladName;
                context.SaveChanges();
            }
        }

        public void Delete(SkladBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.SkladIngredients.RemoveRange(context.SkladIngredients.Where(rec => rec.SkladId == model.Id));
                        Sklad element = context.Sklads.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Sklads.Remove(element);
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

        public List<SkladViewModel> Read(SkladBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Sklads.Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new SkladViewModel
                {
                    Id = rec.Id,
                    SkladName = rec.SkladName,
                    SkladIngredients = context.SkladIngredients.Include(recSM => recSM.Ingredient)
                    .Where(recSM => recSM.SkladId == rec.Id)
                    .ToDictionary(recSM => recSM.Ingredient.IngredientName, recSM => recSM.Count)
                }).ToList();
            }
        }

        public bool RemoveIngredients(OrderViewModel order)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var pizzaIngredients = context.PizzaIngredients.Where(dm => dm.PizzaId == order.PizzaId).ToList();
                        var skladIngredientss = context.SkladIngredients.ToList();
                        foreach (var ingredient in pizzaIngredients)
                        {
                            var ingredientCount = ingredient.Count * order.Count;
                            foreach (var sm in skladIngredientss)
                            {
                                if (sm.IngredientId == ingredient.IngredientId && sm.Count >= ingredientCount)
                                {
                                    sm.Count -= ingredientCount;
                                    ingredientCount = 0;
                                    context.SaveChanges();
                                    break;
                                }
                                else if (sm.IngredientId == ingredient.IngredientId && sm.Count < ingredientCount)
                                {
                                    ingredientCount -= sm.Count;
                                    sm.Count = 0;
                                    context.SaveChanges();
                                }
                            }
                            if (ingredientCount > 0)
                                throw new Exception("Не хватает материалов на складах!");
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void AddIngredientToSklad(AddIngredientBindingModels model)
        {
            using (var context = new PizzeriaDatabase())
            {
                var skladIngr = context.SkladIngredients
                    .FirstOrDefault(sm => sm.IngredientId == model.IngredientId && sm.SkladId == model.SkladId);
                if (skladIngr != null)
                    skladIngr.Count += model.Count;
                else
                    context.SkladIngredients.Add(new SkladIngredient()
                    {
                        IngredientId = model.IngredientId,
                        SkladId = model.SkladId,
                        Count = model.Count
                    });
                context.SaveChanges();
            }
        }
    }
}
