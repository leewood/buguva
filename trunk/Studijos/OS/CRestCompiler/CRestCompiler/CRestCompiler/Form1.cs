using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRestCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public void compile()
        {
            Reader rdr = new Reader(richTextBox1, richTextBox2, richTextBox3);
            Compiler comp = new Compiler(rdr);
            comp.outputResult();
            listBox1.Items.Clear();
            int memNr = 0;
            string sabl = "00";
            for (int i = 0; i < richTextBox2.Lines.Length - 1; i++ )
            {
                string text = richTextBox2.Lines.GetValue(i).ToString();
                string s = memNr.ToString("X");
                if ((text.Length > 0) && (text[0] == '$'))
                {
                    s = "";
                    if (text.Length == 3)
                    {
                        memNr = 0;
                        sabl = text.Substring(1);
                    }
                }
                else
                {
                    while (s.Length < 2)
                    {
                        s = "0" + s;
                    }
                    s = sabl + s;
                    memNr++;
                }
                s = s + "\t" + richTextBox2.Lines.GetValue(i).ToString();
                listBox1.Items.Add(s);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  richTextBox1.Lines.GetValue(0).ToString();
        }




        public void paintBaseWord(int start, int count)
        {
            int tempSel = richTextBox1.SelectionStart;
            Color tempColor = richTextBox1.SelectionColor;
            richTextBox1.SelectionStart = start;
            richTextBox1.SelectionLength = count;
            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectionStart = tempSel;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = Color.Black;
            
        }

        public void paintSystemCall(int start, int count)
        {
            int tempSel = richTextBox1.SelectionStart;
            Color tempColor = richTextBox1.SelectionColor;
            richTextBox1.SelectionStart = start;
            richTextBox1.SelectionLength = count;
            richTextBox1.SelectionColor = Color.Aqua;
            richTextBox1.SelectionStart = tempSel;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = Color.Black;

        }



        public void colorWord(int start, int count, string word)
        {
            switch (word)
            {
                case "if": paintBaseWord(start, count); break;
                case "while": paintBaseWord(start, count); break;
                case "for": paintBaseWord(start, count); break;
                case "else": paintBaseWord(start, count); break;
                case "int": paintBaseWord(start, count); break;
                case "string": paintBaseWord(start, count); break;
                case "return": paintBaseWord(start, count); break;
                case "fileOpen": paintSystemCall(start, count); break;
                case "read": paintSystemCall(start, count); break;
                case "write": paintSystemCall(start, count); break;
                case "getParam": paintSystemCall(start, count); break;
                case "start": paintSystemCall(start, count); break;
                case "scanf": paintSystemCall(start, count); break;
                case "printf": paintSystemCall(start, count); break;  
                default: break;
            }
            bool isOk = true;
            for (int i = 0; i < word.Length; i++)
            {
                if (!(char.IsDigit(word[i]) || ((word[i] == 'x') && (i == 1))))
                {
                    isOk = false;
                }
            }
            if (isOk)
            {
                int tempSel = richTextBox1.SelectionStart;
                Color tempColor = richTextBox1.SelectionColor;
                richTextBox1.SelectionStart = start;
                richTextBox1.SelectionLength = count;
                richTextBox1.SelectionColor = Color.Green;
                richTextBox1.SelectionStart = tempSel;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = Color.Black;
            }

        }

        public int nextWord(int i, string Line, int realStart)
        {
            int start = i;
            string s = "";
            if (char.IsLetterOrDigit(Line[i]))
            {
                
                while ((i < Line.Length) && (char.IsLetterOrDigit(Line[i])))
                {
                    s += Line[i];
                    i++;
                }
            }
            colorWord(start + realStart, i - start, s);
            return i;
        }

        public void colorAnalize(int start, string Line)
        {
            int i = 0;
            while ((i < Line.Length) && (!char.IsLetterOrDigit(Line[i]) && (Line[i] != '\'')))
            {
                i++;
            }
            while (i < Line.Length)
            {
                if (char.IsLetterOrDigit(Line[i]))
                {
                    i = nextWord(i, Line, start);
                }
                else if (Line[i] == '\'')
                {
                    int j = Line.IndexOf('\'', i + 1);
                    if (j < 0)
                    {
                        j = i;
                    }
                    int tempSel = richTextBox1.SelectionStart;
                    Color tempColor = richTextBox1.SelectionColor;
                    richTextBox1.SelectionStart = start + i;
                    richTextBox1.SelectionLength = j - i + 1;
                    richTextBox1.SelectionColor = Color.Red;
                    richTextBox1.SelectionStart = tempSel;
                    richTextBox1.SelectionLength = 0;
                    richTextBox1.SelectionColor = Color.Black;
                    i = j + 1;
                }
                else
                {
                    i++;
                }
            }
        }

        public void coloring()
        {
            string s = richTextBox1.Text;
            int i = 0;
            while (i < s.Length)
            {
                int j = s.IndexOf('\n', i);
                if (j < 0)
                {
                    j = s.Length;
                }
                string curLine = s.Substring(i, j - i);
                colorAnalize(i, curLine);
                i = j + 1;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            coloring();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "{\n}";
            savedName = "";
            this.Text = "C-- Compiler - noname00.cmm";
        }
        
        string savedName = "";

        public void saveFile(string fname)
        {
            richTextBox1.SaveFile(fname, RichTextBoxStreamType.PlainText);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savedName == "")
            {
                saveFileDialog1.FileName = (savedName != "") ? savedName : "noname00.cmm";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    savedName = saveFileDialog1.FileName;
                    this.Text = "C-- Compiler - " + savedName;
                }
            }
            if (savedName != "")
            {
                saveFile(savedName);
            }
            else
            {
                this.Text = "C-- Compiler - noname00.cmm";
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = (savedName != "") ? savedName : "noname00.cmm";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savedName = saveFileDialog1.FileName;
                if (savedName != "")
                {
                    this.Text = "C-- Compiler - " + savedName;
                    saveFile(savedName);
                }
                else
                {
                    this.Text = "C-- Compiler - noname00.cmm";
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = (savedName != "") ? savedName : "noname00.cmm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savedName = openFileDialog1.FileName;
                this.Text = "C-- Compiler - " + savedName;
                richTextBox1.LoadFile(savedName, RichTextBoxStreamType.PlainText);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compile();
        }

        private void exportAssemblerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                richTextBox2.SaveFile(saveFileDialog2.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void richTextBox2_HScroll(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar = false;
            
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            compile();
        }



    }
}
