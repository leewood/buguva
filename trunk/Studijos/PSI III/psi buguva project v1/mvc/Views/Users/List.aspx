<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="mvc.Views.Users.List" %>
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
	  <%= Html.Pager(ViewData.Model.PageSize, ViewData.Model.PageNumber, ViewData.Model.TotalItemCount, new { sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	</div>   
	<table class = "grid">
	   <tr>
	     <td colspan="5">
	        <% Html.BeginForm("List", "Users", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", ViewData.Model.PageNumber) %>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>
	   <tr>
	      <%= Html.SortingHeader("ID", "id", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter= ViewData["filter"] })%>
	      <%= Html.SortingHeader("Prisijungimo vardas", "login_name", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <%= Html.SortingHeader("Teisė", "LevelName", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <%= Html.SortingHeader("Susietas su darbuotoju", "WorkerName", "", 0, new { page = ViewData.Model.PageNumber, sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
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
	        <% if (user.administrationEdit())  %>
	          <%= Html.ActionImageLink("../Content/edit.png", "Koreguoti", "Edit", new {id = user.id}) %>
            <% if (user.administrationDelete())  %>	          
	          <%= Html.ActionImageLink("../Content/delete.png", "Trinti", "Delete", new {id = user.id}, true, "Ar tikrai norite ištrinti šį vartotoją?") %>
	          <% mvc.Common.UserSession userSession = new UserSession(); %>
	        <% if (user.administrationEdit() || user.id == userSession.userId)  %>  
	          <%= Html.ActionImageLink("../Content/key.png", "Keisti slaptažodį", "ChangePassword", new {id = user.id}) %>
	        </td>
	      </tr>
	   <% } %>
	</table>
	<% if (mvc.Models.User.administrationNew())
    { %>
	  <%= Html.ActionImageLink("../Content/new.png", "", "New", new { })%><%= Html.ActionLink("Naujas vartotojas", "New")%>
    <% } %>
	<div class="description">
	    <img src="../../Content/delete.png" alt="img" /> - Ištrinti įrašą <br />
	    <img src="../../Content/edit.png" alt="img" /> - Redaguoti įrašą<br />
	    <img src="../../Content/key.png" alt="img" /> - Keisti vartotojo prisijungimo slaptažodį
	</div>

</asp:Content>
