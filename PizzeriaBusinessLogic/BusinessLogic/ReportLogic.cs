﻿using System;
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
        private readonly ISkladLogic skladLogic;
        public ReportLogic(IPizzaLogic pizzaLogic, IIngredientLogic ingredientLogic, IOrderLogic orderLLogic, ISkladLogic skladLogic)
        {
            this.pizzaLogic = pizzaLogic;
            this.orderLogic = orderLLogic;
            this.skladLogic = skladLogic;
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

        public List<IGrouping<string, ReportOrdersViewModel>> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .ToList()
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.TimeCreate,
                PizzaName = x.PizzaName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .GroupBy(x => x.DateCreate.ToShortDateString())
           .ToList();
        }

        public List<ReportSkladViewModel> GetSklads()
        {
            return skladLogic.Read(null).Select(s => new ReportSkladViewModel()
            {
                SkladName = s.SkladName,
                Ingredients = s.SkladIngredients
            }).ToList();
        }

        public List<ReportIngredientSkladViewModel> GetIngredientSklads()
        {
            var storages = skladLogic.Read(null);
            List<ReportIngredientSkladViewModel> reportMaterialStorages = new List<ReportIngredientSkladViewModel>();
            foreach (var storage in storages)
            {
                foreach (var material in storage.SkladIngredients)
                {
                    reportMaterialStorages.Add(new ReportIngredientSkladViewModel()
                    {
                        SkladName = storage.SkladName,
                        IngredientName = material.Key,
                        Count = material.Value
                    });
                }
            }
            return reportMaterialStorages;
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

        public void SaveSkladToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfoSklad
            {
                FileName = model.FileName,
                Title = "Хранилища",
                Sklads = skladLogic.Read(null)
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

        public void SaveSkladToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfoSklad
            {
                FileName = model.FileName,
                Title = "Cклад",
                Sklads = GetSklads()
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

        public void SaveIngredientSkladsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfoIngredientSklad
            {
                FileName = model.FileName,
                Title = "Ингредиент на складах",
                IngredientSklads = GetIngredientSklads()
            });
        }
    }
}
