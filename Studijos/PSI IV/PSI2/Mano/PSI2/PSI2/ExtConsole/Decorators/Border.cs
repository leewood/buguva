using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole.Decorators
{
    class Border: AbstractDecorator
    {
        public Border()
            : base()
        {
        }

        public Border(Printable decoratedOutput)
            : base(decoratedOutput)
        {
        }

        public override string Print(string outputData)
        {
            if (decoratedOutput != null)
            {
                string s = decoratedOutput.Print(outputData);
                if (s[s.Length - 1] == '\n')
                {
                    s = s.Remove(s.Length - 1);
                }
                string[] lines = s.Split(new char[] { '\n' });
                int maxSize = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length > maxSize)
                    {
                        maxSize = lines[i].Length;
                    }
                }
                string headerFooter = "+";
                for (int i = 0; i < maxSize; i++)
                {
                    headerFooter += "-";
                }
                headerFooter += "+";
                string result = headerFooter + "\n";
                for (int j = 0; j < lines.Length; j++)
                {
                    string newLine = lines[j];
                    for (int i = lines[j].Length; i < maxSize; i++)
                    {
                        newLine += " ";
                    }
                    result += "|" + newLine + "|";
                    result += "\n";
                }
                result += headerFooter + "\n";
                return result;
            }
            else
            {
                return "";
            }
        }
    }
}
