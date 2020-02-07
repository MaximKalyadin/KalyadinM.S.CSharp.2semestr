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
        private List<PizzaIngredientViewModel> pizzaIngr;
        public FormPizza(IPizzaLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }
        private void FormPizza_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PizzaViewModel view = logic.GetElement(id.Value);
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
                pizzaIngr = new List<PizzaIngredientViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (pizzaIngr != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = pizzaIngr;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                if (form.ModelView != null)
                {
                    if (id.HasValue)
                    {
                        form.ModelView.PizzaId = id.Value;
                    }
                    pizzaIngr.Add(form.ModelView);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
        {
                var form = Container.Resolve<FormPizzaIngredients>();
                form.ModelView = pizzaIngr[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    pizzaIngr[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.ModelView;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        pizzaIngr.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                List<PizzaIngredientBindingModel> productComponentBM = new List<PizzaIngredientBindingModel>();
            for (int i = 0; i < pizzaIngr.Count; ++i)
                {
                    productComponentBM.Add(new PizzaIngredientBindingModel
                    {
                        Id = pizzaIngr[i].Id,
                        PizzaId = pizzaIngr[i].PizzaId,
                        IngredientId = pizzaIngr[i].IngredientId,
                        Count = pizzaIngr[i].Count
                    });
                }
                if (id.HasValue)
                {
                    logic.UpdElement(new PizzaBindingModel
                    {
                        Id = id.Value,
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        PizzaIngredients = productComponentBM
                    });
                }
                else
                {
                    logic.AddElement(new PizzaBindingModel
                    {
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        PizzaIngredients = productComponentBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
