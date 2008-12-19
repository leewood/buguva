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
 <img src="../../Content/Images/Icons/Print30.png" onclick="printLandscapeTable()" class="print_image" alt="logo" style="float: right;" />  

   	</div> 
   	<div class="pager">   	
		<%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount) %>
	</div>
	<table class = "grid">
	   <tr>
	     <td colspan="6">
	        <% Html.BeginForm("ListMyProjects", "Projects", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", ViewData.Model.PageNumber) %>
	           <%= Html.Hidden("id", ViewData["currentWorkerID"]) %>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>	
	   <tr>
	      <%= Html.SortingHeader("Kodas", "title", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"], id = ViewData["currentWorkerID"] })%>
	      <%= Html.SortingHeader("Vadovas", "ManagerName", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"], id = ViewData["currentWorkerID"] })%>
	      <%= Html.SortingHeader("Pradėta dalyvauti", "StartedAt", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"], id = ViewData["currentWorkerID"] })%>
	      <%= Html.SortingHeader("Baigta dalyvauti", "EndedAt", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"], id = ViewData["currentWorkerID"] })%>
	      <%= Html.SortingHeader("Išdirbta(val.)", "TotalWorkedHours", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"], id = ViewData["currentWorkerID"] })%>	      
	      <th style="width:90px">Peržiūra</th>
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
                 <%}
                  }%>
	        <% if (userSession.canEditProjects()) %>
	        <% { %>	        
	          <% if (project.administrationEdit()) %>
	          <%= Html.ActionImageLink("/Content/edit.png", "Koreguoti", "Edit", new { id = project.id, back = true })%>
	          <% if (project.administrationDelete()) %>
	          <%= Html.ActionImageLink("/Content/delete.png", "Trinti", "Delete", new { id = project.id, bakc = true}, true, "Ar tikrai norite ištrinti šį projektą?")%>	          
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
		<% if (mvc.Models.Project.administrationNew())
    { %>
	<%= Html.ActionImageLink("/Content/new.png", "", "New", new {back = true})%><%= Html.ActionLink("Naujas projektas", "New", new { back = true})%>
    <% } %>

	<div class="description">
	    <img src="../../Content/Images/Icons/Tasks.png" alt="img" /> - Projekte esančios užduotys <br />
	    <img src="../../Content/Images/Icons/ManagerReport.png" alt="img" /> - Vadovaujamo projekto ataskaita
	</div>
	
</asp:Content>
