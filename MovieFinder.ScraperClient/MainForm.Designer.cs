namespace MovieFinder.ScraperClient
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxRemoved = new System.Windows.Forms.RichTextBox();
            this.checkBoxQuickValidate = new System.Windows.Forms.CheckBox();
            this.buttonStopValidation = new System.Windows.Forms.Button();
            this.labelUnknownLink = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelRemovedLink = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelValidatedLink = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelTotalValidated = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelValidatingLink = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonValidateLinks = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonI4M = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonTamiz = new System.Windows.Forms.Button();
            this.buttonTVCD = new System.Windows.Forms.Button();
            this.buttonABC = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLastExportedTime = new System.Windows.Forms.Label();
            this.buttonEIH = new System.Windows.Forms.Button();
            this.labelCount = new System.Windows.Forms.Label();
            this.buttonHL4U = new System.Windows.Forms.Button();
            this.buttonDoAll = new System.Windows.Forms.Button();
            this.labelMsg = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelDBPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.textBoxMsg = new System.Windows.Forms.RichTextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonFetchImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.labelDBPath);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1315, 610);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBoxRemoved);
            this.groupBox2.Controls.Add(this.checkBoxQuickValidate);
            this.groupBox2.Controls.Add(this.buttonStopValidation);
            this.groupBox2.Controls.Add(this.labelUnknownLink);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.labelRemovedLink);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.labelValidatedLink);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.labelTotalValidated);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.labelValidatingLink);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonValidateLinks);
            this.groupBox2.Location = new System.Drawing.Point(790, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(506, 214);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Validator";
            // 
            // richTextBoxRemoved
            // 
            this.richTextBoxRemoved.Location = new System.Drawing.Point(22, 107);
            this.richTextBoxRemoved.Name = "richTextBoxRemoved";
            this.richTextBoxRemoved.Size = new System.Drawing.Size(478, 104);
            this.richTextBoxRemoved.TabIndex = 19;
            this.richTextBoxRemoved.Text = "";
            this.richTextBoxRemoved.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxRemoved_LinkClicked);
            // 
            // checkBoxQuickValidate
            // 
            this.checkBoxQuickValidate.AutoSize = true;
            this.checkBoxQuickValidate.Checked = true;
            this.checkBoxQuickValidate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxQuickValidate.Location = new System.Drawing.Point(188, 23);
            this.checkBoxQuickValidate.Name = "checkBoxQuickValidate";
            this.checkBoxQuickValidate.Size = new System.Drawing.Size(95, 17);
            this.checkBoxQuickValidate.TabIndex = 18;
            this.checkBoxQuickValidate.Text = "Quick Validate";
            this.checkBoxQuickValidate.UseVisualStyleBackColor = true;
            // 
            // buttonStopValidation
            // 
            this.buttonStopValidation.Location = new System.Drawing.Point(98, 19);
            this.buttonStopValidation.Name = "buttonStopValidation";
            this.buttonStopValidation.Size = new System.Drawing.Size(75, 23);
            this.buttonStopValidation.TabIndex = 17;
            this.buttonStopValidation.Text = "Stop";
            this.buttonStopValidation.UseVisualStyleBackColor = true;
            this.buttonStopValidation.Click += new System.EventHandler(this.buttonStopValidation_Click);
            // 
            // labelUnknownLink
            // 
            this.labelUnknownLink.AutoSize = true;
            this.labelUnknownLink.Location = new System.Drawing.Point(346, 77);
            this.labelUnknownLink.Name = "labelUnknownLink";
            this.labelUnknownLink.Size = new System.Drawing.Size(10, 13);
            this.labelUnknownLink.TabIndex = 16;
            this.labelUnknownLink.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(281, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Unknown: ";
            // 
            // labelRemovedLink
            // 
            this.labelRemovedLink.AutoSize = true;
            this.labelRemovedLink.Location = new System.Drawing.Point(260, 76);
            this.labelRemovedLink.Name = "labelRemovedLink";
            this.labelRemovedLink.Size = new System.Drawing.Size(10, 13);
            this.labelRemovedLink.TabIndex = 14;
            this.labelRemovedLink.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Removed: ";
            // 
            // labelValidatedLink
            // 
            this.labelValidatedLink.AutoSize = true;
            this.labelValidatedLink.Location = new System.Drawing.Point(154, 76);
            this.labelValidatedLink.Name = "labelValidatedLink";
            this.labelValidatedLink.Size = new System.Drawing.Size(10, 13);
            this.labelValidatedLink.TabIndex = 12;
            this.labelValidatedLink.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(115, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Valid: ";
            // 
            // labelTotalValidated
            // 
            this.labelTotalValidated.AutoSize = true;
            this.labelTotalValidated.Location = new System.Drawing.Point(62, 77);
            this.labelTotalValidated.Name = "labelTotalValidated";
            this.labelTotalValidated.Size = new System.Drawing.Size(10, 13);
            this.labelTotalValidated.TabIndex = 10;
            this.labelTotalValidated.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total: ";
            // 
            // labelValidatingLink
            // 
            this.labelValidatingLink.AutoSize = true;
            this.labelValidatingLink.Location = new System.Drawing.Point(62, 53);
            this.labelValidatingLink.Name = "labelValidatingLink";
            this.labelValidatingLink.Size = new System.Drawing.Size(10, 13);
            this.labelValidatingLink.TabIndex = 8;
            this.labelValidatingLink.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Link: ";
            // 
            // buttonValidateLinks
            // 
            this.buttonValidateLinks.Location = new System.Drawing.Point(17, 19);
            this.buttonValidateLinks.Name = "buttonValidateLinks";
            this.buttonValidateLinks.Size = new System.Drawing.Size(75, 23);
            this.buttonValidateLinks.TabIndex = 6;
            this.buttonValidateLinks.Text = "Validate";
            this.buttonValidateLinks.UseVisualStyleBackColor = true;
            this.buttonValidateLinks.Click += new System.EventHandler(this.buttonValidateLinks_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonFetchImage);
            this.groupBox1.Controls.Add(this.buttonI4M);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.buttonTamiz);
            this.groupBox1.Controls.Add(this.buttonTVCD);
            this.groupBox1.Controls.Add(this.buttonABC);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelLastExportedTime);
            this.groupBox1.Controls.Add(this.buttonEIH);
            this.groupBox1.Controls.Add(this.labelCount);
            this.groupBox1.Controls.Add(this.buttonHL4U);
            this.groupBox1.Controls.Add(this.buttonDoAll);
            this.groupBox1.Controls.Add(this.labelMsg);
            this.groupBox1.Controls.Add(this.buttonExport);
            this.groupBox1.Controls.Add(this.buttonStop);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 144);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scrapper";
            // 
            // buttonI4M
            // 
            this.buttonI4M.Location = new System.Drawing.Point(422, 19);
            this.buttonI4M.Name = "buttonI4M";
            this.buttonI4M.Size = new System.Drawing.Size(75, 23);
            this.buttonI4M.TabIndex = 19;
            this.buttonI4M.Text = "Start I4M";
            this.buttonI4M.UseVisualStyleBackColor = true;
            this.buttonI4M.Click += new System.EventHandler(this.buttonI4M_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(65, 86);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(62, 20);
            this.numericUpDown1.TabIndex = 18;
            this.numericUpDown1.Value = new decimal(new int[] {
            2012,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "From:";
            // 
            // buttonTamiz
            // 
            this.buttonTamiz.Location = new System.Drawing.Point(341, 19);
            this.buttonTamiz.Name = "buttonTamiz";
            this.buttonTamiz.Size = new System.Drawing.Size(75, 23);
            this.buttonTamiz.TabIndex = 16;
            this.buttonTamiz.Text = "Start Tamiz";
            this.buttonTamiz.UseVisualStyleBackColor = true;
            this.buttonTamiz.Click += new System.EventHandler(this.buttonTamiz_Click);
            // 
            // buttonTVCD
            // 
            this.buttonTVCD.Location = new System.Drawing.Point(260, 19);
            this.buttonTVCD.Name = "buttonTVCD";
            this.buttonTVCD.Size = new System.Drawing.Size(75, 23);
            this.buttonTVCD.TabIndex = 15;
            this.buttonTVCD.Text = "Start TVCD";
            this.buttonTVCD.UseVisualStyleBackColor = true;
            this.buttonTVCD.Click += new System.EventHandler(this.buttonTVCD_Click);
            // 
            // buttonABC
            // 
            this.buttonABC.Location = new System.Drawing.Point(17, 19);
            this.buttonABC.Name = "buttonABC";
            this.buttonABC.Size = new System.Drawing.Size(75, 23);
            this.buttonABC.TabIndex = 3;
            this.buttonABC.Text = "Start ABC";
            this.buttonABC.UseVisualStyleBackColor = true;
            this.buttonABC.Click += new System.EventHandler(this.buttonABC_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Exported Time";
            // 
            // labelLastExportedTime
            // 
            this.labelLastExportedTime.AutoSize = true;
            this.labelLastExportedTime.Location = new System.Drawing.Point(95, 119);
            this.labelLastExportedTime.Name = "labelLastExportedTime";
            this.labelLastExportedTime.Size = new System.Drawing.Size(10, 13);
            this.labelLastExportedTime.TabIndex = 13;
            this.labelLastExportedTime.Text = "-";
            // 
            // buttonEIH
            // 
            this.buttonEIH.Location = new System.Drawing.Point(98, 19);
            this.buttonEIH.Name = "buttonEIH";
            this.buttonEIH.Size = new System.Drawing.Size(75, 23);
            this.buttonEIH.TabIndex = 4;
            this.buttonEIH.Text = "Start EIH";
            this.buttonEIH.UseVisualStyleBackColor = true;
            this.buttonEIH.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(304, 119);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(31, 13);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "Total";
            // 
            // buttonHL4U
            // 
            this.buttonHL4U.Location = new System.Drawing.Point(179, 19);
            this.buttonHL4U.Name = "buttonHL4U";
            this.buttonHL4U.Size = new System.Drawing.Size(75, 23);
            this.buttonHL4U.TabIndex = 5;
            this.buttonHL4U.Text = "Start HL4U";
            this.buttonHL4U.UseVisualStyleBackColor = true;
            this.buttonHL4U.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonDoAll
            // 
            this.buttonDoAll.Location = new System.Drawing.Point(98, 48);
            this.buttonDoAll.Name = "buttonDoAll";
            this.buttonDoAll.Size = new System.Drawing.Size(75, 23);
            this.buttonDoAll.TabIndex = 10;
            this.buttonDoAll.Text = "Do All";
            this.buttonDoAll.UseVisualStyleBackColor = true;
            this.buttonDoAll.Click += new System.EventHandler(this.buttonDoAll_Click);
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.Location = new System.Drawing.Point(21, 97);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(0, 13);
            this.labelMsg.TabIndex = 8;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(503, 19);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(179, 48);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelDBPath
            // 
            this.labelDBPath.AutoSize = true;
            this.labelDBPath.Location = new System.Drawing.Point(560, 245);
            this.labelDBPath.Name = "labelDBPath";
            this.labelDBPath.Size = new System.Drawing.Size(0, 13);
            this.labelDBPath.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "DB Path: ";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxMsg);
            this.splitContainer2.Size = new System.Drawing.Size(1315, 364);
            this.splitContainer2.SplitterDistance = 261;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.Size = new System.Drawing.Size(1315, 261);
            this.dataGridView.TabIndex = 0;
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMsg.Location = new System.Drawing.Point(0, 0);
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.Size = new System.Drawing.Size(1315, 99);
            this.textBoxMsg.TabIndex = 0;
            this.textBoxMsg.Text = "";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 36000000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // buttonFetchImage
            // 
            this.buttonFetchImage.Location = new System.Drawing.Point(260, 48);
            this.buttonFetchImage.Name = "buttonFetchImage";
            this.buttonFetchImage.Size = new System.Drawing.Size(75, 23);
            this.buttonFetchImage.TabIndex = 20;
            this.buttonFetchImage.Text = "Fetch Image";
            this.buttonFetchImage.UseVisualStyleBackColor = true;
            this.buttonFetchImage.Click += new System.EventHandler(this.buttonFetchImage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 610);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonHL4U;
        private System.Windows.Forms.Button buttonEIH;
        private System.Windows.Forms.Button buttonABC;
        private System.Windows.Forms.Button buttonValidateLinks;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Button buttonDoAll;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelDBPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLastExportedTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelUnknownLink;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelRemovedLink;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelValidatedLink;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelTotalValidated;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelValidatingLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonStopValidation;
        private System.Windows.Forms.CheckBox checkBoxQuickValidate;
        private System.Windows.Forms.RichTextBox richTextBoxRemoved;
        private System.Windows.Forms.Button buttonTVCD;
        private System.Windows.Forms.Button buttonTamiz;
        private System.Windows.Forms.RichTextBox textBoxMsg;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonI4M;
        private System.Windows.Forms.Button buttonFetchImage;
    }
}

