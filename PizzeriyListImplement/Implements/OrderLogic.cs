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
                if (order.TimeCreate == model.TimeCreate && order.Count == model.Count && order.PizzaId == model.PizzaId
                    && order.Sum == model.Sum && order.Status == model.Status && order.Id != model.Id)
                {
                    throw new Exception("Уже есть такой заказ");
                }
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
                if (model != null)
                {
                    if (order.Id == model.Id)
                    {
                        result.Add(CreateViewModel(order));
                        break;
                    }
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
                Sum = order.Sum
            };
        }
    }
}
