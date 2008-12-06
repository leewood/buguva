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
      <%= Html.ErrorSummary("Įvyko klaida:", (string[])TempData["errors"]) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount) %>
	</div>   
	<table class = "grid">
	   <tr>
	      <th style="display:none">ID</th>
	      <th style="width: 100px">Kodas/pavadinimas</th>
	      <th>Vadovas</th>
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
	          <%= Html.ActionImageLink("/Content/edit.png", "Koreguoti", "Edit", new { id = project.id })%>
	          <%= Html.ActionImageLink("/Content/delete.png", "Trinti", "Delete", new { id = project.id }, true, "Ar tikrai norite ištrinti šį projektą?")%>	          
	        <% } %>
	        </td>
	      </tr>
	   <% } %>
	</table>
	<%= Html.ActionImageLink("/Content/new.png", "", "New", new {}) %><%= Html.ActionLink("Naujas projektas", "New") %>

	<div class="description">
	    <img src="../../Content/Images/Icons/Tasks.png" alt="img" /> - Projekte esančios užduotys <br />
	    <img src="../../Content/Images/Icons/ManagerReport.png" alt="img" /> - Vadovaujamo projekto ataskaita <br />
	    <img src="../../Content/delete.png" alt="img" /> - Ištrinti įrašą <br />
	    <img src="../../Content/edit.png" alt="img" /> - Redaguoti įrašą
	</div>
	
</asp:Content>
