using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RecordsEditor.EditorForm.Decorators
{
    class Header : AbstractDecorator
    {
        public Header(Showable decoratedField)
            : base(decoratedField)
        {
            init();
        }

        public Header()
            : base()
        {
            init();
        }

        private void init()
        {
            this.setOption("header", "headris nenurodytas");
        }

        public override void Show()
        {
            this.form.Text = this.getOption("header");
            base.Show();
        }

        public void setCaption(string caption)
        {
            this.setOption("header", caption);
        }
    }
}
