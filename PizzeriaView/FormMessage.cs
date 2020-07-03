﻿using PizzeriaBusinessLogic.Interfaces;
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
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BindingModels;

namespace PizzeriaView
{
    public partial class FormMessage : Form
    {
        private readonly IMessageInfoLogic logic;
        private int page = 0;
        public FormMessage(IMessageInfoLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            labelPage.Text = "Страница: 0";
            LoadData();

        }

        private void LoadData()
        {
            try
            {
                dataGridView.DataSource = logic.ReadPage(new MessagePageBindingModel() { pageNumber = page });
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка получения списка сообщений", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = logic.ReadPage(new MessagePageBindingModel() { pageNumber = ++page });
            labelPage.Text = $"Страница: {page}";
        }

        private void buttonNazad_Click(object sender, EventArgs e)
        {
            if (page == 0) return;
            dataGridView.DataSource = logic.ReadPage(new MessagePageBindingModel() { pageNumber = --page });
            labelPage.Text = $"Страница: {page}";
        }
    }
}
