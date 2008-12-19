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
       <%= Html.ActionLink("Visos užduotys", "ListMyTasksInProject", new { project_id = ViewData.Model.ProjectID, page = ViewData.Model.Tasks.PageNumber, filter = ViewData["filter"], sorting = ViewData["sorting"] })%>       
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
   	            <%= Html.ActionLink(MonthOfYear.getMonthName(monthOfYear.Month), "ListMyTasksInProject", new { project_id = ViewData.Model.ProjectID, page = ViewData.Model.Tasks.PageNumber, year = monthOfYear.Year, month = monthOfYear.Month, filter=ViewData["filter"], sorting = ViewData["sorting"] })%>
   	     <% }
                } %>
   	  <% } %>
   	  <%= "</td></tr>" %>
   	  </table>
   	</div>
   	<% if (ViewData.Model.Tasks.PageCount > 1)
       { %>
   	<div class="pager">   	
		<%= Html.Pager(ViewData.Model.Tasks.PageSize, ViewData.Model.Tasks.PageNumber, ViewData.Model.Tasks.TotalItemCount, new { filter = ViewData["filter"], sorting = ViewData["sorting"],  year = ViewData["year"], month = ViewData["month"], id=ViewData["workedID"], project_id=ViewData["project_id"]})%>
	</div>
	<% } %>
	<table class = "grid">
	   <tr>
	     <td colspan="3">
	        <% Html.BeginForm("ListMyTasksInProject", "Projects", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", ViewData.Model.Tasks.PageNumber) %>
	           <%= Html.Hidden("id", ViewData["workerID"]) %>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <%= Html.Hidden("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("project_id", ViewData["project_id"]) %>
	           <%= Html.Hidden("year", ViewData["year"])%>
	           <%= Html.Hidden("month", ViewData["month"])%>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>	
	
	   <tr>
	      <%= Html.SortingHeader("Kodas", "id", "", 0, new { page = ViewData.Model.Tasks.PageNumber, filter = ViewData["filter"], sorting = ViewData["sorting"], year = ViewData["year"], month = ViewData["month"], id = ViewData["workedID"], project_id = ViewData["project_id"] })%>
	      <%= Html.SortingHeader("Išdirbta(val.)", "worked_hours", "", 0, new { page = ViewData.Model.Tasks.PageNumber, filter = ViewData["filter"], sorting = ViewData["sorting"], year = ViewData["year"], month = ViewData["month"], id = ViewData["workedID"], project_id = ViewData["project_id"] })%>	      	      
	      <th>Veiksmai</th>
	   </tr>
	   <% foreach (Task task in ViewData.Model.Tasks) %>
       <% { %>
            <tr class=''>
             <td><%= task.id.ToString() %></td>
             <td style="text-align:right"><%= task.worked_hours.ToString() %></td>
             <td> 
	        <% if (task.administrationEdit()) %>
	          <%= Html.ActionImageLink("/Content/edit.png", "Koreguoti", "Edit", new {controller = "Tasks", id = task.id, back = true}) %>
            <% if (task.administrationDelete()) %>	          
	          <%= Html.ActionImageLink("/Content/delete.png", "Trinti", "Delete", new {controller = "Tasks", id = task.id, back = true}, true, "Ar tikrai norite ištrinti šią užduotį?") %>	          
             </td>
          </tr>
	   <% } %>
	   <% if (ViewData.Model.Tasks.Count == 0) %>
	   <% { %>
	      <tr>
	        <td>Nebuvo jokių užduočių</td>
	      </tr>
	   <% } %>
	</table>
	<% if (mvc.Models.Task.administrationNew())
    { %>
	<%= Html.ActionImageLink("/Content/new.png", "", "New", new { })%><%= Html.ActionLink("Nauja užduotis", "New", new { project_id = ViewData["project_id"], controller = "Tasks", year = ViewData["year"], month = ViewData["month"], back = true })%>
	<% } %>
	
</asp:Content>
