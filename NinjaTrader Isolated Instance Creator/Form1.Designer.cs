namespace NinjaTrader_Starter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fullPanel1 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addNew = new System.Windows.Forms.Button();
            this.newInstanceName = new System.Windows.Forms.TextBox();
            this.startButtonsPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.startOnClick = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.creatingMessage = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.creatingLabel = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.fullPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.progressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fullPanel1
            // 
            this.fullPanel1.Controls.Add(this.panel1);
            this.fullPanel1.Controls.Add(this.startButtonsPanel);
            this.fullPanel1.Location = new System.Drawing.Point(1, 2);
            this.fullPanel1.Name = "fullPanel1";
            this.fullPanel1.Size = new System.Drawing.Size(251, 301);
            this.fullPanel1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addNew);
            this.panel1.Controls.Add(this.newInstanceName);
            this.panel1.Location = new System.Drawing.Point(0, 269);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 32);
            this.panel1.TabIndex = 6;
            // 
            // addNew
            // 
            this.addNew.AutoEllipsis = true;
            this.addNew.BackColor = System.Drawing.Color.Transparent;
            this.addNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNew.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.addNew.Location = new System.Drawing.Point(180, 0);
            this.addNew.Name = "addNew";
            this.addNew.Size = new System.Drawing.Size(63, 32);
            this.addNew.TabIndex = 1;
            this.addNew.Text = "Add New";
            this.addNew.UseVisualStyleBackColor = false;
            this.addNew.Click += new System.EventHandler(this.addNew_Click);
            // 
            // newInstanceName
            // 
            this.newInstanceName.BackColor = System.Drawing.Color.Snow;
            this.newInstanceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newInstanceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newInstanceName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.newInstanceName.Location = new System.Drawing.Point(6, 3);
            this.newInstanceName.Name = "newInstanceName";
            this.newInstanceName.Size = new System.Drawing.Size(171, 26);
            this.newInstanceName.TabIndex = 3;
            this.newInstanceName.Text = "my-instance 1";
            // 
            // startButtonsPanel
            // 
            this.startButtonsPanel.Location = new System.Drawing.Point(3, 3);
            this.startButtonsPanel.Name = "startButtonsPanel";
            this.startButtonsPanel.Size = new System.Drawing.Size(245, 263);
            this.startButtonsPanel.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RosyBrown;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.startOnClick);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(1, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 34);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "NT version";
            // 
            // startOnClick
            // 
            this.startOnClick.AutoSize = true;
            this.startOnClick.Checked = true;
            this.startOnClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startOnClick.Location = new System.Drawing.Point(227, 10);
            this.startOnClick.Name = "startOnClick";
            this.startOnClick.Size = new System.Drawing.Size(15, 14);
            this.startOnClick.TabIndex = 4;
            this.startOnClick.UseVisualStyleBackColor = true;
            this.startOnClick.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.Snow;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Items.AddRange(new object[] {
            "7",
            "8"});
            this.comboBox1.Location = new System.Drawing.Point(64, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(32, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(118, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start NT after clicking the instance name";
            // 
            // progressPanel
            // 
            this.progressPanel.Controls.Add(this.creatingMessage);
            this.progressPanel.Controls.Add(this.progressBar1);
            this.progressPanel.Controls.Add(this.creatingLabel);
            this.progressPanel.Location = new System.Drawing.Point(4, 306);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(240, 40);
            this.progressPanel.TabIndex = 6;
            this.progressPanel.Visible = false;
            // 
            // creatingMessage
            // 
            this.creatingMessage.AutoSize = true;
            this.creatingMessage.Location = new System.Drawing.Point(73, 8);
            this.creatingMessage.Name = "creatingMessage";
            this.creatingMessage.Size = new System.Drawing.Size(75, 13);
            this.creatingMessage.TabIndex = 8;
            this.creatingMessage.Text = "Creating folder";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 24);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(228, 10);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 7;
            // 
            // creatingLabel
            // 
            this.creatingLabel.AutoSize = true;
            this.creatingLabel.Location = new System.Drawing.Point(12, 8);
            this.creatingLabel.Name = "creatingLabel";
            this.creatingLabel.Size = new System.Drawing.Size(0, 13);
            this.creatingLabel.TabIndex = 6;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(152, 349);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(97, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "By Puvox.Software";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(1, 349);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(29, 13);
            this.linkLabel2.TabIndex = 8;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Help";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(250, 365);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.progressPanel);
            this.Controls.Add(this.fullPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "NinjaTrader instance starter";
            this.TransparencyKey = System.Drawing.Color.Linen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.fullPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.progressPanel.ResumeLayout(false);
            this.progressPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel fullPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addNew;
        private System.Windows.Forms.TextBox newInstanceName;
        private System.Windows.Forms.Panel startButtonsPanel;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Label creatingLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label creatingMessage;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.CheckBox startOnClick;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
    }
}

