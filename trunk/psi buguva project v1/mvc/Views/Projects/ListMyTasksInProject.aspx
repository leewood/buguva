<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ListMyTasksInProject.aspx.cs" Inherits="mvc.Views.Projects.ListMyTasksInProject" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["Title"]%></span>
<img src="../../Content/Images/Icons/Print30.png" onclick="printLandscapeTable()" class="print_image" alt="logo" style="float: right;" />  

   	</div> 
   	<div id="monthChoose" style="height: auto">
   	  <% int currentYear = 0; %>
   	  <% int lastMonth = 0; %>
   	  <% string next = "";
         string nextStyle = " style=\"\"";
   	   %>
   	  <table style="margin: 0px">
   	   <%= "<tr><td>"%>
   	   <% if (ViewData.Model.CurrentMonth == null)
          { %>     
   	      <span class="selected" style="">Visos užduotys</span>
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
                     <%= "</td></tr><tr><td><label" + nextStyle + ">" + monthOfYear.Year.ToString() + "</label>"%>
                    <%  next = "";
                        nextStyle = " style=\"\"";
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
   	  <%= "</td></tr>" %>
   	  </table>
   	</div>
   	<% if (ViewData.Model.Tasks.PageCount > 1)
       { %>
   	<div class="pager">   	
		<%= Html.Pager(ViewData.Model.Tasks.PageSize, ViewData.Model.Tasks.PageNumber, ViewData.Model.Tasks.TotalItemCount)%>
	</div>
	<% } %>
	<table class = "grid">
	   <tr>
	      <th>Kodas</th>
	      <th>Išdirbta(val.)</th>
	   </tr>
	   <% foreach (Task task in ViewData.Model.Tasks) %>
       <% { %>
            <tr class=''>
             <td><%= task.id.ToString() %></td>
             <td style="text-align:right"><%= task.worked_hours.ToString() %></td>
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
