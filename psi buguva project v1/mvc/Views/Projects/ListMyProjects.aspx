<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ListMyProjects.aspx.cs" Inherits="mvc.Views.Projects.ListMyProjects" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
   	</div> 
   	<div class="pager">   	
		<%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount) %>
	</div>
	<table class = "grid">
	   <tr>
	      <th>Kodas/Pavadinimas</th>	      
	      <th>Vadovas</th>
	      <th>Pradėta dalyvauti</th>
	      <th>Baigta dalyvauti</th>
	      <th>Išdirbta(val.)</th>
	      <th>Peržiūra</th>
	   </tr>	  
	   
	   <% foreach (Project project in ViewData.Model) %>
       <% { %>
            <% className = (project.project_manager_id == (int)ViewData["currentWorkerID"]) ? "marked" : ""; %>
            <tr class='<%= className.ToString()%>'>
             <td><%= Html.ActionLink(project.title, "ListMyTasksInProject", new {project_id = project.id}) %></td>             
             <td><%= (project.Worker == null)?"<span style=\"color: Red\">Nepaskirtas</span>":Html.ActionLink(project.Worker.Fullname, "ListMyProjects", new { project_id = project.id, id = project.project_manager_id })%></td>             
             <td><%= project.StartedAt %></td>             
             <td><%= project.EndedAt %></td>
             <td><%= project.TotalWorkedHours.ToString() %></td>
             <td width="60">
               <%= Html.ActionImageLink("/Content/Images/Icons/Tasks.png", "Ataskaita", "ListMyTasksInProject", new { project_id = project.id, id = ViewData["currentWorkerID"] })%>
               <% if (project.project_manager_id == (int)ViewData["currentWorkerID"]) %>
               <% { %>
                 <%= Html.ActionImageLink("/Content/Images/Icons/ManagerReport.png", "Vadovo Ataskaita", "ProjectManagerReport", new { project_id = project.id })%>
               <% } %>
             </td>
          </tr>
	   <% } %>
	   <% if (ViewData.Model.Count == 0) %>
	   <% { %>
	      <tr>
	        <td>Nėra jokių projektų</td>
	      </tr>
	   <% } %>
	</table>
	
</asp:Content>
