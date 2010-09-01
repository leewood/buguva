using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole.Decorators
{
    class EncodedOutput: AbstractDecorator
    {
        public EncodedOutput()
            : base()
        {
        }

        public EncodedOutput(Printable decoratedOutput)
            : base(decoratedOutput)
        {
        }

        public override string Print(string outputData)
        {
            if (decoratedOutput != null)
            {
                string text = decoratedOutput.Print(outputData);
                string result = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsLetterOrDigit(text[i]))
                    {
                        result += char.ConvertFromUtf32(char.ConvertToUtf32(text, i) + 2);
                    }
                    else
                    {
                        result += text[i];
                    }
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
