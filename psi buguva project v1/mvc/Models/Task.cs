using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

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
                BackupTask backup = new BackupTask();
                backup.deleted_by_id = currentUserID;
                backup.deleted = DateTime.Now;
                backup.id = this.id;
                backup.year = this.year;
                backup.month = this.month;
                backup.project_id = this.project_id;
                backup.project_participant_id = this.project_participant_id;
                backup.worked_hours = this.worked_hours;
                backupDataContext.SubmitChanges();
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
