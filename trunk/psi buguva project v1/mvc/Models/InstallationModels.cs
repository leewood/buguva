using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
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
