using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
namespace AutoTest
{
    public partial class add : Form
    {

        public MetronodeLTPortsForm metronodeLTForm1;
        public List<bool> portStates;

        Output output = new Output();
        Connection connection = new Connection();
        private AutoResetEvent _reset1 = new AutoResetEvent(false);
        private AutoResetEvent _reset2 = new AutoResetEvent(false);
        private AutoResetEvent _reset3 = new AutoResetEvent(false);
        private AutoResetEvent _reset4 = new AutoResetEvent(false);
        private AutoResetEvent _reset5 = new AutoResetEvent(false);
        private AutoResetEvent _reset6 = new AutoResetEvent(false);
        private AutoResetEvent _reset7 = new AutoResetEvent(false);
        private AutoResetEvent _reset8 = new AutoResetEvent(false);
        private AutoResetEvent _reset9 = new AutoResetEvent(false);
        private AutoResetEvent _reset10 = new AutoResetEvent(false);
        private AutoResetEvent _reset11 = new AutoResetEvent(false);
        private AutoResetEvent _reset12 = new AutoResetEvent(false);
        public String switchChoice;

        // Set up abstract parser for strategy selection
        public AbstractAccedianParser selectedParser;

        // Global list of chars for trimming whitespace
        public readonly char[] trimChars = { ' ', '\n', '\t', '\r' };
        public add()
        {
            // Inititalize port states to be false
            portStates = new List<bool>();

            // Initialize parser to be usual Metronode 10GE parser
            selectedParser = new Metronode10GEParserInstance();
            for (int x = 0; x < 13; x++)
            {
                portStates.Add(false);
            }
            InitializeComponent();
        }

        //Checks if it is safe to provide the option for the user to start testing
        private void checkStart()
        {
            //There must be a connection made, a directory set
            if (output.checkDirectory() && connection.connected())
            {
                start.Enabled = true;
            }
            else
            {
                start.Enabled = false;
            }
        }

        // Check if it is safe to enable the "Pull First Option"
        private void checkPull()
        {
            if (output.checkDirectory() && connection.connected())
            {
                pullDownButton.Enabled = true;
            } else
            {
                pullDownButton.Enabled = false;
            }
        }

        public void savePortStates(List<bool> states)
        {
            this.portStates = states;
        }

        //Set the first status cell to have an untested red color
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[0].Style.BackColor = Color.Red;
        }

