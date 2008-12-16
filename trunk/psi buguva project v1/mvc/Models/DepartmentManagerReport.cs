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
    public class DepartmentManagerReport
    {
        private Period _period = null;
        public Period Period
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

        private int _workersCount = 0;
        public int WorkersCount
        {
            get
            {
                return _workersCount;
            }
            set
            {
                _workersCount = value;
            }
        }


        private Department _department = null;
        public Department DepartmentInfo
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
            }
        }

        private bool _useChart = false;

        public bool ShowAsChart
        {
            get
            {
                return _useChart;
            }
            set
            {
                _useChart = value;
            }
        }

        public string DepartmentManagerTitle
        {
            get
            {
                if (DepartmentInfo != null)
                {
                    if (DepartmentInfo.Worker != null)
                    {
                        return DepartmentInfo.Worker.Fullname;
                    }
                }
                return "Nepaskirtas";
            }
        }

        private int _totalDepartmentWorked = 0;
        private int _workersOfDepartmentWorked = 0;
        private List<AssociatedWorkedHours> _hoursOfOthers = new List<AssociatedWorkedHours>();

        public List<AssociatedWorkedHours> WorkedHoursOfOthers
        {
            get
            {
                return _hoursOfOthers;
            }
            set
            {
                _hoursOfOthers = value;
            }
        }

        public int TotalDepartmentWorked
        {
            get
            {
                return _totalDepartmentWorked;
            }
            set
            {
                _totalDepartmentWorked = value;
            }
        }

        public int ThisDepartmentWorkersWorkedInDepartmentProjects
        {
            get
            {
                return _workersOfDepartmentWorked;
            }
            set
            {
                _workersOfDepartmentWorked = value;
            }
        }

        public int ThisDepartmentWorkersWorkedInOtherProjects
        {
            get
            {
                return TotalDepartmentWorked - ThisDepartmentWorkersWorkedInDepartmentProjects;
            }
        }

        public int OthersTotalWorked
        {
            get
            {
                return _hoursOfOthers.Sum(h => h.Hours);
            }
        }

        public int WorkedNoWhere
        {
            get
            {
                return (Period.TotalWorkHoursInPeriod * WorkersCount) - TotalDepartmentWorked;
            }
        }

        public string PercentNotWorked
        {
            get
            {
                if (Period.TotalWorkHoursInPeriod != 0)
                {
                    return (((double)WorkedNoWhere / ((double)Period.TotalWorkHoursInPeriod * WorkersCount)) * (double)100).ToString("0.00") + "%";
                }
                else
                {
                    return "0.00%";
                }
            }
        }
    }
}
