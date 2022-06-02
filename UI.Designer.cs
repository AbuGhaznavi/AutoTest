namespace AutoTest
{
    partial class add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(add));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTestReportFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTestLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.directory = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.connect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ip = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.sfp1Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp2Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp3Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp4Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp5Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp6Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp7Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp8Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp9Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp10Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp11Worker = new System.ComponentModel.BackgroundWorker();
            this.sfp12Worker = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Wavelength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.serialEnd = new System.Windows.Forms.TextBox();
            this.cons = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.Retest = new System.Windows.Forms.Button();
            this.delRow = new System.Windows.Forms.Button();
            this.updateAll = new System.Windows.Forms.Button();
            this.configurePorts = new System.Windows.Forms.Button();
            this.switchTypeMethod = new System.Windows.Forms.ComboBox();
            this.pullDownButton = new System.Windows.Forms.Button();
            this.copper = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setTestReportFolderToolStripMenuItem,
            this.consoleTestingToolStripMenuItem,
            this.openTestLogToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // setTestReportFolderToolStripMenuItem
            // 
            this.setTestReportFolderToolStripMenuItem.Name = "setTestReportFolderToolStripMenuItem";
            this.setTestReportFolderToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setTestReportFolderToolStripMenuItem.Text = "Set Test Report Folder";
            this.setTestReportFolderToolStripMenuItem.Click += new System.EventHandler(this.setTestReportFolderToolStripMenuItem_Click);
            // 
            // consoleTestingToolStripMenuItem
            // 
            this.consoleTestingToolStripMenuItem.Enabled = false;
            this.consoleTestingToolStripMenuItem.Name = "consoleTestingToolStripMenuItem";
            this.consoleTestingToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.consoleTestingToolStripMenuItem.Text = "Console Testing";
            this.consoleTestingToolStripMenuItem.Click += new System.EventHandler(this.consoleTestingToolStripMenuItem_Click);
            // 
            // openTestLogToolStripMenuItem
            // 
            this.openTestLogToolStripMenuItem.Enabled = false;
            this.openTestLogToolStripMenuItem.Name = "openTestLogToolStripMenuItem";
            this.openTestLogToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openTestLogToolStripMenuItem.Text = "Open Test Log";
            this.openTestLogToolStripMenuItem.Click += new System.EventHandler(this.openTestLogToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(355, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(282, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Save Folder:";
            // 
            // directory
            // 
            this.directory.Location = new System.Drawing.Point(461, 14);
            this.directory.Name = "directory";
            this.directory.Size = new System.Drawing.Size(101, 23);
            this.directory.TabIndex = 4;
            this.directory.Text = "Create Directory";
            this.directory.UseVisualStyleBackColor = true;
            this.directory.Click += new System.EventHandler(this.directory_Click);
            // 
            // start
            // 
            this.start.Enabled = false;
            this.start.ForeColor = System.Drawing.SystemColors.ControlText;
            this.start.Location = new System.Drawing.Point(763, 40);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(103, 23);
            this.start.TabIndex = 5;
            this.start.Text = "Start Test";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // stop
            // 
            this.stop.Enabled = false;
            this.stop.Location = new System.Drawing.Point(763, 66);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(103, 23);
            this.stop.TabIndex = 6;
            this.stop.Text = "Stop Test";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(527, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(469, 576);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(763, 14);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(103, 23);
            this.connect.TabIndex = 8;
            this.connect.Text = "Connect to Switch";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(568, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Switch Address:";
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(657, 16);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(100, 20);
            this.ip.TabIndex = 10;
            this.ip.Text = "192.168.1.3";
            this.ip.TextChanged += new System.EventHandler(this.ip_TextChanged);
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(657, 42);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(100, 20);
            this.user.TabIndex = 11;
            this.user.Text = "admin";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(657, 68);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(100, 20);
            this.pass.TabIndex = 12;
            this.pass.Text = "admin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(588, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "User Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(595, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Password:";
            // 
            // sfp1Worker
            // 
            this.sfp1Worker.WorkerSupportsCancellation = true;
            this.sfp1Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp1Worker_DoWork);
            // 
            // sfp2Worker
            // 
            this.sfp2Worker.WorkerSupportsCancellation = true;
            this.sfp2Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp2Worker_DoWork);
            // 
            // sfp3Worker
            // 
            this.sfp3Worker.WorkerSupportsCancellation = true;
            this.sfp3Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp3Worker_DoWork);
            // 
            // sfp4Worker
            // 
            this.sfp4Worker.WorkerSupportsCancellation = true;
            this.sfp4Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp4Worker_DoWork);
            // 
            // sfp5Worker
            // 
            this.sfp5Worker.WorkerSupportsCancellation = true;
            this.sfp5Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp5Worker_DoWork);
            // 
            // sfp6Worker
            // 
            this.sfp6Worker.WorkerSupportsCancellation = true;
            this.sfp6Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp6Worker_DoWork);
            // 
            // sfp7Worker
            // 
            this.sfp7Worker.WorkerSupportsCancellation = true;
            this.sfp7Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp7Worker_DoWork);
            // 
            // sfp8Worker
            // 
            this.sfp8Worker.WorkerSupportsCancellation = true;
            this.sfp8Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp8Worker_DoWork);
            // 
            // sfp9Worker
            // 
            this.sfp9Worker.WorkerSupportsCancellation = true;
            this.sfp9Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp9Worker_DoWork);
            // 
            // sfp10Worker
            // 
            this.sfp10Worker.WorkerSupportsCancellation = true;
            this.sfp10Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp10Worker_DoWork);
            // 
            // sfp11Worker
            // 
            this.sfp11Worker.WorkerSupportsCancellation = true;
            this.sfp11Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp11Worker_DoWork);
            // 
            // sfp12Worker
            // 
            this.sfp12Worker.WorkerSupportsCancellation = true;
            this.sfp12Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sfp12Worker_DoWork);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(878, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Start \"idx\":";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(942, 73);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(30, 20);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.Serial,
            this.PartNum,
            this.Vendor,
            this.Rev,
            this.Speed,
            this.Wavelength});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(12, 100);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.Size = new System.Drawing.Size(509, 576);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // Status
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.Status.DefaultCellStyle = dataGridViewCellStyle1;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Status.ToolTipText = "Red: not tested\nOrange: testing\nGreen: tested";
            this.Status.Width = 40;
            // 
            // Serial
            // 
            this.Serial.HeaderText = "Serial";
            this.Serial.Name = "Serial";
            // 
            // PartNum
            // 
            this.PartNum.HeaderText = "Part Number";
            this.PartNum.Name = "PartNum";
            // 
            // Vendor
            // 
            this.Vendor.HeaderText = "Vendor";
            this.Vendor.Name = "Vendor";
            this.Vendor.Width = 65;
            // 
            // Rev
            // 
            this.Rev.HeaderText = "Rev";
            this.Rev.Name = "Rev";
            this.Rev.Width = 40;
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Speed";
            this.Speed.Items.AddRange(new object[] {
            "100M",
            "1G",
            "10G",
            "155M",
            "622M",
            "2.5G",
            "8G",
            "4G",
            "2.5G/1.2G",
            "2x 1G",
            "1.25G",
            "25G",
            "100G",
            "40G",
            "100M-2.7G",
            "2.7G",
            "1000 BASE-T",
            "10/100/1000 BASE-T",
            "10GBASE-T",
            "1G/2G",
            "---",
            "N/A"});
            this.Speed.Name = "Speed";
            this.Speed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Speed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Speed.Width = 55;
            // 
            // Wavelength
            // 
            this.Wavelength.HeaderText = "Wavelength";
            this.Wavelength.Name = "Wavelength";
            this.Wavelength.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Wavelength.ToolTipText = "Must include \"nm\"";
            this.Wavelength.Width = 75;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(291, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "End Serial:";
            // 
            // serialEnd
            // 
            this.serialEnd.Location = new System.Drawing.Point(355, 73);
            this.serialEnd.Name = "serialEnd";
            this.serialEnd.Size = new System.Drawing.Size(100, 20);
            this.serialEnd.TabIndex = 26;
            // 
            // cons
            // 
            this.cons.Location = new System.Drawing.Point(461, 71);
            this.cons.Name = "cons";
            this.cons.Size = new System.Drawing.Size(101, 23);
            this.cons.TabIndex = 27;
            this.cons.Text = "Add Consecutive";
            this.cons.UseVisualStyleBackColor = true;
            this.cons.Click += new System.EventHandler(this.cons_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label6.Location = new System.Drawing.Point(12, 679);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Beta 1.8.13.19";
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(15, 71);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 30;
            this.clear.Text = " Clear All";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Retest
            // 
            this.Retest.Location = new System.Drawing.Point(15, 42);
            this.Retest.Name = "Retest";
            this.Retest.Size = new System.Drawing.Size(75, 23);
            this.Retest.TabIndex = 31;
            this.Retest.Text = "Retest";
            this.Retest.UseVisualStyleBackColor = true;
            this.Retest.Click += new System.EventHandler(this.Retest_Click);
            // 
            // delRow
            // 
            this.delRow.Location = new System.Drawing.Point(96, 42);
            this.delRow.Name = "delRow";
            this.delRow.Size = new System.Drawing.Size(75, 23);
            this.delRow.TabIndex = 32;
            this.delRow.Text = "Delete Row";
            this.delRow.UseVisualStyleBackColor = true;
            this.delRow.Click += new System.EventHandler(this.delRow_Click);
            // 
            // updateAll
            // 
            this.updateAll.Location = new System.Drawing.Point(96, 71);
            this.updateAll.Name = "updateAll";
            this.updateAll.Size = new System.Drawing.Size(75, 23);
            this.updateAll.TabIndex = 33;
            this.updateAll.Text = "Update All";
            this.updateAll.UseVisualStyleBackColor = true;
            this.updateAll.Click += new System.EventHandler(this.updateAll_Click);
            // 
            // configurePorts
            // 
            this.configurePorts.Location = new System.Drawing.Point(872, 14);
            this.configurePorts.Name = "configurePorts";
            this.configurePorts.Size = new System.Drawing.Size(115, 23);
            this.configurePorts.TabIndex = 35;
            this.configurePorts.Text = "Configure Ports";
            this.configurePorts.UseVisualStyleBackColor = true;
            this.configurePorts.Click += new System.EventHandler(this.configurePorts_Click);
            // 
            // switchTypeMethod
            // 
            this.switchTypeMethod.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.switchTypeMethod.FormattingEnabled = true;
            this.switchTypeMethod.Items.AddRange(new object[] {
            "Switch A",
            "Switch B",
            "Switch C"});
            this.switchTypeMethod.Location = new System.Drawing.Point(873, 41);
            this.switchTypeMethod.Name = "switchTypeMethod";
            this.switchTypeMethod.Size = new System.Drawing.Size(114, 21);
            this.switchTypeMethod.TabIndex = 36;
            this.switchTypeMethod.Text = "Choose Switch";
            this.switchTypeMethod.SelectedIndexChanged += new System.EventHandler(this.switchType_SelectedIndexChanged);
            // 
            // pullDownButton
            // 
            this.pullDownButton.AccessibleName = "pullDownButton";
            this.pullDownButton.Enabled = false;
            this.pullDownButton.Location = new System.Drawing.Point(177, 71);
            this.pullDownButton.Name = "pullDownButton";
            this.pullDownButton.Size = new System.Drawing.Size(75, 23);
            this.pullDownButton.TabIndex = 37;
            this.pullDownButton.Text = "Pull First";
            this.pullDownButton.UseVisualStyleBackColor = true;
            this.pullDownButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // copper
            // 
            this.copper.AutoSize = true;
            this.copper.Location = new System.Drawing.Point(192, 44);
            this.copper.Name = "copper";
            this.copper.Size = new System.Drawing.Size(60, 17);
            this.copper.TabIndex = 38;
            this.copper.Text = "Copper";
            this.copper.UseVisualStyleBackColor = true;
            this.copper.CheckedChanged += new System.EventHandler(this.copper_CheckedChanged);
            // 
            // add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 695);
            this.Controls.Add(this.copper);
            this.Controls.Add(this.pullDownButton);
            this.Controls.Add(this.switchTypeMethod);
            this.Controls.Add(this.configurePorts);
            this.Controls.Add(this.updateAll);
            this.Controls.Add(this.delRow);
            this.Controls.Add(this.Retest);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cons);
            this.Controls.Add(this.serialEnd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.user);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.start);
            this.Controls.Add(this.directory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sandstone AutoTest";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTestReportFolderToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button directory;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem openTestLogToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker sfp1Worker;
        private System.ComponentModel.BackgroundWorker sfp2Worker;
        private System.ComponentModel.BackgroundWorker sfp3Worker;
        private System.ComponentModel.BackgroundWorker sfp4Worker;
        private System.ComponentModel.BackgroundWorker sfp5Worker;
        private System.ComponentModel.BackgroundWorker sfp6Worker;
        private System.ComponentModel.BackgroundWorker sfp7Worker;
        private System.ComponentModel.BackgroundWorker sfp8Worker;
        private System.ComponentModel.BackgroundWorker sfp9Worker;
        private System.ComponentModel.BackgroundWorker sfp10Worker;
        private System.ComponentModel.BackgroundWorker sfp11Worker;
        private System.ComponentModel.BackgroundWorker sfp12Worker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox serialEnd;
        private System.Windows.Forms.Button cons;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleTestingToolStripMenuItem;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serial;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rev;
        private System.Windows.Forms.DataGridViewComboBoxColumn Speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wavelength;
        private System.Windows.Forms.Button Retest;
        private System.Windows.Forms.Button delRow;
        private System.Windows.Forms.Button updateAll;
        private System.Windows.Forms.Button configurePorts;
        private System.Windows.Forms.ComboBox switchTypeMethod;
        private System.Windows.Forms.Button pullDownButton;
        private System.Windows.Forms.CheckBox copper;
    }
}

