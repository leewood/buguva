using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecordsEditor.EditorForm.Elements;

namespace RecordsEditor.EditorForm.Decorators
{
    class Border : AbstractDecorator
    {
        public Border(Showable decoratedField) 
            : base(decoratedField)
        {
        }

        public Border() : base()
        {
        }

        public override void Show()
        {
            System.Windows.Forms.GroupBox groupBox1 = new System.Windows.Forms.GroupBox();

            form.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(292, 266);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Remelis";
            // 
            // Field
            // 
            form.Controls.Add(groupBox1);
            form.ResumeLayout(false);

            base.Show();
        }
    }
}
