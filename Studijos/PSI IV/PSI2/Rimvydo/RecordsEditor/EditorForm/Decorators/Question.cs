using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordsEditor.EditorForm.Elements;
using System.Windows.Forms;

namespace RecordsEditor.EditorForm.Decorators
{
    class Question : AbstractDecorator
    {
        public Panel panel1 = new System.Windows.Forms.Panel();
        public Button button1 = new System.Windows.Forms.Button();
        public Button button2 = new System.Windows.Forms.Button();
        
        public Question(Showable decoratedField)
            : base(decoratedField)
        {
            this.init();
        }

        public Question()
            : base()
        {
            this.init();
        }

        private void init()
        {
            this.setOption("button1", "Yes");
            this.setOption("button2", "No");
        }

        public override void Show()
        {
            panel1.SuspendLayout();
            form.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 239);
            panel1.Name = "panel1_question";
            panel1.Size = new System.Drawing.Size(292, 27);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Dock = System.Windows.Forms.DockStyle.Right;
            button1.Location = new System.Drawing.Point(217, 0);
            button1.Name = "button1_question";
            button1.Size = new System.Drawing.Size(75, 27);
            button1.TabIndex = 0;
            button1.Text = this.getOption("button1");
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Dock = System.Windows.Forms.DockStyle.Right;
            button2.Location = new System.Drawing.Point(142, 0);
            button2.Name = "button2_question";
            button2.Size = new System.Drawing.Size(75, 27);
            button2.TabIndex = 1;
            button2.Text = this.getOption("button2");
            button2.UseVisualStyleBackColor = true;
            // 
            // Field
            // 
            form.Controls.Add(panel1);
            panel1.ResumeLayout(false);
            form.ResumeLayout(false);

            base.Show();
        }
    }
}
