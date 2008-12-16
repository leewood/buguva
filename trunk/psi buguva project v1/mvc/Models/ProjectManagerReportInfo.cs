using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class ProjectManagerReportInfo
    {
        Project _project;
        System.Collections.Generic.List<Models.DepartmentInfoForProject> _departmentsInfo;
        int _totalHours = 0;
        int _workersCount = 0;
        public int TotalCountOfWorkers
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

        public Project Project
        {
            get
            {
                return _project;
            }
            set
            {
                _project = value;
            }
        }

        public System.Collections.Generic.List<Models.DepartmentInfoForProject> DepartmentsInfo
        {
            get
            {
                return _departmentsInfo;
            }
            set
            {
                _departmentsInfo = value;
            }
        }

        public int TotalWorkedHours
        {
            get
            {
                return _totalHours;
            }
            set
            {
                _totalHours = value;
            }
        }

        public ProjectManagerReportInfo(Project project, int totalHours, System.Collections.Generic.List<Models.DepartmentInfoForProject> departmentsInfo, int workersCount)
        {
            Project = project;
            TotalCountOfWorkers = workersCount;
            TotalWorkedHours = totalHours;
            DepartmentsInfo = departmentsInfo;
        }

    }
}
