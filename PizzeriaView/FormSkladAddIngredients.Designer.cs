namespace PizzeriaView
{
    partial class FormSkladAddIngredients
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
            this.labelSklad = new System.Windows.Forms.Label();
            this.labelIngredient = new System.Windows.Forms.Label();
            this.labelKol = new System.Windows.Forms.Label();
            this.comboBoxSklad = new System.Windows.Forms.ComboBox();
            this.comboBoxIngredient = new System.Windows.Forms.ComboBox();
            this.textBoxKol = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSklad
            // 
            this.labelSklad.AutoSize = true;
            this.labelSklad.Location = new System.Drawing.Point(12, 9);
            this.labelSklad.Name = "labelSklad";
            this.labelSklad.Size = new System.Drawing.Size(48, 17);
            this.labelSklad.TabIndex = 0;
            this.labelSklad.Text = "Склад";
            // 
            // labelIngredient
            // 
            this.labelIngredient.AutoSize = true;
            this.labelIngredient.Location = new System.Drawing.Point(12, 45);
            this.labelIngredient.Name = "labelIngredient";
            this.labelIngredient.Size = new System.Drawing.Size(86, 17);
            this.labelIngredient.TabIndex = 1;
            this.labelIngredient.Text = "Ингредиент";
            // 
            // labelKol
            // 
            this.labelKol.AutoSize = true;
            this.labelKol.Location = new System.Drawing.Point(12, 85);
            this.labelKol.Name = "labelKol";
            this.labelKol.Size = new System.Drawing.Size(86, 17);
            this.labelKol.TabIndex = 2;
            this.labelKol.Text = "Количество";
            // 
            // comboBoxSklad
            // 
            this.comboBoxSklad.FormattingEnabled = true;
            this.comboBoxSklad.Location = new System.Drawing.Point(108, 9);
            this.comboBoxSklad.Name = "comboBoxSklad";
            this.comboBoxSklad.Size = new System.Drawing.Size(279, 24);
            this.comboBoxSklad.TabIndex = 3;
            // 
            // comboBoxIngredient
            // 
            this.comboBoxIngredient.FormattingEnabled = true;
            this.comboBoxIngredient.Location = new System.Drawing.Point(108, 45);
            this.comboBoxIngredient.Name = "comboBoxIngredient";
            this.comboBoxIngredient.Size = new System.Drawing.Size(279, 24);
            this.comboBoxIngredient.TabIndex = 4;
            // 
            // textBoxKol
            // 
            this.textBoxKol.Location = new System.Drawing.Point(108, 82);
            this.textBoxKol.Name = "textBoxKol";
            this.textBoxKol.Size = new System.Drawing.Size(279, 22);
            this.textBoxKol.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(157, 110);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(118, 32);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(281, 110);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(106, 32);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // FormSkladAddIngredients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 150);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxKol);
            this.Controls.Add(this.comboBoxIngredient);
            this.Controls.Add(this.comboBoxSklad);
            this.Controls.Add(this.labelKol);
            this.Controls.Add(this.labelIngredient);
            this.Controls.Add(this.labelSklad);
            this.Name = "FormSkladAddIngredients";
            this.Text = "Добавление Ингредиентов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSklad;
        private System.Windows.Forms.Label labelIngredient;
        private System.Windows.Forms.Label labelKol;
        private System.Windows.Forms.ComboBox comboBoxSklad;
        private System.Windows.Forms.ComboBox comboBoxIngredient;
        private System.Windows.Forms.TextBox textBoxKol;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}