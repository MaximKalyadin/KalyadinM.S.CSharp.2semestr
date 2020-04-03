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
        private void FormReportMaterialsToDresses_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetOrders();
                if (dict != null)
                {
                    Dictionary<string, List<ReportOrdersViewModel>> dictOrders = new Dictionary<string, List<ReportOrdersViewModel>>();
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                            if (!dictOrders.ContainsKey(elem.DateCreate.ToShortDateString()))
                            {
                                dictOrders.Add(elem.DateCreate.ToShortDateString(), new List<ReportOrdersViewModel>() { elem });
                            }
                            else
                            {
                                dictOrders[elem.DateCreate.ToShortDateString()].Add(elem);
                            }
                    }
                    foreach (var order in dictOrders)
                    {
                        dataGridView.Rows.Add(order.Key, "", "");
                        decimal totalPrice = 0;
                        foreach (var pizza in order.Value)
                        {
                            dataGridView.Rows.Add("", pizza.PizzaName, pizza.Sum);
                            totalPrice += pizza.Sum;
                        }
                        dataGridView.Rows.Add("Всего", "", totalPrice);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
