<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ProjectManagerReport.aspx.cs" Inherits="mvc.Views.Projects.ProjectManagerReport" %>



<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  

    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["Title"]%></span>

   	</div> 
<ul id="menu">
   <li class="selected">
      <%= Html.ActionLink("Projekto ataskaita", "ProjectManagerReport", new { project_id = ViewData.Model.Project.id }) %>
   </li>
   <li class="simple">
      <%= Html.ActionLink("Projekto intensyvumas", "ProjectIntensivityReport", new { project_id = ViewData.Model.Project.id }) %>                    
   </li>
</ul>
<fieldset class="report">
<table>
 <tr>
   <td>Projekto kodas</td><td><%= ViewData.Model.Project.title %></td>
 </tr>
 <tr>
  <td>Projecto vadovas</td><td><%= (ViewData.Model.Project.Worker != null)?((ViewData.Model.Project.Worker.canBeSeen())?(Html.ActionLink(ViewData.Model.Project.Worker.Fullname, "ListMyProjects", new {id = ViewData.Model.Project.Worker.id})):ViewData.Model.Project.Worker.Fullname):"Nepaskirtas" %></td>
 </tr>  
 <tr>
  <td>Projekto vadovo skyrius</td><td><%= (ViewData.Model.Project.Worker != null)?((ViewData.Model.Project.Worker.Department != null)?ViewData.Model.Project.Worker.Department.title:"Nepaskirtas"):"Nepaskirtas" %></td>
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
    <td colspan="2"><b>Dalyviai:</b><br /><br /></td>
  </tr>
  
  <% foreach (DepartmentInfoForProject departmentInfo in ViewData.Model.DepartmentsInfo) %>
  <% { %>
      <tr>
        <td style="width:10px">
        </td>
        <td>
        <div class = "subTable">
          <label><%= (departmentInfo.Department.canBeSeen())?Html.ActionLink("Skyrius: " + departmentInfo.Department.title, "DepartmentManagerReport", new {controller="Departments", department_id = departmentInfo.Department.id}): "Skyrius: " + departmentInfo.Department.title%></label>
          <table class = "report_table" cellpadding="0" cellspacing="0">
            <tr>
              <th>Dalyvis</th>
              <th>Dirbo val.</th>
            </tr>
            <% foreach (WorkerAndHours workerInfo in departmentInfo.Workers) %>
            <% { %>
            <tr>
              <td><%= (workerInfo.Worker != null)?((workerInfo.Worker.canBeSeen())?(Html.ActionLink(workerInfo.Worker.Fullname, "ListMyProjects", new {id = workerInfo.Worker.id})):workerInfo.Worker.Fullname):"<span style=\"color:Red\">Nepaskirtas</span>" %></td>
              <td style="text-align:right"><%= workerInfo.Hours %></td>
            </tr>            
            <% } %>
            
            <tr>
              <td class="sum">
                Viso skyriui:
              </td>
              <td class="sum" style="text-align:right">
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
          <label><b>Viso projekte dirbta </b></label><label style=" font-weight: 200"><%= ViewData.Model.TotalWorkedHours %></label><label><b> val.</b></label>
        </td>
      </tr>
  </table>
</fieldset>
</asp:Content>
