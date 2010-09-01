using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class OvertimeReportRow
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

        private List<OvertimeReportCell> _cells = new List<OvertimeReportCell>();
        public List<OvertimeReportCell> Cells
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
