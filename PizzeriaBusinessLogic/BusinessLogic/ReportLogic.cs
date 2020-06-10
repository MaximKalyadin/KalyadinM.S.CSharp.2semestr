using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.HelperModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using System.Linq;

namespace PizzeriaBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IPizzaLogic pizzaLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IPizzaLogic pizzaLogic, IIngredientLogic ingredientLogic, IOrderLogic orderLLogic)
        {
            this.pizzaLogic = pizzaLogic;
            this.orderLogic = orderLLogic;
        }

        public List<ReportPizzaIngredientViewModel> GetPizzaIngredients()
        {
            List<ReportPizzaIngredientViewModel> reports = new List<ReportPizzaIngredientViewModel>();
            foreach (var pizza in pizzaLogic.Read(null))
            {
                foreach (var ingredient in pizza.PizzaIngredients)
                {
                    reports.Add(new ReportPizzaIngredientViewModel()
                    {
                        PizzaName = pizza.PizzaName,
                        IngredientName = ingredient.Value.Item1,
                        Count = ingredient.Value.Item2
                    });
                }
            }
            return reports;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.TimeCreate,
                PizzaName = x.PizzaName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        public void SavePizzaToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Пиццы",
                Pizzas = pizzaLogic.Read(null)
            });
        }

        public void SaveProductComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Заказы",
                DateTo = model.DateTo,
                DateFrom = model.DateFrom,
                Orders = GetOrders(model)
            });
        }

        public void SavePizzaIngredientsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Пицца",
                Pizzas = GetPizzaIngredients()
            });
        }
    }
}
