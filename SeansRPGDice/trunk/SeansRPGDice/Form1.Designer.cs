namespace DiceRoller
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.formulasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rollHistoryToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.rollHistoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rollHistoryToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.campaignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkOpenRoll = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.chkReroll = new System.Windows.Forms.CheckBox();
            this.btnReroll = new System.Windows.Forms.Button();
            this.txtReroll = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridHistory = new System.Windows.Forms.DataGridView();
            this.colRolls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridFormulas = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.colFormula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Formula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFormulas)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInput.Location = new System.Drawing.Point(13, 277);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(267, 20);
            this.txtInput.TabIndex = 0;
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_OnEnter);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.campaignToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem2,
            this.saveToolStripMenuItem2,
            this.clearToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem2
            // 
            this.loadToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formulasToolStripMenuItem1,
            this.rollHistoryToolStripMenuItem3});
            this.loadToolStripMenuItem2.Name = "loadToolStripMenuItem2";
            this.loadToolStripMenuItem2.Size = new System.Drawing.Size(121, 22);
            this.loadToolStripMenuItem2.Text = "Load...";
            // 
            // formulasToolStripMenuItem1
            // 
            this.formulasToolStripMenuItem1.Name = "formulasToolStripMenuItem1";
            this.formulasToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.formulasToolStripMenuItem1.Text = "Formulas";
            this.formulasToolStripMenuItem1.Click += new System.EventHandler(this.formulasToolStripMenuItem1_Click);
            // 
            // rollHistoryToolStripMenuItem3
            // 
            this.rollHistoryToolStripMenuItem3.Name = "rollHistoryToolStripMenuItem3";
            this.rollHistoryToolStripMenuItem3.Size = new System.Drawing.Size(139, 22);
            this.rollHistoryToolStripMenuItem3.Text = "Roll History";
            this.rollHistoryToolStripMenuItem3.Click += new System.EventHandler(this.rollHistoryToolStripMenuItem3_Click);
            // 
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rollHistoryToolStripMenuItem1,
            this.rollHistoryToolStripMenuItem2});
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            this.saveToolStripMenuItem2.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem2.Text = "Save...";
            // 
            // rollHistoryToolStripMenuItem1
            // 
            this.rollHistoryToolStripMenuItem1.Name = "rollHistoryToolStripMenuItem1";
            this.rollHistoryToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.rollHistoryToolStripMenuItem1.Text = "Formulas";
            this.rollHistoryToolStripMenuItem1.Click += new System.EventHandler(this.rollHistoryToolStripMenuItem1_Click);
            // 
            // rollHistoryToolStripMenuItem2
            // 
            this.rollHistoryToolStripMenuItem2.Name = "rollHistoryToolStripMenuItem2";
            this.rollHistoryToolStripMenuItem2.Size = new System.Drawing.Size(139, 22);
            this.rollHistoryToolStripMenuItem2.Text = "Roll History";
            this.rollHistoryToolStripMenuItem2.Click += new System.EventHandler(this.rollHistoryToolStripMenuItem2_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(118, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // campaignToolStripMenuItem
            // 
            this.campaignToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chkOpenRoll});
            this.campaignToolStripMenuItem.Name = "campaignToolStripMenuItem";
            this.campaignToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.campaignToolStripMenuItem.Text = "Campaign Options";
            // 
            // chkOpenRoll
            // 
            this.chkOpenRoll.CheckOnClick = true;
            this.chkOpenRoll.Name = "chkOpenRoll";
            this.chkOpenRoll.Size = new System.Drawing.Size(136, 22);
            this.chkOpenRoll.Text = "Open Rolls";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instructionsToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // instructionsToolStripMenuItem
            // 
            this.instructionsToolStripMenuItem.Name = "instructionsToolStripMenuItem";
            this.instructionsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.instructionsToolStripMenuItem.Text = "Instructions";
            this.instructionsToolStripMenuItem.Click += new System.EventHandler(this.instructionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.aboutToolStripMenuItem1.Text = "About...";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // chkReroll
            // 
            this.chkReroll.AutoSize = true;
            this.chkReroll.Location = new System.Drawing.Point(12, 28);
            this.chkReroll.Name = "chkReroll";
            this.chkReroll.Size = new System.Drawing.Size(53, 17);
            this.chkReroll.TabIndex = 3;
            this.chkReroll.Text = "Reroll";
            this.chkReroll.UseVisualStyleBackColor = true;
            // 
            // btnReroll
            // 
            this.btnReroll.Location = new System.Drawing.Point(287, 274);
            this.btnReroll.Name = "btnReroll";
            this.btnReroll.Size = new System.Drawing.Size(64, 23);
            this.btnReroll.TabIndex = 4;
            this.btnReroll.Text = "Re-roll";
            this.btnReroll.UseVisualStyleBackColor = true;
            this.btnReroll.Click += new System.EventHandler(this.btnReroll_Click);
            // 
            // txtReroll
            // 
            this.txtReroll.Location = new System.Drawing.Point(60, 26);
            this.txtReroll.Name = "txtReroll";
            this.txtReroll.Size = new System.Drawing.Size(28, 20);
            this.txtReroll.TabIndex = 5;
            this.txtReroll.Text = "1";
            this.txtReroll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "and lower";
            // 
            // gridHistory
            // 
            this.gridHistory.AllowUserToAddRows = false;
            this.gridHistory.AllowUserToDeleteRows = false;
            this.gridHistory.AllowUserToResizeColumns = false;
            this.gridHistory.AllowUserToResizeRows = false;
            this.gridHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridHistory.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHistory.ColumnHeadersVisible = false;
            this.gridHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRolls});
            this.gridHistory.Location = new System.Drawing.Point(12, 52);
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridHistory.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridHistory.Size = new System.Drawing.Size(268, 219);
            this.gridHistory.TabIndex = 8;
            // 
            // colRolls
            // 
            this.colRolls.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colRolls.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRolls.HeaderText = "Result";
            this.colRolls.Name = "colRolls";
            this.colRolls.ReadOnly = true;
            this.colRolls.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colRolls.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gridFormulas
            // 
            this.gridFormulas.AllowUserToAddRows = false;
            this.gridFormulas.AllowUserToDeleteRows = false;
            this.gridFormulas.AllowUserToResizeColumns = false;
            this.gridFormulas.AllowUserToResizeRows = false;
            this.gridFormulas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFormulas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridFormulas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridFormulas.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridFormulas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFormulas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFormula,
            this.Formula});
            this.gridFormulas.Location = new System.Drawing.Point(287, 24);
            this.gridFormulas.Name = "gridFormulas";
            this.gridFormulas.RowHeadersVisible = false;
            this.gridFormulas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFormulas.Size = new System.Drawing.Size(151, 247);
            this.gridFormulas.TabIndex = 9;
            this.gridFormulas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFormulas_CellDoubleClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(374, 274);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete f(x)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // colFormula
            // 
            this.colFormula.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colFormula.DefaultCellStyle = dataGridViewCellStyle3;
            this.colFormula.HeaderText = "Saved (Double-click)";
            this.colFormula.MinimumWidth = 100;
            this.colFormula.Name = "colFormula";
            this.colFormula.ReadOnly = true;
            this.colFormula.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colFormula.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFormula.ToolTipText = "Formulas you want to be able to run repeatedly.";
            // 
            // Formula
            // 
            this.Formula.HeaderText = "Formula";
            this.Formula.Name = "Formula";
            this.Formula.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 305);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.gridFormulas);
            this.Controls.Add(this.gridHistory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReroll);
            this.Controls.Add(this.btnReroll);
            this.Controls.Add(this.chkReroll);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Sean\'s Dice Roller";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFormulas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkReroll;
        private System.Windows.Forms.Button btnReroll;
        private System.Windows.Forms.TextBox txtReroll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridHistory;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView gridFormulas;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRolls;
        private System.Windows.Forms.ToolStripMenuItem instructionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem formulasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rollHistoryToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem rollHistoryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rollHistoryToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem campaignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chkOpenRoll;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormula;
        private System.Windows.Forms.DataGridViewTextBoxColumn Formula;
    }
}

