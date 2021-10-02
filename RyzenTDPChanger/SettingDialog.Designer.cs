
namespace RyzenTDPChanger
{
    partial class SettingDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.launchBoxNumUD = new System.Windows.Forms.NumericUpDown();
            this.bigBoxNumUD = new System.Windows.Forms.NumericUpDown();
            this.saveBtn = new System.Windows.Forms.Button();
            this.defaultGameNumUD = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.platformGridView = new System.Windows.Forms.DataGridView();
            this.platformHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tdpHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.ryzenAdjPathTB = new System.Windows.Forms.TextBox();
            this.ryzenAdjPathBrowseBtn = new System.Windows.Forms.Button();
            this.enabledCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.launchBoxNumUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bigBoxNumUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultGameNumUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "LaunchBox TDP: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "BigBox TDP: ";
            // 
            // launchBoxNumUD
            // 
            this.launchBoxNumUD.Location = new System.Drawing.Point(127, 55);
            this.launchBoxNumUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.launchBoxNumUD.Name = "launchBoxNumUD";
            this.launchBoxNumUD.Size = new System.Drawing.Size(57, 20);
            this.launchBoxNumUD.TabIndex = 2;
            this.launchBoxNumUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bigBoxNumUD
            // 
            this.bigBoxNumUD.Location = new System.Drawing.Point(127, 89);
            this.bigBoxNumUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bigBoxNumUD.Name = "bigBoxNumUD";
            this.bigBoxNumUD.Size = new System.Drawing.Size(57, 20);
            this.bigBoxNumUD.TabIndex = 3;
            this.bigBoxNumUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(15, 285);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(216, 39);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "Save Settings";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // defaultGameNumUD
            // 
            this.defaultGameNumUD.Location = new System.Drawing.Point(127, 125);
            this.defaultGameNumUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.defaultGameNumUD.Name = "defaultGameNumUD";
            this.defaultGameNumUD.Size = new System.Drawing.Size(57, 20);
            this.defaultGameNumUD.TabIndex = 6;
            this.defaultGameNumUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Default Game TDP:";
            // 
            // platformGridView
            // 
            this.platformGridView.AllowUserToAddRows = false;
            this.platformGridView.AllowUserToDeleteRows = false;
            this.platformGridView.AllowUserToResizeColumns = false;
            this.platformGridView.AllowUserToResizeRows = false;
            this.platformGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.platformGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.platformHeader,
            this.tdpHeader});
            this.platformGridView.Location = new System.Drawing.Point(200, 51);
            this.platformGridView.Name = "platformGridView";
            this.platformGridView.RowHeadersVisible = false;
            this.platformGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.platformGridView.Size = new System.Drawing.Size(254, 213);
            this.platformGridView.TabIndex = 9;
            this.platformGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.platformGridView_CellValidating);
            // 
            // platformHeader
            // 
            this.platformHeader.HeaderText = "Platform";
            this.platformHeader.Name = "platformHeader";
            this.platformHeader.ReadOnly = true;
            this.platformHeader.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.platformHeader.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.platformHeader.Width = 150;
            // 
            // tdpHeader
            // 
            this.tdpHeader.HeaderText = "Default TDP (W)";
            this.tdpHeader.Name = "tdpHeader";
            this.tdpHeader.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tdpHeader.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(299, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "This project utilizes RyzenAdj and has the same limited liability.";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(318, 267);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(52, 13);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "RyzenAdj";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "RyzenAdj Path:";
            // 
            // ryzenAdjPathTB
            // 
            this.ryzenAdjPathTB.Location = new System.Drawing.Point(103, 13);
            this.ryzenAdjPathTB.Name = "ryzenAdjPathTB";
            this.ryzenAdjPathTB.ReadOnly = true;
            this.ryzenAdjPathTB.Size = new System.Drawing.Size(267, 20);
            this.ryzenAdjPathTB.TabIndex = 13;
            // 
            // ryzenAdjPathBrowseBtn
            // 
            this.ryzenAdjPathBrowseBtn.Location = new System.Drawing.Point(376, 11);
            this.ryzenAdjPathBrowseBtn.Name = "ryzenAdjPathBrowseBtn";
            this.ryzenAdjPathBrowseBtn.Size = new System.Drawing.Size(78, 23);
            this.ryzenAdjPathBrowseBtn.TabIndex = 14;
            this.ryzenAdjPathBrowseBtn.Text = "Browse ...";
            this.ryzenAdjPathBrowseBtn.UseVisualStyleBackColor = true;
            this.ryzenAdjPathBrowseBtn.Click += new System.EventHandler(this.ryzenAdjPathBrowseBtn_Click);
            // 
            // enabledCB
            // 
            this.enabledCB.AutoSize = true;
            this.enabledCB.Location = new System.Drawing.Point(256, 307);
            this.enabledCB.Name = "enabledCB";
            this.enabledCB.Size = new System.Drawing.Size(65, 17);
            this.enabledCB.TabIndex = 15;
            this.enabledCB.Text = "Enabled";
            this.enabledCB.UseVisualStyleBackColor = true;
            // 
            // SettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 336);
            this.Controls.Add(this.enabledCB);
            this.Controls.Add(this.ryzenAdjPathBrowseBtn);
            this.Controls.Add(this.ryzenAdjPathTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.platformGridView);
            this.Controls.Add(this.defaultGameNumUD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.bigBoxNumUD);
            this.Controls.Add(this.launchBoxNumUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SettingDialog";
            this.Text = "TDP Settings";
            this.Load += new System.EventHandler(this.SettingDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.launchBoxNumUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bigBoxNumUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultGameNumUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown launchBoxNumUD;
        private System.Windows.Forms.NumericUpDown bigBoxNumUD;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.NumericUpDown defaultGameNumUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView platformGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn platformHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn tdpHeader;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ryzenAdjPathTB;
        private System.Windows.Forms.Button ryzenAdjPathBrowseBtn;
        private System.Windows.Forms.CheckBox enabledCB;
    }
}