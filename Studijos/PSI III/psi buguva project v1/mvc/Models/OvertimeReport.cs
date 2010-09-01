using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class OvertimeReport
    {
        private List<string> _columnCaptions = new List<string>();
        private List<string> _columnActions = new List<string>();
        private List<System.Web.Routing.RouteValueDictionary> _columnRedirections = new List<System.Web.Routing.RouteValueDictionary>();
        private List<OvertimeReportRow> _rows = new List<OvertimeReportRow>();

        public List<string> Captions
        {
            get
            {
                return _columnCaptions;
            }
            set
            {
                _columnCaptions = value;
            }
        }

        public List<System.Web.Routing.RouteValueDictionary> Redirections
        {
            get
            {
                return _columnRedirections;
            }
            set
            {
                _columnRedirections = value;
            }
        }

        public List<string> Actions
        {
            get
            {
                return _columnActions;
            }
            set
            {
                _columnActions = value;
            }
        }

        public List<OvertimeReportRow> Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }
    }
}
