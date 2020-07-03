﻿using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaFileImplement.Models;
using PizzeriaBusinessLogic.BindingModels;
using System.Linq;
using PizzeriaBusinessLogic.Enums;

namespace PizzeriaFileImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;
        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order order;
            if (model.Id.HasValue)
            {
                order = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (order == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec =>
               rec.Id) : 0;
                order = new Order { Id = maxId + 1 };
                source.Orders.Add(order);
            }

            order.PizzaId = model.PizzaId;
            order.Status = model.Status;
            order.PizzaId = model.PizzaId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.TimeCreate = model.TimeCreate;
            order.TimeImplement = model.TimeImplement;
            order.ClientId = model.ClientId;
            order.ImplementerId = model.ImplementerId;
        }
        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            return source.Orders
            .Where(rec => model == null
                          || (model.Id.HasValue && rec.Id == model.Id)
                          || (model.DateTo.HasValue && model.DateFrom.HasValue && rec.TimeCreate >= model.DateFrom && rec.TimeCreate <= model.DateTo)
                          || (model.ClientId.HasValue && rec.ClientId == model.ClientId)
                          || (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue)
                          || (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId.Value && rec.Status == OrderStatus.Выполняется))
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                PizzaId = rec.PizzaId,
                PizzaName = source.Pizzas.FirstOrDefault((r) => r.Id == rec.PizzaId).PizzaName,
                ClientFIO = source.Clients.FirstOrDefault(recC => recC.Id == rec.ClientId)?.ClientFIO,
                ClientId = rec.ClientId,
                ImplementerId = rec.ImplementerId,
                ImplementerFIO = source.Implementers.FirstOrDefault(i => i.Id == rec.ImplementerId)?.ImplementerFIO,
                Count = rec.Count,
                TimeCreate = rec.TimeCreate,
                TimeImplement = rec.TimeImplement,
                Status = rec.Status,
                Sum = rec.Sum
            }).ToList();
        }
    }
}
