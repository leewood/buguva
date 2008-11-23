<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Projects.List" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class = "path">
   	  <%= Html.Path() %>
   	</div>   
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", (string[])TempData["errors"]) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount) %>
	</div>   
	<table class = "grid">
	   <tr>
	      <th>ID</th>
	      <th>Pavadinimas</th>
	      <th>Vadovas</th>
	      <th>Veiksmai</th>
	   </tr>    
	   <% foreach (Project project in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td><%= project.id %></td>
	        <td><%= project.title %></td>
	        <td><%= (project.Worker != null)?project.Worker.Fullname:"Nepaskirtas" %></td>
	        <td>
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = project.id}) %>
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = project.id}, true, "Ar tikrai norite ištrinti šį projektą?") %>	          
	        </td>
	      </tr>
	   <% } %>
	</table>
	<%= Html.ActionImageLink("../Content/new.png", "", "New", new {}) %><%= Html.ActionLink("Naujas projektas", "New") %>
</asp:Content>
