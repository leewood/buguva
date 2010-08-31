using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Operacine_sistema.Interface
{



    public partial class ConsoleWindow : Form
    {
        public ConsoleWindow()
        {
            InitializeComponent();
            ActiveTextColor = (byte)CGAColors.White;
            ActiveBackgroundColor = (byte)CGAColors.Black;
            PositionX = 0;
            PositionY = 0;
            ActiveWindow = new Rectangle(0, 0, 80, 25);
            TextColorData = new ConsoleArray(this, ConsoleArrayType.TextColor, ActiveTextColor);
            BackgroundColorData = new ConsoleArray(this, ConsoleArrayType.BackgroundColor, ActiveBackgroundColor);
            TextData = new ConsoleArray(this, ConsoleArrayType.Symbol, (byte)0);
            Clear();
            ReadingBufferMutex = new Mutex();
            ReadingBuffer = new byte[4096];
            //this.richTextBox1.ReadOnly = true;
            this.richTextBox1.KeyDown += new KeyEventHandler(richTextBox1_KeyDown);
        }

        void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            var b = (byte)e.KeyValue;
            if (b == 8)
            {
                ReadingBuffer[BufferPosition] = 0;
                BufferPosition--;
                BackByte();
            }
            else if (b == EnterByte)
            {
                WriteByte(EnterByte);
                ReadingBufferMutex.WaitOne(new TimeSpan(1));
                ReadingBufferMutex.ReleaseMutex();
            }
            else
            {
                ReadingBuffer[BufferPosition] = b;
                BufferPosition++;
                WriteByte(b);
            }
        }

        public enum ConsoleArrayType
        {
            TextColor,
            BackgroundColor,
            Symbol
        }

        public class ConsoleArray
        {
            public ConsoleWindow Parent { get; set; }
            private byte[][] _map;
            public ConsoleArrayType UseType { get; set; }

            public ConsoleArray(ConsoleWindow parent, ConsoleArrayType type, byte color)
            {
                Parent = parent;
                UseType = type;
                Init(color);
            }

            public byte this[int j, int i]
            {
                get
                {
                    var line = this[i];
                    if (line.Length > j)
                    {
                        return line[j];
                    }
                    return 0;
                }
                set
                {
                    ColorSymbol(j, i, value);
                }
            }

            public byte[] this[int i]
            {
                get
                {
                    if (_map.Length > i)
                    {
                        return _map[i];
                    }
                    return new byte[0];
                }
                set
                {
                    _map[i] = value;
                    Recolor(0, i, 80);
                }
            }

            public void AddWindowLine()
            {                
                var rect = Parent.ActiveWindow;
                var color = (UseType == ConsoleArrayType.Symbol) ? (byte)' ' :
                                    ((UseType == ConsoleArrayType.BackgroundColor) ? Parent.ActiveBackgroundColor : Parent.ActiveTextColor);   
                for (int i = rect.Top + 1; i < rect.Bottom; i++)
                {
                    for (int j = rect.Left; j < rect.Right; j++)
                    {
                        _map[i - 1][j] = _map[i][j];
                    }
                    Recolor(rect.Left, i - 1, rect.Width);
                }
                int n = rect.Bottom - 1;
                for (int j = rect.Left; j < rect.Right; j++)
                {
                    _map[n][j] = color;
                }
                Recolor(rect.Left, n, rect.Width);
                
            }

            public void Clear()
            {
                var rect = Parent.ActiveWindow;
                var color = (UseType == ConsoleArrayType.Symbol)?(byte)' ':
                    ((UseType == ConsoleArrayType.BackgroundColor)?Parent.ActiveBackgroundColor: Parent.ActiveTextColor);                
                for (int i = rect.Top; i < rect.Bottom; i++)
                {
                    for(int j = rect.Left; j < rect.Right; j++)
                    {
                        _map[i][j] = color;
                    }
                    Recolor(rect.Left, i, rect.Width);
                }
            }

            private void Recolor(int j, int i, int count)
            {
                /*if (UseType != ConsoleArrayType.Symbol)
                {
                    int x = j;
                    int y = i;
                    int len = 0;
                    byte color = _map[y][x];
                    while (len < count)
                    {
                        var mcolor = new ConsoleColor(color);
                        Parent.MainBox.Select(i * 80 + j, 0);
                        while ((y < 25) && (_map[y][x] == color))
                        {
                            Parent.MainBox.SelectionLength++;
                            len++;
                            x++;
                            if (x >= 80)
                            {
                                y++;
                                x = 0;
                            }
                        }
                        color = (y < 25) ? _map[y][x] : (byte)0;
                        if (UseType == ConsoleArrayType.BackgroundColor)
                        {
                            Parent.MainBox.SelectionBackColor = mcolor.RGBValue;
                        }
                        else
                        {
                            Parent.MainBox.SelectionColor = mcolor.RGBValue;
                        }
                    }
                    Parent.MainBox.Select(0, 0);
                }*/
            }

            public void ColorSymbol(int j, int i, byte color)
            {
                if ((_map.Length > i) && (_map[i].Length > j))
                {
                    _map[i][j] = color;
                }
                /*if (UseType != ConsoleArrayType.Symbol)
                {
                    var mcolor = new ConsoleColor(color);
                    Parent.MainBox.Select(i * 80 + j, 1);
                    if (UseType == ConsoleArrayType.BackgroundColor)
                    {
                        Parent.MainBox.SelectionBackColor = mcolor.RGBValue;
                    }
                    else if (UseType == ConsoleArrayType.TextColor)
                    {
                        Parent.MainBox.SelectionColor = mcolor.RGBValue;
                    }
                    Parent.MainBox.Select(0, 0);
                }*/
            }

            public void ColorLine(int i, byte color)
            {
                var arr = new byte[80];
                for (int j = 0; j < 80; j++)
                {
                    arr[j] = color;
                }
                _map[i] = arr;
                /*if (UseType != ConsoleArrayType.Symbol)
                {
                    var mcolor = new ConsoleColor(color);
                    Parent.MainBox.Select(i * 80, 80);
                    if (UseType == ConsoleArrayType.BackgroundColor)
                    {
                        Parent.MainBox.SelectionBackColor = mcolor.RGBValue;
                    }
                    else
                    {
                        Parent.MainBox.SelectionColor = mcolor.RGBValue;
                    }
                    Parent.MainBox.Select(0, 0);
                }*/
            }

            private void Init(byte color)
            {
                _map = new byte[25][];
                for (int i = 0; i < 25; i++)
                {
                    var arr = new byte[80];
                    for (int j = 0; j < 80; j++)
                    {
                        arr[j] = color;
                    }
                    _map[i] = arr;
                }
                /*if (UseType != ConsoleArrayType.Symbol)
                {
                    Parent.MainBox.SelectAll();
                    var mcolor = new ConsoleColor(color);
                    if (UseType == ConsoleArrayType.BackgroundColor)
                    {
                        Parent.MainBox.SelectionBackColor = mcolor.RGBValue;
                    }
                    else
                    {
                        Parent.MainBox.SelectionColor = mcolor.RGBValue;
                    }
                    Parent.MainBox.Select(0, 0);
                }*/
            }
        }

        public int ConsoleID { get; set; }

        public ConsoleArray TextColorData { get; private set; }
        public ConsoleArray BackgroundColorData { get; private set; }
        public ConsoleArray TextData { get; private set; }

        public Mutex ReadingBufferMutex { get; set; }
        public Mutex CopyBufferMutex { get; set; }

        public byte[] GetCopyBufferData()
        {
            byte[] result = new byte[BufferPosition];
            for (int i = 0; i < BufferPosition; i++)
            {
                result[i] = ReadingBuffer[i];
            }
            BufferPosition = 0;
            CopyBufferMutex.ReleaseMutex();
            return result;
        }

        public Mutex StartReading()
        {
            CopyBufferMutex.WaitOne();
            if (ReadingBufferMutex.WaitOne())
            {                
                return ReadingBufferMutex;
            }
            throw new Exception("Error reading data");
        }

        public RichTextBox MainBox { get { return richTextBox1; } }

        private bool CheckCoordinates(int x, int y)
        {
            return (x >= 0) && (x < 80) && (y >= 0) && (y < 25);
        }

        public byte ActiveTextColor { get; set; }
        public byte ActiveBackgroundColor { get; set; }
        public byte[] ReadingBuffer { get; set; }

        private int _positionX;
        private int _positionY;
        public int PositionX 
        {
            get
            {
                return _positionX;
            }
            set
            {
                _positionX = value;
                richTextBox1.Select(_positionY * 81 + _positionX, 0);
            }
        }
        public int PositionY 
        {
            get
            {
                return _positionY;
            }
            set
            {
                _positionY = value;
                richTextBox1.Select(_positionY * 81 + _positionX, 0);
            }
        }
        public int BufferPosition { get; set; }


        public string ConstructRtf()
        {
            string rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Courier New;}}\r\n";
            rtf += "{\\colortbl ;";
            for (byte i = 0; i < 16; i++)
            {
                var color = new ConsoleColor(i);
                rtf += String.Format("\\red{0}\\green{1}\\blue{2};", color.RGBValue.R, color.RGBValue.G, color.RGBValue.B);
            }
            rtf += "}\r\n\\viewkind4\\pard\\cf16\\highlight1\\f0\\fs17";
            int x = 0;
            int y = 0;
            bool end = false;
            byte lastColorT = 15;
            byte lastColorB = 0;
            while (!end)
            {
                var tColor = TextColorData[x, y];
                var bColor = BackgroundColorData[x, y];
                var symbol = this[x, y];
                string addition = "";
                if (lastColorB != bColor)
                {
                    rtf += String.Format("\\highlight{0}", bColor + 1);
                    lastColorB = bColor;
                    addition = "";
                }
                if (lastColorT != tColor)
                {
                    rtf += String.Format("\\cf{0}", tColor + 1);
                    lastColorT = tColor;
                    addition = "";
                }
                if ((symbol != (byte)' ') && (symbol != 0) && (symbol != 13))
                {
                    rtf += "{" + addition + (char)symbol + "}";
                }
                else 
                {
                    rtf += "{ }";
                }
                x++;
                if (x > 80)
                {
                    
                    x = 0;
                    y++;
                    if (y >= 25)
                    {
                        end = true;
                        rtf += "\\highlight2\\cf3";
                    }
                    //rtf += String.Format("\\par\\pard\\cf{1}\\highlight{0}\\f0\\fs17", bColor + 1, tColor + 1);
                    rtf += "\\par\r\n";
                }
            }
            rtf += "}\r\n";
            return rtf;
        }

        public Rectangle ActiveWindow { get; set; }

        public void UpdateView()
        {
            richTextBox1.Rtf = ConstructRtf();
            richTextBox1.Select(_positionY * 82 + _positionX, 0);
        }

        public void AddWindowLine()
        {
            /*for (int i = ActiveWindow.Top + 1; i < ActiveWindow.Bottom; i++)
            {
                var source = this[i];
                var dest = this[i - 1];
                for (int j = ActiveWindow.Left; j < ActiveWindow.Right; j++)
                {
                    dest[j] = source[j];
                }
                this[i - 1] = dest;
            }
            int n = ActiveWindow.Bottom - 1;
            var eb = ConstructEmpty()[0];
            var last = this[n];
            for (int j = ActiveWindow.Left; j < ActiveWindow.Right; j++)
            {
                this[n] = last;
            }*/
            TextData.AddWindowLine();
            TextColorData.AddWindowLine();
            BackgroundColorData.AddWindowLine();
            UpdateView();
        }

        private byte EnterByte = 13;

        public void BackByte()
        {
            if (PositionX > ActiveWindow.Left)
            {
                PositionX--;
                WriteByte(ConstructEmpty()[0]);
                PositionX--;
            }
            else
            {
                PositionY--;
                var newY = PositionY;
                var line = TextData[PositionY];
                var pos = line.ToList().IndexOf(13, ActiveWindow.Left);
                PositionX = (pos < ActiveWindow.Right) ? pos : ActiveWindow.Right - 1;
                WriteByte(ConstructEmpty()[0]);
                PositionY = newY;
                PositionX = (pos < ActiveWindow.Right) ? pos : ActiveWindow.Right - 1;
            }
            UpdateView();
        }

        public void WriteLine(string data)
        {
            Write(data + "\n");
        }

        public void Write(string data, int start, int count)
        {
            var sub = data.Substring(start, count);
            var bytes = Encoding.ASCII.GetBytes(sub);
            WriteBytes(bytes);
        }

        public void Write(string data, int start)
        {
            Write(data, start, data.Length - start);
        }

        public void Write(string data)
        {
            Write(data, 0);
        }

        public void WriteBytes(byte[] data, int start)
        {
            WriteBytes(data, start, data.Length - start);
        }

        public void WriteBytes(byte[] data)
        {
            WriteBytes(data, 0);
        }

        public void WriteBytes(byte[] data, int start, int count)
        {
            for (int i = start; i < start + count; i++)
            {
                byte b = (data.Length > i)? data[i]: (byte)0;
                WriteByte(b);
            }
        }

        public void WriteByte(byte b)
        {
            if (b != EnterByte)
            {
                this[PositionX, PositionY] = b;
                TextColorData[PositionX, PositionY] = ActiveTextColor;
                BackgroundColorData[PositionX, PositionY] = ActiveBackgroundColor;
                PositionX++;
                if (ActiveWindow.Right <= PositionX)
                {
                    PositionX = ActiveWindow.Left;
                    PositionY++;
                    if (PositionY >= ActiveWindow.Bottom)
                    {
                        AddWindowLine();
                        PositionY--;
                        PositionX = ActiveWindow.Left;
                    }
                }
            }
            else
            {
                TextData[PositionX, PositionY] = 13;
                PositionY++;
                PositionX = ActiveWindow.Left;
                if (PositionY >= ActiveWindow.Bottom)
                {
                    AddWindowLine();
                    PositionY--;                    
                }
            }
            UpdateView();
        }

        


        public void Clear()
        {
            /*var eb = ConstructEmpty()[0];
            for (int i = ActiveWindow.Top; i < ActiveWindow.Bottom; i++ )
            {
                var dest = this[i];
                for (int j = ActiveWindow.Left; j < ActiveWindow.Right; j++)
                {
                    dest[j] = eb;
                }
                this[i] = dest;
            }*/
            TextData.Clear();
            TextColorData.Clear();
            BackgroundColorData.Clear();
            _positionX = ActiveWindow.Left;
            _positionY = ActiveWindow.Top;
            UpdateView();
        }


        public void SetColor(int x, int y, Color textColor, Color backColor)
        {
            richTextBox1.HideSelection = true;
            richTextBox1.SelectionStart = y * 80 + x;
            richTextBox1.SelectionLength = 1;
            richTextBox1.SelectionColor = textColor;
            richTextBox1.SelectionBackColor = backColor;
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = 0;
        }

        private byte[] ConstructEmpty()
        {
            return Encoding.ASCII.GetBytes("                                                                                ");
        }

        public byte[] this[int y]
        {
            get
            {
                if ((y >= 0) && (y < 25))
                {
                    return TextData[y];
                    //return Encoding.ASCII.GetBytes(this.richTextBox1.Lines[y]);
                }
                else
                {
                    return ConstructEmpty();
                }
            }
            set
            {
                if ((y >= 0) && (y < 25))
                {
                    //var s = Encoding.ASCII.GetString(value);
                    //var lines = richTextBox1.Lines;
                    //lines[y] = s;
                    //richTextBox1.Lines = lines;
                    TextData[y] = value;
                    richTextBox1.Rtf = ConstructRtf();
                }
            }
        }

        //public int 

        public byte this[int x, int y]
        {
            get

            {
                if (CheckCoordinates(x, y))
                {
                    //var c = this.richTextBox1.Lines[y][x];
                    //int i = (int)c;
                    //return (byte)i;
                    var line = TextData[y];
                    return line[x];
                }
                return 0;                                
            }
            set
            {
                if (CheckCoordinates(x, y))
                {
                    /*var line = this[y];
                    if (line.Length < x + 1)
                    {
                        var newLine = new byte[x + 1];
                        for (int i = 0; i < line.Length; i++)
                        {
                            newLine[i] = line[i];
                        }
                        for (int i = line.Length; i < x; i++)
                        {
                            newLine[i] = (byte)' ';
                        }
                        newLine[x] = value;
                        line = newLine;
                    }
                    else
                    {
                        line[x] = value;
                    }
                    this[y] = line;*/
                    var line = TextData[y];
                    line[x] = value;
                    richTextBox1.Rtf = ConstructRtf();
                }
            }
        }
    }

    public enum CGAColors
    {
        Black,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Brown,
        LightGray,
        Gray,
        LightBlue,
        LightGreen,
        LightCyan,
        LightRed,
        LightMagenta,
        Yellow,
        White
    }

    public class ConsoleColor
    {
        public byte Value { get; set; }
        public CGAColors CGAValue
        {
            get
            {
                return (CGAColors)Value;
            }
            set
            {
                Value = (byte)value;
            }
        }

        public ConsoleColor(byte b)
        {
            Value = b;
        }

        public Color RGBValue
        {
            get
            {
                var colorNumber = Value;
                if (colorNumber < 16)
                {
                    //var red = (2 * (colorNumber & 4) + (colorNumber & 8)) / 3;
                    //var green = (2 * (colorNumber & 2) + (colorNumber & 8)) / 3;
                    //var blue = (2 * (colorNumber & 1) + (colorNumber & 8)) / 3;
                    ////var red = (byte)((int)(255 * ((double)2 / (double)3 * (double)(colorNumber & 4) + (double)1 / (double)3 *(double)(colorNumber & 8))));
                    //var green = (byte)((int)(255 * ((double)2 / (double)3 * (double)(colorNumber & 2) + (double)1 / (double)3 * (double)(colorNumber & 8))));
                    //var blue = (byte)((int)(255 * ((double)2 / (double)3 * (double)(colorNumber & 1) + (double)1 / (double)3 * (double)(colorNumber & 8))));
                    byte red = 0;
                    byte green = 0;
                    byte blue = 0;
                    switch (colorNumber)
                    {
                        case 0: red = 0; green = 0; blue = 0; break;
                        case 1: red = 0; green = 0; blue = 0xaa; break;
                        case 2: red = 0; green = 0xaa; blue = 0; break;
                        case 3: red = 0; green = 0xaa; blue = 0xaa; break;
                        case 4: red = 0xaa; green = 0; blue = 0; break;
                        case 5: red = 0xaa; green = 0; blue = 0xaa; break;
                        case 6: red = 0xaa; green = 0x55; blue = 0; break;
                        case 7: red = 0xaa; green = 0xaa; blue = 0xaa; break;
                        case 8: red = 0x55; green = 0x55; blue = 0x55; break;
                        case 9: red = 0x55; green = 0x55; blue = 0xff; break;
                        case 10: red = 0x55; green = 0xff; blue = 0x55; break;
                        case 11: red = 0x55; green = 0xff; blue = 0xff; break;
                        case 12: red = 0xff; green = 0x55; blue = 0x55; break;
                        case 13: red = 0xff; green = 0x55; blue = 0xff; break;
                        case 14: red = 0xff; green = 0xff; blue = 0x55; break;
                        case 15: red = 0xff; green = 0xff; blue = 0xff; break;
                    }
                    return Color.FromArgb(0, (int)red, (int)green, (int)blue);
                }
                else
                {
                    int cN = colorNumber - 15;
                    return Color.FromArgb(cN * 17895697);
                }
            }
            set
            {
                bool isSet = false;
                for (byte i = 0; i < 16; i++)
                {
                    if (new ConsoleColor(i).RGBValue == value)
                    {
                        Value = i;
                        isSet = true;
                        break;
                    }
                }
                if (!isSet)
                {
                    Value = (byte)(value.ToArgb() / 17895697);
                }
            }
        }

    }


}
