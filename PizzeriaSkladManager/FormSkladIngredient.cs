using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PizzeriaSkladManager
{
    public partial class FormSkladIngredient : Form
    {
        public int? Id { set; get; }
        public FormSkladIngredient()
        {
            InitializeComponent();
            dataGridView.Columns.Add("IngredientName", "Материал");
            dataGridView.Columns.Add("Count", "Количество");
        }

        private void ButtonCancel_Click(object sender, EventArgs args)
        {
            Close();
        }

        private void FormSkladIngredient_Load(object sender, EventArgs args)
        {
            if (Id.HasValue)
            {
                var model = ApiClient.GetRequest<List<SkladViewModel>>($"api/sklad/getsklads")
                    .FirstOrDefault(s => s.Id == Id.Value);
                textBoxNameSklad.Text = model.SkladName;
                foreach (var mat in model.SkladIngredients)
                {
                    dataGridView.Rows.Add(mat.Key, mat.Value);
                }
            }
        }
    }
}
