using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaFileImplement.Models;
using PizzeriaBusinessLogic.BindingModels;
using System.Linq;
using System.Text;

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
            Order element = source.Orders.FirstOrDefault(rec =>  rec.Id != model.Id && rec.PizzaId == model.PizzaId && rec.Status == model.Status);
            if (model.Id.HasValue)
            {
                element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec =>
               rec.Id) : 0;
                element = new Order { Id = maxId + 1 };
                source.Orders.Add(element);
            }
            element.Status = model.Status;
            element.PizzaId = model.PizzaId;
            element.Count = model.Count;
            element.Sum = model.Sum;
            element.TimeCreate = model.TimeCreate;
            element.TimeImplement = model.TimeImplement;
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
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                Count = rec.Count,
                TimeCreate = rec.TimeCreate,
                TimeImplement = rec.TimeImplement,
                PizzaId = rec.PizzaId,
                Status = rec.Status,
                Sum = rec.Sum
            })
            .ToList();
        }
    }
}
