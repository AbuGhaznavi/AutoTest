using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class ConsoleTest : Form
    {
        public ConsoleTest()
        {
            InitializeComponent();
        }

        private void ConsoleTest_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comList.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            this.Close();
        }

        private void accedianTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version:\nBeta 0.4.12.19", "About");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                ip.Enabled = true;
                comList.Enabled = false;
            } else if (comboBox1.SelectedIndex == 1)
            {
                ip.Enabled = false;
                comList.Enabled = true;
            }
        }

        private void comList_Click(object sender, EventArgs e)
        {
            comList.Items.Clear();
            comList.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        }

        private void connect_Click(object sender, EventArgs e)
        {
            serialPort1.BaudRate = 115200;
            serialPort1.PortName = comList.Text;
            serialPort1.Open();

            //System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            //client.Connect("towel.blinkenlights.nl", 23);
            //System.IO.Stream stream = client.GetStream();
            //byte[] data = new Byte[1024];

            //// String to store the response ASCII representation.
            //String responseData = String.Empty;

            //// Read the first batch of the TcpServer response bytes.
            //Int32 bytes = stream.Read(data, 0, data.Length);
            //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //Console.Write(responseData);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            String s = serialPort1.ReadLine();
           
            Console.Write(s);
        }

        private void serialWrite_Click(object sender, EventArgs e)
        {
            serialPort1.Write(textBox2.Text);
            textBox2.Text = "";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                serialWrite.PerformClick();
            }
        }
    }
}
