using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/**
 * 
 * class: Debugger
 * 
 * Author: R.
 * 
 * Info: grazina pranesimus i master page
 * 
 */

namespace mvc.Common
{
    public class Debugger
    {
        //--------------------------------------------------------------------------

        static readonly Debugger instance = new Debugger();

        public string messages;

        private bool displayExceptions = true;

        private bool displaMessages = true;

        //--------------------------------------------------------------------------

        public static Debugger Instance
        {
            get
            {
                return instance;
            }
        }

        public Debugger()
        {
            
        }

        private void addLine(string line)
        {
            this.messages += line + "<br /> \n";
        }

        public void addException(string message)
        {
            if (this.displayExceptions)
                this.addLine("<b><i>Sugauta išimtis</i><b/> > " + message);
        }

        public void addMessage(string message)
        {
            if (this.displaMessages)
                this.addLine("<b><i>Pranešimas</i><b/> > " + message);
        }

        public bool empty()
        {
            return (this.messages.Length == 0) ? true : false;
        }

        public override string ToString()
        {
            return "<font color='#FFF'>" + this.messages + "</font> \n";
        }

        public void clear()
        {
            this.messages = "";
        }
    }
}
