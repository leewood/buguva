<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Users.List" %>
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
	      <th>ID</th>
	      <th>Prisijungimo vardas</th>
	      <th>Teisė</th>
	      <th>Susietas su darbuotoju</th>
	      <th>Veiksmai</th>
	   </tr>    
	   <% foreach (User user in ViewData.Model) %>
	   <% { %>
	      <tr>
	        <td width="60"><%= user.id %></td>
	        <td><%= user.login_name %></td>
	        <td><%= user.LevelName %></td>
	        <td><%= (user.Worker != null)?user.Worker.Fullname:"<span style=\"color:Red\">Nesusietas</span>" %></td>
	        <td width="100">
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = user.id}) %>
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = user.id}, true, "Ar tikrai norite ištrinti šį vartotoją?") %>
	          <%= Html.ActionImageLink("../Content/key.png", "Keisti slaptažodį", "ChangePassword", new {id = user.id}) %>
	        </td>
	      </tr>
	   <% } %>
	</table>
	<%= Html.ActionImageLink("../Content/new.png", "", "New", new {}) %><%= Html.ActionLink("Naujas vartotojas", "New") %>
</asp:Content>
