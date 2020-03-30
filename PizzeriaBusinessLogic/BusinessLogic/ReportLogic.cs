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
        private readonly IIngredientLogic componentLogic;
        private readonly IPizzaLogic productLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IPizzaLogic productLogic, IIngredientLogic componentLogic, IOrderLogic orderLLogic)
        {
            this.productLogic = productLogic;
            this.componentLogic = componentLogic;
            this.orderLogic = orderLLogic;
        }

        public List<ReportPizzaIngredientViewModel> GetProductComponent()
        {
            var components = componentLogic.Read(null);
            var products = productLogic.Read(null);
            var list = new List<ReportPizzaIngredientViewModel>();

            foreach (var product in products)
            {
                foreach (var component in components)
                {
                    if (product.PizzaIngredients.ContainsKey(component.Id))
                    {
                        var record = new ReportPizzaIngredientViewModel
                        {
                            PizzaName = product.PizzaName,
                            IngredientName = component.IngredientName,
                            Count = product.PizzaIngredients[component.Id].Item2
                        };

                        list.Add(record);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        public void SaveProductsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Pizzas = productLogic.Read(null)
            });
        }

        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            var a = GetOrders(model);

            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }

        public void SaveProductComponentsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список издлий с компонентами",
                PizzaIngredients = GetProductComponent()
            });
        }
    }
}
