using System;
using System.Collections.Generic;
using System.Linq;

namespace mvc.Models
{

    public partial class Task
    {
        public string FullMonthName
        {
            get
            {
                return (new MonthOfYear(this.year, this.month)).ToString();
            }
        }
    }


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

    partial class Worker
    {
        public bool hasTasksInProject(int project_id)
        {

            if (this.Tasks.Count() > 0)
            {
                return (this.Tasks.Where(task => task.project_id == project_id).Count() > 0);
            }
            else
            {
                return false;
            }
        }


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
