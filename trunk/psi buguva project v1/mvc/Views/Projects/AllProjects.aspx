<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AllProjects.aspx.cs" Inherits="mvc.Views.Projects.AllProjects" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["Title"]%></span>
   	  <img src="../../Content/Images/Icons/Print30.png" onclick="printSimpleReport()" class="print_image" alt="logo" style="float: right;" />  


   	</div> 
<ul id="menu">
   <li class="simple">
      <%= Html.ActionLink("Bendra ataskaita", "GrandMastersReport", new {startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"] })%>
   </li>
   <li class="selected">
      <%= Html.ActionLink("Projektai", "AllProjects", new {startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = (bool)ViewData["chart"], page = (int)ViewData["page"], pageSize = (int)ViewData["pageSize"] })%>                    
   </li>
</ul>

<% className = ((bool)ViewData["chart"])? "simple" : "selected"; %>
<% className2 = ((bool)ViewData["chart"])? "selected" : "simple"; %>
<ul id="menu2">
   <li class='<%= className.ToString() %>'>
      <%= Html.ActionLink("Ataskaita", "AllProjects", new {startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, page = (int)ViewData["page"], pageSize = (int)ViewData["pageSize"] , filter = ViewData["filter"], sorting=ViewData["sorting"] })%>
   </li>
   <li class='<%= className2.ToString() %>'>
      <%= Html.ActionLink("Grafikas", "AllProjects", new { startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = true, page = (int)ViewData["page"], pageSize = (int)ViewData["pageSize"], filter = ViewData["filter"], sorting = ViewData["sorting"] })%>                    
   </li>
</ul>

<% Html.BeginForm("AllProjects", "Projects", FormMethod.Get); %> 
<fieldset class="years">
    <legend>Ataskaitos laikotarpis</legend>
    
  <div style="width: 70px; float: left;">Metai nuo:</div>
  
  <%=Html.TextBox("startYear", ViewData["startYear"],new { style="width:50px; float: left;" } ) %> 
  
  <div style="width: 70px; float: left; text-align: center;">Mėnuo:</div>
  
  <%=Html.DropDownList("startMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %><br />

  <div style="width: 70px; float: left;">Metai iki:</div>
  
  <%=Html.TextBox("endYear", ViewData["endYear"], new { style = "width:50px; float: left;" })%>
  
  <div style="width: 70px; float: left; text-align: center;">Mėnuo:</div>
  
  <%=Html.DropDownList("endMonth", MonthOfYear.monthsList((int)ViewData["startMonth"])) %>
  <%= Html.Hidden("filter", ViewData["filter"]) %>
  <%= Html.Hidden("sorting", ViewData["sorting"]) %>
  <br />
  <input type="submit" value="Pasirinkti" /> 
  
 </fieldset> 
  <%= Html.Hidden("chart") %>
  <%= Html.Hidden("pageSize") %>
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

 <script type="text/javascript">

     var scrool_by = 5;

     var scrooled = 100;

     function scroolBy() {
         var element = document.getElementById("scrool_container");
         element.scrollLeft = element.scrollLeft + scrool_by;

         if (scrooled > 0)
             setTimeout("scroolBy()", 5);

         scrooled = scrooled - 1;
     }

     function scrool(left) {
         scrool_by = left;
         scrooled = 25;
         scroolBy();
     }
 
 </script>

	<% string[] legends = { "Darbuotojai dirbo" }; %>
	<% string[] yAxes = { "TotalWorked" }; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Green };  %>
	<% System.Drawing.Color[] colors2 = { System.Drawing.Color.Navy, System.Drawing.Color.LightGreen, System.Drawing.Color.RoyalBlue };  %>
	
<div id="monthChoose">
 <label style="">Paslinkti sąrašą: </label>
 <a href="javascript:scrool(-10);">< į kairę</a>
 <a href="javascript:scrool(10);">į dešinę ></a>
 </div>
 
 <div style="display: run-in; overflow : auto;" id="scrool_container">	
	
	<%= Html.BarChart<DepartmentProjectReport>(legends, ViewData.Model, "Title", yAxes, colors, System.Drawing.Color.White, "Skyriaus darbuotojų darbo projektuose grafikas", 90, 15, true, (ViewData.Model.Count > 5) ? ViewData.Model.Count * 30 + 160 : 600, (ViewData.Model.Count > 5) ? /*ViewData.Model.Count * 20 + 110*/400 : 400, "Projektas ", true, "", "")%>
</div>
<% } %>
<% else %>
<% { %>
 <div class="pager">
   <%= Html.Pager((int)ViewData["pageSize"], (int)ViewData["page"], (int)ViewData["pageCount"], new { startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, page = (int)ViewData["page"], pageSize = (int)ViewData["pageSize"], filter = ViewData["filter"], sorting=ViewData["sorting"]})%>
 </div>
 <table class="grid">   
	   <tr>
	     <td colspan="5">
	        <% Html.BeginForm("AllProjects", "Projetcs", FormMethod.Get); %>	         
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("startYear", ViewData["startYear"]) %>
	           <%= Html.Hidden("endYear", ViewData["endYear"])%>
	           <%= Html.Hidden("startMonth", ViewData["startMonth"])%>
	           <%= Html.Hidden("endMonth", ViewData["endMonth"])%>
	           <%= Html.Hidden("chart", ViewData["chart"])%>
	           <%= Html.Hidden("pageSize", ViewData["pageSize"]) %>
	           <%= Html.Hidden("page", ViewData["page"])%>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr> 
    <tr>
       
       <%= Html.SortingHeader("Projekto kodas", "Title", "", 0, new { page = ViewData["page"], sorting = ViewData["sorting"], filter = ViewData["filter"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, pageSize = (int)ViewData["pageSize"] })%>
       <%= Html.SortingHeader("Vadovas", "Manager", "", 0, new { page = ViewData["page"], sorting = ViewData["sorting"], filter = ViewData["filter"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, pageSize = (int)ViewData["pageSize"] })%>       
       <%= Html.SortingHeader("Skyrius", "ManagerDepartment", "", 0, new { page = ViewData["page"], sorting = ViewData["sorting"], filter = ViewData["filter"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, pageSize = (int)ViewData["pageSize"] })%>
       <%= Html.SortingHeader("Pradžia", "Started", "", 0, new { page = ViewData["page"], sorting = ViewData["sorting"], filter = ViewData["filter"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, pageSize = (int)ViewData["pageSize"] })%>
       <%= Html.SortingHeader("Pabaiga", "Ended", "", 0, new { page = ViewData["page"], sorting = ViewData["sorting"], filter = ViewData["filter"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, pageSize = (int)ViewData["pageSize"] })%>
       <%= Html.SortingHeader("Viso dirbta", "TotalWorked", "", 0, new { page = ViewData["page"], sorting = ViewData["sorting"], filter = ViewData["filter"], startYear = (int)ViewData["startYear"], endYear = (int)ViewData["endYear"], startMonth = (int)ViewData["startMonth"], endMonth = (int)ViewData["endMonth"], chart = false, pageSize = (int)ViewData["pageSize"] })%>       
    </tr>
  <% foreach (DepartmentProjectReport projectLine in ViewData.Model) %>
  <% { %>
        <tr>
           <td><%= Html.ActionLink(projectLine.Title, "ProjectManagerReport", new { controller = "Projects", project_id = projectLine.ProjectID })%></td>
           <td><%= (projectLine.ManagerID > 0) ? Html.ActionLink(projectLine.Manager, "ListMyProjects", new { controller = "Projects", id = projectLine.ManagerID }) : "<span style=\"color:Red\">" + projectLine.Manager + "</span>"%></td>
           <td><%= (projectLine.DepartmentID > 0) ? Html.ActionLink(projectLine.ManagerDepartment, "DepartmentManagerReport", new { controller = "Departments", department_id = projectLine.DepartmentID }) : "<span style=\"color:Red\">" + projectLine.ManagerDepartment + "</span>"%></td>
           <td><%= projectLine.Started%></td>
           <td><%= projectLine.Ended%></td>
           <td style="text-align:right"><%= projectLine.TotalWorked%></td>
        </tr>            
  <% } %>
	   <% if (ViewData.Model.Count == 0) %>
	   <% { %>
	      <tr>
	        <td>Nėra jokių projektų</td>
	      </tr>
	   <% } %>  
 </table>
 <% Html.BeginForm("AllProjects", "Projects", FormMethod.Get); %>  

   <%= Html.Hidden("chart")%>
   <%= Html.Hidden("startYear")%>
   <%= Html.Hidden("startMonth")%>
   <%= Html.Hidden("endYear")%>
   <%= Html.Hidden("endMonth")%>
   <%= Html.Hidden("sorting", ViewData["sorting"]) %>
   <%= Html.Hidden("filter", ViewData["filter"]) %>
   <% List<int> pageSizes = new List<int>(); %>
   <% for (int i = 5; i <= 50; i += 5) pageSizes.Add(i); %>
   <label>Įrašų per puslapį</label><%= Html.DropDownList("pageSize", new SelectList(pageSizes, ViewData["pageSize"]), new { onChange = "javascript: form.submit();" })%>
 <% Html.EndForm(); %>
<% }
   }%>
</asp:Content>
