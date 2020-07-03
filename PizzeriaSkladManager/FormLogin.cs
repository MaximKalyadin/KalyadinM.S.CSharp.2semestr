using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PizzeriaSkladManager
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Button_AcceptEvent(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(textBoxPassword.Text))
            {
                if (ConfigurationManager.AppSettings["Password"].Equals(textBoxPassword.Text))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                    MessageBox.Show("Неверный пароль!!!", "Ошибка", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Пароль не может быть пустым!", "Ошибка", MessageBoxButtons.OK);
        }
    }
}
