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

        public int _couldWork = 0;

        public int CouldWorked
        {
            get
            {
                return _couldWork;
            }
            set
            {
                _couldWork = value;
            }
        }

        public int NotWorked
        {
            get
            {
                int total = CouldWorked - Hours;
                return (total > 0) ? total : 0;
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

        public AssociatedWorkedHours(string title, int hours, int total, int associationID)
        {
            Title = title;
            Hours = hours;
            CouldWorked = total;
            AssociationID = associationID;
        }


    }
}
