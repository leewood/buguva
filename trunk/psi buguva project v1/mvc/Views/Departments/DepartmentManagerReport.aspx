<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentManagerReport.aspx.cs" Inherits="mvc.Views.Departments.DepartmentManagerReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
   	</div> 
<ul id="menu">
   <li class="selected">
      <%= Html.ActionLink("Bendra Ataskaita", "DepartmentManagerReport", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"] })%>
   </li>
   <li class="simple">
      <%= Html.ActionLink("Projektai", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"] })%>                    
   </li>
</ul>

<% className = ((bool)ViewData["chart"])? "simple" : "selected"; %>
<% className2 = ((bool)ViewData["chart"])? "selected" : "simple"; %>
<ul id="menu2">
   <li class='<%= className.ToString() %>'>
      <%= Html.ActionLink("Ataskaita", "DepartmentManagerReport", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false })%>
   </li>
   <li class='<%= className2.ToString() %>'>
      <%= Html.ActionLink("Grafikas", "DepartmentManagerReport", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = true })%>                    
   </li>
</ul>
<div id="years_form">
<% Html.BeginForm("DepartmentManagerReport", "Departments", FormMethod.Get); %>  

<fieldset class="years">
    <legend>Ataskaitos laikotarpis</legend>

  <div class="label">Metai nuo:</div>
  
  <%=Html.TextBox("startYear", ViewData["startYear"],new { style="width:50px; float: left;" } ) %> 
  
  <div class="label">Mėnuo:</div>
  
  <%=Html.DropDownList("startMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %><br />

  <div class="label">Metai iki:</div>
  
  <%=Html.TextBox("endYear", ViewData["endYear"], new { style = "width:50px; float: left;" })%>
  
  <div class="label">Mėnuo:</div>
  
  <%=Html.DropDownList("endMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %><br />
  
  
  <input type="submit" value="Pasirinkti" />
  <%= Html.Hidden("department_id") %>
  <%= Html.Hidden("chart") %>
 </fieldset>
<% Html.EndForm(); %>
</div>
<% bool paintContent = true; %>
<% if (TempData.ContainsKey("errors")) {paintContent = false;}; %>
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>  
<% if (paintContent) { %>
<% if ((bool)ViewData["chart"]) %>
<% { %>
	<% string[] legends = {"Savo projektuose", "Kitų projektuose", "Nedirbo" }; %>
	<% string[] yAxes = { "ThisDepartmentWorkersWorkedInDepartmentProjects", "ThisDepartmentWorkersWorkedInOtherProjects", "WorkedNoWhere" }; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Red, System.Drawing.Color.Yellow, System.Drawing.Color.Green };  %>
	<% System.Drawing.Color[] colors2 = { System.Drawing.Color.Red, System.Drawing.Color.Yellow, System.Drawing.Color.Green, System.Drawing.Color.YellowGreen, System.Drawing.Color.Khaki, System.Drawing.Color.Lavender };  %>
    <%= Html.PieChart<DepartmentManagerReport>(legends, ViewData.Model, "Period", yAxes, colors, System.Drawing.Color.White, "Skyriaus darbuotojų darbas", "", 90) %>
    <%= Html.PieChart<AssociatedWorkedHours>(legends, ViewData.Model.WorkedHoursOfOthers, "Title", "Hours", colors, System.Drawing.Color.White, "Kitų skyrių darbuotojų darbas šio skyriaus projektuose", "Skyrius: ", 90)%>
<% } %>
<% else %>
<% { %>
<fieldset class="report">

<table style="margin-bottom: 0px;">
 <tr>
    <td>Skyriaus kodas/pavadinimas</td><td><%= ViewData.Model.DepartmentInfo.title %></td>
 </tr>
 <tr>
   <td>Skyriaus vadovas</td><td><%= (ViewData.Model.DepartmentInfo.Worker != null) ? ((ViewData.Model.DepartmentInfo.Worker.canBeSeen())?Html.ActionLink(ViewData.Model.DepartmentManagerTitle, "ListMyProjects", new {controller="Projects", id = ViewData.Model.DepartmentInfo.Worker.id }):ViewData.Model.DepartmentManagerTitle) : ViewData.Model.DepartmentManagerTitle%></td>
 </tr>  
 <tr>
   <td>Viso skyrius dirbo projektuose</td><td style="text-align:right"><%= ViewData.Model.TotalDepartmentWorked %></td>
 </tr>
 <tr> 
  <td>Dirbo savo projektuose</td><td style="text-align:right"><%= ViewData.Model.ThisDepartmentWorkersWorkedInDepartmentProjects %></td>
 </tr> 
 <tr>
  <td>Dirbo kituose projektuose</td><td style="text-align:right"><%= ViewData.Model.ThisDepartmentWorkersWorkedInOtherProjects %></td>
 </tr> 
 <tr>
  <td>Kitų skyrių darbuotojai dirbo</td><td style="text-align:right"><%= ViewData.Model.OthersTotalWorked %></td>
 </tr> 
</table>  
 <table class="grid" style="max-width:250px">   
  <% foreach (AssociatedWorkedHours hours in ViewData.Model.WorkedHoursOfOthers) %>
  <% { %>
        <tr>          
           <%  mvc.Models.POADataModelsDataContext DBDataContext = new POADataModelsDataContext();
               Department department = DBDataContext.Departments.First(d => d.id == hours.AssociationID); %>
           <td style="width:145px"><%= (department.canBeSeen()) ? Html.ActionLink("Skyrius: " + hours.Title, "DepartmentManagerReport", new { controller = "Departments", department_id = hours.AssociationID }) : "Skyrius: " + hours.Title%></td>
           <td style="text-align:right"><%= hours.Hours %></td>
        </tr>            
  <% } %>
 </table>
 <table style="margin-top: 0px;"><tr><td>
 <label>Nedirbta jokiame projekte </label><label><%= ViewData.Model.WorkedNoWhere %> val.( <%=ViewData.Model.PercentNotWorked%> viso įmonės darbo laiko per laikotarpį) </label>
 </td></tr></table>
<% } %>

</fieldset>

<% } %>

</asp:Content>
