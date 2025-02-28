﻿using System;
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
using System.Data.OleDb;
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


        public string[] getAllSheetNames(string connection)
        {
            OleDbConnection con = new OleDbConnection(connection);

            con.Open();
            DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return new string[0];
            }
            string[] excelSheetNames = new string[dt.Rows.Count];
            int i = 0;
            

            foreach (DataRow row in dt.Rows)
            {
                string name = row[2].ToString();

                if (name[name.Length - 1] == '\'')
                {
                    name = name.Substring(0, name.Length - 1);
                }
                if (name[0] == '\'')
                {
                    name = name.Substring(1);
                }
                if (name[name.Length - 1] == '$')
                {
                    name = name.Substring(0, name.Length - 1);
                }

                excelSheetNames[i] = name;
                i++;
                
            }
            con.Close();
            return excelSheetNames;
        }

        public string realSheetName(string wanted, string textBoxChoose, string[] possibilities)
        {
            System.Collections.Generic.List<string> pos = possibilities.ToList();
            if (pos.IndexOf(wanted) >= 0)
            {
                return wanted;
            }
            else if ((textBoxChoose != "") && (pos.IndexOf(textBoxChoose) >= 0))
            {
                return textBoxChoose;
            }
            else
            {
                for (int i = 0; i < pos.Count; i++)
                {
                    if ((possibilities[i].IndexOf(wanted) >= 0) || ((textBoxChoose != "") && (possibilities[i].IndexOf(textBoxChoose) >= 0)))
                    {
                        return possibilities[i];
                    }
                }
            }
            return "";
        }

        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            string path = this.Page.Request.PhysicalApplicationPath;
            if (FileUploadImport.FileName != "")
            {
                try
                {

                    FileUploadImport.PostedFile.SaveAs(path + "\\"+FileUploadImport.PostedFile.FileName);

                    Span1.InnerHtml = "Duomenų importas sėkmingai įvykdytas!";
                }
                catch (Exception ex)
                {
                    Span1.InnerHtml = "Error saving file <b>" + path + "\\" +
                       FileUploadImport.PostedFile.FileName + "</b><br>" + ex.ToString();
                    return;
                }
                try
                {

                    FileInfo fileInfo = new FileInfo(path + "\\" + FileUploadImport.PostedFile.FileName);
                    switch (fileInfo.Extension)
                    {
                        case ".xlsx": xlsConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "\\" + FileUploadImport.PostedFile.FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            break;
                        case ".xls": xlsConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "\\" + FileUploadImport.PostedFile.FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\""; 
                            break;
                        default:
                            Span1.InnerHtml = "Pasirinktas blogo formato failas";
                            return;
                    }

                    //string xlsConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
                    string[] sheetNames = getAllSheetNames(xlsConnectionString);
                    factory = DbProviderFactories.GetFactory("System.Data.OleDb");

                    using (connection = factory.CreateConnection())
                    {
                        connection.ConnectionString = xlsConnectionString;
                        connection.Open();
                        using (command = connection.CreateCommand())
                        {
                            if (RadioButtonListImport.SelectedValue == "1")
                            {
                                if (CheckBoxDepartments.Checked) ImportDepartmentsToDatabase1(realSheetName("Skyriai", "", sheetNames));
                                if (CheckBoxEmployees.Checked) ImportWorkersToDatabase(realSheetName("Darbuotojai", "", sheetNames));
                                if (CheckBoxDepartments.Checked) ImportDepartmentsToDatabase2(realSheetName("Skyriai", "", sheetNames));
                                if (CheckBoxProjects.Checked) ImportProjectsToDatabase(realSheetName("Projektai", "", sheetNames));
                                if (CheckBoxTasks.Checked) ImportTasksToDatabase(realSheetName("Užduotys", "", sheetNames));
                            }
                            else
                                if (RadioButtonListImport.SelectedValue == "2")
                                {
                                    ImportMatrixToDatabase(sheetNames[0]);
                                }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Span1.InnerHtml = "Klaida importuojant duomenis!";
                    return;
                }
            }
            else
            {
                Span1.InnerHtml = "Klaida: Nepasirinktas failas";
            }

        }

        private void ImportMatrixToDatabase(String strTable)
        {

            Models.POADataModelsDataContext DBDataContext = new mvc.Models.POADataModelsDataContext();
            int intDepart = 0;
            int intWorker = 0;
            string tableName;
            foreach (Models.Department department in DBDataContext.Departments)
            {
                intDepart++;
                List<Models.Worker> workers = DBDataContext.Workers.Where(w => w.department_id == department.id).ToList();
                workers = workers.Where(w => w.deleted.HasValue == false).ToList();
                intWorker = 0;
                foreach (Models.Worker worker in workers)
                {
                    intWorker++;
                    if (intWorker < 10)
                        tableName = "S" + intDepart.ToString() + "-0" + intWorker.ToString() + " dirba";
                    else
                        tableName = "S" + intDepart.ToString() + "-" + intWorker.ToString() + " dirba";

                    command.CommandText = "SELECT [Metai], [Mėnuo], [" + tableName + "] FROM [" + strTable + "$]";
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr[tableName].ToString() != "")
                            {
                                Models.WorkerStatus workerStatus = new Models.WorkerStatus();
                                workerStatus.worker_id = worker.id;
                                workerStatus.year = int.Parse(dr[0].ToString());
                                workerStatus.month = int.Parse(dr[1].ToString());
                                workerStatus.status = int.Parse(dr[2].ToString());

                                var errors = workerStatus.Validate();
                                if (errors != null)
                                {
                                    TempData["errors"] = errors.ErrorMessages;
                                }
                                else
                                {
                                    DBDataContext.WorkerStatus.InsertOnSubmit(workerStatus);
                                    DBDataContext.SubmitChanges();
                                }
                            }
                        }
                    }
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
