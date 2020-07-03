using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PizzeriaDatabaseImplement.Models;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using PizzeriaBusinessLogic.Enums;

namespace PizzeriaDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                Order order;
                if (model.Id.HasValue)
                {
                    order = context.Orders.ToList().FirstOrDefault(rec => rec.Id == model.Id);
                    if (order == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    order = new Order();
                    context.Orders.Add(order);
                }
                order.ClientId = model.ClientId;
                order.PizzaId = model.PizzaId;
                order.Status = model.Status;
                order.Count = model.Count;
                order.Sum = model.Sum;
                order.ImplementerId = model.ImplementerId;
                order.TimeCreate = model.TimeCreate;
                order.TimeImplement = model.TimeImplement;
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
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
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Orders.Where(rec => model == null
                                                    || (model.Id.HasValue && rec.Id == model.Id)
                                                    || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.TimeCreate >= model.DateFrom && rec.TimeCreate <= model.DateTo)
                                                    || (model.ClientId.HasValue && model.ClientId == rec.ClientId)
                                                    || (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue)
                                                    || (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId.Value && rec.Status == OrderStatus.Выполняется))
                .Include(ord => ord.Pizza)
                .Select(rec => new OrderViewModel()
                {
                    Id = rec.Id,
                    PizzaId = rec.PizzaId,
                    ClientFIO = rec.Client.ClientFIO,
                    ClientId = rec.ClientId,
                    PizzaName = rec.Pizza.PizzaName,
                    ImplementerFIO = rec.Implementer.ImplementerFIO,
                    Count = rec.Count,
                    TimeCreate = rec.TimeCreate,
                    TimeImplement = rec.TimeImplement,
                    Status = rec.Status,
                    Sum = rec.Sum
                }).ToList();
            }
        }
    }
}
