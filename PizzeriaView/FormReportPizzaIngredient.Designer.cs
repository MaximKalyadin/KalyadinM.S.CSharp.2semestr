namespace PizzeriaView
{
    partial class FormReportPizzaIngredient
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
            this.ToPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // ToPdf
            // 
            this.ToPdf.Location = new System.Drawing.Point(869, 12);
            this.ToPdf.Name = "ToPdf";
            this.ToPdf.Size = new System.Drawing.Size(126, 34);
            this.ToPdf.TabIndex = 0;
            this.ToPdf.Text = "в pdf";
            this.ToPdf.UseVisualStyleBackColor = true;
            this.ToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Location = new System.Drawing.Point(2, 60);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(997, 460);
            this.reportViewer.TabIndex = 1;
            this.reportViewer.Load += new System.EventHandler(this.FormReportPizzaIngredient_Load);
            // 
            // FormReportPizzaIngredient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 519);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.ToPdf);
            this.Name = "FormReportPizzaIngredient";
            this.Text = "FormReportPizzaIngredient";
            this.Load += new System.EventHandler(this.FormReportPizzaIngredient_Load);
            this.Click += new System.EventHandler(this.FormReportPizzaIngredient_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}