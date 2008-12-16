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
    public class DepartmentProjectReport
    {
        private string _title = "";
        private string _managerTitle = "";
        private string _managerDepartment = "";
        private MonthOfYear _started = null;
        private MonthOfYear _ended = null;
        private int _totalWorked = 0;
        private int _worked = 0;
        private int _project_id = 0;
        private int _manager_id = 0;
        private int _department_id = 0;


        public int DepartmentID
        {
            get
            {
                return _department_id;
            }
            set
            {
                _department_id = value;
            }
        }

        public int ProjectID
        {
            get
            {
                return _project_id;
            }
            set
            {
                _project_id = value;
            }
        }

        public int ManagerID
        {
            get
            {
                return _manager_id;
            }
            set
            {
                _manager_id = value;
            }
        }

        public String Title
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


        public String Manager
        {
            get
            {
                return _managerTitle;
            }
            set
            {
                _managerTitle = value;
            }
        }

        public String ManagerDepartment
        {
            get
            {
                return _managerDepartment;
            }
            set
            {
                _managerDepartment = value;
            }
        }

        public MonthOfYear Started
        {
            get
            {
                return _started;
            }
            set
            {
                _started = value;
            }
        }

        public MonthOfYear Ended
        {
            get
            {
                return _ended;
            }
            set
            {
                _ended = value;
            }
        }

        public int TotalWorked
        {
            get
            {
                return _totalWorked;
            }
            set
            {
                _totalWorked = value;
            }
        }

        public int DepartmentWorkersWorked
        {
            get
            {
                return _worked;
            }
            set
            {
                _worked = value;
            }
        }

        public int OthersWorked
        {
            get
            {
                return TotalWorked - DepartmentWorkersWorked;
            }
        }
    }
}
