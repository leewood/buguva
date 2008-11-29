<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Departments.List" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
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
	      <th>Kodas/Pavadinimas</th>
	      <th>Vadovas</th>
	      <th>Veiksmai</th>
	   </tr>    
	   <% foreach (Department department in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td style="display:none"><%= department.id %></td>
	        <td><%= department.title %></td>
	        <td><%= (department.Worker != null)?department.Worker.Fullname:"Nepaskirtas" %></td>
	        <td width="60">
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = department.id}) %>
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = department.id}, true, "Ar tikrai norite ištrinti šį skyrių?") %>	          
	        </td>
	      </tr>
	   <% } %>
	</table>
	<%= Html.ActionImageLink("../Content/new.png", "", "New", new {}) %><%= Html.ActionLink("Naujas skyrius", "New") %>
</asp:Content>
