using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
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
                return this.project_manager_id == userSession.workerID;
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


        public bool administationView()
        {
            return canBeSeen();
        }

        public bool administrationEdit()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (userSession.isAntanas())
            {
                return false;
            }
            else
            {
                return canBeSeen();
            }
        }

        public static bool administrationNew()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (!userSession.isAntanas())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool administrationDelete()
        {
            mvc.Common.UserSession userSession = new mvc.Common.UserSession();
            if (userSession.isAntanas())
            {
                return false;
            }
            else
            {
                return canBeSeen();
            }
        }

        public void makeBackup(int currentUserID)
        {

            if (Common.UserSession.BackupDeletedData)
            {
                BackupModelsDataContext backupDataContext = new BackupModelsDataContext();
                BackupProject backup = new BackupProject();
                backup.deleted_by_id = currentUserID;
                backup.deleted = DateTime.Now;
                backup.id = this.id;
                backup.project_manager_id = this.project_manager_id;
                backup.title = this.title;
                backupDataContext.SubmitChanges();
            }
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
}
