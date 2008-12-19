<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ProjectIntensivityReport.aspx.cs" Inherits="mvc.Views.Projects.ProjectIntensivityReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["Title"]%></span>
<img src="../../Content/Images/Icons/Print30.png" onclick="printLandscapeTable()" class="print_image" alt="logo" style="float: right;" />  

   	</div> 
<ul id="menu">
   <li class="simple">
      <%= Html.ActionLink("Projekto ataskaita", "ProjectManagerReport", new { project_id = (int)ViewData["project_id"]}) %>
   </li>
   <li class="selected">
      <%= Html.ActionLink("Projekto intensyvumas", "ProjectIntensivityReport", new { project_id = (int)ViewData["project_id"] })%>                    
   </li>
</ul>
    <!--
    <label style="margin-left: 30px; font-size: larger"><b>Projekto kodas: </b><%= ((ViewData["projectCode"] != null)?ViewData["projectCode"].ToString():"") %></label><br /><br />
	-->
	<% string[] legends = {"Viso", "Projekto skyriaus darbuotojų", "Kitų darbuotojų" }; %>
	<% string[] yAxes = {"TotalWorkedHours", "ProjectsWorkersWorkedHours", "OthersWorkedHours"}; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Black };  %>
    
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
 <div id="monthChoose" class="noprint">
 <label style="width: 100px;">Paslinkti sąrašą: </label>
 <a href="javascript:scrool(-10);">< į kairę</a>
 <a href="javascript:scrool(10);">į dešinę ></a>
 </div>
	<div style="display: run-in; overflow : auto;" id="scrool_container">
    <%= Html.LineChart<ProjectIntensivity>(legends, ViewData.Model, "Period", yAxes, colors, System.Drawing.Color.White, "Projekto intensyvumas", "\nLaikotarpis (mėn.)", "Dirbo (Val.)", (ViewData.Model.Count > 5) ? ViewData.Model.Count * 50 + 160 : 600, 400)%>
    </div>
    <% IPagedList<ProjectIntensivity> pagedModel = ViewData.Model.ToPagedList((int)ViewData["curPage"] - 1, (int)ViewData["itemsPerPage"]); %>    
    <div class = "pager">
       <%= Html.Pager(pagedModel.PageSize, pagedModel.PageNumber, pagedModel.TotalItemCount, new { project_id = ViewData["project_id"]})%>
    </div>
    
    
    
	<table class = "grid">
	   <tr>
	     <td colspan="4">
	        <% Html.BeginForm("List", "Tasks", FormMethod.Get); %>
	           <%= Html.TextBox("filter", ViewData["filter"]) %>
	           <%= Html.Hidden("page", pagedModel.PageNumber) %>
	           <%= Html.Hidden("sorting", ViewData["sorting"]) %>
	           <%= Html.Hidden("project_id", ViewData["project_id"]) %>
	           <input type="submit" value="Filtruoti" />	           
	        <% Html.EndForm(); %>
	      </td>
	    </tr>
	
	   <tr>
	      <%= Html.SortingHeader("Laikotarpis", "Period", "", 0, new { page = pagedModel.PageNumber, project_id = ViewData["project_id"], sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <%= Html.SortingHeader("Viso dirbta", "TotalWorkedHours", "", 0, new { page = pagedModel.PageNumber, project_id = ViewData["project_id"], sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <%= Html.SortingHeader("Projekto skyriaus darbuotojų dirbta", "ProjectsWorkersWorkedHours", "", 0, new { page = pagedModel.PageNumber, project_id = ViewData["project_id"], sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      <%= Html.SortingHeader("Darbuotojų iš kitų skyrių dirbta", "OthersWorkedHours", "", 0, new { page = pagedModel.PageNumber, project_id = ViewData["project_id"], sorting = ViewData["sorting"], filter = ViewData["filter"] })%>
	      
	   </tr>	  	  
	   <% foreach (ProjectIntensivity projectIntensivity in pagedModel) %>
       <% { %>            
            <tr>             
             <td><%= projectIntensivity.Period %></td>
             <td style="text-align:right"><%= projectIntensivity.TotalWorkedHours %></td>             
             <td style="text-align:right"><%= projectIntensivity.ProjectsWorkersWorkedHours %></td>             
             <td style="text-align:right"><%= projectIntensivity.OthersWorkedHours %></td>
          </tr>
	   <% } %>
	   <% if (ViewData.Model.Count == 0) %>
	   <% { %>
	      <tr>
	        <td>Projekte dar neatlikta jokių darbų</td>
	      </tr>
	   <% } %>
	</table>

</asp:Content>
