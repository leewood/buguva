<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Tasks.List" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class = "path">
   	  <%= Html.Path() %>
   	</div>   
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount, new { sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	</div>   
	<table class = "grid" style="max-width: 400px;text-align:left;">
	   <tr>
	     <td colspan="5">
	        <% Html.BeginForm("List", "Tasks", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", ViewData.Model.PageNumber) %>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>
	
	   <tr>
	      <th>ID</th>
	      <%= Html.SortingHeader("ID", "id", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter= ViewData["filter"] })%>
	      <%= Html.SortingHeader("Projektas", "ProjectCode", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter= ViewData["filter"] })%>
	      <%= Html.SortingHeader("Dalyvis", "ParticipantName", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter= ViewData["filter"] })%>
	      <%= Html.SortingHeader("Mėnuo", "FullMonthName", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter= ViewData["filter"] })%>
	      <%= Html.SortingHeader("Dirbtos valandos", "worked_hours", "text-align:right", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <th>Veiksmai</th>
	   </tr>    
	   <% foreach (Task task in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td style="text-align:right"><%= task.id %></td>
	        <td><%= (task.Project != null)?task.Project.title:"Nepaskirtas" %></td>
	        <td><%= (task.Worker != null)?task.Worker.Fullname:"Nepaskirtas" %></td>
	        <td><%= task.FullMonthName %></td>
	        <td style="text-align:right"><%= task.worked_hours %></td>
	        <td>
	        <% if (task.administrationEdit()) %>
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = task.id}) %>
            <% if (task.administrationDelete()) %>	          
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = task.id}, true, "Ar tikrai norite ištrinti šią užduotį?") %>	          
	        </td>
	      </tr>
	   <% } %>
	</table>
	<% if (mvc.Models.Task.administrationNew())
    { %>
	<%= Html.ActionImageLink("../Content/new.png", "", "New", new { })%><%= Html.ActionLink("Nauja užduotis", "New")%>
	<% } %>
</asp:Content>
