using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole.Decorators
{
    class AbstractDecorator: Printable
    {
        protected Printable decoratedOutput = null;

        protected Dictionary<string, string> options = new Dictionary<string, string>();

        public AbstractDecorator(Printable decoratedOutput)
        {
            this.decoratedOutput = decoratedOutput;
        }

        public AbstractDecorator()
        {
        }
       
        public string GetOption(string name)
        {
            try
            {
                string opt = this.options[name];
                return opt;
            }
            catch (Exception)
            {
                return "option not set";
            }
        }

        public AbstractDecorator SetOption(string name, string option)
        {
            try
            {
                this.options.Add(name, option);
            }
            catch (Exception)
            {
                try
                {
                    this.options[name] = option;
                }
                catch (Exception)
                {
                    this.options[name] = "";
                }
            }

            return this;
        }

        //
        //  dekoratorius prijungiamas prie nurodyto objecto
        //  targetObject tures si dekoratoriu
        //
        public void AssignDecorator(Printable targetObject)
        {
            this.decoratedOutput = targetObject;
        }

        public Printable GetDecoratedObject()
        {
            return this.decoratedOutput;
        }



        #region Printable Members

        public virtual string Print(string outputData)
        {
            if (decoratedOutput != null)
            {
                return decoratedOutput.Print(outputData);
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