        //Handles creating the PO/SO directory folder
        //Checks if it is safe to start 
        private void directory_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                output.createDirectory(textBox1.Text);
                checkStart();
                checkPull();
            }
        }

        private void cleanDataGrid()
        {
            int width = dataGridView1.Rows.Count;
            int height = dataGridView1.Columns.Count;
            // Loop through and trim all the boxes

            for (int row_idx = 0; row_idx < width; row_idx++)
            {
                for (int col_idx = 0; col_idx < dataGridView1.Rows[row_idx].Cells.Count; col_idx++)
                {

                    // Interpret datagridview item as string
                    string currentItem = (string)dataGridView1.Rows[row_idx].Cells[col_idx].Value;

                    // Skip anything that cannot be interpreted as string
                    if (currentItem == null) continue;
                    string trimmedItem = currentItem.Trim(trimChars);
                    dataGridView1.Rows[row_idx].Cells[col_idx].Value = trimmedItem;
                }
            }
        }

        //Disables buttons so the user cannot mess stuff up while the tests are running
        //Starts the test and sets the base idx for the switch
        private void start_Click(object sender, EventArgs e)
        {
            consoleTestingToolStripMenuItem.Enabled = false;
            metronodeLTForm1.checkBox1.Enabled = false;
            metronodeLTForm1.checkBox2.Enabled = false;
            metronodeLTForm1.checkBox3.Enabled = false;
            metronodeLTForm1.checkBox4.Enabled = false;
            metronodeLTForm1.checkBox5.Enabled = false;
            metronodeLTForm1.checkBox6.Enabled = false;
            metronodeLTForm1.checkBox7.Enabled = false;
            metronodeLTForm1.checkBox8.Enabled = false;
            metronodeLTForm1.checkBox9.Enabled = false;
            metronodeLTForm1.checkBox10.Enabled = false;
            metronodeLTForm1.checkBox11.Enabled = false;
            metronodeLTForm1.checkBox12.Enabled = false;
            updateAll.Enabled = false;
            Retest.Enabled = false;
            delRow.Enabled = false;
            numericUpDown1.Enabled = false;
            stop.Enabled = true;
            start.Enabled = false;
            clear.Enabled = false;
            directory.Enabled = false;
            connect.Enabled = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            cons.Enabled = false;
            pullDownButton.Enabled = false;
            connection.idx = (int) numericUpDown1.Value;

            // Clean up the information displayed in data grid
            cleanDataGrid();

            startTest();
        }

        //Re-enables buttons and cancels all workers
        //Checks if it is still ok for the user to start the tests
        private void stop_Click(object sender, EventArgs e)
        {
            consoleTestingToolStripMenuItem.Enabled = true;
            metronodeLTForm1.checkBox1.Enabled = true;
            metronodeLTForm1.checkBox2.Enabled = true;
            metronodeLTForm1.checkBox3.Enabled = true;
            metronodeLTForm1.checkBox4.Enabled = true;
            metronodeLTForm1.checkBox5.Enabled = true;
            metronodeLTForm1.checkBox6.Enabled = true;
            metronodeLTForm1.checkBox7.Enabled = true;
            metronodeLTForm1.checkBox8.Enabled = true;
            metronodeLTForm1.checkBox9.Enabled = true;
            metronodeLTForm1.checkBox10.Enabled = true;
            metronodeLTForm1.checkBox11.Enabled = true;
            metronodeLTForm1.checkBox12.Enabled = true;
            updateAll.Enabled = true;
            Retest.Enabled = true;
            delRow.Enabled = true;
            numericUpDown1.Enabled = true;
            stop.Enabled = false;
            directory.Enabled = true;
            connect.Enabled = true;
            clear.Enabled = true;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            pullDownButton.Enabled = true;
            dataGridView1.ReadOnly = false;
            cons.Enabled = true;
            if (metronodeLTForm1.checkBox1.Checked)
            {
                sfp1Worker.CancelAsync();
                _reset1.WaitOne();
            }
            if (metronodeLTForm1.checkBox2.Checked)
            {
                sfp2Worker.CancelAsync();
                _reset2.WaitOne();
            }
            if (metronodeLTForm1.checkBox3.Checked)
            {
                sfp3Worker.CancelAsync();
                _reset3.WaitOne();
            }
            if (metronodeLTForm1.checkBox4.Checked)
            {
                sfp4Worker.CancelAsync();
                _reset4.WaitOne();
            }
            if (metronodeLTForm1.checkBox5.Checked)
            {
                sfp5Worker.CancelAsync();
                _reset5.WaitOne();
            }
            if (metronodeLTForm1.checkBox6.Checked)
            {
                sfp6Worker.CancelAsync();
                _reset6.WaitOne();
            }
            if (metronodeLTForm1.checkBox7.Checked)
            {
                sfp7Worker.CancelAsync();
                _reset7.WaitOne();
            }
            if (metronodeLTForm1.checkBox8.Checked)
            {
                sfp8Worker.CancelAsync();
                _reset8.WaitOne();
            }
            if (metronodeLTForm1.checkBox9.Checked)
            {
                sfp9Worker.CancelAsync();
                _reset9.WaitOne();
            }
            if (metronodeLTForm1.checkBox10.Checked)
            {
                sfp10Worker.CancelAsync();
                _reset10.WaitOne();
            }
            if (metronodeLTForm1.checkBox11.Checked)
            {
                sfp11Worker.CancelAsync();
                _reset11.WaitOne();
            }
            if (metronodeLTForm1.checkBox12.Checked)
            {
                sfp12Worker.CancelAsync();
                _reset12.WaitOne();
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Style.BackColor == Color.Orange)
                {
                    row.Cells[0].Style.BackColor = Color.Red;
                }
            }
            checkStart();
            checkPull();
        }

        //Handles setting the base test report folder
        private void setTestReportFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            Console.WriteLine(folderBrowserDialog1.SelectedPath);
            output.setDirectory(folderBrowserDialog1.SelectedPath);
        }

        //At some point for adding a text based report that can populate the table and picture box
        private void openTestLogToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Exit button in file menu to close the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Connect to the server specified in the text boxes
        //Render the response to show the user
        //Checks if it is safe to start 
        private void connect_Click(object sender, EventArgs e)
        {
            connection.login(ip.Text, user.Text, pass.Text);
            String s = connection.getList();
            pictureBox1.Image = new HtmlToBitmapConverter().Render(s, new Size(900, 1200));
            checkStart();
            checkPull();
        }

        //Checks what ports are selected and starts the corresponding workers/threads
        private void startTest()
        {
            if (portStates[0])
            {
                sfp1Worker.RunWorkerAsync();
            }
            if (portStates[1])
            {
                sfp2Worker.RunWorkerAsync();
            }
            if (portStates[2])
            {
                sfp3Worker.RunWorkerAsync();
            }
            if (portStates[3])
            {
                sfp4Worker.RunWorkerAsync();
            }
            if (portStates[4])
            {
                sfp5Worker.RunWorkerAsync();
            }
            if (portStates[5])
            {
                sfp6Worker.RunWorkerAsync();
            }
            if (portStates[6])
            {
                sfp7Worker.RunWorkerAsync();
            }
            if (portStates[7])
            {
                sfp8Worker.RunWorkerAsync();
            }
            if (portStates[8])
            {
                sfp9Worker.RunWorkerAsync();
            }
            if (portStates[9])
            {
                sfp10Worker.RunWorkerAsync();
            }
            if (portStates[10])
            {
                sfp11Worker.RunWorkerAsync();
            }
            if (portStates[11])
            {
                sfp12Worker.RunWorkerAsync();
            }
        }

        private void sfp1Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort1();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    } else if (oldrow != -1)
                        {
                            dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                            oldrow = -1;
                        }
                } else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp2Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp2Worker.CancellationPending)
            {
                html = connection.getPort2();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset2.Set();
        }

        private void sfp3Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp3Worker.CancellationPending)
            {
                html = connection.getPort3();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset3.Set();
        }

        private void sfp4Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp4Worker.CancellationPending)
            {
                html = connection.getPort4();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset4.Set();
        }

        private void sfp5Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort5();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp6Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort6();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp7Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort7();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp8Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort8();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp9Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort9();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp10Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort10();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp11Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort11();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }

        private void sfp12Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int temp = 0;
            int newtemp = 0;
            int vcc = 0;
            int newvcc = 0;
            int bias = 0;
            int newbias = 0;
            float tx = 0;
            float newtx = 0;
            float rx = 0;
            float newrx = 0;
            String html, serial;
            int row, oldrow = -1;
            while (!Grid.done(dataGridView1) && !sfp1Worker.CancellationPending)
            {
                html = connection.getPort12();
                newtemp = selectedParser.getTemp(html);
                newvcc = selectedParser.getVcc(html);
                newbias = selectedParser.getBias(html);
                newtx = selectedParser.getTx(html);
                newrx = selectedParser.getRx(html);
                if (selectedParser.present(html))
                {
                    serial = selectedParser.getSerial(html);
                    if (serial.Length >= 1)
                    {
                        row = Grid.findSerial(dataGridView1, serial);
                        if (oldrow == -1)
                        {
                            if (row != -1)
                            {
                                if (!Grid.tested(dataGridView1, row))
                                {
                                    dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                    oldrow = row;
                                    if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = selectedParser.getPart(html);
                                        String vendor = selectedParser.getVendor(html);
                                        String rev = selectedParser.getRev(html);
                                        String spd = selectedParser.getSpd(html);
                                        String wl = selectedParser.getWL(html);
                                        if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                        {
                                            Image image = null;
                                            Thread renderThread = new Thread(() => {
                                                image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                            });
                                            renderThread.SetApartmentState(ApartmentState.STA);
                                            renderThread.Start();
                                            renderThread.Join();
                                            Image saveImage = (Image)image.Clone();
                                            pictureBox1.Image = image;
                                            output.saveReport(saveImage, partnum, serial);
                                            dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                            oldrow = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (row == oldrow)
                            {
                                dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Orange;
                                oldrow = row;
                                if (selectedParser.checkTemp(html) && selectedParser.checkVcc(html) && selectedParser.checkBias(html) && selectedParser.checkTx(html) && selectedParser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = selectedParser.getPart(html);
                                    String vendor = selectedParser.getVendor(html);
                                    String rev = selectedParser.getRev(html);
                                    String spd = selectedParser.getSpd(html);
                                    String wl = selectedParser.getWL(html);
                                    if (dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))
                                    {
                                        Image image = null;
                                        Thread renderThread = new Thread(() => {
                                            image = HtmlToBitmapConverter.start(html, new Size(900, 1200));
                                        });
                                        renderThread.SetApartmentState(ApartmentState.STA);
                                        renderThread.Start();
                                        renderThread.Join();
                                        Image saveImage = (Image)image.Clone();
                                        pictureBox1.Image = image;
                                        output.saveReport(saveImage, partnum, serial);
                                        dataGridView1.Rows[row].Cells[0].Style.BackColor = Color.Green;
                                        oldrow = -1;
                                    }
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                                oldrow = -1;
                            }
                        }
                    }
                    else if (oldrow != -1)
                    {
                        dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                        oldrow = -1;
                    }
                }
                else if (oldrow != -1)
                {
                    dataGridView1.Rows[oldrow].Cells[0].Style.BackColor = Color.Red;
                    oldrow = -1;
                }
                temp = newtemp;
                vcc = newvcc;
                bias = newbias;
                tx = newtx;
                rx = newrx;
            }
            _reset1.Set();
        }



        //Handles clicking the consecutive add button
        private void cons_Click(object sender, EventArgs e)
        {
            if (serialEnd.Text.Length >= 11 && dataGridView1.RowCount > 0 && dataGridView1.SelectedCells.Count > 0) Grid.addCons(dataGridView1, serialEnd.Text.ToString(), dataGridView1.SelectedCells[0].RowIndex);
        }

        //When rows are added they need to have an untested red state
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for(int i = 0; i < e.RowCount; i++)
            {
                dataGridView1.Rows[e.RowIndex + i].Cells[0].Style.BackColor = Color.Red;
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version:\nBeta 1.8.13.19", "About");
        }

        private void consoleTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConsoleTest console = new ConsoleTest();
            console.FormClosed += (s, args) => this.Show();
            console.Show();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void Retest_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell item in dataGridView1.SelectedCells)
            {
                item.OwningRow.Cells[0].Style.BackColor = Color.Red;
            }
        }

        private void delRow_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            if (i < dataGridView1.RowCount - 1)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }

        private void updateAll_Click(object sender, EventArgs e)
        {
            int ri = dataGridView1.SelectedCells[0].RowIndex;
            if (ri < dataGridView1.RowCount - 1)
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    if (dataGridView1.SelectedCells[0].Value != null)
                    {
                        string v = dataGridView1.SelectedCells[0].Value.ToString();
                        int i = dataGridView1.SelectedCells[0].ColumnIndex;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Index != dataGridView1.RowCount - 1)
                            {
                                row.Cells[i].Value = v;
                            }
                        }
                    }
                }
            }
        }

  

        private void configurePorts_Click(object sender, EventArgs e)
        {
            
            metronodeLTForm1 = new MetronodeLTPortsForm(this);
            
            metronodeLTForm1.Show();
        }

        private void switchType_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Set port usabiility and parser
           if (switchTypeMethod.Text.Equals("Switch A")) {
                switchChoice = "A";
                selectedParser = new Metronode10GEParserInstance();
                numericUpDown1.Value = 3;
            }
           else if (switchTypeMethod.Text.Equals("Switch B")) {
                switchChoice = "B";
                selectedParser = new Metronode10GEParserInstance();
                numericUpDown1.Value = 3;
            }
           else if (switchTypeMethod.Text.Equals("Switch C")) {
                switchChoice = "C";
                selectedParser = new MicronodeLTParserInstance();
                numericUpDown1.Value = 1;
            }
           else {
                switchChoice = "A";
                selectedParser = new Metronode10GEParserInstance();
                numericUpDown1.Value = 3;
            }
           // Null check the form before injecting the switch choice
            if (metronodeLTForm1 != null)
            {
                metronodeLTForm1.setCheckBoxUsability(switchChoice);
            }
        }

        // Pull from the first port
        private void pullFirst()
        {
            // WILL ALWAYS USE PORT ONE
            String html = connection.getPort1();
            String partnum = selectedParser.getPart(html);
            String vendor = selectedParser.getVendor(html);
            String rev = selectedParser.getRev(html);
            String spd = selectedParser.getSpd(html);
            String wl = selectedParser.getWL(html);
            String serial = selectedParser.getSerial(html);
            // dataGridView1.Rows[row].Cells[2].Value.Equals(partnum) && dataGridView1.Rows[row].Cells[3].Value.Equals(vendor) && dataGridView1.Rows[row].Cells[4].Value.Equals(rev) && dataGridView1.Rows[row].Cells[5].Value.Equals(spd) && dataGridView1.Rows[row].Cells[6].Value.Equals(wl))

            dataGridView1.Rows[0].Cells[1].Value = serial;
            dataGridView1.Rows[0].Cells[2].Value = partnum;
            dataGridView1.Rows[0].Cells[3].Value = vendor;
            dataGridView1.Rows[0].Cells[4].Value = rev;
            dataGridView1.Rows[0].Cells[5].Value = spd;
            dataGridView1.Rows[0].Cells[6].Value = wl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pullFirst();
        }
    }
}
