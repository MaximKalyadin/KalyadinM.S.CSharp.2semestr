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
    public partial class FormAddIngredient : Form
    {
        public FormAddIngredient()
        {
            InitializeComponent();
        }

        private void Load_Data(object sender, EventArgs args)
        {
            try
            {
                comboBoxSklad.DataSource = ApiClient.GetRequest<List<SkladViewModel>>($"api/sklad/getsklads");
                comboBoxSklad.DisplayMember = "SkladName";
                comboBoxComponent.DataSource = ApiClient.GetRequest<List<IngredientViewModel>>($"api/main/getingredients");
                comboBoxComponent.DisplayMember = "IngredientName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void ButtonSave_Click(object sender, EventArgs args)
        {
            try
            {
                if (comboBoxSklad.SelectedItem != null && comboBoxComponent.SelectedItem != null &&
                    !string.IsNullOrEmpty(textBoxCountComponent.Text))
                {
                    ApiClient.PostRequest($"api/sklad/addingredienttosklad",
                        new AddIngredientBindingModels()
                        {
                            SkladId = (comboBoxSklad.SelectedItem as SkladViewModel).Id,
                            IngredientId = (comboBoxComponent.SelectedItem as IngredientViewModel).Id,
                            Count = Convert.ToInt32(textBoxCountComponent.Text)
                        });
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs args)
        {
            Close();
        }
    }
}
