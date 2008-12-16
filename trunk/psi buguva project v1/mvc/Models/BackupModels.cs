namespace mvc.Models
{
    partial class BackupModelsDataContext
    {
        public void ClearBackup()
        {
            BackupTasks.DeleteAllOnSubmit(BackupTasks);
            BackupProjects.DeleteAllOnSubmit(BackupProjects);
            BackupWorkerStatus.DeleteAllOnSubmit(BackupWorkerStatus);
            BackupDepartments.DeleteAllOnSubmit(BackupDepartments);
            BackupWorkers.DeleteAllOnSubmit(BackupWorkers);
            BackupUsers.DeleteAllOnSubmit(BackupUsers);
            SubmitChanges();
        }

        public void Install()
        {

        }
    }
}
