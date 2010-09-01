using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecordsEditor.EditorForm.Decorators;

namespace RecordsEditor.EditorForm.Elements
{
    class Field : Form, Showable
    {
        public Decorator decorator = null;
    
        public Field(string name)
        {
            this.Text = name;
            this.decorator = new Decorator(this);
        }

        public void render()
        {
            this.decorator.dispalyDecoratedField();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Field
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "Field";
            this.ResumeLayout(false);

        }

    }
}
