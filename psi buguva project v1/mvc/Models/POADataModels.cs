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
                    return ( ((double)TimeOvertime / (double)TimeNormal) * 100.00).ToString("0.00");
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

    public partial class User
    {
        public static string[] LevelNames = { "Paprastas darbuotojas", "Skyriaus vadovas", "Administratorius", "Antanas" };

        public SelectList LevelsList
        {
            get
            {
                System.Collections.Generic.Dictionary<string, int> list = new Dictionary<string, int>();
                for (int i = 0; i < LevelNames.Length; i++)
                {
                    list.Add(LevelNames[i], i + 1);
                }
                int selectedLevel = level;
                if (level == 0)
                {
                    selectedLevel = 1;
                }
                SelectList result = new SelectList(list, "Value", "Key", selectedLevel);
                return result;
            }
        }

        public bool canBeSeen()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (userSession.isSimpleUser())
            {
                if (userSession.userId != this.id)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (userSession.isAdministrator())
            {
                return true;
            }
            else if (userSession.isAntanas())
            {
                return true;
            }
            else if (userSession.isDepartmentMaster())
            {
                if (this.Worker == null)
                {
                    return false;
                }
                else 
                {
                    if (this.Worker.Department == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (this.Worker.Department.Worker == null)
                        {
                            return false;
                        }
                        else
                        {
                            return this.Worker.Department.Worker.id == userSession.workerID;
                        }
                      
                    }
                }
            }
            return false;
        }

        private string _repeatPass = null;
        [ValidateSameAs("password", "Slapta�odis ir pakartotasis slapta�odis turi sutapti")]        
        public string repeated_password
        {
            get
            {
                return _repeatPass;
            }
            set
            {
                _repeatPass = value;
            }
        }

        private string _newRepeatPass = null;
        private string _newPass = null;


        public string new_password
        {
            get
            {
                return _newPass;
            }
            set
            {
                _newPass = value;
            }
        }


        [ValidateSameAs("new_password", "Naujasis slapta�odis ir pakartotasis slapta�odis turi sutapti")]        
        public string new_repeated_password
        {
            get
            {
                return _newRepeatPass;
            }
            set
            {
                _newRepeatPass = value;
            }
        }




        public string LevelName
        {
            get
            {
                if ((this.level <= LevelNames.Length) && (this.level > 0))
                {
                    return LevelNames[this.level - 1];
                }
                else
                {
                    return "Lygis " + level.ToString();
                }
            }
        }


        public ErrorSummary Validate()
        {
            ValidatorRunner vr = new ValidatorRunner(true, new CachedValidationRegistry());
            if (!vr.IsValid(this))
                return vr.GetErrorSummary(this);
            else
                return null;
        }
    }

    public partial class Task
    {
        public string FullMonthName
        {
            get
            {
                return (new MonthOfYear(this.year, this.month)).ToString();
            }
        }

        public bool canBeSeen()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (userSession.isAntanas() || userSession.isAdministrator())
            {
                return true;
            }
            else if (userSession.isSimpleUser())
            {
                if (this.Worker != null)
                {
                    return this.Worker.id == userSession.workerID;
                }
            }
            else if (userSession.isDepartmentMaster())
            {
                if ((this.Worker != null) && (this.Worker.Department != null) && (this.Worker.Department.Worker != null))
                {
                    return this.Worker.Department.Worker.id == userSession.workerID;
                }
            }
            return false;
        }


        public ErrorSummary Validate()
        {
            ValidatorRunner vr = new ValidatorRunner(true, new CachedValidationRegistry());
            if (!vr.IsValid(this))
                return vr.GetErrorSummary(this);
            else
                return null;
        }
    }

    public partial class Department
    {
        public ErrorSummary Validate()
        {
            ValidatorRunner vr = new ValidatorRunner(true, new CachedValidationRegistry());
            if (!vr.IsValid(this))
                return vr.GetErrorSummary(this);
            else
                return null;
        }

        public bool canBeSeen()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (userSession.isAntanas() || userSession.isAdministrator())
            {
                return true;
            }
            else if (userSession.isSimpleUser())
            {
                return false;
            }
            else if (userSession.isDepartmentMaster())
            {
                if (this.Worker == null)
                {
                    return false;
                }
                else
                {
                    return this.Worker.id == userSession.workerID;
                }
            }
            return false;
        }


        public Models.Department getDepartment(int workerId)
        {
            POADataModelsDataContext data = new POADataModelsDataContext();
            return data.Workers.Where(Worker => (Worker.id == workerId)).First().Department;
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

    public class MonthOfYear: IComparable
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
                case 5: return "Gegu��";
                case 6: return "Bir�elis";
                case 7: return "Liepa";
                case 8: return "Rugpj�tis";
                case 9: return "Rugs�jis";
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

    public partial class Project
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

        public bool canBeSeen()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (userSession.isAntanas() || userSession.isAdministrator())
            {
                return true;
            }
            else if (userSession.isSimpleUser())
            {
                return false;
            }
            else if (userSession.isDepartmentMaster())
            {
                if (this.Worker != null)
                {
                    if (this.Worker.Department != null)
                    {
                        if (this.Worker.Department.Worker != null)
                        {
                            return this.Worker.Department.Worker.id == userSession.workerID;
                        }
                    }
                }
            }
            return false;
        }


        public int Length
        {
            get
            {
                if ((FirstTask != null) && (LastTask != null))
                {
                    Task last = LastTask;
                    Task first = FirstTask;
                    return last.year * 12 + last.month - first.year * 12 - first.month + 1;
                }
                return 0;
            }
        }

        public int MiddleMonth
        {
            get
            {
                int length = Length;
                int middle = length / 2 + length % 2;
                Task firstTask = FirstTask;
                if (firstTask != null)
                {
                    return firstTask.year * 12 + firstTask.month + middle - 1;
                }
                return middle;
            }
        }

        public int Month8
        {
            get
            {
                Task firstTask = FirstTask;
                if (firstTask != null)
                {
                    return firstTask.year * 12 + firstTask.month + 7;
                }
                return 8;

            }
        }


        public int Month15
        {
            get
            {
                Task firstTask = FirstTask;
                if (firstTask != null)
                {
                    return firstTask.year * 12 + firstTask.month + 14;
                }
                return 15;

            }
        }

        public int LastMonth
        {
            get
            {
                Task lastTask = LastTask;
                if (lastTask != null)
                {
                    return lastTask.year * 12 + lastTask.month;
                }
                return 0;
            }
        }

        public int FullIncome
        {
            get
            {
                if (this.Tasks.Count > 0)
                {
                    return this.Tasks.Sum(t => t.worked_hours);
                }
                return 0;
            }
        }

        public List<MonthOfYear> workedMonthsInProject()
        {
            List<MonthOfYear> result = new List<MonthOfYear>();
            List<Task> tasks;
            if (this.Tasks.Count > 0)
            {                
                tasks = this.Tasks.ToList();
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

        public ErrorSummary Validate()
        {
            ValidatorRunner vr = new ValidatorRunner(true, new CachedValidationRegistry());
            if (!vr.IsValid(this))
                return vr.GetErrorSummary(this);
            else
                return null;
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

    public partial class Worker
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

        public bool canBeSeen()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (userSession.isAntanas() || userSession.isAdministrator())
            {
                return true;
            }
            else if (userSession.isSimpleUser())
            {
                return this.id == userSession.workerID;
            }
            else if (userSession.isDepartmentMaster())
            {
                if (this.Department == null)
                {
                    return false;
                }
                else if (this.Department.Worker == null)
                {
                    return false;
                }
                else
                {
                    return this.Department.Worker.id == userSession.workerID;
                }
            }
            return false;
        }

        public List<MonthOfYear> workedMonthsInProject(int project_id, int workerID)
        {
            List<MonthOfYear> result = new List<MonthOfYear>();
            List<Task> tasks;            
            if (this.Tasks.Count > 0)
            {
                tasks = this.Tasks.Where(task => (task.project_id == project_id) && (task.project_participant_id == workerID)).ToList();
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


        public ErrorSummary Validate()
        {
            ValidatorRunner vr = new ValidatorRunner(true, new CachedValidationRegistry());
            if (!vr.IsValid(this))
                return vr.GetErrorSummary(this);
            else
                return null;
        }
    }

    public partial class POADataModelsDataContext
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
