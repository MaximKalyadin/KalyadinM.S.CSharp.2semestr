using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PizzeriaView
{
    public partial class FormReportSklad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportSklad(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            dataGridView.Columns.Add("Склад", "Склад");
            dataGridView.Columns.Add("Ингредиент", "Ингредиент");
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns.Add("Количество", "Количество");
        }

        private void FormReportSklad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = logic.GetSklads();
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var storage in dict)
                    {
                        dataGridView.Rows.Add(storage.SkladName, "", "");
                        int totalCount = 0;
                        foreach (var mat in storage.Ingredients)
                        {
                            dataGridView.Rows.Add("", mat.Value.Item1, mat.Value.Item2);
                            totalCount += mat.Value.Item2;
                        }
                        dataGridView.Rows.Add("Всего", "", totalCount);
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
                        logic.SaveSkladToExcelFile(new ReportBindingModel
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
