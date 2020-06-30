namespace PizzeriaSkladManager
{
    partial class FormSkladIngredient
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxComponents = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.textBoxNameSklad = new System.Windows.Forms.TextBox();
            this.labelNameSklad = new System.Windows.Forms.Label();
            this.groupBoxComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(322, 261);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 24);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // groupBoxComponents
            // 
            this.groupBoxComponents.Controls.Add(this.dataGridView);
            this.groupBoxComponents.Location = new System.Drawing.Point(12, 37);
            this.groupBoxComponents.Name = "groupBoxComponents";
            this.groupBoxComponents.Size = new System.Drawing.Size(390, 219);
            this.groupBoxComponents.TabIndex = 11;
            this.groupBoxComponents.TabStop = false;
            this.groupBoxComponents.Text = "Текстильные материалы";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(8, 17);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(374, 196);
            this.dataGridView.TabIndex = 0;
            // 
            // textBoxNameSklad
            // 
            this.textBoxNameSklad.Location = new System.Drawing.Point(85, 4);
            this.textBoxNameSklad.Name = "textBoxNameSklad";
            this.textBoxNameSklad.Size = new System.Drawing.Size(173, 20);
            this.textBoxNameSklad.TabIndex = 9;
            this.textBoxNameSklad.Enabled = false;
            // 
            // labelNameSklad
            // 
            this.labelNameSklad.AutoSize = true;
            this.labelNameSklad.Location = new System.Drawing.Point(19, 7);
            this.labelNameSklad.Name = "labelNameSklad";
            this.labelNameSklad.Size = new System.Drawing.Size(60, 13);
            this.labelNameSklad.TabIndex = 7;
            this.labelNameSklad.Text = "Название:";
            // 
            // FormSkladIngredient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 287);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxComponents);
            this.Controls.Add(this.textBoxNameSklad);
            this.Controls.Add(this.labelNameSklad);
            this.Name = "FormSkladIngredient";
            this.Text = "Хранилище";
            this.Load += new System.EventHandler(this.FormSkladIngredient_Load);
            this.groupBoxComponents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxComponents;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox textBoxNameSklad;
        private System.Windows.Forms.Label labelNameSklad;
    }
}