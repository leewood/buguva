using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class IncompleteWorkValueReportRow
    {
        private string _period = "";
        public string Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
            }
        }

        private List<IncompleteWorkValueReportCell> _cells = new List<IncompleteWorkValueReportCell>();
        public List<IncompleteWorkValueReportCell> Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
            }
        }
    }
}
