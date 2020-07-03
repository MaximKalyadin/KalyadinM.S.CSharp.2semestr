namespace PizzeriaSkladManager
{
    partial class FormMain
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonAddSklad = new System.Windows.Forms.Button();
            this.buttonEditSklad = new System.Windows.Forms.Button();
            this.buttonDelSklad = new System.Windows.Forms.Button();
            this.buttonAddIngredient = new System.Windows.Forms.Button();
            this.buttonShowIngredient = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(1, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(369, 288);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonAddSklad
            // 
            this.buttonAddSklad.Location = new System.Drawing.Point(376, 12);
            this.buttonAddSklad.Name = "buttonAddSklad";
            this.buttonAddSklad.Size = new System.Drawing.Size(100, 26);
            this.buttonAddSklad.TabIndex = 1;
            this.buttonAddSklad.Text = "Создать";
            this.buttonAddSklad.UseVisualStyleBackColor = true;
            this.buttonAddSklad.Click += new System.EventHandler(this.ButtonAddSklad_Click);
            // 
            // buttonEditSklad
            // 
            this.buttonEditSklad.Location = new System.Drawing.Point(376, 44);
            this.buttonEditSklad.Name = "buttonEditSklad";
            this.buttonEditSklad.Size = new System.Drawing.Size(100, 26);
            this.buttonEditSklad.TabIndex = 2;
            this.buttonEditSklad.Text = "Изменить";
            this.buttonEditSklad.UseVisualStyleBackColor = true;
            this.buttonEditSklad.Click += new System.EventHandler(this.ButtonEditSklad_Click);
            // 
            // buttonDelSklad
            // 
            this.buttonDelSklad.Location = new System.Drawing.Point(376, 76);
            this.buttonDelSklad.Name = "buttonDelSklad";
            this.buttonDelSklad.Size = new System.Drawing.Size(100, 26);
            this.buttonDelSklad.TabIndex = 3;
            this.buttonDelSklad.Text = "Удалить";
            this.buttonDelSklad.UseVisualStyleBackColor = true;
            this.buttonDelSklad.Click += new System.EventHandler(this.ButtonDelSklad_Click);
            // 
            // buttonAddIngredient
            // 
            this.buttonAddIngredient.Location = new System.Drawing.Point(376, 175);
            this.buttonAddIngredient.Name = "buttonAddIngredient";
            this.buttonAddIngredient.Size = new System.Drawing.Size(100, 26);
            this.buttonAddIngredient.TabIndex = 4;
            this.buttonAddIngredient.Text = "Пополнить";
            this.buttonAddIngredient.UseVisualStyleBackColor = true;
            this.buttonAddIngredient.Click += new System.EventHandler(this.ButtonAddIngredient_Click);
            // 
            // buttonShowIngredient
            // 
            this.buttonShowIngredient.Location = new System.Drawing.Point(376, 220);
            this.buttonShowIngredient.Name = "buttonShowIngredient";
            this.buttonShowIngredient.Size = new System.Drawing.Size(100, 26);
            this.buttonShowIngredient.TabIndex = 5;
            this.buttonShowIngredient.Text = "Материалы";
            this.buttonShowIngredient.UseVisualStyleBackColor = true;
            this.buttonShowIngredient.Click += new System.EventHandler(this.ButtonShowIngredient_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 293);
            this.Controls.Add(this.buttonAddIngredient);
            this.Controls.Add(this.buttonDelSklad);
            this.Controls.Add(this.buttonEditSklad);
            this.Controls.Add(this.buttonAddSklad);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonShowIngredient);
            this.Name = "FormMain";
            this.Text = "Менеджер складов";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonAddSklad;
        private System.Windows.Forms.Button buttonEditSklad;
        private System.Windows.Forms.Button buttonDelSklad;
        private System.Windows.Forms.Button buttonAddIngredient;
        private System.Windows.Forms.Button buttonShowIngredient;
    }
}