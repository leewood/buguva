<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Departments.List" %>
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
	  <%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount, new { sorting = ViewData["sorting"], filter = ViewData["filter"] }) %>
	</div>   
	<table class = "grid">
	   <tr>
	     <td colspan="5">
	        <% Html.BeginForm("List", "Departments", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", ViewData.Model.PageNumber) %>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>
	
	   <tr>
	      <%= Html.SortingHeader("ID", "id", "display:none", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <%= Html.SortingHeader("Kodas", "title", "width: 150px", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
          <%= Html.SortingHeader("Vadovas", "ManagerName", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>	      	      
	   	  <th>Veiksmai</th>
	   </tr>    
	   <% foreach (Department department in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td style="display:none"><%= department.id %></td>
	        <td><%= department.title %></td>
	        <td><%= department.ManagerName %></td>
	        <td width="60">
	         <% if (department.administrationEdit()) %>
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = department.id}) %>
             <% if (department.administrationDelete()) %>	          
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = department.id}, true, "Ar tikrai norite ištrinti šį skyrių?") %>	          
	        </td>
	      </tr>
	   <% } %>
	</table>
	<% if (mvc.Models.Department.administrationNew())
    { %>
	<%= Html.ActionImageLink("../Content/new.png", "", "New", new { })%><%= Html.ActionLink("Naujas skyrius", "New")%>
	<% } %>

	<div class="description">
	    <img src="../../Content/delete.png" alt="img" /> - Ištrinti įrašą <br />
	    <img src="../../Content/edit.png" alt="img" /> - Redaguoti įrašą
	</div>

</asp:Content>
