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
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.BindingModels;

namespace PizzeriaView
{
    public partial class FormPizza : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IPizzaLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> pizzaIngr;
        public FormPizza(IPizzaLogic service)
        {
            InitializeComponent();
            dataGridView.Columns.Add("Id", "Id");
            dataGridView.Columns.Add("IngredientName", "Ингредиент");
            dataGridView.Columns.Add("Count", "Количество");
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.logic = service;
        }
        private void FormPizza_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PizzaViewModel view = logic.Read(new PizzaBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.PizzaName;
                        textBoxPrice.Text = view.Price.ToString();
                        pizzaIngr = view.PizzaIngredients;
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
                pizzaIngr = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (pizzaIngr != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in pizzaIngr)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPizzaIngredients>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (pizzaIngr.ContainsKey(form.Id))
                {
                    pizzaIngr[form.Id] = (form.IngredientName, form.Count);
                }
                else
                {
                    pizzaIngr.Add(form.Id, (form.IngredientName, form.Count));
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormPizzaIngredients>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = pizzaIngr[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    pizzaIngr[form.Id] = (form.IngredientName, form.Count);
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        pizzaIngr.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pizzaIngr == null || pizzaIngr.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new PizzaBindingModel
                {
                    Id = id,
                    PizzaName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    PizzaIngredients = pizzaIngr
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
