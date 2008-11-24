<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ProjectManagerReport.aspx.cs" Inherits="mvc.Views.Projects.ProjectManagerReport" %>



<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  

<%= Html.Path() %>
<ul id="menu">
   <li class="selected">
      <%= Html.ActionLink("Projekto ataskaita", "ProjectManagerReport", new { project_id = ViewData.Model.Project.id }) %>
   </li>
   <li class="simple">
      <%= Html.ActionLink("Projekto intensyvumas", "ProjectIntensivityReport", new { project_id = ViewData.Model.Project.id }) %>                    
   </li>
</ul>
<table>
 <tr>
   <td>Projekto kodas</td><td><%= ViewData.Model.Project.id %></td>
 </tr>
 <tr>
  <td>Projecto vadovas</td><td><%= (ViewData.Model.Project.Worker != null)?(Html.ActionLink(ViewData.Model.Project.Worker.Fullname, "ListMyProjects", new {id = ViewData.Model.Project.Worker.id})):"Nepaskirtas" %></td>
 </tr>  
 <tr>
  <td>Projekto vadovo skyrius</td><td><%= (ViewData.Model.Project.Worker != null)?ViewData.Model.Project.Worker.department_id.ToString():"Nepaskirtas" %></td>
 </tr>
 <tr> 
  <td>Prasid&#279;jo</td><td><%= (ViewData.Model.Project.FirstTask != null)?ViewData.Model.Project.FirstTask.FullMonthName:"Neprasid&#279;jo" %></td>
 </tr> 
 <tr>
  <td>Baig&#279;si</td><td><%= (ViewData.Model.Project.LastTask != null)?ViewData.Model.Project.LastTask.FullMonthName:"Neprasid&#279;jo" %></td>
 </tr> 
 <tr>
  <td>Dalyvi&#371; skai&#269;ius</td><td><%= (ViewData.Model.TotalCountOfWorkers) %></td>
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
          <label><%= Html.ActionLink("Skyrius: " + departmentInfo.Department.title, "DepartmentManagerReport", new {controller="Departments", department_id = departmentInfo.Department.id})%></label>
          <table class = "grid">
            <tr>
              <th>Dalyvis</th>
              <th>Dirbo val.</th>
            </tr>
            <% foreach (WorkerAndHours workerInfo in departmentInfo.Workers) %>
            <% { %>
            <tr>
              <td><%= (workerInfo.Worker != null)?(Html.ActionLink(workerInfo.Worker.Fullname, "ListMyProjects", new {id = workerInfo.Worker.id})):"<span style=\"color:Red\">Nepaskirtas</span>" %></td>
              <td><%= workerInfo.Hours %></td>
            </tr>            
            <% } %>
            
            <tr style="border-top: solid 2px Black">
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
