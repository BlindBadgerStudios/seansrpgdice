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
            this.chkHighlight = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gridHistory = new System.Windows.Forms.DataGridView();
            this.colRolls = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridFormulas = new System.Windows.Forms.DataGridView();
            this.colFormula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Formula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHistory = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFormulas)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInput.Location = new System.Drawing.Point(26, 533);
            this.txtInput.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(530, 31);
            this.txtInput.TabIndex = 0;
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_OnEnter);
            this.txtInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtTest);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.campaignToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 46);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 38);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem2
            // 
            this.loadToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formulasToolStripMenuItem1,
            this.rollHistoryToolStripMenuItem3});
            this.loadToolStripMenuItem2.Name = "loadToolStripMenuItem2";
            this.loadToolStripMenuItem2.Size = new System.Drawing.Size(213, 44);
            this.loadToolStripMenuItem2.Text = "Load...";
            // 
            // formulasToolStripMenuItem1
            // 
            this.formulasToolStripMenuItem1.Name = "formulasToolStripMenuItem1";
            this.formulasToolStripMenuItem1.Size = new System.Drawing.Size(268, 44);
            this.formulasToolStripMenuItem1.Text = "Formulas";
            this.formulasToolStripMenuItem1.Click += new System.EventHandler(this.formulasToolStripMenuItem1_Click);
            // 
            // rollHistoryToolStripMenuItem3
            // 
            this.rollHistoryToolStripMenuItem3.Name = "rollHistoryToolStripMenuItem3";
            this.rollHistoryToolStripMenuItem3.Size = new System.Drawing.Size(268, 44);
            this.rollHistoryToolStripMenuItem3.Text = "Roll History";
            this.rollHistoryToolStripMenuItem3.Click += new System.EventHandler(this.rollHistoryToolStripMenuItem3_Click);
            // 
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rollHistoryToolStripMenuItem1,
            this.rollHistoryToolStripMenuItem2});
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            this.saveToolStripMenuItem2.Size = new System.Drawing.Size(213, 44);
            this.saveToolStripMenuItem2.Text = "Save...";
            // 
            // rollHistoryToolStripMenuItem1
            // 
            this.rollHistoryToolStripMenuItem1.Name = "rollHistoryToolStripMenuItem1";
            this.rollHistoryToolStripMenuItem1.Size = new System.Drawing.Size(268, 44);
            this.rollHistoryToolStripMenuItem1.Text = "Formulas";
            this.rollHistoryToolStripMenuItem1.Click += new System.EventHandler(this.rollHistoryToolStripMenuItem1_Click);
            // 
            // rollHistoryToolStripMenuItem2
            // 
            this.rollHistoryToolStripMenuItem2.Name = "rollHistoryToolStripMenuItem2";
            this.rollHistoryToolStripMenuItem2.Size = new System.Drawing.Size(268, 44);
            this.rollHistoryToolStripMenuItem2.Text = "Roll History";
            this.rollHistoryToolStripMenuItem2.Click += new System.EventHandler(this.rollHistoryToolStripMenuItem2_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(213, 44);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(213, 44);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // campaignToolStripMenuItem
            // 
            this.campaignToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chkOpenRoll,
            this.chkHighlight});
            this.campaignToolStripMenuItem.Name = "campaignToolStripMenuItem";
            this.campaignToolStripMenuItem.Size = new System.Drawing.Size(233, 38);
            this.campaignToolStripMenuItem.Text = "Campaign Options";
            // 
            // chkOpenRoll
            // 
            this.chkOpenRoll.Checked = true;
            this.chkOpenRoll.CheckOnClick = true;
            this.chkOpenRoll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenRoll.Name = "chkOpenRoll";
            this.chkOpenRoll.Size = new System.Drawing.Size(458, 44);
            this.chkOpenRoll.Text = "Open Rolls";
            this.chkOpenRoll.ToolTipText = "Enable open rolls for all the rolling performed.";
            // 
            // chkHighlight
            // 
            this.chkHighlight.Checked = true;
            this.chkHighlight.CheckOnClick = true;
            this.chkHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHighlight.Name = "chkHighlight";
            this.chkHighlight.Size = new System.Drawing.Size(458, 44);
            this.chkHighlight.Text = "Highlight Crit Success/Failure";
            this.chkHighlight.ToolTipText = "Highlight Natural 20s or Open Rolls as well as Natural 1s and Fumbles";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instructionsToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(84, 38);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // instructionsToolStripMenuItem
            // 
            this.instructionsToolStripMenuItem.Name = "instructionsToolStripMenuItem";
            this.instructionsToolStripMenuItem.Size = new System.Drawing.Size(356, 44);
            this.instructionsToolStripMenuItem.Text = "Instructions";
            this.instructionsToolStripMenuItem.Click += new System.EventHandler(this.instructionsToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(356, 44);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(353, 6);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(356, 44);
            this.aboutToolStripMenuItem1.Text = "About...";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // gridHistory
            // 
            this.gridHistory.AllowUserToAddRows = false;
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
            this.colRolls,
            this.colForm});
            this.gridHistory.Location = new System.Drawing.Point(24, 77);
            this.gridHistory.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.RowHeadersVisible = false;
            this.gridHistory.RowHeadersWidth = 82;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridHistory.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridHistory.Size = new System.Drawing.Size(536, 444);
            this.gridHistory.TabIndex = 8;
            // 
            // colRolls
            // 
            this.colRolls.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colRolls.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRolls.HeaderText = "Result";
            this.colRolls.MinimumWidth = 10;
            this.colRolls.Name = "colRolls";
            this.colRolls.ReadOnly = true;
            this.colRolls.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colRolls.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colForm
            // 
            this.colForm.HeaderText = "Formula";
            this.colForm.MinimumWidth = 10;
            this.colForm.Name = "colForm";
            this.colForm.Visible = false;
            // 
            // gridFormulas
            // 
            this.gridFormulas.AllowUserToAddRows = false;
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
            this.gridFormulas.Location = new System.Drawing.Point(574, 46);
            this.gridFormulas.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gridFormulas.MultiSelect = false;
            this.gridFormulas.Name = "gridFormulas";
            this.gridFormulas.ReadOnly = true;
            this.gridFormulas.RowHeadersVisible = false;
            this.gridFormulas.RowHeadersWidth = 82;
            this.gridFormulas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFormulas.Size = new System.Drawing.Size(302, 525);
            this.gridFormulas.TabIndex = 9;
            this.gridFormulas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFormulas_CellDoubleClick);
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
            this.Formula.MinimumWidth = 10;
            this.Formula.Name = "Formula";
            this.Formula.ReadOnly = true;
            this.Formula.Visible = false;
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Location = new System.Drawing.Point(18, 46);
            this.lblHistory.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(122, 25);
            this.lblHistory.TabIndex = 10;
            this.lblHistory.Text = "Roll History";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 587);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.gridFormulas);
            this.Controls.Add(this.gridHistory);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "Sean\'s Dice Roller [Beta]";
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
        private System.Windows.Forms.DataGridView gridHistory;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView gridFormulas;
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
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.ToolStripMenuItem chkHighlight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRolls;
        private System.Windows.Forms.DataGridViewTextBoxColumn colForm;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
    }
}

