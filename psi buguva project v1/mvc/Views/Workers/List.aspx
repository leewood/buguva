<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Workers.List" %>
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
	      <th>Vardas</th>
	      <th>Pavardė</th>
	      <th>Skyrius</th>
	      <th>Veiksmai</th>
	   </tr>    
	   <% foreach (Worker worker in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td><%= worker.id %></td>
	        <td><%= worker.name %></td>
	        <td><%= worker.surname %></td>
	        <td><%= (worker.Department != null)?worker.Department.title:"" %></td>
	        <td>
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = worker.id}) %>
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = worker.id}) %>
	        </td>
	      </tr>
	   <% } %>
	</table>
	<%= Html.ActionImageLink("../Content/new.png", "", "New", new {}) %><%= Html.ActionLink("Naujas darbuotojas", "New") %>
</asp:Content>
