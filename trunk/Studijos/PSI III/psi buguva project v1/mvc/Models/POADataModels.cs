using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{                                                       
    partial class POADataModelsDataContext
    {
        private BackupModelsDataContext _backupDataContext = null;

        public BackupModelsDataContext BackupDB
        {
            get
            {
                if (_backupDataContext == null)
                {
                    if (mvc.Common.UserSession.BackupDeletedData)
                    {
                        _backupDataContext = new BackupModelsDataContext();
                    }
                }
                return _backupDataContext;
            }
        }


        public bool Install()
        {
            mvc.Installation.InstallationModelsDataContext installation = new mvc.Installation.InstallationModelsDataContext();
            return installation.Install();            
        }
        
        public List<Worker> GetWorkers()
        {
            return Workers.Where(c => c.deleted == null).ToList<Worker>().Where(u => u.administationView()).ToList();
        }

        public void AddWorker(Worker worker)
        {
            Workers.InsertOnSubmit(worker);
            this.SubmitChanges();
        }

        public void ClearDB(bool clearUsers, bool clearBackup)
        {
            if (mvc.Common.UserSession.BackupDeletedData && clearBackup)
            {
                BackupDB.ClearBackup();
            }
            Tasks.DeleteAllOnSubmit(Tasks);
            Projects.DeleteAllOnSubmit(Projects);
            WorkerStatus.DeleteAllOnSubmit(WorkerStatus);
            foreach (Worker worker in Workers)
            {
                worker.department_id = null;
            }
            Departments.DeleteAllOnSubmit(Departments);
            foreach (User user in Users)
            {
                user.worker_id = null;
            }
            Workers.DeleteAllOnSubmit(Workers);
            if (clearUsers)
            {
                Users.DeleteAllOnSubmit(Users);
            }
            this.SubmitChanges();
        }
    }
}
