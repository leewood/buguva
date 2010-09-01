using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSI2.ExtConsole
{
    class Decorator
    {
        Printable owner = null;

        public Decorator(Printable owner)
        {
            this.owner = owner;
        }

        public Decorators.AbstractDecorator AddDecorator(string name)
        {
            Type type = Type.GetType("PSI2.ExtConsole.Decorators." + name, true);
            object newInstance = Activator.CreateInstance(type);

            if (newInstance is Decorators.AbstractDecorator)
            {
                return AddDecorator((Decorators.AbstractDecorator)newInstance);
            }
            else
            {
                throw new Exception(name + " is not AbtractDecorator");
            }
        }

        public Decorators.AbstractDecorator AddDecorator(Decorators.AbstractDecorator field)
        {            
            field.AssignDecorator(owner);            
            this.owner = field;
            return field;
        }

        public Decorators.AbstractDecorator GetDecorator(string name)
        {
            Printable decoratedObj = this.owner;

            while (decoratedObj != null)
            {
                if (decoratedObj is Decorators.AbstractDecorator)
                {
                    Decorators.AbstractDecorator abstractObj = (Decorators.AbstractDecorator)decoratedObj;

                    if (abstractObj.GetType().Name == name)
                    {
                        return abstractObj;
                    }

                    decoratedObj = abstractObj.GetDecoratedObject();
                }                
                else
                {
                    return null;
                }

            }

            return null;
        }

        public void RemoveDecorator(string name)
        {
            if (owner is Decorators.AbstractDecorator == false)
            {
                throw new Exception("jokie dekoratoriai nepriskirti");
            }

            if (owner.GetType().Name == name)
            {
                owner = ((Decorators.AbstractDecorator)owner).GetDecoratedObject();
            }
            else
            {
                Printable top = owner;
                Printable checking = ((Decorators.AbstractDecorator)owner).GetDecoratedObject();

                while (checking != null && checking is Decorators.AbstractDecorator)
                {
                    if (checking.GetType().Name == name)
                    {
                        ((Decorators.AbstractDecorator)top).AssignDecorator(
                            ((Decorators.AbstractDecorator)checking).GetDecoratedObject()
                        );

                        return;
                    }

                    top = checking;
                    checking = ((Decorators.AbstractDecorator)checking).GetDecoratedObject();
                }

                throw new Exception("dekoratorus nerastas");
            }
        }

        public string GetDecoratorsList()
        {
            Printable decoratedObj = this.owner;

            string list = "";

            while (decoratedObj != null)
            {
                if (decoratedObj is Decorators.AbstractDecorator)
                {
                    list += decoratedObj.GetType().Name + "; ";
                    decoratedObj = ((Decorators.AbstractDecorator)decoratedObj).GetDecoratedObject();
                }
                else
                {
                    decoratedObj = null;
                }
            }

            return list;
        }


        public void PrintDecoratedField(string data)
        {
            System.Console.Write(owner.Print(data));
        }
    }
}
