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
        private readonly object locker = new object();
        private readonly ISkladLogic skladLogic;
        public MainLogic(IOrderLogic orderLogic, ISkladLogic skladLogic)
        {
            this.orderLogic = orderLogic;
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
                ClientId = model.ClientId,
                ClientFIO = model.ClientFIO,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.Требуются_материалы)
                {
                    throw new Exception("Заказ не в статусе \"Принят\" или \"Требуются материалы\"");
                }
                if (order.ImplementerId.HasValue && order.ImplementerId != model.ImplementerId)
                {
                    throw new Exception("У заказа уже есть исполнитель");
                }
                try
                {
                    skladLogic.RemoveIngredients(order);
                    orderLogic.CreateOrUpdate(new OrderBindingModel
                    {
                        Id = order.Id,
                        PizzaId = order.PizzaId,
                        Count = order.Count,
                        Sum = order.Sum,
                        ClientId = order.ClientId,
                        ClientFIO = order.ClientFIO,
                        ImplementerId = model.ImplementerId.Value,
                        TimeCreate = order.TimeCreate,
                        TimeImplement = DateTime.Now,
                        Status = OrderStatus.Выполняется
                    });
                } catch (Exception)
                {
                    orderLogic.CreateOrUpdate(new OrderBindingModel
                    {
                        Id = order.Id,
                        PizzaId = order.PizzaId,
                        Count = order.Count,
                        Sum = order.Sum,
                        ClientId = order.ClientId,
                        ClientFIO = order.ClientFIO,
                        ImplementerId = model.ImplementerId.Value,
                        TimeCreate = order.TimeCreate,
                        TimeImplement = DateTime.Now,
                        Status = OrderStatus.Требуются_материалы
                    });
                }
            }
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
                ClientId = order.ClientId,
                ClientFIO = order.ClientFIO,
                ImplementerId = order.ImplementerId.Value,
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
                ClientId = order.ClientId,
                ClientFIO = order.ClientFIO,
                ImplementerId = order.ImplementerId.Value,
                Status = OrderStatus.Оплачен
            });
        }
        public void AddIngredients(AddIngredientBindingModels models)
        {
            skladLogic.AddIngredientToSklad(models);
        }
    }
}
