using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Installation
{
    partial class InstallationModelsDataContext
    {
        public bool Install()
        {
            try
            {
                if (!DatabaseExists())
                {
                    CreateDatabase();
                    User admin = new User();
                    admin.login_name = "admin";
                    admin.password = "admin";
                    admin.level = 3;
                    Users.InsertOnSubmit(admin);
                    SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
