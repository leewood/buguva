<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="IncompleteWorkReport.aspx.cs" Inherits="mvc.Views.Projects.IncompleteWorkReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
   	</div>   
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", (string[])TempData["errors"]) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager((int)ViewData["size"], (int)ViewData["page"], (int)ViewData["total"])%>
	</div>   
    <% Html.BeginForm("IncompleteWorkReport", "Projects", FormMethod.Get); %>  
       <% List<KeyValuePair<string, int>> types = new List<KeyValuePair<string, int>>(); %>
       <% types.Add(new KeyValuePair<string,int>("Metai", 1));%>
       <% types.Add(new KeyValuePair<string,int>("Pusmečiai", 2));%>
       <% types.Add(new KeyValuePair<string,int>("Ketvirčiai", 3));%>
       <% types.Add(new KeyValuePair<string,int>("Mėnesiai", 4));%>
   
   <label>Periodo tipas:</label><%= Html.DropDownList("type", new SelectList(types, "Value", "Key", ViewData["type"]), new { onChange = "javascript: form.submit();"})%>
 <% Html.EndForm(); %>
	
	<table class = "grid">
	   <tr>
	   <th rowspan="2"><%= ViewData.Model.Captions[0] %></th>
	   <% for (int i = 1; i < ViewData.Model.Captions.Count; i++)
       { %>
         <th colspan="4"><%= (ViewData.Model.Redirections[i] == null) ? ViewData.Model.Captions[i] : Html.ActionLink(ViewData.Model.Captions[i], ViewData.Model.Actions[i], ViewData.Model.Redirections) %></th>
	   <%} %>
	   </tr>    
	   <tr>
	   <% for (int i = 1; i < ViewData.Model.Captions.Count; i++)
       { %>
         <th>Vertė</th><th>Įplaukos</th><th>Skirtumas</th><th>Dalis %</th>
	   <%} %>
	   
	   </tr>
	   <% foreach (IncompleteWorkValueReportRow row in ViewData.Model.Rows)
       { %>
       <tr>
         <td>
           <%= row.Period %>
         </td>
         <% for (int i = 0; i < row.Cells.Count; i++)
            { %>
            <td><%= row.Cells[i].Value.ToString() %></td>
            <td><%= row.Cells[i].Income.ToString("0.00") %></td>
            <td><%= row.Cells[i].Difference.ToString("0.00") %></td>
            <td><%= row.Cells[i].Percent %></td>
         <% } %>
       </tr>
	   <%} %>
	</table>

</asp:Content>
