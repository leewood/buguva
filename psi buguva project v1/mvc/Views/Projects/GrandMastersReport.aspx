<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GrandMastersReport.aspx.cs" Inherits="mvc.Views.Projects.GrandMastersReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%= Html.Path() %>
<ul id="menu">
   <li class="selected">
      <%= Html.ActionLink("Bendra Ataskaita", "GrandMastersReport", new {startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"] })%>
   </li>
   <li class="simple">
      <%= Html.ActionLink("Projektai", "AllProjects", new {startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"] })%>                    
   </li>
</ul>

<% className = ((bool)ViewData["chart"])? "simple" : "selected"; %>
<% className2 = ((bool)ViewData["chart"])? "selected" : "simple"; %>
<ul id="menu2">
   <li class='<%= className.ToString() %>'>
      <%= Html.ActionLink("Ataskaita", "GrandMastersReport", new {startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false })%>
   </li>
   <li class='<%= className2.ToString() %>'>
      <%= Html.ActionLink("Grafikas", "GrandMastersReport", new {startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = true })%>                    
   </li>
</ul>

<% Html.BeginForm("GrandMastersReport", "Projects", FormMethod.Get); %>  
  <label>Nuo:</label>
  Metai:<%=Html.TextBox("startYear") %> Mėnuo:<%=Html.DropDownList("startMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %>
  <label>Iki:</label>
  Metai:<%=Html.TextBox("endYear") %> Mėnuo:<%=Html.DropDownList("endMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %>
  <input type="submit" value="Pasirinkti" />  
  <%= Html.Hidden("chart") %>
<% Html.EndForm(); %>
<% if ((bool)ViewData["chart"]) %>
<% { %>
	<% string[] legends = {"Dirbo", "Nedirbo"}; %>
	<% string[] yAxes = { "TotalDepartmentWorked", "WorkedNoWhere" }; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Blue, System.Drawing.Color.Red};  %>
	<% System.Drawing.Color[] colors2 = { System.Drawing.Color.Navy, System.Drawing.Color.LightGreen, System.Drawing.Color.RoyalBlue };  %>
    <%= Html.PieChart<DepartmentManagerReport>(legends, ViewData.Model, "Period", yAxes, colors, System.Drawing.Color.White, "Visas įmonės darbas") %>
    <%= Html.PieChart<AssociatedWorkedHours>(legends, ViewData.Model.WorkedHoursOfOthers, "Title", "Hours", colors2, System.Drawing.Color.White, "Skyrių darbas projektuose")%>
<% } %>
<% else %>
<% { %>
<table>
 <tr>
   <td>Viso per laikotarpį dirbta</td><td><%= ViewData.Model.TotalDepartmentWorked %></td>
 </tr>
 <tr>
   <td>Nedirbta jokiame projekte</td><td><%= ViewData.Model.WorkedNoWhere %> val.( <%=ViewData.Model.PercentNotWorked%> viso įmonės darbo laiko per laikotarpį) </td>
 </tr>
</table>  
 <table class="grid">   
  <% foreach (AssociatedWorkedHours hours in ViewData.Model.WorkedHoursOfOthers) %>
  <% { %>
        <tr>
           <td style="width:145px"><%= Html.ActionLink("Skyrius: " + hours.Title, "DepartmentManagerReport", new { controller = "Departments", department_id = hours.AssociationID })%></td>
           <td><%= hours.Hours %></td>
        </tr>            
  <% } %>
 </table>

<% } %>
</asp:Content>
