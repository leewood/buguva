using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HomeunixMangaClient
{
    public partial class Form1 : Form
    {
        public delegate void UpdateProgressDelegate();        
        public delegate void InitProgress(int min, int max);
        private Downloader _downloader;

        public Form1()
        {
            InitializeComponent();
            _downloader = new Downloader(this);
        }

        public void UpdateMainProgress()
        {
            totalProgressBar.Value++;
        }

        public void UpdateSubProgress()
        {
            currentProgressBar.Value++;
        }

        public void InitMainProgress(int min, int max)
        {
            totalProgressBar.Minimum = min;
            totalProgressBar.Maximum = max + 1;
            totalProgressBar.Value = min;
        }

        public void InitSubProgress(int min, int max)
        {
            currentProgressBar.Minimum = min;
            currentProgressBar.Maximum = max + 1;
            currentProgressBar.Value = min;
        }

        public void CompleteProgress()
        {
            currentProgressBar.Value = currentProgressBar.Maximum;
            totalProgressBar.Value = totalProgressBar.Maximum;
        }

        private void StartDownload()
        {
            _downloader.MangaDir = dirTextBox.Text;
            _downloader.PerformDownload(mangaTextBox.Text, fromTextBox.Text, toTextBox.Text);
        }

        private void startButton_Click(object sender, EventArgs e)
        {

            new Thread(new ThreadStart(StartDownload)).Start();
            
        }

    }
}
