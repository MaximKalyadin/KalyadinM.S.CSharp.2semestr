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
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;
        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order
            {
                Id = 1
            };
            foreach (var order in source.Orders)
            {
                if (!model.Id.HasValue && order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
                else if (model.Id.HasValue && order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            foreach (var order in source.Orders)
            {
                if (
                    model != null && order.Id == model.Id
                    || model.DateFrom.HasValue && model.DateTo.HasValue && order.TimeCreate >= model.DateFrom && order.TimeCreate <= model.DateTo
                    || model.ClientId.HasValue && order.ClientId == model.ClientId
                )
                {
                    if (order.Id == model.Id && model.Id.HasValue)
                    {
                        result.Add(CreateViewModel(order));
                        break;
                    }
                    else if (model.DateFrom.HasValue && model.DateTo.HasValue && order.TimeCreate >= model.DateFrom &&
                      order.TimeCreate <= model.DateTo)
                        result.Add(CreateViewModel(order));
                    else if (model.ClientId.HasValue && order.ClientId == model.ClientId)
                        result.Add(CreateViewModel(order));
                    else if (model.FreeOrder.HasValue && model.FreeOrder.Value && !order.ImplementerId.HasValue)
                        result.Add(CreateViewModel(order));
                    else if (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId.Value && order.Status == OrderStatus.Выполняется)
                        result.Add(CreateViewModel(order));
                    continue;

                }

                result.Add(CreateViewModel(order));
            }

            return result;
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.Count = model.Count;
            order.TimeCreate = model.TimeCreate;
            order.TimeImplement = model.TimeImplement;
            order.PizzaId = model.PizzaId;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.ImplementerId = model.ImplementerId;
            order.ClientId = model.ClientId;
            order.ClientFIO = model.ClientFIO;
            return order;
        }
        private OrderViewModel CreateViewModel(Order order)
        {
            var pizzaName = source.Pizza.FirstOrDefault((n) => n.Id == order.PizzaId).PizzaName;
            return new OrderViewModel
            {
                Id = order.Id,
                Count = order.Count,
                TimeCreate = order.TimeCreate,
                TimeImplement = order.TimeImplement,
                PizzaName = pizzaName,
                PizzaId = order.PizzaId,
                Status = order.Status,
                Sum = order.Sum,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = source.Implementers.FirstOrDefault(i => i.Id == order.ImplementerId)?.ImplementerFIO,
                ClientId = order.ClientId,
                ClientFIO = order.ClientFIO
            };
        }
    }
}
