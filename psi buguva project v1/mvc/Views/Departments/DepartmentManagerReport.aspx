<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentManagerReport.aspx.cs" Inherits="mvc.Views.Departments.DepartmentManagerReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%= Html.Path() %>
<ul id="menu">
   <li class="selected">
      <%= Html.ActionLink("Bendra Ataskaita", "DepartmentManagerReport", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false })%>
   </li>
   <li class="simple">
      <%= Html.ActionLink("Projektai", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = true })%>                    
   </li>
</ul>

<% if ((bool)ViewData["chart"]) %>
<% { %>
<% } %>
<% else %>
<% { %>
<table>
 <tr>
    <td>Nr.</td><td><%= ViewData.Model.DepartmentInfo.id %></td>
 </tr>
 <tr>
   <td>Skyriaus vadovas</td><td><%= ViewData.Model.DepartmentManagerTitle %></td>
 </tr>  
 <tr>
   <td>Viso skyrius dirbo projektuose</td><td><%= ViewData.Model.TotalDepartmentWorked %></td>
 </tr>
 <tr> 
  <td>Dirbo savo projektuose</td><td><%= ViewData.Model.ThisDepartmentWorkersWorkedInDepartmentProjects %></td>
 </tr> 
 <tr>
  <td>Dirbo kituose projektuose</td><td><%= ViewData.Model.ThisDepartmentWorkersWorkedInOtherProjects %></td>
 </tr> 
 <tr>
  <td>Kitų skyrių darbuotojai dirbo</td><td><%= ViewData.Model.OthersTotalWorked %></td>
 </tr> 
</table>  
 <table>   
  <% foreach (AssociatedWorkedHours hours in ViewData.Model.WorkedHoursOfOthers) %>
  <% { %>
        <tr>
           <td><%= hours.Title %></td>
           <td><%= hours.Hours %></td>
        </tr>            
  <% } %>
 </table>
 <label>Nedirbta jokiame projekte</label><label><%= ViewData.Model.WorkedNoWhere %> ( <%=ViewData.Model.PercentNotWorked%> viso įmonės darbo laiko per laikotarpį) </label>
<% } %>
</asp:Content>
