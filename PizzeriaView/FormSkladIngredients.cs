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
using PizzeriaBusinessLogic.Interfaces;

namespace PizzeriaView
{
    public partial class FormSkladIngredients : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ISkladLogic logic;
        private int? id;
        private Dictionary<string, int> skladIngredients;
        public FormSkladIngredients(ISkladLogic service)
        {
            InitializeComponent();
            dataGridView.Columns.Add("Id", "Id");
            dataGridView.Columns.Add("IngredientName", "Ингредиент");
            dataGridView.Columns.Add("Count", "Количество");
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.logic = service;
        }
        private void FormSkladIngredients_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SkladViewModel view = logic.Read(new SkladBindingModel { Id = id.Value })[0];
                    if (view != null)
                    {
                        textBoxSklad.Text = view.SkladName;
                        skladIngredients = view.SkladIngredients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                skladIngredients = new Dictionary<string, int>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (skladIngredients != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in skladIngredients)
                    {
                        dataGridView.Rows.Add(new object[] { "", pc.Key, pc.Value });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка tyta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSklad.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new SkladBindingModel
                {
                    Id = id ?? null,
                    SkladName = textBoxSklad.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
