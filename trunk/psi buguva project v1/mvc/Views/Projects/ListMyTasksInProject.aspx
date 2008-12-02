<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ListMyTasksInProject.aspx.cs" Inherits="mvc.Views.Projects.ListMyTasksInProject" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["Title"]%></span>
   	</div> 
   	<div id="monthChoose">
   	  <% int currentYear = 0; %>
   	  <% int lastMonth = 0; %>
   	  <% string next = "<br/>";
         string nextStyle = " style=\"clear:left;\"";
   	   %>
   	   <% if (ViewData.Model.CurrentMonth == null)
          { %>     
   	      <span class="selected">Visos užduotys</span>
       <% }
          else
          { %>
       <%= Html.ActionLink("Visos užduotys", "ListMyTasksInProject", new { project_id = ViewData.Model.ProjectID, page = ViewData.Model.Tasks.PageNumber })%>
       <%} %>
   	  <% foreach (MonthOfYear monthOfYear in ViewData.Model.Months) %>
   	  <% { %>   	     	        
   	     <% if ((monthOfYear != null)) %>
   	     <% { %>
   	            <%
                    if (monthOfYear.Year != currentYear)
                    { %>
                    <%= next %>
                     <%= "<label" + nextStyle + ">" + monthOfYear.Year.ToString() +  "</label>" %>
                    <%  next = "<br/>";
                        nextStyle = " style=\"clear:left;\"";
                        if (currentYear != 0)
                        for (int i = lastMonth + 1; i <= 12; i++)
                        {  %>
                        <span><%= MonthOfYear.getMonthName(i) %></span>    
                     <%   }    
                        currentYear = monthOfYear.Year;
                        lastMonth = 0;
                    }
                   for (int i = lastMonth + 1; i < monthOfYear.Month; i++)
                   { %>
                   <span><%= MonthOfYear.getMonthName(i) %></span>    
                <% }
                   lastMonth = monthOfYear.Month;
                   if ((this.ViewData.Model.CurrentMonth != null) && (this.ViewData.Model.CurrentMonth.Equals(monthOfYear)))
                   { %>
                    <span class="selected"><%= MonthOfYear.getMonthName(monthOfYear.Month)%></span>
             <% }
                   else
                   { %>   
   	            <%= Html.ActionLink(MonthOfYear.getMonthName(monthOfYear.Month), "ListMyTasksInProject", new { project_id = ViewData.Model.ProjectID, page = ViewData.Model.Tasks.PageNumber, year = monthOfYear.Year, month = monthOfYear.Month })%>
   	     <% }
                } %>
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
