namespace XphoneCreateUserData
{
    partial class Form1
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
            this.buttonViewCreated = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxTextFoder = new System.Windows.Forms.TextBox();
            this.buttonOpenTextFoder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.dataGridViewVariable = new System.Windows.Forms.DataGridView();
            this.ColumnVariable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumeValue1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumeValue2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumeValue3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVariable)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonViewCreated
            // 
            this.buttonViewCreated.Location = new System.Drawing.Point(24, 203);
            this.buttonViewCreated.Name = "buttonViewCreated";
            this.buttonViewCreated.Size = new System.Drawing.Size(159, 38);
            this.buttonViewCreated.TabIndex = 0;
            this.buttonViewCreated.Text = "View Created Text";
            this.buttonViewCreated.UseVisualStyleBackColor = true;
            this.buttonViewCreated.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // textBoxTextFoder
            // 
            this.textBoxTextFoder.Location = new System.Drawing.Point(21, 54);
            this.textBoxTextFoder.Name = "textBoxTextFoder";
            this.textBoxTextFoder.Size = new System.Drawing.Size(495, 20);
            this.textBoxTextFoder.TabIndex = 2;
            // 
            // buttonOpenTextFoder
            // 
            this.buttonOpenTextFoder.Location = new System.Drawing.Point(21, 80);
            this.buttonOpenTextFoder.Name = "buttonOpenTextFoder";
            this.buttonOpenTextFoder.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenTextFoder.TabIndex = 3;
            this.buttonOpenTextFoder.Text = "Brows";
            this.buttonOpenTextFoder.UseVisualStyleBackColor = true;
            this.buttonOpenTextFoder.Click += new System.EventHandler(this.ButtonOpenTextFoder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Source text foder";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(883, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateFileToolStripMenuItem,
            this.editConfigToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // generateFileToolStripMenuItem
            // 
            this.generateFileToolStripMenuItem.Name = "generateFileToolStripMenuItem";
            this.generateFileToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.generateFileToolStripMenuItem.Text = "Generate File";
            // 
            // editConfigToolStripMenuItem
            // 
            this.editConfigToolStripMenuItem.Name = "editConfigToolStripMenuItem";
            this.editConfigToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.editConfigToolStripMenuItem.Text = "Edit Config";
            this.editConfigToolStripMenuItem.Click += new System.EventHandler(this.EditConfigToolStripMenuItem_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(24, 136);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(159, 38);
            this.buttonGenerate.TabIndex = 6;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
            // 
            // dataGridViewVariable
            // 
            this.dataGridViewVariable.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridViewVariable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVariable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnVariable,
            this.ColumeValue1,
            this.ColumeValue2,
            this.ColumeValue3});
            this.dataGridViewVariable.Location = new System.Drawing.Point(234, 103);
            this.dataGridViewVariable.Name = "dataGridViewVariable";
            this.dataGridViewVariable.Size = new System.Drawing.Size(612, 291);
            this.dataGridViewVariable.TabIndex = 7;
            // 
            // ColumnVariable
            // 
            this.ColumnVariable.HeaderText = "Variable";
            this.ColumnVariable.Name = "ColumnVariable";
            // 
            // ColumeValue1
            // 
            this.ColumeValue1.HeaderText = "Value1";
            this.ColumeValue1.Name = "ColumeValue1";
            this.ColumeValue1.Width = 150;
            // 
            // ColumeValue2
            // 
            this.ColumeValue2.HeaderText = "Value 2";
            this.ColumeValue2.Name = "ColumeValue2";
            this.ColumeValue2.Width = 150;
            // 
            // ColumeValue3
            // 
            this.ColumeValue3.HeaderText = "Value 3";
            this.ColumeValue3.Name = "ColumeValue3";
            this.ColumeValue3.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 450);
            this.Controls.Add(this.dataGridViewVariable);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpenTextFoder);
            this.Controls.Add(this.textBoxTextFoder);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonViewCreated);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Create Gphone Data";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVariable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonViewCreated;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxTextFoder;
        private System.Windows.Forms.Button buttonOpenTextFoder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateFileToolStripMenuItem;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.ToolStripMenuItem editConfigToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewVariable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVariable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumeValue1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumeValue2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumeValue3;
    }
}

