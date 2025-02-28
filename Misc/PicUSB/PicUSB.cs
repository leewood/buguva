using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; //Clase para abrir página web

namespace PicUSB
{
    public partial class PicUSB : Form
    {
        PicUSBAPI usbapi = new PicUSBAPI();
        Oscilloscope osc = null;
        Oscilloscope osc2 = null;
        public PicUSB()
        {
            InitializeComponent();
            osc = Oscilloscope.Create("Scope_Desk.ini", "");
            osc2 = Oscilloscope.Create("Scope_Desk.ini", "");
            
        }

        private void leds_off_Click(object sender, EventArgs e)
        {
            usbapi.LedPIC(0x00);    
        }

        private void led_verde_Click(object sender, EventArgs e)
        {
            usbapi.LedPIC(0x01);    
        }

        private void led_rojo_Click(object sender, EventArgs e)
        {
            usbapi.LedPIC(0x02);    
        }

        private void PICsuma_Click(object sender, EventArgs e)
        {
            usbapi.Osciloskopas();
            byte[] buffer = usbapi.ReadBuf(64);
			double result = (double)buffer[0];
			resultado.Text = result.ToString();
           
        }

        private void avatar_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.TI.com");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            osc.Show();
        }

        private void PicUSB_FormClosed(object sender, FormClosedEventArgs e)
        {
            osc.Dispose();
        }

        private void updateTime_Tick(object sender, EventArgs e)
        {
            usbapi.Osciloskopas();
            //osc.Clear();
            byte[] buffer = usbapi.ReadBuf(128);
            double result = (double)buffer[0];
            resultado.Text = result.ToString();
            for (int i = 0; i < 128; i++)
            {
                //osc.AddExternalData((double)buffer[i]);
                osc.AddData((double)buffer[i], 0, 0);
            }
            
            osc.Update();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (updateTime.Enabled)
            {
                updateTime.Enabled = false;
                osc.Hide();
                button2.Text = "Pradėti";
            }
            else
            {
                osc.Show();
                
                updateTime.Enabled = true;
                button2.Text = "Stabdyti";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            usbapi.keisti_dazni(B1, B2, B3, B4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            usbapi.keisti_stiprinima(0x01, 0xFF, 0xFF);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            usbapi.keisti_stiprinima(0x02, 0xFF, 0xFF);
        }

        public byte DCB1;

        public void skaiciuotiDCB1()
        {
            DCB1 = 0;
            byte mask1 = (checkBox1.Checked) ? 255 : 0;
            byte mask2 = (radioButton1.Checked) ? 127 : 0;
            byte mask3 = (radioButton2.Checked) ? 63 : 0;
            byte mask4 = (radioButton3.Checked) ? 31 : 0;
            byte mask5 = (checkBox2.Checked) ? 15 : 0;
            byte mask6 = (radioButton6.Checked) ? 7 : 0;
            byte mask7 = (radioButton5.Checked) ? 3 : 0;
            byte mask8 = (radioButton4.Checked) ? 1 : 0;
            DCB1 = mask1 | mask2 | mask3 | mask4 | mask5 | mask6 | mask7 | mask8;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            usbapi.reles(0x85);

        }



        public int NP;
        public int M;

        public double fOutTiksl;

        public bool parinktiNM(double fOut, double fIn, double Pdiv, double tikl)
        {
            double kart = fIn / Pdiv;
            for (int j = 1; j < 511; j++)
            {

                for (int i = 1; i <= 4095; i++)
                {
                    double res = kart * ((double)i / (double)j);
                    double diff = Math.Abs(res - fOut);

                    if (diff <= tikl)
                    {
                        fOutTiksl = res;
                        NP = i;
                        M = j;
                        return true;
                    }
                }
            }
            return false;
        }


        public void parinktiNM(double fOut, double fIn, double Pdiv)
        {
            double tiksl = 0.0;
            if (!parinktiNM(fOut, fIn, Pdiv, tiksl))
            {
                tiksl = 0.0000511;
                while (!parinktiNM(fOut, fIn, Pdiv, tiksl))
                {
                    tiksl = tiksl * 2;
                }
            }
        }

        public int N;
        public int P;
        public int Q;
        public int R;

        public byte B1;
        public byte B2;
        public byte B3;
        public byte B4;

        public void skaiciuotiNPQR(int n, int m)
        {
            P = 4 - (int)Math.Log((double)n / (double)m, 2);
            if (P < 0)
            {
                P = 0;
            }
            N = n * (int)Math.Pow(2, P);
            Q = (int)((double)N / (double)m);
            R = N - m * Q;
        }

        public void skaiciuotiBaitus()        
        {
            B1 = (byte)(N / 16);
            B2 = (byte)((N % 16) * 16 + R / 32);
            B3 = (byte)((R % 32) * 8 + Q / 8);
            B4 = (byte)((Q % 8) * 32 + P * 4);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

            label3.Text = ((double)trackBar1.Value / (double)100).ToString("0.00");
            //usbapi.keisti_dazni(0x01, 0x02, 0x03, 0x04);
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            parinktiNM((double)trackBar1.Value / (double)100, 200, 2);
            skaiciuotiNPQR(NP, M);
            skaiciuotiBaitus();
            label3.Text = fOutTiksl.ToString("0.000000");
        }

        double AStip;
        double AStipB1;
        double AStipB2;
        double AStipB3;

        private void skaiciuotiBaitus()
        {
            AStipB1 = 0; // Israsysi pats;
            AStipB2 = AStip / 4;
            AStipB3 = (AStip % 4) * 64;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            AStip = (double)trackBar2.Value / (double)100;
            label4.Text = AStip.ToString("0.00");
            skaiciuotiBaitus();
        }
    }
}