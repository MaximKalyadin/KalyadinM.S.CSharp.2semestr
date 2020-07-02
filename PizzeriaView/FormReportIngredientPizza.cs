using System;
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
            if (dateTimePickerTo.Value.Date <= dateTimePickerFrom.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveProductComponentToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
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
        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerTo.Value.Date <= dateTimePickerFrom.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var dict = logic.GetOrders(new ReportBindingModel { DateFrom = dateTimePickerFrom.Value.Date, DateTo = dateTimePickerTo.Value.Date });

                if (dict != null)
                {
                    dataGridView.Rows.Clear();

                    foreach (var date in dict)
                    {
                        decimal dateSum = 0;

                        dataGridView.Rows.Add(new object[] { date.Key, "", "" });

                        foreach (var order in date)
                        {
                            dataGridView.Rows.Add(new object[] { "", order.PizzaName, order.Sum });
                            dateSum += order.Sum;
                        }

                        dataGridView.Rows.Add(new object[] { "Итого", "", dateSum });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
