﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Unity;
using PizzeriaBusinessLogic.BusinessLogic;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BindingModels;

namespace PizzeriaView
{
    public partial class FormReportIngredientPizza : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportIngredientPizza(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            dataGridView.Columns.Add("Дата", "Дата");
            dataGridView.Columns.Add("Заказ", "Заказ");
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns.Add("Сумма заказа", "Сумма заказа");
        }


        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveProductComponentToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonToMake_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePickerFrom.Value <= dateTimePickerTo.Value)
                {
                    var orders = logic.GetOrders(new ReportBindingModel()
                    {
                        DateFrom = dateTimePickerFrom.Value,
                        DateTo = dateTimePickerTo.Value
                    });

                    foreach (var orderGroup in orders)
                    {
                        dataGridView.Rows.Add(orderGroup.Key, "", "");
                        decimal sum = 0;
                        foreach (var order in orderGroup)
                        {
                            dataGridView.Rows.Add("", order.PizzaName, order.Sum);
                            sum += order.Sum;
                        }
                        dataGridView.Rows.Add("Всего:", "", sum);
                    }
                }
                else
                    MessageBox.Show("Начало периода не должно превышать его конец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
