using System;
using System.Collections.Generic;
using System.Linq;

namespace mvc.Models
{
    public class MonthOfYear: IComparable
    {
        private int _year = 0;
        private int _month = 1;

        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }

        public int Month
        {
            get
            {
                return _month;
            }
            set
            {
                if ((value < 13) && (value > 0))
                {
                    _month = value;
                }
                else
                {
                    //throw new FormatException("Month must be between 1 and 12");
                }
            }
        }


        public MonthOfYear()
        {
            Year = DateTime.Today.Year;
            Month = DateTime.Today.Month;
        }

        public MonthOfYear(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public MonthOfYear(int month)
        {
            Year = DateTime.Today.Year;
            Month = month;
        }

        public override string ToString()
        {
            string yearString = Year.ToString();
            string monthString = Month.ToString();
            if (monthString.Length < 2)
            {
                monthString = "0" + monthString;
            }
            while (yearString.Length < 4)
            {
                yearString = "0" + yearString;
            }
            return yearString + "-" + monthString;
        }

        public override bool Equals(object obj)
        {
            try
            {
                MonthOfYear toCompareWith = (MonthOfYear)obj;
                return ((this.Year == toCompareWith.Year) && (this.Month == toCompareWith.Month));
            }
            catch (Exception)
            {
                return base.Equals(obj);
            }
        }

        public int CompareTo(object o)
        {
            try
            {
                MonthOfYear toCompareWith = (MonthOfYear)o;
                if (this.Year < toCompareWith.Year)
                {
                    return -1;
                }
                else if (this.Year > toCompareWith.Year)
                {
                    return 1;
                }
                else if (this.Month < toCompareWith.Month)
                {
                    return -1;
                }
                else if (this.Month > toCompareWith.Month)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }
    }

    public class TasksAndMonths
    {
        private Common.IPagedList<Task> _tasks = null;
        private List<MonthOfYear> _months = null;

        public Common.IPagedList<Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }

        public List<MonthOfYear> Months
        {
            get
            {
                return _months;
            }
            set
            {
                _months = value;
            }
        }

        private MonthOfYear _currentMonth = null;
        public MonthOfYear CurrentMonth
        {
            get
            {
                return _currentMonth;
            }
            set
            {
                _currentMonth = value;
            }
        }

        private int _projectID = 0;
        public int ProjectID
        {
            get
            {
                return _projectID;
            }
            set
            {
                _projectID = value;
            }
        }

        public TasksAndMonths(Common.IPagedList<Task> tasks, List<MonthOfYear> months, MonthOfYear current, int projectID)
        {
            Tasks = tasks;
            Months = months;
            ProjectID = projectID;
            CurrentMonth = current;
        }
    }

    partial class Project
    {
        public Task FirstTask
        {
            get
            {
                if (this.Tasks.Count > 0)
                {
                    return this.Tasks.OrderBy(t => (t.year * 12 + t.month)).First();
                }
                else
                {
                    return null;
                }
            }
        }

        public Task LastTask
        {
            get
            {
                if (this.Tasks.Count > 0)
                {
                    return this.Tasks.OrderByDescending(t => (t.year * 12 + t.month)).First();
                }
                else
                {
                    return null;
                }
            }

        }

        public String StartedAt
        {
            get
            {
                if (FirstTask != null)
                {
                    return (new MonthOfYear(FirstTask.year, FirstTask.month)).ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public String EndedAt
        {
            get
            {
                if (LastTask != null)
                {
                    return (new MonthOfYear(LastTask.year, LastTask.month)).ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public int TotalWorkedHours
        {
            get
            {
                if (this.Tasks.Count > 0)
                {
                    return this.Tasks.Sum(t => t.worked_hours);
                }
                else
                {
                    return 0;
                }
            }
        }

    }

    partial class Worker
    {
        public List<MonthOfYear> workedMonthsInProject(int project_id)
        {
            List<MonthOfYear> result = new List<MonthOfYear>();
            List<Task> tasks;
            if (this.Tasks.Count > 0)
            {
                tasks = this.Tasks.Where(task => task.project_id == project_id).ToList();
            }
            else
            {
                tasks = new List<Task>();
            }
            if (tasks.Count > 0)
                foreach (Task task in tasks)
                {
                    MonthOfYear current = new MonthOfYear(task.year, task.month);
                    if (result.IndexOf(current) < 0)
                    {
                        result.Add(current);
                    }
                }
            result.Sort();
            return result;
        }

        public String Fullname
        {
            get
            {
                return this.name + " " + this.surname;
            }
        }
    }

    partial class POADataModelsDataContext
    {
        public void get()
        {            
        }
        public List<Worker> GetWorkers()
        {
            return Workers.Where(c => c.deleted == null).ToList<Worker>();
        }

        public void AddWorker(Worker worker)
        {
            Workers.InsertOnSubmit(worker);
            this.SubmitChanges();
        }
    }
}
