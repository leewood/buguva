using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole.Decorators
{
    class SourceDuplication: AbstractDecorator
    {
        public SourceDuplication()
            : base()
        {
        }

        public SourceDuplication(Printable decoratedObject)
            : base(decoratedObject)
        {
        }

        public override string Print(string outputData)
        {
            if (decoratedOutput != null)
            {                
                Printable current = decoratedOutput;             
                while ((current is AbstractDecorator) && (((AbstractDecorator)current).GetDecoratedObject() != null))
                {
                    current = ((AbstractDecorator)current).GetDecoratedObject();
                }
                string text = current.Print(outputData);
                string[] lines = text.Split(new char[] { '\n' });
                string result = decoratedOutput.Print(outputData);
                result += "\n";
                foreach (string line in lines)
                {
                    string newLine = line;
                    /*
                    for (int i = line.Length - 1; i >= 0; i--)
                    {
                        newLine += line[i];
                    }
                     */
                    result += newLine + "\n";
                }
                return result;
            }
            else
            {
                return "";
            }
        }
    }
}
