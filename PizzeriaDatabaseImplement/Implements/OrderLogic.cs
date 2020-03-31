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
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order order;
                        if (model.Id.HasValue)
                        {
                            order = context.Orders.ToList().FirstOrDefault(rec => rec.Id == model.Id);
                            if (order == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            order.PizzaId = model.PizzaId;
                            order.Status = model.Status;
                            order.PizzaId = model.PizzaId;
                            order.Count = model.Count;
                            order.Sum = model.Sum;
                            order.TimeCreate = model.TimeCreate;
                            order.TimeImplement = model.TimeImplement;
                        }
                        else
                        {
                            order = new Order();
                            order.PizzaId = model.PizzaId;
                            order.Status = model.Status;
                            order.PizzaId = model.PizzaId;
                            order.Count = model.Count;
                            order.Sum = model.Sum;
                            order.TimeCreate = model.TimeCreate;
                            order.TimeImplement = model.TimeImplement;
                            context.Orders.Add(order);
                        }
                        context.SaveChanges();
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
        public void Delete(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Orders.Remove(element);
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Orders
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    Count = rec.Count,
                    TimeCreate = rec.TimeCreate,
                    TimeImplement = rec.TimeImplement,
                    PizzaName = context.Pizzas.FirstOrDefault((r) => r.Id == rec.PizzaId).PizzaName,
                    PizzaId = rec.PizzaId,
                    Status = rec.Status,
                    Sum = rec.Sum
                })
                .ToList();
            }
        }
    }
}
