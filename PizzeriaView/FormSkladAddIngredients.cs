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
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BusinessLogic;
using PizzeriaBusinessLogic.BindingModels;

namespace PizzeriaView
{
    public partial class FormSkladAddIngredients : Form
    {
        private readonly MainLogic mainLogic;
        private readonly ISkladLogic skladLogic;
        private readonly IIngredientLogic IngredientLogic;
        private List<SkladViewModel> skladViews;
        private List<IngredientViewModel> IngredientViews;
        public FormSkladAddIngredients(MainLogic mainLogic, ISkladLogic skladLogic, IIngredientLogic IngredientLogic)
        {
            InitializeComponent();
            this.mainLogic = mainLogic;
            this.skladLogic = skladLogic;
            this.IngredientLogic = IngredientLogic;
            LoadData();
        }
        private void LoadData()
        {
            skladViews = skladLogic.Read(null);
            if (skladViews != null)
            {
                comboBoxSklad.DataSource = skladViews;
                comboBoxSklad.DisplayMember = "SkladName";
            }
            IngredientViews = IngredientLogic.Read(null);
            if (IngredientViews != null)
            {
                comboBoxIngredient.DataSource = IngredientViews;
                comboBoxIngredient.DisplayMember = "IngredientName";
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (textBoxKol.Text == string.Empty)
            {
                throw new Exception("Введите количество ингредиентов");
            }
            mainLogic.AddIngredients(new AddIngredientInSkladBindingModel()
            {
                SkladId = (comboBoxSklad.SelectedItem as SkladViewModel).Id,
                IngredientId = (comboBoxIngredient.SelectedItem as IngredientViewModel).Id,
                Count = Convert.ToInt32(textBoxKol.Text)
            });
            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
