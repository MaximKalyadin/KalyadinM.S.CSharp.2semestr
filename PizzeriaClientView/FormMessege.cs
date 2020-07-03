using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace PizzeriaClientView
{
    public partial class FormMessege : Form
    {
        private int page = 0;
        public FormMessege()
        {
            InitializeComponent();
            labelPage.Text = "Страница: 0";
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView.DataSource = APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getpageofmessages?clientid={Program.Client.Id}&pagenumber={page}");
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка получения спаска сообщений", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getpageofmessages?clientid={Program.Client.Id}&pagenumber={++page}");
            labelPage.Text = $"Страница: {page}";
        }

        private void buttonNazad_Click(object sender, EventArgs e)
        {
            if (page == 0) return;
            dataGridView.DataSource = APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getpageofmessages?clientid={Program.Client.Id}&pagenumber={--page}");
            labelPage.Text = $"Страница: {page}";
        }
    }
}
