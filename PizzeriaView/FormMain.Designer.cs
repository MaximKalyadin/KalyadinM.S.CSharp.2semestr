namespace PizzeriaView
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
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.buttonakeOrderInWork = new System.Windows.Forms.Button();
            this.buttonOrderReady = new System.Windows.Forms.Button();
            this.buttonPayOrder = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.menuStripSpravochnik = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ингредиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пиццаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокИнгредиентовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ингредиентыПоПиццамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокПиццToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStripSpravochnik.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 31);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(852, 393);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Location = new System.Drawing.Point(875, 45);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(214, 28);
            this.buttonCreateOrder.TabIndex = 1;
            this.buttonCreateOrder.Text = "Создать заказ ";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.buttonCreateOrder_Click);
            // 
            // buttonakeOrderInWork
            // 
            this.buttonakeOrderInWork.Location = new System.Drawing.Point(875, 93);
            this.buttonakeOrderInWork.Name = "buttonakeOrderInWork";
            this.buttonakeOrderInWork.Size = new System.Drawing.Size(214, 28);
            this.buttonakeOrderInWork.TabIndex = 2;
            this.buttonakeOrderInWork.Text = "Отдать на выполнение";
            this.buttonakeOrderInWork.UseVisualStyleBackColor = true;
            this.buttonakeOrderInWork.Click += new System.EventHandler(this.buttonTakeOrderInWork_Click);
            // 
            // buttonOrderReady
            // 
            this.buttonOrderReady.Location = new System.Drawing.Point(875, 138);
            this.buttonOrderReady.Name = "buttonOrderReady";
            this.buttonOrderReady.Size = new System.Drawing.Size(214, 29);
            this.buttonOrderReady.TabIndex = 3;
            this.buttonOrderReady.Text = "Заказ готов";
            this.buttonOrderReady.UseVisualStyleBackColor = true;
            this.buttonOrderReady.Click += new System.EventHandler(this.buttonOrderReady_Click);
            // 
            // buttonPayOrder
            // 
            this.buttonPayOrder.Location = new System.Drawing.Point(875, 185);
            this.buttonPayOrder.Name = "buttonPayOrder";
            this.buttonPayOrder.Size = new System.Drawing.Size(214, 31);
            this.buttonPayOrder.TabIndex = 4;
            this.buttonPayOrder.Text = "Заказ оплчен";
            this.buttonPayOrder.UseVisualStyleBackColor = true;
            this.buttonPayOrder.Click += new System.EventHandler(this.buttonPayOrder_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(875, 238);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(214, 30);
            this.buttonRef.TabIndex = 5;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // menuStripSpravochnik
            // 
            this.menuStripSpravochnik.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripSpravochnik.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.пополнитьСкладToolStripMenuItem});
            this.справочникToolStripMenuItem,
            this.отчетыToolStripMenuItem});
            this.menuStripSpravochnik.Location = new System.Drawing.Point(0, 0);
            this.menuStripSpravochnik.Name = "menuStripSpravochnik";
            this.menuStripSpravochnik.Size = new System.Drawing.Size(1101, 28);
            this.menuStripSpravochnik.TabIndex = 6;
            this.menuStripSpravochnik.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ингредиентыToolStripMenuItem,
            this.пиццаToolStripMenuItem,
            this.складыToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.справочникToolStripMenuItem.Text = "Справочник";
            // 
            // ингредиентыToolStripMenuItem
            // 
            this.ингредиентыToolStripMenuItem.Name = "ингредиентыToolStripMenuItem";
            this.ингредиентыToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.ингредиентыToolStripMenuItem.Text = "Ингредиенты";
            this.ингредиентыToolStripMenuItem.Click += new System.EventHandler(this.ингредиентыToolStripMenuItem_Click);
            // 
            // пиццаToolStripMenuItem
            // 
            this.пиццаToolStripMenuItem.Name = "пиццаToolStripMenuItem";
            this.пиццаToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.пиццаToolStripMenuItem.Text = "Пицца";
            this.пиццаToolStripMenuItem.Click += new System.EventHandler(this.пиццыToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокИнгредиентовToolStripMenuItem,
            this.ингредиентыПоПиццамToolStripMenuItem,
            this.списокПиццToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // списокИнгредиентовToolStripMenuItem
            // 
            this.списокИнгредиентовToolStripMenuItem.Name = "списокИнгредиентовToolStripMenuItem";
            this.списокИнгредиентовToolStripMenuItem.Size = new System.Drawing.Size(267, 26);
            this.списокИнгредиентовToolStripMenuItem.Text = "Список ингредиентов";
            this.списокИнгредиентовToolStripMenuItem.Click += new System.EventHandler(this.списокИнгредиентовToolStripMenuItem_Click);
            // 
            // ингредиентыПоПиццамToolStripMenuItem
            // 
            this.ингредиентыПоПиццамToolStripMenuItem.Name = "ингредиентыПоПиццамToolStripMenuItem";
            this.ингредиентыПоПиццамToolStripMenuItem.Size = new System.Drawing.Size(267, 26);
            this.ингредиентыПоПиццамToolStripMenuItem.Text = "Ингредиенты по пиццам";
            this.ингредиентыПоПиццамToolStripMenuItem.Click += new System.EventHandler(this.ингредиентыПоПиццамToolStripMenuItem_Click);
            // 
            // списокПиццToolStripMenuItem
            // 
            this.списокПиццToolStripMenuItem.Name = "списокПиццToolStripMenuItem";
            this.списокПиццToolStripMenuItem.Size = new System.Drawing.Size(267, 26);
            this.списокПиццToolStripMenuItem.Text = "Список пицц";
            this.списокПиццToolStripMenuItem.Click += new System.EventHandler(this.списокПиццToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 450);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayOrder);
            this.Controls.Add(this.buttonOrderReady);
            this.Controls.Add(this.buttonakeOrderInWork);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStripSpravochnik);
            this.Name = "FormMain";
            this.Text = "Пиццерия";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStripSpravochnik.ResumeLayout(false);
            this.menuStripSpravochnik.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.Button buttonakeOrderInWork;
        private System.Windows.Forms.Button buttonOrderReady;
        private System.Windows.Forms.Button buttonPayOrder;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.MenuStrip menuStripSpravochnik;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ингредиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пиццаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem складыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьСкладToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокИнгредиентовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ингредиентыПоПиццамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокПиццToolStripMenuItem;
    }
}