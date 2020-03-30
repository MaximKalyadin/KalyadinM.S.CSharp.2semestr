namespace PizzeriaView
{
    partial class FormReportPzzaIngredients
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveToExcel = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Ingredient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pizza = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveToExcel
            // 
            this.SaveToExcel.Location = new System.Drawing.Point(12, 12);
            this.SaveToExcel.Name = "SaveToExcel";
            this.SaveToExcel.Size = new System.Drawing.Size(173, 36);
            this.SaveToExcel.TabIndex = 0;
            this.SaveToExcel.Text = "Созранить в Excel";
            this.SaveToExcel.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ingredient,
            this.Pizza,
            this.Count});
            this.dataGridView.Location = new System.Drawing.Point(2, 48);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(515, 400);
            this.dataGridView.TabIndex = 1;
            // 
            // Ingredient
            // 
            this.Ingredient.HeaderText = "Ингредиент";
            this.Ingredient.MinimumWidth = 6;
            this.Ingredient.Name = "Ingredient";
            this.Ingredient.Width = 125;
            // 
            // Pizza
            // 
            this.Pizza.HeaderText = "Пицца";
            this.Pizza.MinimumWidth = 6;
            this.Pizza.Name = "Pizza";
            this.Pizza.Width = 125;
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.MinimumWidth = 6;
            this.Count.Name = "Count";
            this.Count.Width = 125;
            // 
            // FormReportPzzaIngredients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.SaveToExcel);
            this.Name = "FormReportPzzaIngredients";
            this.Text = "Ингредиенты по пиццам";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveToExcel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ingredient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pizza;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
    }
}