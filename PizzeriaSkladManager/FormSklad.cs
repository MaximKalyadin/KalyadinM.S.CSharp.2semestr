using PizzeriaBusinessLogic.BindingModels;
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
    public partial class FormSklad : Form
    {
        public int? Id { set; get; }
        public FormSklad()
        {
            InitializeComponent();
        }

        private void FormSklad_Load(object sender, EventArgs args)
        {
            if (Id.HasValue)
            {
                var model = ApiClient.GetRequest<List<SkladViewModel>>($"api/sklad/getsklads")
                    .FirstOrDefault(s => s.Id == Id.Value);
                textBoxSkladName.Text = model.SkladName;
            }
        }

        private void ButtonAccept_Click(object sender, EventArgs args)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxSkladName.Text))
                {
                    ApiClient.PostRequest($"api/sklad/createorupdatesklad", new SkladBindingModel()
                    {
                        Id = Id ?? null,
                        SkladName = textBoxSkladName.Text
                    });
                    DialogResult = DialogResult.OK;
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
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
