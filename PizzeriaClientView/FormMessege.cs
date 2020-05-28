using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PizzeriaClientView
{
    public partial class FormMessege : Form
    {
        public FormMessege()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                    dataGridView.DataSource =
                    APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getmessages?clientid={Program.Client.Id}");
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
