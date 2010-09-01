using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecordsEditor.EditorForm;
using RecordsEditor.EditorForm.Decorators;
using RecordsEditor.EditorForm.Elements;

namespace RecordsEditor
{
    public partial class Form1 : Form, Showable
    {
        public Form1()
        {
            InitializeComponent();



            // remove grazina ne void
            // klientas diba per Field
            // dekoratoriui padaryti strtatinius metodus, kuris valdo FIeld objekto dekoratorius
            // salinamas pirmas
            // dekoratoriai turi tureti papildomas funkcijas
            // idomesnius gavimo ir koregavimo veiksmus

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Field field = new Field("Forma 1");

            field.decorator.addDecorator("Question")
                .setOption("button1", "OK")
                .setOption("button2", "CANCEL");

            field.decorator.addDecorator("Header")
                .setOption("header", "Nauja forma 1");

            textBox1.Text = field.decorator.getDecoratorsList();

            field.render();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Field field = new Field("Forma 2");

            field.decorator.addDecorator("Question")
                .setOption("button1", "Forma 1")
                .setOption("button2", "CANCEL");

            field.decorator.addDecorator("Border");

            field.decorator.removeDecorator("Border");

            field.decorator.addDecorator("Header");

            ((Header)field.decorator.getDecorator("Header"))
                .setCaption("Forma 2nr.");

            ((Question)field.decorator.getDecorator("Question"))
                .button1.Click += new System.EventHandler(this.button1_Click);

            textBox1.Text = field.decorator.getDecoratorsList();

            field.render();
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Decorator decorator = new Decorator(this);

            decorator.addDecorator("Header");
            decorator.addDecorator("Border");

            decorator.dispalyDecoratedField();

            textBox1.Text = decorator.getDecoratorsList();
        }
    }
}
