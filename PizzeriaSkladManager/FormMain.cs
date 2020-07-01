using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PizzeriaSkladManager
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            Load_Data();
        }

        private void Load_Data()
        {
            try
            {
                dataGridView.DataSource =
                    ApiClient.GetRequest<List<SkladViewModel>>($"api/sklad/getsklads");
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[2].Visible = false;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void ButtonAddSklad_Click(object sender, EventArgs args)
        {
            var form = new FormSklad();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Load_Data();
            }
        }

        private void ButtonEditSklad_Click(object sender, EventArgs args)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormSklad();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Load_Data();
                }
            }
        }

        private void ButtonDelSklad_Click(object sender, EventArgs args)
        {
            try
            {
                if (dataGridView.SelectedRows.Count == 1)
                {
                    ApiClient.PostRequest($"api/sklad/deletesklad", new SkladBindingModel()
                    {
                        Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value)
                    });
                    Load_Data();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void ButtonAddIngredient_Click(object sender, EventArgs args)
        {
            var form = new FormAddIngredient();
            form.Show();
        }

        private void ButtonShowIngredient_Click(object sender, EventArgs args)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormSkladIngredient();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Show();
            }
        }
    }
}
