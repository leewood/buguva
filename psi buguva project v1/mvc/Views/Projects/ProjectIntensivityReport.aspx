<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ProjectIntensivityReport.aspx.cs" Inherits="mvc.Views.Projects.ProjectIntensivityReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%= Html.Path() %>
<ul id="menu">
   <li class="selected">
      <%= Html.ActionLink("Projekto ataskaita", "ProjectManagerReport", new { project_id = (int)ViewData["project_id"]}) %>
   </li>
   <li class="simple">
      <%= Html.ActionLink("Projekto intensyvumas", "ProjectIntensivityReport", new { project_id = (int)ViewData["project_id"] })%>                    
   </li>
</ul>
	<table class = "grid">
	   <tr>
	      <th>Laikotarpis</th>
	      <th>Viso dirbta</th>
	      <th>Projekto skyriaus darbuotojų dirbta</th>
	      <th>Darbuotojų iš kitų skyrių dirbta</th>
	   </tr>	  
	   
	   <% foreach (ProjectIntensivity projectIntensivity in ViewData.Model) %>
       <% { %>            
            <tr>             
             <td>#<%= projectIntensivity.Period %></td>
             <td><%= projectIntensivity.TotalWorkedHours %></td>             
             <td><%= projectIntensivity.ProjectsWorkersWorkedHours %></td>             
             <td><%= projectIntensivity.OthersWorkedHours %></td>
          </tr>
	   <% } %>
	   <% if (ViewData.Model.Count == 0) %>
	   <% { %>
	      <tr>
	        <td>Projekte dar neatlikta jokių darbų</td>
	      </tr>
	   <% } %>
	</table>
	<% string[] legends = {"Viso dirbta", "Projekto skyriaus darbuotojų", "Kitų darbuotojų" }; %>
	<% string[] yAxes = {"TotalWorkedHours", "ProjectsWorkersWorkedHours", "OthersWorkedHours"}; %>
	<% System.Drawing.Color[] colors = { System.Drawing.Color.Blue, System.Drawing.Color.Violet, System.Drawing.Color.Yellow };  %>
    <%= Html.LineChart<ProjectIntensivity>(legends, ViewData.Model, "Period", yAxes, colors) %>

</asp:Content>
