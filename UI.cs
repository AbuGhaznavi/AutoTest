using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class add : Form
    {
        Output output = new Output();
        Connection connection = new Connection();
        private AutoResetEvent _reset1 = new AutoResetEvent(false);
        private AutoResetEvent _reset2 = new AutoResetEvent(false);
        private AutoResetEvent _reset3 = new AutoResetEvent(false);
        private AutoResetEvent _reset4 = new AutoResetEvent(false);

        public add()
        {
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
            }
        }

        //Disables buttons so the user cannot mess stuff up while the tests are running
        //Starts the test and sets the base idx for the switch
        private void start_Click(object sender, EventArgs e)
        {
            consoleTestingToolStripMenuItem.Enabled = false;
            port1ck.Enabled = false;
            port2ck.Enabled = false;
            port3ck.Enabled = false;
            port4ck.Enabled = false;
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
            connection.idx = (int) numericUpDown1.Value;
            startTest();
        }

        //Re-enables buttons and cancels all workers
        //Checks if it is still ok for the user to start the tests
        private void stop_Click(object sender, EventArgs e)
        {
            consoleTestingToolStripMenuItem.Enabled = true;
            port1ck.Enabled = true;
            port2ck.Enabled = true;
            port3ck.Enabled = true;
            port4ck.Enabled = true;
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
            dataGridView1.ReadOnly = false;
            cons.Enabled = true;
            if (port1ck.Checked)
            {
                sfp1Worker.CancelAsync();
                _reset1.WaitOne();
            }
            if (port2ck.Checked)
            {
                sfp2Worker.CancelAsync();
                _reset2.WaitOne();
            }
            if (port3ck.Checked)
            {
                sfp3Worker.CancelAsync();
                _reset3.WaitOne();
            }
            if (port4ck.Checked)
            {
                sfp4Worker.CancelAsync();
                _reset4.WaitOne();
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Style.BackColor == Color.Orange)
                {
                    row.Cells[0].Style.BackColor = Color.Red;
                }
            }
            checkStart();
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
        }

        //Checks what ports are selected and starts the corresponding workers/threads
        private void startTest()
        {
            if (port1ck.Checked)
            {
                sfp1Worker.RunWorkerAsync();
            }
            if (port2ck.Checked)
            {
                sfp2Worker.RunWorkerAsync();
            }
            if (port3ck.Checked)
            {
                sfp3Worker.RunWorkerAsync();
            }
            if (port4ck.Checked)
            {
                sfp4Worker.RunWorkerAsync();
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
                newtemp = Parser.getTemp(html);
                newvcc = Parser.getVcc(html);
                newbias = Parser.getBias(html);
                newtx = Parser.getTx(html);
                newrx = Parser.getRx(html);
                if (Parser.present(html))
                {
                    serial = Parser.getSerial(html);
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
                                    if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = Parser.getPart(html);
                                        String vendor = Parser.getVendor(html);
                                        String rev = Parser.getRev(html);
                                        String spd = Parser.getSpd(html);
                                        String wl = Parser.getWL(html);
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
                                if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = Parser.getPart(html);
                                    String vendor = Parser.getVendor(html);
                                    String rev = Parser.getRev(html);
                                    String spd = Parser.getSpd(html);
                                    String wl = Parser.getWL(html);
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
                newtemp = Parser.getTemp(html);
                newvcc = Parser.getVcc(html);
                newbias = Parser.getBias(html);
                newtx = Parser.getTx(html);
                newrx = Parser.getRx(html);
                if (Parser.present(html))
                {
                    serial = Parser.getSerial(html);
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
                                    if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = Parser.getPart(html);
                                        String vendor = Parser.getVendor(html);
                                        String rev = Parser.getRev(html);
                                        String spd = Parser.getSpd(html);
                                        String wl = Parser.getWL(html);
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
                                if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = Parser.getPart(html);
                                    String vendor = Parser.getVendor(html);
                                    String rev = Parser.getRev(html);
                                    String spd = Parser.getSpd(html);
                                    String wl = Parser.getWL(html);
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
                newtemp = Parser.getTemp(html);
                newvcc = Parser.getVcc(html);
                newbias = Parser.getBias(html);
                newtx = Parser.getTx(html);
                newrx = Parser.getRx(html);
                if (Parser.present(html))
                {
                    serial = Parser.getSerial(html);
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
                                    if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = Parser.getPart(html);
                                        String vendor = Parser.getVendor(html);
                                        String rev = Parser.getRev(html);
                                        String spd = Parser.getSpd(html);
                                        String wl = Parser.getWL(html);
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
                                if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = Parser.getPart(html);
                                    String vendor = Parser.getVendor(html);
                                    String rev = Parser.getRev(html);
                                    String spd = Parser.getSpd(html);
                                    String wl = Parser.getWL(html);
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
                newtemp = Parser.getTemp(html);
                newvcc = Parser.getVcc(html);
                newbias = Parser.getBias(html);
                newtx = Parser.getTx(html);
                newrx = Parser.getRx(html);
                if (Parser.present(html))
                {
                    serial = Parser.getSerial(html);
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
                                    if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                    {
                                        String partnum = Parser.getPart(html);
                                        String vendor = Parser.getVendor(html);
                                        String rev = Parser.getRev(html);
                                        String spd = Parser.getSpd(html);
                                        String wl = Parser.getWL(html);
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
                                if (Parser.checkTemp(html) && Parser.checkVcc(html) && Parser.checkBias(html) && Parser.checkTx(html) && Parser.checkRx(html) && (temp != newtemp || vcc != newvcc || bias != newbias || tx != newtx || rx != newrx))
                                {
                                    String partnum = Parser.getPart(html);
                                    String vendor = Parser.getVendor(html);
                                    String rev = Parser.getRev(html);
                                    String spd = Parser.getSpd(html);
                                    String wl = Parser.getWL(html);
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
    }
}
