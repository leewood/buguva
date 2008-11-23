<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentProjects.aspx.cs" Inherits="mvc.Views.Departments.DepartmentProjects" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%= Html.Path() %>
<ul id="menu">
   <li class="simple">
      <%= Html.ActionLink("Bendra Ataskaita", "DepartmentManagerReport", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"] })%>
   </li>
   <li class="selected">
      <%= Html.ActionLink("Projektai", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"], page = (int)ViewData["page"], pageSize = (int)ViewData["pageSize"] })%>                    
   </li>
</ul>

<% className = ((bool)ViewData["chart"])? "simple" : "selected"; %>
<% className2 = ((bool)ViewData["chart"])? "selected" : "simple"; %>
<ul id="menu2">
   <li class='<%= className.ToString() %>'>
      <%= Html.ActionLink("Ataskaita", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, page = (int)ViewData["page"], pageSize = (int)ViewData["pageSize"] })%>
   </li>
   <li class='<%= className2.ToString() %>'>
      <%= Html.ActionLink("Grafikas", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = true, page = (int)ViewData["page"], pageSize = (int)ViewData["pageSize"] })%>                    
   </li>
</ul>

<% Html.BeginForm("DepartmentProjects", "Departments", FormMethod.Get); %>  
  <label>Nuo:</label>
  Metai:<%=Html.TextBox("startYear") %> Mėnuo:<%=Html.DropDownList("startMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %>
  <label>Iki:</label>
  Metai:<%=Html.TextBox("endYear") %> Mėnuo:<%=Html.DropDownList("endMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %>  
  <input type="submit" value="Pasirinkti" />
  <%= Html.Hidden("department_id") %>
  <%= Html.Hidden("chart") %>
  <%= Html.Hidden("page") %>
  <%= Html.Hidden("pageSize") %>
<% Html.EndForm(); %>
<% if ((bool)ViewData["chart"]) %>
<% { %>
	<% string[] legends = {"Savo", "Kitų", "Nedirbo" }; %>
	<% string[] yAxes = { "ThisDepartmentWorkersWorkedInDepartmentProjects", "ThisDepartmentWorkersWorkedInOtherProjects", "WorkedNoWhere" }; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Green };  %>
	<% System.Drawing.Color[] colors2 = { System.Drawing.Color.Navy, System.Drawing.Color.LightGreen, System.Drawing.Color.RoyalBlue };  %>
<% } %>
<% else %>
<% { %>
 <div class="pager">
   <%= Html.Pager((int)ViewData["pageSize"], (int)ViewData["page"], (int)ViewData["pageCount"]) %>
 </div>
 <table class="grid">   
    <tr>
       <td>Projekto kodas</td>
       <td>Vadovas</td>
       <td>Skyrius</td>
       <td>Pradžia</td>
       <td>Pabaiga</td>
       <td>Viso dirbta</td>
       <td>Skyriaus darbuotojų dirbta</td>
       <td>Kitų darbuotojų dirbta</td>
    </tr>
  <% foreach (DepartmentProjectReport projectLine in ViewData.Model) %>
  <% { %>
        <tr>
           <td><%= projectLine.Title %></td>
           <td><%= projectLine.Manager %></td>
           <td><%= projectLine.ManagerDepartment %></td>
           <td><%= projectLine.Started %></td>
           <td><%= projectLine.Ended %></td>
           <td><%= projectLine.TotalWorked %></td>
           <td><%= projectLine.DepartmentWorkersWorked %></td>
           <td><%= projectLine.OthersWorked %></td>
        </tr>            
  <% } %>
	   <% if (ViewData.Model.Count == 0) %>
	   <% { %>
	      <tr>
	        <td>Nėra jokių projektų</td>
	      </tr>
	   <% } %>  
 </table>
 
<% } %>
</asp:Content>
