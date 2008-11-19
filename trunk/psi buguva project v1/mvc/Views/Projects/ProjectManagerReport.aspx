<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ProjectManagerReport.aspx.cs" Inherits="mvc.Views.Projects.ProjectManagerReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
<table>
 <tr>
   <td>Projekto kodas</td><td><%= ViewData.Model.Project.id %></td>
 </tr>
 <tr>
  <td>Projecto vadovas</td><td><%= (ViewData.Model.Project.Worker != null)?ViewData.Model.Project.Worker.Fullname:"Nepaskirtas" %></td>
 </tr>  
 <tr>
  <td>Projekto vadovo skyrius</td><td><%= (ViewData.Model.Project.Worker != null)?ViewData.Model.Project.Worker.department_id.ToString():"Nepaskirtas" %></td>
 </tr> 
  <td>Prasidėjo</td><td><%= (ViewData.Model.Project.FirstTask != null)?ViewData.Model.Project.FirstTask.FullMonthName:"Neprasidėjo" %></td>
 </tr> 
 <tr>
  <td>Baigėsi</td><td><%= (ViewData.Model.Project.LastTask != null)?ViewData.Model.Project.LastTask.FullMonthName:"Neprasidėjo" %></td>
 </tr> 
 <tr>
  <td>Dalyvių skaičius</td><td><%= (ViewData.Model.TotalCountOfWorkers) %></td>
 </tr>
</table>  
<table> 
  <tr>
    <td colspan="2">Dalyviai:</td>
  </tr>
  
  <% foreach (DepartmentInfoForProject departmentInfo in ViewData.Model.DepartmentsInfo) %>
  <% { %>
      <tr>
        <td style="width:10px">
        </td>
        <td>
        <div class = "subTable">
          <label>Skyrius <%=departmentInfo.Department.id %></label>
          <table class = "grid">
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
            <tr class = "total">
              <td>
                Viso skyriui:
              </td>
              <td>
                <%=departmentInfo.Hours %>
              </td>
            </tr>
          </table>
          
        </div>
        </td>
      </tr>
      <% } %>
      <tr>
        <td colspan="2">
          <label>Viso:</label><label><%= ViewData.Model.TotalWorkedHours %></label>
        </td>
      </tr>
  </table>
</asp:Content>
