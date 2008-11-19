<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ProjectManagerReport.aspx.cs" Inherits="mvc.Views.Projects.ProjectManagerReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
  <label>Projekto kodas</label><label><%= ViewData.Model.Project.id %></label>
  <label>Projecto vadovas</label><label><%= (ViewData.Model.Project.Worker != null)?ViewData.Model.Project.Worker.Fullname:"Nepaskirtas" %></label>
  <label>Projekto vadovo skyrius</label><label><%= (ViewData.Model.Project.Worker != null)?ViewData.Model.Project.Worker.department_id.ToString():"Nepaskirtas" %></label>
  <label>Prasidėjo</label><%= (ViewData.Model.Project.FirstTask != null)?ViewData.Model.Project.FirstTask.FullMonthName:"Neprasidėjo" %>
  <label>Baigėsi</label><%= (ViewData.Model.Project.LastTask != null)?ViewData.Model.Project.LastTask.FullMonthName:"Neprasidėjo" %>
  <label>Dalyvių skaičius</label><%= (ViewData.Model.TotalCountOfWorkers) %>
  <label>Dalyviai:</label>
  <% foreach (DepartmentInfoForProject departmentInfo in ViewData.Model.DepartmentsInfo) %>
  <% { %>
        <div class = "subTable">
          <label>Skyrius <%=departmentInfo.Department.id %></label>
          <table>
            <tr>
              <th>Dalyvis</th>
              <th>Dirbo val.</th>
            </tr>
            <% foreach (WorkerAndHours workerInfo in departmentInfo.Workers) %>
            <% { %>
            <tr>
              <td><%= workerInfo.Worker.Fullname %></td>
              <td><%= workerInfo.Hours %></td>
            </tr>
            <% } %>
          </table>
          <label>Viso skyriui:</label><label><%=departmentInfo.Hours %></label>
        </div>
        <label>Viso:</label><label><%= ViewData.Model.TotalWorkedHours %></label>
  <% } %>
</asp:Content>
