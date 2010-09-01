using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSI2
{
    public partial class ControlPanel : Form
    {
        public ControlPanel()
        {
            InitializeComponent();
            updatePaddingControls();
            updateAligmentsControls();
        }

        ExtConsole.Decorator decorator = new PSI2.ExtConsole.Decorator(new ExtConsole.Items.SimpleOutput());

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                decorator.RemoveDecorator("EncodedOutput");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateConsole();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            decorator.AddDecorator("EncodedOutput");
            UpdateConsole();
        }

        public void UpdateConsole()
        {
            System.Console.Clear();

            decorator.PrintDecoratedField(this.richTextBox1.Text);
            label13.Text = decorator.GetDecoratorsList();
        }


        private void button11_Click(object sender, EventArgs e)
        {
            UpdateConsole();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decorator.AddDecorator("Border");
            UpdateConsole();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                decorator.RemoveDecorator("Border");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            UpdateConsole();
        }

        private void updatePaddingControls()
        {
            ExtConsole.Decorators.AbstractDecorator dec = decorator.GetDecorator("Padding");
            numericUpDown1.Enabled = dec != null;
            numericUpDown2.Enabled = dec != null;
            numericUpDown3.Enabled = dec != null;
            numericUpDown4.Enabled = dec != null;
            if (dec != null)
            {
                try
                {
                    numericUpDown1.Value = int.Parse(dec.GetOption("Padding-Left"));
                }
                catch (Exception)
                {
                    numericUpDown1.Value = 0;
                }
                try
                {
                    numericUpDown2.Value = int.Parse(dec.GetOption("Padding-Right"));
                }
                catch (Exception)
                {
                    numericUpDown2.Value = 0;
                }
                try
                {
                    numericUpDown3.Value = int.Parse(dec.GetOption("Padding-Top"));
                }
                catch (Exception)
                {
                    numericUpDown3.Value = 0;
                }
                try
                {
                    numericUpDown4.Value = int.Parse(dec.GetOption("Padding-Bottom"));
                }
                catch (Exception)
                {
                    numericUpDown4.Value = 0;
                }

            }
        }

        private void updateAligmentsControls()
        {
            ExtConsole.Decorators.AbstractDecorator dec = decorator.GetDecorator("Aligment");
            comboBox1.Enabled = dec != null;
            if (dec != null)
            {
                try
                {
                    comboBox1.SelectedIndex = int.Parse(dec.GetOption("Aligment"));
                }
                catch (Exception)
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            decorator.AddDecorator("Padding");
            updatePaddingControls();
            UpdateConsole();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                decorator.RemoveDecorator("Padding");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            updatePaddingControls();
            UpdateConsole();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            decorator.AddDecorator("Aligment");
            updateAligmentsControls();
            UpdateConsole();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                decorator.RemoveDecorator("Aligment");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                     
            updateAligmentsControls();
            UpdateConsole();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            decorator.AddDecorator("SourceDuplication");
            UpdateConsole();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                decorator.RemoveDecorator("SourceDuplication");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            UpdateConsole();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExtConsole.Decorators.AbstractDecorator dec = decorator.GetDecorator("Aligment");
            if (dec != null)
            {
                dec.SetOption("Aligment", comboBox1.SelectedIndex.ToString());
            }
            UpdateConsole();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ExtConsole.Decorators.AbstractDecorator dec = decorator.GetDecorator("Padding");
            if (dec != null)
            {
                dec.SetOption("Padding-Left", numericUpDown1.Value.ToString("0"));
            }
            UpdateConsole();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ExtConsole.Decorators.AbstractDecorator dec = decorator.GetDecorator("Padding");
            if (dec != null)
            {
                dec.SetOption("Padding-Right", numericUpDown2.Value.ToString("0"));
            }
            UpdateConsole();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            ExtConsole.Decorators.AbstractDecorator dec = decorator.GetDecorator("Padding");
            if (dec != null)
            {
                dec.SetOption("Padding-Top", numericUpDown3.Value.ToString("0"));
            }
            UpdateConsole();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            ExtConsole.Decorators.AbstractDecorator dec = decorator.GetDecorator("Padding");
            if (dec != null)
            {
                dec.SetOption("Padding-Bottom", numericUpDown4.Value.ToString("0"));
            }
            UpdateConsole();
        }
    }
}
