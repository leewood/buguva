using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class IncompleteWorkValueReportCell
    {
        private double _value = 0;
        private double _income = 0;

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public double Income
        {
            get
            {
                return _income;
            }
            set
            {
                _income = value;
            }
        }
        public double Difference
        {
            get
            {
                return Value - Income;
            }
        }
        public string Percent
        {
            get
            {
                if (Value > 0)
                {
                    return (((double)(Difference) / ((double)Value)) * 100.00).ToString("0.00");
                }
                else
                {
                    return "-";
                    
                }
            }
        }
    }

    public class IncompleteWorkValueReportRow
    {
        private string _period = "";
        public string Period
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

        private List<IncompleteWorkValueReportCell> _cells = new List<IncompleteWorkValueReportCell>();
        public List<IncompleteWorkValueReportCell> Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
            }
        }
    }

    public class IncompleteWorkValueReport
    {
        private List<string> _columnCaptions = new List<string>();
        private List<string> _columnActions = new List<string>();
        private List<System.Web.Routing.RouteValueDictionary> _columnRedirections = new List<System.Web.Routing.RouteValueDictionary>();
        private List<IncompleteWorkValueReportRow> _rows = new List<IncompleteWorkValueReportRow>();
        public List<string> Captions
        {
            get
            {
                return _columnCaptions;
            }
            set
            {
                _columnCaptions = value;
            }
        }


        public List<System.Web.Routing.RouteValueDictionary> Redirections
        {
            get
            {
                return _columnRedirections;
            }
            set
            {
                _columnRedirections = value;
            }
        }

        public List<string> Actions
        {
            get
            {
                return _columnActions;
            }
            set
            {
                _columnActions = value;
            }
        }

        public List<IncompleteWorkValueReportRow> Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }
    }

    public class OvertimeReportCell
    {
        private int _timeSum = 0;
        private int _timeNormal = 0;

        public int TimeSum
        {
            get
            {
                return _timeSum;
            }
            set
            {
                _timeSum = value;
            }
        }
        public int TimeNormal
        {
            get
            {
                return _timeNormal;
            }
            set
            {
                _timeNormal = value;
            }
        }
        public int TimeOvertime
        {
            get
            {
                if (TimeNormal < TimeSum)
                {
                    return TimeSum - TimeNormal;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string PercentUndertime
        {
            get
            {
                if (TimeSum < TimeNormal)
                {
                    return ((((double)TimeNormal - (double)TimeSum) / (double)TimeNormal) * 100.00).ToString("0.00");
                }
                else
                {
                    return "-";
                }
            }
        }
        public string PercentOvertime
        {
            get
            {
                if (TimeOvertime > 0)
                {
                    return (((double)TimeOvertime / (double)TimeNormal) * 100.00).ToString("0.00");
                }
                else
                {
                    return "-";
                }
            }
        }
    }

    public class OvertimeReportRow
    {
        private string _period = "";
        public string Period
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

        private List<OvertimeReportCell> _cells = new List<OvertimeReportCell>();
        public List<OvertimeReportCell> Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
            }
        }
    }

    public class OvertimeReport
    {
        private List<string> _columnCaptions = new List<string>();
        private List<string> _columnActions = new List<string>();
        private List<System.Web.Routing.RouteValueDictionary> _columnRedirections = new List<System.Web.Routing.RouteValueDictionary>();
        private List<OvertimeReportRow> _rows = new List<OvertimeReportRow>();

        public List<string> Captions
        {
            get
            {
                return _columnCaptions;
            }
            set
            {
                _columnCaptions = value;
            }
        }

        public List<System.Web.Routing.RouteValueDictionary> Redirections
        {
            get
            {
                return _columnRedirections;
            }
            set
            {
                _columnRedirections = value;
            }
        }

        public List<string> Actions
        {
            get
            {
                return _columnActions;
            }
            set
            {
                _columnActions = value;
            }
        }

        public List<OvertimeReportRow> Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }
    }
    
    public partial class WorkerStatus
    {
        public ErrorSummary Validate()
        {
            ValidatorRunner vr = new ValidatorRunner(true, new CachedValidationRegistry());
            if (!vr.IsValid(this))
                return vr.GetErrorSummary(this);
            else
                return null;
        }
    }

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

    public class MonthOfYear : IComparable
    {
        private int _year = 0;
        private int _month = 1;

        public static string getMonthName(int month)
        {
            switch (month)
            {
                case 1: return "Sausis";
                case 2: return "Vasaris";
                case 3: return "Kovas";
                case 4: return "Balandis";
                case 5: return "Geguþë";
                case 6: return "Birþelis";
                case 7: return "Liepa";
                case 8: return "Rugpjûtis";
                case 9: return "Rugsëjis";
                case 10: return "Spalis";
                case 11: return "Lapkritis";
                case 12: return "Groudis";

            }
            return "";
        }

        public static string getMonth(int month)
        {
            string result = month.ToString();
            if (result.Length < 2)
            {
                result = "0" + result;
            }
            return result;
        }


        public string ShortName()
        {
            return getMonthName(this.Month);
        }

        public static SelectList monthsList(object selected)
        {
            System.Collections.Generic.Dictionary<string, int> list = new Dictionary<string, int>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(getMonthName(i), i);
            }
            return new SelectList(list, "Value", "Key", selected);
        }

        public static SelectList monthsListUnTitled(object selected)
        {
            System.Collections.Generic.Dictionary<string, int> list = new Dictionary<string, int>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(getMonth(i), i);
            }
            return new SelectList(list, "Value", "Key", selected);
        }


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
