using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
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

        public void makeBackup(int currentUserID)
        {

            if (Common.UserSession.BackupDeletedData)
            {
                BackupModelsDataContext backupDataContext = new BackupModelsDataContext();
                BackupWorkerStatus backup = new BackupWorkerStatus();
                backup.deleted_by_id = currentUserID;
                backup.deleted = DateTime.Now;
                backup.id = this.id;
                backup.year = this.year;
                backup.month = this.month;
                backup.status = this.status;
                backup.worker_id = this.worker_id;
                backupDataContext.BackupWorkerStatus.InsertOnSubmit(backup);
                backupDataContext.SubmitChanges();
            }
        }
    }
}
