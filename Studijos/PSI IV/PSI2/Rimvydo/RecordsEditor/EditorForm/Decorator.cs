using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordsEditor.EditorForm.Decorators;
using RecordsEditor.EditorForm.Elements;
using System.Windows.Forms;

namespace RecordsEditor.EditorForm
{
    class Decorator
    {
        private Showable owner = null;

        private Control form = null;
        
        public Decorator(Control owner)
        {
            this.owner = (Showable)owner;
            this.form = owner;
        }

        public AbstractDecorator addDecorator(string name)
        {
            Type type = Type.GetType("RecordsEditor.EditorForm.Decorators." + name, true);
            object newInstance = Activator.CreateInstance(type);

            if (newInstance is AbstractDecorator)
            {
                return addDecorator((AbstractDecorator)newInstance);
            }
            else
            {
                throw new Exception(name + " is not AbtractDecorator");
            }
        }

        public AbstractDecorator addDecorator(AbstractDecorator field)
        {
            field.assignDecorator(owner);
            field.setForm(this.form);
            this.owner = field;
            return field;
        }

        public AbstractDecorator getDecorator(string name)
        {
            Showable decoratedObj = this.owner;

            while (decoratedObj != null)
            {
                if (decoratedObj is AbstractDecorator)
                {
                    AbstractDecorator abstractObj = (AbstractDecorator)decoratedObj;

                    if (abstractObj.GetType().Name == name)
                    {
                        return abstractObj;
                    }

                    decoratedObj = abstractObj.getDecoratedObject();
                }
                else
                {
                    throw new Exception("neatpazystas dekoratorius arba objektas");
                }

            }

            throw new Exception("programavimo klaida: dekoratoriaus nera");
        }

        public void removeDecorator(string name)
        {
            if (owner is AbstractDecorator == false)
            {
                throw new Exception("jokie dekoratoriai nepriskirti");
            }

            if (owner.GetType().Name == name)
            {
                owner = ((AbstractDecorator)owner).getDecoratedObject();
            }
            else
            {
                Showable top = owner;
                Showable checking = ((AbstractDecorator)owner).getDecoratedObject();

                while (checking != null && checking is AbstractDecorator)
                {
                    if (checking.GetType().Name == name)
                    {
                        ((AbstractDecorator)top).assignDecorator(
                            ((AbstractDecorator)checking).getDecoratedObject()
                        );

                        return;
                    }

                    top = checking;
                    checking = ((AbstractDecorator)checking).getDecoratedObject();
                }

                throw new Exception("dekoratorus nerastas");
            }
        }

        public string getDecoratorsList()
        {
            Showable decoratedObj = this.owner;

            string list = "decorators: ";

            while (decoratedObj != null)
            {
                if (decoratedObj is AbstractDecorator)
                {
                    list += decoratedObj.GetType().Name + "; ";
                    decoratedObj = ((AbstractDecorator)decoratedObj).getDecoratedObject();
                }
                else
                {
                    decoratedObj = null;
                }
            }

            return list;
        }

        public Control getForm()
        {
            return this.form;
        }

        public void dispalyDecoratedField()
        {
            owner.Show();
        }
    }
}
