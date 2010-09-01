using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecordsEditor.EditorForm;
using RecordsEditor.EditorForm.Elements;


namespace RecordsEditor.EditorForm.Decorators
{
    abstract class AbstractDecorator : Showable
    {
        protected Showable decoratedField = null;

        protected Control form = null;

        protected Dictionary<string, string> options = new Dictionary<string, string>();

        public AbstractDecorator(Showable decoratedField)
        {
            this.decoratedField = decoratedField;
        }

        public AbstractDecorator()
        {
        }

        public void setForm(Control form)
        {
            this.form = form;
        }

        public virtual void Show()
        {
            if (this.decoratedField != null)
                this.decoratedField.Show();
        }
        
        public string getOption(string name)
        {
            try
            {
                string opt = this.options[name];
                return opt;
            }
            catch (Exception e)
            {
                return "option not set";
            }
        }

        public AbstractDecorator setOption(string name, string option)
        {
            try
            {
                this.options.Add(name, option);
            }
            catch (Exception e1)
            {
                try
                {
                    this.options[name] = option;
                }
                catch (Exception e2)
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
        public void assignDecorator(Showable targetObject)
        {
            this.decoratedField = targetObject;
        }

        public Showable getDecoratedObject()
        {
            return this.decoratedField;
        }
    }
}
