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
        private List<MessageInfoViewModel> message;
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
                message = APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getmessages?clientid={Program.Client.Id}");
                dataGridView.DataSource = message.Take(5).ToList();
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
            dataGridView.DataSource = message.Skip(++page * 5).Take(5).ToList();
            labelPage.Text = $"Страница: {page}";
        }

        private void buttonNazad_Click(object sender, EventArgs e)
        {
            if (page == 0) return;
            dataGridView.DataSource = message.Skip(--page * 5).Take(5).ToList();
            labelPage.Text = $"Страница: {page}";
        }
    }
}
