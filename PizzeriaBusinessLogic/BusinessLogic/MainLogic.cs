using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.Enums;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;

namespace PizzeriaBusinessLogic.BusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly ISkladLogic skladLogic;
        public MainLogic(IOrderLogic orderLogic, ISkladLogic skladLogic)
        private readonly IIngredientLogic ingredientLogic;
        public MainLogic(IOrderLogic orderLogic, IIngredientLogic ingredientLogic)
        {
            this.orderLogic = orderLogic;
            this.ingredientLogic = ingredientLogic;
            this.skladLogic = skladLogic;
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                PizzaId = model.PizzaId,
                Count = model.Count,
                Sum = model.Sum,
                TimeCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel
            {
                Id = model.OrderId
            })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            (skladLogic as ISkladLigicRemove).RemoveIngredients(order.PizzaId, order.Count);
            if (order.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                TimeCreate = order.TimeCreate,
                TimeImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                TimeCreate = order.TimeCreate,
                TimeImplement = order.TimeImplement,
                Status = OrderStatus.Готов
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                TimeCreate = order.TimeCreate,
                TimeImplement = order.TimeImplement,
                Status = OrderStatus.Оплачен
            });
        }
        public void AddIngredients(SkladViewModel sklad, int count, IngredientViewModel material)
        {
            if (sklad.SkladIngredients.ContainsKey(material.Id))
            {
                sklad.SkladIngredients[material.Id] = (sklad.SkladIngredients[material.Id].Item1, sklad.SkladIngredients[material.Id].Item2 + count);
            }
            else
            {
                sklad.SkladIngredients.Add(material.Id, (material.IngredientName, count));
            }
            skladLogic.CreateOrUpdate(new SkladBindingModel()
            {
                Id = sklad.Id,
                SkladName = sklad.SkladName,
                SkladIngredients = sklad.SkladIngredients
            });
        }
    }
}
