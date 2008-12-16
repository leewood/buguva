using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class ProjectIntensivity
    {
        private MonthOfYear _period;
        private int _total;
        private int _projectsWorkers;
        private int _projectId;

        public MonthOfYear Period
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

        public int TotalWorkedHours
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
            }
        }

        public int ProjectsWorkersWorkedHours
        {
            get
            {
                return _projectsWorkers;
            }
            set
            {
                _projectsWorkers = value;
            }
        }

        public int OthersWorkedHours
        {
            get
            {
                return _total - _projectsWorkers;
            }
        }

        public int ProjectID
        {
            get
            {
                return _projectId;
            }
            set
            {
                _projectId = value;
            }
        }

        public ProjectIntensivity(MonthOfYear period, int projectID, int total, int workers)
        {
            Period = period;
            ProjectID = projectID;
            TotalWorkedHours = total;
            ProjectsWorkersWorkedHours = workers;
        }
    }
}
