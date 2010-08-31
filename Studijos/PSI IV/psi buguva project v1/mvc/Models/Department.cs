using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
    public class MyDepartmentFirstComparer : IComparer<Project>
    {

        private int department_id = 0;
        public MyDepartmentFirstComparer(int department)
        {
            department_id = department;
        }

        public int Compare(Project x, Project y)
        {
            if ((x.Worker.department_id == department_id) && (y.Worker.department_id != department_id))
            {
                return -1;
            }
            else if ((x.Worker.department_id != department_id) && (y.Worker.department_id == department_id))
            {
                return 1;
            }
            else if ((x.Worker.department_id != department_id) && (y.Worker.department_id != department_id))
            {
                if (x.Worker.department_id > y.Worker.department_id)
                {
                    return -1;
                }
                else if (x.Worker.department_id < y.Worker.department_id)
                {
                    return 1;
                }
                else
                {
                    if (x.id > y.id)
                    {
                        return -1;
                    }
                    else if (x.id < y.id)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                if (x.id > y.id)
                {
                    return -1;
                }
                else if (x.id < y.id)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

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

        public string ManagerName
        {
            get
            {
                return ((Worker != null) && (Worker.deleted.HasValue == false)) ? Worker.Fullname : "Nepaskirtas";
            }
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
            if (userSession.isAdministrator())
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
                return userSession.isAdministrator();
            }
        }

        public void makeBackup(int currentUserID)
        {

            if (Common.UserSession.BackupDeletedData)
            {
                BackupModelsDataContext backupDataContext = new BackupModelsDataContext();
                BackupDepartment backup = new BackupDepartment();
                backup.deleted_by_id = currentUserID;
                backup.deleted = DateTime.Now;
                backup.id = this.id;
                backup.headmaster_id = this.headmaster_id;
                backup.title = this.title;
                backupDataContext.BackupDepartments.InsertOnSubmit(backup);
                backupDataContext.SubmitChanges();
            }
        }

        public int workedMonthsInPeriod(Period period)
        {
            int result = 0;
            foreach (Worker worker in this.Workers)
            {
                result += worker.workedMonthsInPeriod(period);
            }
            return result;
        }

        public int couldWorkedInPeriod(Period period)
        {
            return workedMonthsInPeriod(period) * 160;
        }

        public int TotalWorkedMonths
        {
            get
            {
                return this.Workers.ToList().Sum(w => w.TotalWorkedMonths);
            }
        }

        public Models.Department getDepartment(int workerId)
        {
            POADataModelsDataContext data = new POADataModelsDataContext();
            return data.Workers.Where(Worker => (Worker.id == workerId)).First().Department;
        }
    }
}
