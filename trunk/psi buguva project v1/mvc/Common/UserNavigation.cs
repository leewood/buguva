using System;
using System.Collections.Generic;
using System.Linq;
using mvc;
using mvc.Models;
using mvc.Common;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;


namespace mvc.Common
{
    public class UserNavigation
    {
        public string controller;

        public string action;

        public UserSession userSession = null;

        public UserNavigation(string controller, string action)
        {
            this.controller = controller;
            this.action = action;

            this.userSession = new UserSession();
        }

        public bool isInAdmin()
        {
            return (
                (
                controller == "Users" &&
                action != "EditProfile"
                )
                ||
                controller == "Workers" ||
                controller == "Import" ||
                (
                controller == "Departments" &&
                action != "DepartmentProjects" &&
                action != "DepartmentManagerReport"
                ) ||
                (
                controller == "Projects" &&
                    (
                    action == "List" ||
                    action == "Edit" ||
                    action == "New"
                    )
                )

                )? true : false;
        }

        public bool isInAtaskaitos()
        {
            return (
                (
                controller == "Projects" &&
                action != "List" && 
                action != "New" &&
                action != "Edit"
                ) ||
                controller == "Sections"  ||
                (
                controller == "Departments" &&
                action == "DepartmentManagerReport"
                ) ||
                (
                controller == "Departments" &&
                action == "DepartmentProjects"
                )

                ) ? true : false;
        }

        public Models.Department getDepartment()
        {
            try
            {
                Department data = new Department();
                return data.getDepartment(this.userSession.workerID);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string getDepartmentName()
        {
            try
            {
                return this.getDepartment().title;
            }
            catch (Exception e)
            {
                return "nežinomas skyrius";
            }
        }

        public List<Models.Department> getDepartments()
        {
            //"Paprastas darbuotojas", "Skyriaus vadovas", "Administratorius", "Antanas",            

            POADataModelsDataContext data = new POADataModelsDataContext();

            switch (this.userSession.userLevel)
            {
                case 1:

                    return new List<Department>();

                case 2:

                    return data.Departments.Where(Department => ((Department.headmaster_id == this.userSession.workerID) && (Department.deleted.HasValue == false))).ToList();

                case 3:

                    return data.Departments.Where(d => d.deleted.HasValue == false).ToList();

                case 4:

                    return data.Departments.Where(d => d.deleted.HasValue == false).ToList();
            }

            return new List<Department>();
        }

        public string getLink(string url, string caption, bool marked)
        {
            string mark = (marked) ? "class='selected'" : "";

            return "<a href='" + url + "' " + mark + ">" + caption + "</a>";
        }
    }
}