using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PizzeriaBusinessLogic.BindingModels;

namespace PizzeriaClientView
{
    public partial class FormUpdateData : Form
    {
        public FormUpdateData()
        {
            InitializeComponent();
            textBoxFIO.Text = Program.Client.ClientFIO;
            textBoxLogin.Text = Program.Client.Login;
            textBoxPassword.Text = Program.Client.Password;
        }
        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxLogin.Text) && !string.IsNullOrEmpty(textBoxPassword.Text) && !string.IsNullOrEmpty(textBoxFIO.Text))
            {
                try
                {
                    APIClient.PostRequest($"api/client/updatedata", new ClientBindingModel()
                    {
                        Id = Program.Client.Id,
                        ClientFIO = textBoxFIO.Text,
                        Login = textBoxLogin.Text.ToString(),
                        Password = textBoxPassword.Text.ToString()
                    });
                    MessageBox.Show("Обновление прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.Client.ClientFIO = textBoxFIO.Text;
                    Program.Client.Email = textBoxLogin.Text;
                    Program.Client.Password = textBoxPassword.Text;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин, пароль и ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
