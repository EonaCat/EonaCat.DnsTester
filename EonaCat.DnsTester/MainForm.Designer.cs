namespace EonaCat.DnsTester
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            RunTest = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            panel2 = new System.Windows.Forms.Panel();
            checkBox8 = new System.Windows.Forms.CheckBox();
            checkBox7 = new System.Windows.Forms.CheckBox();
            checkBox6 = new System.Windows.Forms.CheckBox();
            checkBox5 = new System.Windows.Forms.CheckBox();
            checkBox3 = new System.Windows.Forms.CheckBox();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label4 = new System.Windows.Forms.Label();
            StatusBox = new System.Windows.Forms.ListBox();
            ResultView = new System.Windows.Forms.ListView();
            Url = new System.Windows.Forms.ColumnHeader();
            DNS1DATA = new System.Windows.Forms.ColumnHeader();
            DNS1Performance = new System.Windows.Forms.ColumnHeader();
            DNS2DATA = new System.Windows.Forms.ColumnHeader();
            DNS2Performance = new System.Windows.Forms.ColumnHeader();
            panel1 = new System.Windows.Forms.Panel();
            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            chkDns2 = new System.Windows.Forms.CheckBox();
            chkDns1 = new System.Windows.Forms.CheckBox();
            CustomDns2 = new System.Windows.Forms.TextBox();
            CustomDns1 = new System.Windows.Forms.TextBox();
            lblCustom2 = new System.Windows.Forms.Label();
            lblCustom1 = new System.Windows.Forms.Label();
            UseCustomServers = new System.Windows.Forms.CheckBox();
            dnsList2 = new System.Windows.Forms.ComboBox();
            dnsList1 = new System.Windows.Forms.ComboBox();
            lblDns2 = new System.Windows.Forms.Label();
            lblDns1 = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            panel3 = new System.Windows.Forms.Panel();
            btnResolveHost = new System.Windows.Forms.Button();
            btnResolveIP = new System.Windows.Forms.Button();
            txtResolveHost = new System.Windows.Forms.TextBox();
            txtResolveIP = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabPage2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // RunTest
            // 
            RunTest.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
            RunTest.Location = new System.Drawing.Point(1068, 1539);
            RunTest.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            RunTest.Name = "RunTest";
            RunTest.Size = new System.Drawing.Size(435, 115);
            RunTest.TabIndex = 12;
            RunTest.Text = "Execute";
            RunTest.UseVisualStyleBackColor = false;
            RunTest.Click += RunTest_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(105, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(2259, 1517);
            tabControl1.TabIndex = 27;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.LightGray;
            tabPage1.Controls.Add(panel2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new System.Drawing.Point(10, 58);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(2239, 1449);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Dns tester";
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.Gold;
            panel2.Controls.Add(checkBox8);
            panel2.Controls.Add(checkBox7);
            panel2.Controls.Add(checkBox6);
            panel2.Controls.Add(checkBox5);
            panel2.Controls.Add(checkBox3);
            panel2.Controls.Add(checkBox2);
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(StatusBox);
            panel2.Controls.Add(ResultView);
            panel2.Location = new System.Drawing.Point(50, 510);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(2142, 905);
            panel2.TabIndex = 27;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Checked = true;
            checkBox8.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox8.Location = new System.Drawing.Point(1172, 38);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new System.Drawing.Size(143, 45);
            checkBox8.TabIndex = 70;
            checkBox8.Text = "Qwant";
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Checked = true;
            checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox7.Location = new System.Drawing.Point(1172, 135);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new System.Drawing.Size(150, 45);
            checkBox7.TabIndex = 69;
            checkBox7.Text = "Yandex";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Checked = true;
            checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox6.Location = new System.Drawing.Point(959, 135);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new System.Drawing.Size(181, 45);
            checkBox6.TabIndex = 68;
            checkBox6.Text = "StartPage";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Checked = true;
            checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox5.Location = new System.Drawing.Point(659, 135);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new System.Drawing.Size(244, 45);
            checkBox5.TabIndex = 67;
            checkBox5.Text = "WolframAlpha";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3.Location = new System.Drawing.Point(959, 42);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(154, 45);
            checkBox3.TabIndex = 65;
            checkBox3.Text = "Google";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox2.Location = new System.Drawing.Point(819, 41);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(115, 45);
            checkBox2.TabIndex = 64;
            checkBox2.Text = "Bing";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1.Location = new System.Drawing.Point(659, 42);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(138, 45);
            checkBox1.TabIndex = 63;
            checkBox1.Text = "Yahoo";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(56, 42);
            label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(472, 41);
            label4.TabIndex = 62;
            label4.Text = "Use searchengines for url retrieval:";
            label4.Visible = false;
            // 
            // StatusBox
            // 
            StatusBox.BackColor = System.Drawing.Color.OldLace;
            StatusBox.FormattingEnabled = true;
            StatusBox.HorizontalScrollbar = true;
            StatusBox.ItemHeight = 41;
            StatusBox.Location = new System.Drawing.Point(56, 741);
            StatusBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            StatusBox.Name = "StatusBox";
            StatusBox.Size = new System.Drawing.Size(2051, 127);
            StatusBox.TabIndex = 25;
            // 
            // ResultView
            // 
            ResultView.BackColor = System.Drawing.Color.OldLace;
            ResultView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { Url, DNS1DATA, DNS1Performance, DNS2DATA, DNS2Performance });
            ResultView.GridLines = true;
            ResultView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            ResultView.Location = new System.Drawing.Point(56, 214);
            ResultView.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            ResultView.MultiSelect = false;
            ResultView.Name = "ResultView";
            ResultView.Size = new System.Drawing.Size(2051, 487);
            ResultView.TabIndex = 24;
            ResultView.UseCompatibleStateImageBehavior = false;
            ResultView.View = System.Windows.Forms.View.Details;
            // 
            // Url
            // 
            Url.Text = "URL";
            Url.Width = 160;
            // 
            // DNS1DATA
            // 
            DNS1DATA.Text = "Data";
            DNS1DATA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            DNS1DATA.Width = 140;
            // 
            // DNS1Performance
            // 
            DNS1Performance.Text = "Performance";
            DNS1Performance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            DNS1Performance.Width = 120;
            // 
            // DNS2DATA
            // 
            DNS2DATA.Text = "Data";
            DNS2DATA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            DNS2DATA.Width = 140;
            // 
            // DNS2Performance
            // 
            DNS2Performance.Text = "Performance";
            DNS2Performance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            DNS2Performance.Width = 120;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Gold;
            panel1.Controls.Add(numericUpDown2);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(numericUpDown1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(chkDns2);
            panel1.Controls.Add(chkDns1);
            panel1.Controls.Add(CustomDns2);
            panel1.Controls.Add(CustomDns1);
            panel1.Controls.Add(lblCustom2);
            panel1.Controls.Add(lblCustom1);
            panel1.Controls.Add(UseCustomServers);
            panel1.Controls.Add(dnsList2);
            panel1.Controls.Add(dnsList1);
            panel1.Controls.Add(lblDns2);
            panel1.Controls.Add(lblDns1);
            panel1.Location = new System.Drawing.Point(50, 49);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(2142, 396);
            panel1.TabIndex = 26;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new System.Drawing.Point(804, 228);
            numericUpDown2.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new System.Drawing.Size(120, 47);
            numericUpDown2.TabIndex = 61;
            numericUpDown2.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(566, 228);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(201, 41);
            label2.TabIndex = 60;
            label2.Text = "Total Threads:";
            // 
            // comboBox1
            // 
            comboBox1.ForeColor = System.Drawing.Color.Black;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "A", "NS", "CNAME", "MX", "TXT" });
            comboBox1.Location = new System.Drawing.Point(1583, 71);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(302, 49);
            comboBox1.TabIndex = 59;
            comboBox1.Text = "Select record type";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(804, 147);
            numericUpDown1.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(120, 47);
            numericUpDown1.TabIndex = 58;
            numericUpDown1.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(566, 147);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(210, 41);
            label3.TabIndex = 57;
            label3.Text = "Total domains:";
            // 
            // chkDns2
            // 
            chkDns2.AutoSize = true;
            chkDns2.Checked = true;
            chkDns2.CheckState = System.Windows.Forms.CheckState.Checked;
            chkDns2.Location = new System.Drawing.Point(1071, 147);
            chkDns2.Name = "chkDns2";
            chkDns2.Size = new System.Drawing.Size(251, 45);
            chkDns2.TabIndex = 45;
            chkDns2.Text = "Test dns server";
            chkDns2.UseVisualStyleBackColor = true;
            chkDns2.CheckedChanged += chkDns2_CheckedChanged;
            // 
            // chkDns1
            // 
            chkDns1.AutoSize = true;
            chkDns1.Checked = true;
            chkDns1.CheckState = System.Windows.Forms.CheckState.Checked;
            chkDns1.Location = new System.Drawing.Point(46, 147);
            chkDns1.Name = "chkDns1";
            chkDns1.Size = new System.Drawing.Size(251, 45);
            chkDns1.TabIndex = 44;
            chkDns1.Text = "Test dns server";
            chkDns1.UseVisualStyleBackColor = true;
            chkDns1.CheckedChanged += chkDns1_CheckedChanged;
            // 
            // CustomDns2
            // 
            CustomDns2.Enabled = false;
            CustomDns2.Location = new System.Drawing.Point(1317, 289);
            CustomDns2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            CustomDns2.Name = "CustomDns2";
            CustomDns2.Size = new System.Drawing.Size(668, 47);
            CustomDns2.TabIndex = 43;
            CustomDns2.Visible = false;
            // 
            // CustomDns1
            // 
            CustomDns1.BackColor = System.Drawing.Color.White;
            CustomDns1.Enabled = false;
            CustomDns1.Location = new System.Drawing.Point(299, 291);
            CustomDns1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            CustomDns1.Name = "CustomDns1";
            CustomDns1.Size = new System.Drawing.Size(625, 47);
            CustomDns1.TabIndex = 42;
            CustomDns1.Visible = false;
            // 
            // lblCustom2
            // 
            lblCustom2.AutoSize = true;
            lblCustom2.Location = new System.Drawing.Point(1064, 293);
            lblCustom2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            lblCustom2.Name = "lblCustom2";
            lblCustom2.Size = new System.Drawing.Size(219, 41);
            lblCustom2.TabIndex = 41;
            lblCustom2.Text = "Custom Dns 2: ";
            lblCustom2.Visible = false;
            // 
            // lblCustom1
            // 
            lblCustom1.AutoSize = true;
            lblCustom1.Location = new System.Drawing.Point(46, 295);
            lblCustom1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            lblCustom1.Name = "lblCustom1";
            lblCustom1.Size = new System.Drawing.Size(219, 41);
            lblCustom1.TabIndex = 40;
            lblCustom1.Text = "Custom Dns 1: ";
            lblCustom1.Visible = false;
            // 
            // UseCustomServers
            // 
            UseCustomServers.AutoSize = true;
            UseCustomServers.Location = new System.Drawing.Point(1071, 224);
            UseCustomServers.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            UseCustomServers.Name = "UseCustomServers";
            UseCustomServers.Size = new System.Drawing.Size(262, 45);
            UseCustomServers.TabIndex = 39;
            UseCustomServers.Text = "Custom Servers";
            UseCustomServers.UseVisualStyleBackColor = true;
            UseCustomServers.CheckedChanged += UseCustomServers_CheckedChanged;
            // 
            // dnsList2
            // 
            dnsList2.FormattingEnabled = true;
            dnsList2.ItemHeight = 41;
            dnsList2.Location = new System.Drawing.Point(1181, 71);
            dnsList2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            dnsList2.Name = "dnsList2";
            dnsList2.Size = new System.Drawing.Size(320, 49);
            dnsList2.TabIndex = 38;
            dnsList2.Text = "Select Dns2";
            // 
            // dnsList1
            // 
            dnsList1.FormattingEnabled = true;
            dnsList1.ItemHeight = 41;
            dnsList1.Location = new System.Drawing.Point(168, 71);
            dnsList1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            dnsList1.Name = "dnsList1";
            dnsList1.Size = new System.Drawing.Size(451, 49);
            dnsList1.TabIndex = 37;
            dnsList1.Text = "Select Dns 1";
            // 
            // lblDns2
            // 
            lblDns2.AutoSize = true;
            lblDns2.Location = new System.Drawing.Point(1065, 79);
            lblDns2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            lblDns2.Name = "lblDns2";
            lblDns2.Size = new System.Drawing.Size(100, 41);
            lblDns2.TabIndex = 36;
            lblDns2.Text = "Dns 2:";
            // 
            // lblDns1
            // 
            lblDns1.AutoSize = true;
            lblDns1.Location = new System.Drawing.Point(46, 79);
            lblDns1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            lblDns1.Name = "lblDns1";
            lblDns1.Size = new System.Drawing.Size(100, 41);
            lblDns1.TabIndex = 35;
            lblDns1.Text = "Dns 1:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(panel3);
            tabPage2.Location = new System.Drawing.Point(10, 58);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(2239, 1449);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Resolve clients";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            panel3.Controls.Add(btnResolveHost);
            panel3.Controls.Add(btnResolveIP);
            panel3.Controls.Add(txtResolveHost);
            panel3.Controls.Add(txtResolveIP);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(label1);
            panel3.Location = new System.Drawing.Point(15, 12);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(2209, 175);
            panel3.TabIndex = 28;
            // 
            // btnResolveHost
            // 
            btnResolveHost.Location = new System.Drawing.Point(1583, 104);
            btnResolveHost.Name = "btnResolveHost";
            btnResolveHost.Size = new System.Drawing.Size(240, 51);
            btnResolveHost.TabIndex = 71;
            btnResolveHost.Text = "Resolve";
            btnResolveHost.UseVisualStyleBackColor = true;
            btnResolveHost.Click += btnResolveHost_Click;
            // 
            // btnResolveIP
            // 
            btnResolveIP.Location = new System.Drawing.Point(405, 104);
            btnResolveIP.Name = "btnResolveIP";
            btnResolveIP.Size = new System.Drawing.Size(240, 51);
            btnResolveIP.TabIndex = 70;
            btnResolveIP.Text = "Resolve";
            btnResolveIP.UseVisualStyleBackColor = true;
            btnResolveIP.Click += btnResolveIP_Click;
            // 
            // txtResolveHost
            // 
            txtResolveHost.Location = new System.Drawing.Point(1583, 41);
            txtResolveHost.Name = "txtResolveHost";
            txtResolveHost.Size = new System.Drawing.Size(551, 47);
            txtResolveHost.TabIndex = 69;
            // 
            // txtResolveIP
            // 
            txtResolveIP.Location = new System.Drawing.Point(405, 41);
            txtResolveIP.Name = "txtResolveIP";
            txtResolveIP.Size = new System.Drawing.Size(551, 47);
            txtResolveIP.TabIndex = 68;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(1267, 41);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(266, 41);
            label5.TabIndex = 67;
            label5.Text = "Resolve hostname:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(72, 41);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(270, 41);
            label1.TabIndex = 66;
            label1.Text = "Resolve ip address:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.SystemColors.MenuHighlight;
            ClientSize = new System.Drawing.Size(2517, 1693);
            Controls.Add(tabControl1);
            Controls.Add(RunTest);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "EonaCat.DnsTester";
            Load += TesterUI_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabPage2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button RunTest;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox StatusBox;
        private System.Windows.Forms.ListView ResultView;
        private System.Windows.Forms.ColumnHeader Url;
        private System.Windows.Forms.ColumnHeader DNS1DATA;
        private System.Windows.Forms.ColumnHeader DNS1Performance;
        private System.Windows.Forms.ColumnHeader DNS2DATA;
        private System.Windows.Forms.ColumnHeader DNS2Performance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkDns2;
        private System.Windows.Forms.CheckBox chkDns1;
        private System.Windows.Forms.TextBox CustomDns2;
        private System.Windows.Forms.TextBox CustomDns1;
        private System.Windows.Forms.Label lblCustom2;
        private System.Windows.Forms.Label lblCustom1;
        private System.Windows.Forms.CheckBox UseCustomServers;
        private System.Windows.Forms.ComboBox dnsList2;
        private System.Windows.Forms.ComboBox dnsList1;
        private System.Windows.Forms.Label lblDns2;
        private System.Windows.Forms.Label lblDns1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnResolveHost;
        private System.Windows.Forms.Button btnResolveIP;
        private System.Windows.Forms.TextBox txtResolveHost;
        private System.Windows.Forms.TextBox txtResolveIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
    }
}

