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
      <%= Html.ActionLink("Projektai", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"], page = (int)ViewData["pageExt"], pageSize = (int)ViewData["pageSizeExt"], showOnlyMyProjects = (bool)ViewData["viewOnlyMy"] })%>                    
   </li>
</ul>

<% className = ((bool)ViewData["chart"])? "simple" : "selected"; %>
<% className2 = ((bool)ViewData["chart"])? "selected" : "simple"; %>
<ul id="menu2">
   <li class='<%= className.ToString() %>'>
      <%= Html.ActionLink("Ataskaita", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, page = (int)ViewData["pageExt"], pageSize = (int)ViewData["pageSizeExt"], showOnlyMyProjects = (bool)ViewData["viewOnlyMy"] })%>
   </li>
   <li class='<%= className2.ToString() %>'>
      <%= Html.ActionLink("Grafikas", "DepartmentProjects", new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = true, page = (int)ViewData["pageExt"], pageSize = (int)ViewData["pageSizeExt"], showOnlyMyProjects = (bool)ViewData["viewOnlyMy"] })%>                    
   </li>
</ul>

<% Html.BeginForm("DepartmentProjects", "Departments", FormMethod.Get); %>  
  <label>Nuo:</label>
  Metai:<%=Html.TextBox("startYear") %> Mėnuo:<%=Html.DropDownList("startMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %><br />
  <label>Iki:</label>
  Metai:<%=Html.TextBox("endYear") %> Mėnuo:<%=Html.DropDownList("endMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %>  
  <% System.Collections.Generic.Dictionary<string, bool> list = new Dictionary<string,bool>(); %>
  <% list.Add("Visi susiję projektai", false); %>
  <% list.Add("Tik šio skyriaus projektai", true); %>
  <%=Html.DropDownList("showOnlyMyProjects", new SelectList(list, "Value", "Key", ViewData["viewOnlyMy"])) %>

  <input type="submit" value="Pasirinkti" />
  <%= Html.Hidden("department_id") %>
  <%= Html.Hidden("chart") %>
  <%= Html.Hidden("pageSize") %>
<% Html.EndForm(); %>
<% if ((bool)ViewData["chart"]) %>
<% { %>
	<% string[] legends = {"Skyriaus darbuotojai dirbo", "Kiti dirbo"}; %>
	<% string[] yAxes = { "DepartmentWorkersWorked", "OthersWorked"}; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Green };  %>
	<% System.Drawing.Color[] colors2 = { System.Drawing.Color.Navy, System.Drawing.Color.LightGreen, System.Drawing.Color.RoyalBlue };  %>
	
	<%= Html.BarChart<DepartmentProjectReport>(legends, ViewData.Model, "Title", yAxes, colors, System.Drawing.Color.White, "Skyriaus darbuotojų darbo projektuose grafikas", 90, 15, true, (ViewData.Model.Count > 5)? ViewData.Model.Count * 30 + 160: 600, (ViewData.Model.Count > 5)? ViewData.Model.Count * 20 + 110: 400, "Projektas ", true) %>
<% } %>
<% else %>
<% { %>
 <div class="pager">
   <%= Html.Pager((int)ViewData["pageSizeExt"], (int)ViewData["pageExt"], (int)ViewData["pageCountExt"] * (int)ViewData["pageSizeExt"], new { department_id = (int)ViewData["department_id"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, pageSize = (int)ViewData["pageSizeExt"] })%>
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
           <td><%= Html.ActionLink(projectLine.Title, "ProjectManagerReport", new {controller = "Projects", project_id = projectLine.ProjectID}) %></td>
           <td><%= (projectLine.ManagerID > 0) ? Html.ActionLink(projectLine.Manager, "ListMyProjects", new { controller = "Projects", id = projectLine.ManagerID }) : "<span style=\"color:Red\">" + projectLine.Manager + "</span>" %></td>
           <td><%= (projectLine.DepartmentID > 0) ? Html.ActionLink(projectLine.ManagerDepartment, "DepartmentManagerReport", new { department_id = projectLine.DepartmentID }) : "<span style=\"color:Red\">" + projectLine.ManagerDepartment + "</span>"%></td>
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
 <% Html.BeginForm("DepartmentProjects", "Departments", FormMethod.Get); %>  
   <%= Html.Hidden("department_id") %>
   <%= Html.Hidden("chart") %>
   <%= Html.Hidden("startYear") %>
   <%= Html.Hidden("startMonth") %>
   <%= Html.Hidden("endYear") %>
   <%= Html.Hidden("endMonth") %>
   <%= Html.Hidden("showOnlyMyProjects") %>
   <% List<int> pageSizes = new List<int>(); %>
   <% for (int i = 5; i <= 50; i += 5) pageSizes.Add(i); %>
   <label>Įrašų per puslapį</label><%= Html.DropDownList("pageSize", new SelectList(pageSizes, ViewData["pageSizeExt"]), new { onChange = "javascript: form.submit();"})%>
 <% Html.EndForm(); %>
<% } %>
</asp:Content>
