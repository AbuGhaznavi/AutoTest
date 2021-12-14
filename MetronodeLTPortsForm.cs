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
    public partial class MetronodeLTPortsForm : Form
    {
        // Keep a reference to the main form
        public add mainForm;
        public CheckBox checkBox1;
        public CheckBox checkBox2;
        public CheckBox checkBox3;
        public CheckBox checkBox4;
        public CheckBox checkBox5;
        public CheckBox checkBox6;
        public CheckBox checkBox7;
        public CheckBox checkBox8;
        public CheckBox checkBox9;
        public CheckBox checkBox10;
        public CheckBox checkBox11;
        public CheckBox checkBox12;

        public MetronodeLTPortsForm(add _mainForm)
        {
            InitializeComponent();
            checkBox1 = metronodeLTPort1;
            checkBox2 = metronodeLTPort2;
            checkBox3 = metronodeLTPort3;
            checkBox4 = metronodeLTPort4;
            checkBox5 = metronodeLTPort5;
            checkBox6 = metronodeLTPort6;
            checkBox7 = metronodeLTPort7;
            checkBox8 = metronodeLTPort8;
            checkBox9 = metronodeLTPort9;
            checkBox10 = metronodeLTPort10;
            checkBox11 = metronodeLTPort11;
            checkBox12 = metronodeLTPort12;
            this.mainForm = _mainForm;
            
            LoadCheckStates(mainForm.portStates);
            setCheckBoxUsability(mainForm.switchChoice);
        }

        public void LoadCheckStates(List<bool> portStates)
        {
            checkBox1.Checked = portStates[0];
            checkBox2.Checked = portStates[1];
            checkBox3.Checked = portStates[2];
            checkBox4.Checked = portStates[3];
            checkBox5.Checked = portStates[4];
            checkBox6.Checked = portStates[5];
            checkBox7.Checked = portStates[6];
            checkBox8.Checked = portStates[7];
            checkBox9.Checked = portStates[8];
            checkBox10.Checked = portStates[9];
            checkBox11.Checked = portStates[10];
            checkBox12.Checked = portStates[11];

        }

        public List<bool> GetCheckboxStates()
        {
            List<bool> states = new List<bool>();
            states.Add(checkBox1.Checked);
            states.Add(checkBox2.Checked);
            states.Add(checkBox3.Checked);
            states.Add(checkBox4.Checked);
            states.Add(checkBox5.Checked);
            states.Add(checkBox6.Checked);
            states.Add(checkBox7.Checked);
            states.Add(checkBox8.Checked);
            states.Add(checkBox9.Checked);
            states.Add(checkBox10.Checked);
            states.Add(checkBox11.Checked);
            states.Add(checkBox12.Checked);
            return states;
        }

        public void saveCheckBoxStates()
        {
            mainForm.savePortStates(GetCheckboxStates());
        }

        public void setCheckBoxUsability(String switchCode)
        {
            // If switch code has not been set yet
            if (switchCode == null)
            {
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox11.Enabled = false;
                checkBox12.Enabled = false;

                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                return;
            }
            if (switchCode.Equals("A"))
            {
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox11.Enabled = false;
                checkBox12.Enabled = false;

                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;

            } else if (switchCode.Equals("B"))
            {
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox11.Enabled = false;
                checkBox12.Enabled = false;

                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
            } else if (switchCode.Equals("C"))
            {
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
                checkBox8.Enabled = true;
                checkBox9.Enabled = true;
                checkBox10.Enabled = true;
                checkBox11.Enabled = true;
                checkBox12.Enabled = true;
            } else
            {

                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox11.Enabled = false;
                checkBox12.Enabled = false;

                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;

            }
        }


        private void ConfigureMetronodeLTPorts_Click(object sender, EventArgs e)
        {
            saveCheckBoxStates();

            Close();
            //this.Visible = false;
            
        }
      
        private void MetronodeLTPortsForm_Load(object sender, EventArgs e)
        {
            //string checkboxVal = Properties.Settings.Saved();
        }

        
    }
}
