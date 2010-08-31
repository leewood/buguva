using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Operacine_sistema
{
    public partial class ChangePriority : Form
    {
        public int Value
        {
            get
            {
                return decimal.ToInt32(numericUpDown1.Value);
            }
            set
            {
                numericUpDown1.Value = value;
            }
        }


        public ChangePriority(int int_initialValue)
        {
            InitializeComponent();
            Value = int_initialValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
