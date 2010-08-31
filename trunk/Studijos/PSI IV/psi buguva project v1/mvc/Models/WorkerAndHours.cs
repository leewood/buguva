using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class WorkerAndHours
    {
        Worker _worker;
        int _hours = 0;
        public Worker Worker
        {
            get
            {
                return _worker;
            }
            set
            {
                _worker = value;
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

        public WorkerAndHours(Worker worker, int hours)
        {
            Hours = hours;
            Worker = worker;
        }
    }
}
