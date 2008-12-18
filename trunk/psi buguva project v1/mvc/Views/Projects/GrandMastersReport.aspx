<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GrandMastersReport.aspx.cs" Inherits="mvc.Views.Projects.GrandMastersReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
   	  <img src="../../Content/Images/Icons/Print30.png" onclick="printSimpleReport()" class="print_image" alt="logo" style="float: right;" />  

   	</div> 
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
<fieldset class="years">
    <legend>Ataskaitos laikotarpis</legend>

  <div class="label">Metai nuo:</div>
  
  <%=Html.TextBox("startYear", ViewData["startYear"],new { style="width:50px; float: left;" } ) %> 
  
  <div class="label">Mėnuo:</div>
  
  <%=Html.DropDownList("startMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %><br />

  <div class="label">Metai iki:</div>
  
  <%=Html.TextBox("endYear", ViewData["endYear"], new { style = "width:50px; float: left;" })%>
  
  <div class="label">Mėnuo:</div>
  
  <%=Html.DropDownList("endMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %>
  
  <br />
  <input type="submit" value="Pasirinkti" />  
  
  <%= Html.Hidden("chart") %>
</fieldset>  
<% Html.EndForm(); %>

<% bool paintContent = true; %>
<% if (TempData.ContainsKey("errors")) {paintContent = false;}; %>
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>  
<% if (paintContent)
   { %>
<% if ((bool)ViewData["chart"]) %>
<% { %>
	<% string[] legends = { "Dirbo", "Nedirbo" }; %>
	<% string[] yAxes = { "TotalDepartmentWorked", "WorkedNoWhere" }; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Red, System.Drawing.Color.Yellow, System.Drawing.Color.Green };  %>
	<% System.Drawing.Color[] colors2 = { System.Drawing.Color.Red, System.Drawing.Color.Yellow, System.Drawing.Color.Green, System.Drawing.Color.YellowGreen, System.Drawing.Color.Khaki, System.Drawing.Color.Lavender };  %>
    <%= Html.PieChart<DepartmentManagerReport>(legends, ViewData.Model, "Period", yAxes, colors, System.Drawing.Color.White, "Visas įmonės darbas", "", 90)%>
    <%= Html.PieChart<AssociatedWorkedHours>(legends, ViewData.Model.WorkedHoursOfOthers, "Title", "Hours", colors2, System.Drawing.Color.White, "Skyrių darbas projektuose", "Skyrius ", 80)%>
<% } %>
<% else %>
<% { %>
<table>
 <tr>
   <td>Viso per laikotarpį dirbta</td><td><%= ViewData.Model.TotalDepartmentWorked%></td>
 </tr>
 <tr>
   <td>Nedirbta jokiame projekte</td><td><%= ViewData.Model.WorkedNoWhere%> val.( <%=ViewData.Model.PercentNotWorked%> viso įmonės darbo laiko per laikotarpį) </td>
 </tr>
</table>  
 <table class="grid" style="width:290px;">   
    <tr>
      <th style="width:145px">Skyrius</th>
      <th style="width:145px">Išdirbtas laikas (val.)</th>
      <th style="width:145px">Galėjo išdirbti (val.)</th>
      <th style="width:145px">Nedirbo (val.)</th>
    </tr>
  <% foreach (AssociatedWorkedHours hours in ViewData.Model.WorkedHoursOfOthers) %>
  <% { %>
        <tr>
           <td style="width:145px"><%= Html.ActionLink("Skyrius: " + hours.Title, "DepartmentManagerReport", new { controller = "Departments", department_id = hours.AssociationID })%></td>
           <td style="text-align:right;width:70px"><%= hours.Hours%></td>
           <td style="text-align:right;width:70px"><%= hours.CouldWorked%></td>
           <td style="text-align:right;width:70px"><%= hours.NotWorked%></td>
        </tr>            
  <% } %>
 </table>

<% }
   } %>
</asp:Content>
