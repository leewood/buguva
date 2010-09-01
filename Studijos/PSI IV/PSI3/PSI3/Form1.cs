using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSI3
{
    public partial class Form1 : Form
    {
        AbstractFactory factory;
        MachineInterface machine;
        BlackBoxInterface box;
        MachineBirthDocument document;

        public Form1(string[] arg)
        {
            InitializeComponent();
            factory = AbstractFactory.CreateFactory(arg[0]);
            label4.Text = arg[0];
            machine = factory.CreateMachine(bool.Parse(arg[3]));
            checkBox2.Checked = bool.Parse(arg[3]);
            box = factory.CreateBlackBox(int.Parse(arg[1]));
            numericUpDown1.Value = int.Parse(arg[1]);
            machine.SetBlackBox(box);
            document = factory.CreateDocument(arg[2]);
            textBox2.Text = arg[2];
            machine.SetDocument(document);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            machine.Use(textBox1.Text);
            richTextBox1.Clear();
            richTextBox1.Text = box.GetLog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            machine.Crash(textBox1.Text);
            richTextBox1.Text = box.GetLog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            machine = factory.CreateMachine(checkBox2.Checked);
            box = factory.CreateBlackBox((int)numericUpDown1.Value);
            machine.SetBlackBox(box);
            document = factory.CreateDocument(textBox2.Text);
            machine.SetDocument(document);
            richTextBox1.Clear();
            richTextBox1.Text = box.GetLog();

        }
    }
}
