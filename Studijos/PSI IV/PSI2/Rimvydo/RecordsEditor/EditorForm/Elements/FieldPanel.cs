using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RecordsEditor.EditorForm.Elements
{
    public partial class FieldPanel : Panel
    {
        public FieldPanel()
        {
            InitializeComponent();
        }

        public FieldPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
