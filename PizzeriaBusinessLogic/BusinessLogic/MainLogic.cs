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
        private readonly IPizzaLogic pizzaLogic;
        public MainLogic(IOrderLogic orderLogic, IPizzaLogic pizzaLogic, ISkladLogic skladLogic)
        {
            this.orderLogic = orderLogic;
            this.skladLogic = skladLogic;
            this.pizzaLogic = pizzaLogic;
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
            if (checkSklad(order.PizzaId, order.Count))
            {
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
                Remove(order.PizzaId, order.Count);
            }
            else
            {
                throw new Exception("Не хватает ингредиентов на складах!");
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
        private bool checkSklad(int PizzaId, int Count)
        {
            var sklads = skladLogic.Read(null);
            var pizzaIngredients = pizzaLogic.Read(new PizzaBindingModel() { Id = PizzaId })[0].PizzaIngredients;
            var ingredientsklad = new Dictionary<int, int>();
            foreach (var sklad in sklads)
            {
                foreach (var sm in sklad.SkladIngredients)
                {
                    if (ingredientsklad.ContainsKey(sm.Key))
                    {
                        ingredientsklad[sm.Key] += sm.Value.Item2;
                    }
                    else
                    {
                        ingredientsklad.Add(sm.Key, sm.Value.Item2);
                    }
                }
            }

            foreach (var dm in pizzaIngredients)
            {
                if (!ingredientsklad.ContainsKey(dm.Key) || ingredientsklad[dm.Key] < dm.Value.Item2 * Count)
                {
                    return false;
                }
            }
            return true;
        }
        private void Remove(int PizzaId, int Count)
        {
            var sklads = skladLogic.Read(null);
            var pizzaIngredients = pizzaLogic.Read(new PizzaBindingModel() { Id = PizzaId })[0].PizzaIngredients;
            foreach (var dm in pizzaIngredients)
            {
                int ingredientCount = dm.Value.Item2 * Count;
                foreach (var sklad in sklads)
                {
                    if (sklad.SkladIngredients[dm.Key].Item2 >= ingredientCount)
                    {
                        sklad.SkladIngredients[dm.Key] = (sklad.SkladIngredients[dm.Key].Item1, sklad.SkladIngredients[dm.Key].Item2 - ingredientCount);
                        break;
                    }
                    else if (sklad.SkladIngredients[dm.Key].Item2 < ingredientCount)
                    {
                        ingredientCount -= sklad.SkladIngredients[dm.Key].Item2;
                        sklad.SkladIngredients[dm.Key] = (sklad.SkladIngredients[dm.Key].Item1, 0);
                    }
                }
                foreach (var storage in sklads)
                {
                    skladLogic.CreateOrUpdate(new SkladBindingModel()
                    {
                        Id = storage.Id,
                        SkladName = storage.SkladName,
                        SkladIngredients = storage.SkladIngredients
                    });
                }
            }
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
