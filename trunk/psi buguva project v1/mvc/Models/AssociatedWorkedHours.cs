using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using mvc.Common;
using mvc.Models;
using LinqToSqlExtensions;

namespace mvc.Models
{
    public class AssociatedWorkedHours
    {
        private int _hours = 0;
        private string _title = "";
        public int Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                _hours = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        private int _associationID = 0;

        public int AssociationID
        {
            get
            {
                return _associationID;
            }
            set
            {
                _associationID = value;
            }

        }

        public AssociatedWorkedHours(string title, int hours, int associationID)
        {
            Title = title;
            Hours = hours;
            AssociationID = associationID;
        }

    }
}
