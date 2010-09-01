using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole.Decorators
{
    class Aligment: AbstractDecorator
    {
        private int FindLongestLineLength(string[] lines)
        {
            int len = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > len)
                {
                    len = lines[i].Length;
                }
            }
            return len;
        }

        private string Centered(string str, int max)
        {
            string newS = str.Trim();
            int cur = max - newS.Length;
            string result = "";
            for (int i = 0; i < cur / 2; i++)
            {
                result += " ";
            }
            result += newS;
            for (int i = cur / 2 + newS.Length; i < max; i++)
            {
                result += " ";
            }
            return result;
        }

        private string LeftJustified(string str, int max)
        {
            return str.Trim();
        }

        private string RightJustified(string str, int max)
        {
            string result = "";
            str = str.Trim();
            for (int i = 0; i < max - str.Length; i++)
            {
                result += " ";
            }
            result += str;
            return result;
        }

        private string Both(string str, int max)
        {
            str = str.Trim();
            string[] words = str.Split(new char[] { ' ' });
            List<string> wrd = new List<string>();
            int count = 0;
            foreach (string word in words)
            {
                if (word != "")
                {
                    wrd.Add(word);
                    count += word.Length;
                }
            }
            if (wrd.Count > 0)
            {
                int diff = (max - count) / wrd.Count;
                string result = wrd[0];
                for (int i = 1; i < wrd.Count; i++)
                {
                    for (int j = 0; j < diff; j++)
                    {
                        result += " ";
                    }
                    result += wrd[i];
                }
                return result;
            }
            else
            {
                return "";
            }
        }

        public enum AligmentTypes
        {
            LeftAligment,
            RightAligment,
            Centered,
            Justified,
        }

        public AligmentTypes AligmentValue
        {
            get
            {
                try
                {
                    return (AligmentTypes)int.Parse(GetOption("Aligment"));
                }
                catch (Exception)
                {
                    return AligmentTypes.LeftAligment;
                }
            }
            set
            {
                SetOption("Aligment", ((int)value).ToString());
            }
        }

        public override string Print(string outputData)
        {
            string[] data = this.decoratedOutput.Print(outputData).Split(new char[] { '\n' });
            string result = "";
            int max = this.FindLongestLineLength(data);
            foreach (string line in data)
            {
                switch (AligmentValue)
                {
                    case AligmentTypes.LeftAligment: result += LeftJustified(line, max); break;
                    case AligmentTypes.RightAligment: result += RightJustified(line, max); break;
                    case AligmentTypes.Centered: result += Centered(line, max); break;
                    case AligmentTypes.Justified: result += Both(line, max); break;
                }
                result += "\n";
            }
            return result;
        }
    }
}
