using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{
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
                return canBeSeen();
            }
        }

        public void makeBackup(int currentUserID)
        {

            if (Common.UserSession.BackupDeletedData)
            {
                BackupModelsDataContext backupDataContext = new BackupModelsDataContext();
                BackupUser backup = new BackupUser();
                backup.deleted_by_id = currentUserID;
                backup.deleted = DateTime.Now;
                backup.id = this.id;
                backup.level = this.level;
                backup.login_name = this.login_name;
                backup.password = this.password;
                backup.worker_id = this.worker_id;
                backupDataContext.BackupUsers.InsertOnSubmit(backup);
                backupDataContext.SubmitChanges();
            }
        }

        private string _repeatPass = null;
        [ValidateSameAs("password", "Slaptažodis ir pakartotasis slaptažodis turi sutapti")]
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


        [ValidateSameAs("new_password", "Naujasis slaptažodis ir pakartotasis slaptažodis turi sutapti")]
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
}
