using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
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
            if (userSession.isAdministrator() || userSession.isDepartmentMaster())
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
                BackupWorker backup = new BackupWorker();
                backup.deleted_by_id = currentUserID;
                backup.deleted = DateTime.Now;
                backup.id = this.id;
                backup.department_id = this.department_id;
                backup.name = this.name;
                backup.surname = this.surname;                
                backupDataContext.SubmitChanges();
            }
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
}
