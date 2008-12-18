<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Workers.List" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
   	</div>   
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount) %>
	</div>   
	<table class = "grid">
		   <tr>
	      <td colspan="5">
	        <% Html.BeginForm("List", "Workers", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", ViewData.Model.PageNumber) %>
	           <input type="submit" value="Filtruoti" />
	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>

	   <tr>	      
	      <%= Html.SortingHeader("ID", "id", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"] })%>
	      <%= Html.SortingHeader("Vardas", "name", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"] })%>
	      <%= Html.SortingHeader("Pavardė", "surname", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"] })%>
	      <%= Html.SortingHeader("Skyrius", "department_id", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"] })%>
	      <th>Veiksmai</th>
	   </tr>    
	   <% foreach (Worker worker in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td width="60"><%= worker.id %></td>
	        <td><%= worker.name %></td>
	        <td><%= worker.surname %></td>
	        <td><%= (worker.Department != null)?worker.Department.title:"" %></td>
	        <td width="60">
	        <% if (worker.administrationEdit()) %>
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = worker.id}) %>
	        <% if (worker.administrationDelete()) %>
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = worker.id}, true, "Ar tikrai norite ištrinti šį darbuotoją?") %>
	        </td>
	      </tr>
	   <% } %>
	</table>
	<% if (mvc.Models.Worker.administrationNew())
    { %>
	  <%= Html.ActionImageLink("../Content/new.png", "", "New", new { })%><%= Html.ActionLink("Naujas darbuotojas", "New")%>
	<% } %>

	<div class="description">
	    <img src="../../Content/delete.png" alt="img" /> - Ištrinti įrašą <br />
	    <img src="../../Content/edit.png" alt="img" /> - Redaguoti įrašą
	</div>

</asp:Content>
