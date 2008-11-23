using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;


using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.Common;
using System.Data.Odbc;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Web.Util;


namespace mvc.Views.Import
{

    public partial class Index : ViewPage
    {
        private string xlsConnectionString;
        private DbProviderFactory factory;
        private DbConnection connection;
        private DbCommand command;

        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            if (FileUploadImport.PostedFile != null)
            {
                try
                {
                    FileUploadImport.PostedFile.SaveAs("D:\\Temp\\"+FileUploadImport.PostedFile.FileName);
                    Span1.InnerHtml = "Upload Successful!";
                }
                catch (Exception ex)
                {
                    Span1.InnerHtml = "Error saving file <b>D:\\Temp\\" +
                       FileUploadImport.PostedFile + "</b><br>" + ex.ToString();
                    return;
                }
                try
                {
                    xlsConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + "D:\\Temp\\" + FileUploadImport.PostedFile.FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
                    //string xlsConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
            
                    factory = DbProviderFactories.GetFactory("System.Data.OleDb");

                    using (connection = factory.CreateConnection())
                    {
                        connection.ConnectionString = xlsConnectionString;
                        connection.Open();
                        using (command = connection.CreateCommand())
                        {
                            ImportDepartmentsToDatabase1("Skyriai");
                            ImportWorkersToDatabase("Darbuotojai");
                            ImportDepartmentsToDatabase2("Skyriai");
                            ImportProjectsToDatabase("Projektai");
                            ImportTasksToDatabase("Užduotys");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Span1.InnerHtml = "Error importing data<br>" + ex.ToString();
                    return;
                }
            }
        }

        private void ImportTasksToDatabase(String strTable)
        {
            command.CommandText = "SELECT [Projekto kodas], [Projekto dalyvio vardas], [Metai], [Mėnuo], [Išdirbo valandų] FROM [" + strTable + "$]";
            using (DbDataReader dr = command.ExecuteReader())
            {
                Models.POADataModelsDataContext DBDataContext = new mvc.Models.POADataModelsDataContext();
                while (dr.Read())
                {
                    if (dr["Projekto kodas"].ToString() != "")
                    {
                        Models.Task task = new Models.Task();
                        Models.Project project = DBDataContext.Projects.First(p => p.title == dr[0].ToString());
                        task.project_id = project.id;
                        Models.Worker worker = DBDataContext.Workers.First(w => w.name == dr[1].ToString());
                        task.project_participant_id = worker.id;
                        task.year = int.Parse(dr[2].ToString());
                        task.month = int.Parse(dr[3].ToString());
                        task.worked_hours = int.Parse(dr[4].ToString());

                        var errors = task.Validate();
                        if (errors != null)
                        {
                            TempData["errors"] = errors.ErrorMessages;
                        }
                        else
                        {
                            DBDataContext.Tasks.InsertOnSubmit(task);
                            DBDataContext.SubmitChanges();
                        }
                    }
                }
            }
        }

        private void ImportProjectsToDatabase(String strTable)
        {
            command.CommandText = "SELECT [Projekto kodas], [Projekto vadovo vardas] FROM [" + strTable + "$]";
            using (DbDataReader dr = command.ExecuteReader())
            {
                Models.POADataModelsDataContext DBDataContext = new mvc.Models.POADataModelsDataContext();
                while (dr.Read())
                {
                    if (dr["Projekto kodas"].ToString() != "")
                    {
                        Models.Project project = new Models.Project();
                        project.title = dr[0].ToString();
                        if ("NEĮVYKĘS" != dr[1].ToString())
                        {
                            Models.Worker worker = DBDataContext.Workers.First(w => w.name == dr[1].ToString());
                            project.project_manager_id = worker.id;
                        }
                        var errors = project.Validate();
                        if (errors != null)
                        {
                            TempData["errors"] = errors.ErrorMessages;
                        }
                        else
                        {
                            DBDataContext.Projects.InsertOnSubmit(project);
                            DBDataContext.SubmitChanges();
                        }
                    }
                }
            }
        }


        private void ImportWorkersToDatabase(String strTable)
        {
            command.CommandText = "SELECT [Darbuotojo vardas], [Skyrius] FROM [" + strTable + "$]";
            using (DbDataReader dr = command.ExecuteReader())
            {
                Models.POADataModelsDataContext DBDataContext = new mvc.Models.POADataModelsDataContext();
                while (dr.Read())
                {
                    if (dr["Darbuotojo vardas"].ToString() != "")
                    {
                        Models.Worker worker = new Models.Worker();
                        worker.name = dr[0].ToString();
                        worker.surname = " ";
                        Models.Department department = DBDataContext.Departments.First(d => d.title == dr[1].ToString());
                        worker.department_id = department.id;
                        var errors = worker.Validate();
                        if (errors != null)
                        {
                            TempData["errors"] = errors.ErrorMessages;
                        }
                        else
                        {
                            DBDataContext.Workers.InsertOnSubmit(worker);
                            DBDataContext.SubmitChanges();
                        }
                    }
                }
            }
        }

        private void ImportDepartmentsToDatabase1(String strTable)
        {
            command.CommandText = "SELECT [Numeris], [Vadovo vardas] FROM [" + strTable + "$]";
            using (DbDataReader dr = command.ExecuteReader())
            {
                Models.POADataModelsDataContext DBDataContext = new mvc.Models.POADataModelsDataContext();
                while (dr.Read())
                {
                    if (dr["Numeris"].ToString() != "")
                    {
                        Models.Department department = new Models.Department();
                        department.title = dr[0].ToString();
                        var errors = department.Validate();
                        if (errors != null)
                        {
                            TempData["errors"] = errors.ErrorMessages;
                        }
                        else
                        {
                            DBDataContext.Departments.InsertOnSubmit(department);
                            DBDataContext.SubmitChanges();
                        }
                    }
                }
            }
        }

        private void ImportDepartmentsToDatabase2(String strTable)
        {
            command.CommandText = "SELECT [Numeris], [Vadovo vardas] FROM [" + strTable + "$]";
            using (DbDataReader dr = command.ExecuteReader())
            {
                Models.POADataModelsDataContext DBDataContext = new mvc.Models.POADataModelsDataContext();
                while (dr.Read())
                {
                    if (dr["Numeris"].ToString() != "")
                    {
                       // Models.Department department = new Models.Department();
                       // department.title = dr[0].ToString();
                        Models.Worker worker = DBDataContext.Workers.First(w => w.name == dr[1].ToString());
                        Models.Department department = DBDataContext.Departments.First(d => d.title == dr[0].ToString());
                        department.headmaster_id = worker.id;

                        var errors = department.Validate();
                        if (errors != null)
                        {
                            TempData["errors"] = errors.ErrorMessages;
                        }
                        else
                        {
                            DBDataContext.SubmitChanges();
                        }
                    }
                }
            }
        }

    }
}
