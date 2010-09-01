using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole.Decorators
{
    class Padding: AbstractDecorator
    {

        public int PaddingLeft
        {
            get
            {
                try
                {
                    return int.Parse(GetOption("Padding-Left"));
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set
            {
                SetOption("Padding-Left", value.ToString());
            }
        }


        public int PaddingRight
        {
            get
            {
                try
                {
                    return int.Parse(GetOption("Padding-Right"));
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set
            {
                SetOption("Padding-Right", value.ToString());
            }
        }

        public int PaddingTop
        {
            get
            {
                try
                {
                    return int.Parse(GetOption("Padding-Top"));
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set
            {
                SetOption("Padding-Top", value.ToString());
            }
        }

        public int PaddingBottom
        {
            get
            {
                try
                {
                    return int.Parse(GetOption("Padding-Bottom"));
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set
            {
                SetOption("Padding-Bottom", value.ToString());
            }
        }


        public override string Print(string outputData)
        {
            string[] lines = this.decoratedOutput.Print(outputData).Split(new char[] { '\n' });
            string result = "";
            for (int i = 0; i < PaddingTop; i++)
            {
                result += "\n";
            }
            for (int i = 0; i < lines.Length; i++)
            {
                string curLine = "";
                for (int j = 0; j < PaddingLeft; j++)
                {
                    curLine += " ";
                }
                curLine += lines[i];
                for (int j = 0; j < PaddingRight; j++)
                {
                    curLine += " ";
                }
                result += curLine + "\n";
            }
            for (int i = 0; i < PaddingBottom; i++)
            {
                result += "\n";
            }
            return result;
        }
    }
}
