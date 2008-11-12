<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ListMyTasksInProject.aspx.cs" Inherits="mvc.Views.Projects.ListMyTasksInProject" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   	<div class = "path">
   	  <%= Html.Path() %>
   	</div>
   	<div id="monthChoose">
   	  <% foreach (MonthOfYear monthOfYear in ViewData.Model.Months) %>
   	  <% { %>
   	     <% if (!monthOfYear.Equals(ViewData.Model.CurrentMonth)) %>
   	     <% { %>
   	            <%= Html.ActionLink(monthOfYear.ToString(), "ListMyTasksInProject", new {project_id = ViewData.Model.ProjectID, page = ViewData.Model.Tasks.PageNumber, year = monthOfYear.Year, month = monthOfYear.Month}) %>
   	     <% } %>
   	     <% else %>
   	     <% {   %>                
   	           <span><%= monthOfYear.ToString() %></span>
         <% } %>
   	  <% } %>
   	</div>
   	<div class="pager">   	
		<%= Html.Pager(ViewData.Model.Tasks.PageSize, ViewData.Model.Tasks.PageNumber, ViewData.Model.Tasks.TotalItemCount) %>
	</div>
	<table class = "grid">
	   <tr>
	      <th>Pavadinimas</th>
	      <th>Išdirbta(val.)</th>
	   </tr>
	   <% foreach (Task task in ViewData.Model.Tasks) %>
       <% { %>
            <tr class=''>
             <td><%= task.id.ToString() %></td>
             <td><%= task.worked_hours.ToString() %></td>
          </tr>
	   <% } %>
	   <% if (ViewData.Model.Tasks.Count == 0) %>
	   <% { %>
	      <tr>
	        <td>Nebuvo jokių užduočių</td>
	      </tr>
	   <% } %>
	</table>
</asp:Content>
