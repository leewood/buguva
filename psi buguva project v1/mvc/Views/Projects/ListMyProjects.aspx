<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ListMyProjects.aspx.cs" Inherits="mvc.Views.Projects.ListMyProjects" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="System.Linq"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    string controller_name = (string)ViewContext.RouteData.Values["controller"];
    string action_name = (string)ViewContext.RouteData.Values["action"];    
    
    UserNavigation userNav = new UserNavigation(controller_name, action_name);
    
    UserSession userSession = userNav.userSession;

%>
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
	   
	   <% Worker worker = (Worker)ViewData["worker"];
	       foreach (Project project in ViewData.Model) %>
       <% { %>
            <% className = (project.project_manager_id == (int)ViewData["currentWorkerID"]) ? "marked" : ""; %>
            <tr class='<%= className.ToString()%>'>
             <td><%= (worker.canBeSeen()) ? Html.ActionLink(project.title, "ListMyTasksInProject", new { project_id = project.id, id = ViewData["currentWorkerID"] }) : project.title%></td>             
             <td><%= (project.Worker == null) ? "<span style=\"color: Red\">Nepaskirtas</span>" : ((project.Worker.canBeSeen()) ? Html.ActionLink(project.Worker.Fullname, "ListMyProjects", new { project_id = project.id, id = project.project_manager_id }) : project.Worker.Fullname)%></td>             
             <td><%= project.StartedAt %></td>             
             <td><%= project.EndedAt %></td>
             <td style="text-align:right"><%= project.TotalWorkedHours.ToString() %></td>
             <td width="60">
               <% if (worker.canBeSeen())
                  { %>
               <%= Html.ActionImageLink("/Content/Images/Icons/Tasks.png", "Ataskaita", "ListMyTasksInProject", new { project_id = project.id, id = ViewData["currentWorkerID"] })%>
               <% } %>
               <% if (project.project_manager_id == (int)ViewData["currentWorkerID"] || userSession.canViewAllProjectsReoprts()) %>
               <% { %>
                 <% if (project.canBeSeen())
                    { %>
                 <%= Html.ActionImageLink("/Content/Images/Icons/ManagerReport.png", "Vadovo Ataskaita", "ProjectManagerReport", new { project_id = project.id })%>
                 <%} %>
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
	
	<div class="description">
	    <img src="../../Content/Images/Icons/Tasks.png" alt="img" /> - Projekte esančios užduotys <br />
	    <img src="../../Content/Images/Icons/ManagerReport.png" alt="img" /> - Vadovaujamo projekto ataskaita
	</div>
	
</asp:Content>
