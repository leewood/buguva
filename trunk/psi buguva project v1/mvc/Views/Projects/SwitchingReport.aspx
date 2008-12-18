<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="SwitchingReport.aspx.cs" Inherits="mvc.Views.Projects.SwitchingReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class = "path">
   	  <img src="../../Content/Images/Icons/Refresh_big.png" alt="logo"><span class="title"><%= ViewData["Title"]%></span>
   	</div>   
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager((int)ViewData["size"], (int)ViewData["page"], (int)ViewData["total"])%>
	</div>   
   

	<div style="display: run-in; overflow : auto;" id="scrool_container">
	
	<table class = "grid">
	   <tr> 
	   <th><%= ViewData.Model.Captions[0] %></th>
	   <% for (int i = 1; i < ViewData.Model.Captions.Count; i++)
       { %>
       
         <th><%= (ViewData.Model.Redirections[i] == null) ? ViewData.Model.Captions[i] : Html.ActionLink(ViewData.Model.Captions[i], ViewData.Model.Actions[i], ViewData.Model.Redirections[i], new RouteValueDictionary()) %></th>
	   <%} %>
	   </tr>    

	   <% SwitchingReportRow tempRow = new SwitchingReportRow(); %>
	   <% foreach (SwitchingReportRow row in ViewData.Model.Rows)
       { %>
       <tr>
       <% if (row.Period == "")
          {
              tempRow = row;
              %>
             <%= "<th colspan=\"" + (row.Swsums.Count + 1).ToString() + "\"></th>"%>
       <% }
          else if (row.Period == "Viso ")
          {%>
         <td>
           <%= row.Period%>
         </td>
         <% for (int i = 0; i < row.Swsums.Count; i++)
            { %>
            <td style="text-align:right;"><%= row.Swsums[i] %></td>
         <% } %>
          
       <% }
          else
          { %>
         <td>
           <%= row.Period%>
         </td>
         <% for (int i = 0; i < row.Swsums.Count; i++)
            { %>
            <td style="text-align:right;"><%= row.Swsums[i]%></td>
         <% }
          } %>
       </tr>
	   <% } %>
	</table>

    </div>



</asp:Content>
