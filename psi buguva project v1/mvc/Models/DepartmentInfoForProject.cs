using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class DepartmentInfoForProject
    {
        System.Collections.Generic.List<Models.WorkerAndHours> _workersResult;
        int _hours = 0;
        Department _department;
        public System.Collections.Generic.List<Models.WorkerAndHours> Workers
        {
            get
            {
                return _workersResult;
            }
            set
            {
                _workersResult = value;
            }
        }

        public Department Department
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

        public DepartmentInfoForProject(System.Collections.Generic.List<Models.WorkerAndHours> workers, Department department, int hours)
        {
            Hours = hours;
            Workers = workers;
            Department = department;
        }

    }
}
