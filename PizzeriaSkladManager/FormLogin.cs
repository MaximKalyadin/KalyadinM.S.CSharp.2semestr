using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PizzeriaSkladManager
{
    public partial class FormLogin : Form
    {
        public string Password { private set; get; }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void Button_AcceptEvent(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(textBoxPassword.Text))
            {
                Password = textBoxPassword.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Неправильный пароль!", "Ошибка", MessageBoxButtons.OK);
        }
    }
}
