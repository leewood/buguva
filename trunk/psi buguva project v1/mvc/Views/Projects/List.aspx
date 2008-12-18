<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Projects.List" %>
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
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>   	
   	<div class="pager">   	
   	
	  <%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount, new { sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	</div>   
	<table class = "grid">
	   <tr>
	     <td colspan="5">
	        <% Html.BeginForm("List", "Projects", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", ViewData.Model.PageNumber) %>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>
	
	   <tr>	      
	      <%= Html.SortingHeader("ID", "id", "display:none", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <%= Html.SortingHeader("Kodas", "title", "width: 100px", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
          <%= Html.SortingHeader("Vadovas", "ManagerName", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>	      	      
	      <th>Veiksmai</th>
	   </tr>    
	   <% foreach (Project project in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td style="display:none"><%= project.id %></td>
	        <td><%= project.title %></td>
	        <td><%= (project.Worker != null)?project.Worker.Fullname:"<span style=\"color:Red\">Nepaskirtas</span>" %></td>
	        <td width="80">
	        <% if (userSession.canEditProjects()) %>
	        <% { %>	        
	          <% if (project.administrationEdit()) %>
	          <%= Html.ActionImageLink("/Content/edit.png", "Koreguoti", "Edit", new { id = project.id })%>
	          <% if (project.administrationDelete()) %>
	          <%= Html.ActionImageLink("/Content/delete.png", "Trinti", "Delete", new { id = project.id }, true, "Ar tikrai norite ištrinti šį projektą?")%>	          
	        <% } %>
	        </td>
	      </tr>
	   <% } %>
	</table>
	<% if (mvc.Models.Project.administrationNew())
    { %>
	<%= Html.ActionImageLink("/Content/new.png", "", "New", new { })%><%= Html.ActionLink("Naujas projektas", "New")%>
    <% } %>
	<div class="description">
	    <img src="../../Content/Images/Icons/Tasks.png" alt="img" /> - Projekte esančios užduotys <br />
	    <img src="../../Content/Images/Icons/ManagerReport.png" alt="img" /> - Vadovaujamo projekto ataskaita <br />
	    <img src="../../Content/delete.png" alt="img" /> - Ištrinti įrašą <br />
	    <img src="../../Content/edit.png" alt="img" /> - Redaguoti įrašą
	</div>
	
</asp:Content>
