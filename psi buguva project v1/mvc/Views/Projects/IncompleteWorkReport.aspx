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
	  <%= Html.Pager((int)ViewData["size"], (int)ViewData["page"], (int)ViewData["total"], new { type = (int)ViewData["type"]})%>
	</div>   
    <% Html.BeginForm("IncompleteWorkReport", "Projects", FormMethod.Get); %>  
    <fieldset class="years">
     <legend>Ataskaitos laikotarpis</legend>
       <% List<KeyValuePair<string, int>> types = new List<KeyValuePair<string, int>>(); %>
       <% types.Add(new KeyValuePair<string,int>("Metai", 1));%>
       <% types.Add(new KeyValuePair<string,int>("Pusmečiai", 2));%>
       <% types.Add(new KeyValuePair<string,int>("Ketvirčiai", 3));%>
       <% types.Add(new KeyValuePair<string,int>("Mėnesiai", 4));%>
 
 
   
   <label>Periodo tipas:</label><%= Html.DropDownList("type", new SelectList(types, "Value", "Key", ViewData["type"]), new { onChange = "javascript: form.submit();"})%>
 </fieldset>
 <% Html.EndForm(); %>
 
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
 
 <div id="monthChoose">
 <label style="">Paslinkti sąrašą</label>
 <a onclick="scrool(-10)" href="#">< į kairę</a>
 <a onclick="scrool(10)" href="#">į dešinę ></a>
 </div>
	<div style="display: run-in; overflow : auto;" id="scrool_container">
	
	<table class = "grid">
	   <tr>
	   <th rowspan="2"><%= ViewData.Model.Captions[0] %></th>
	   <% for (int i = 1; i < ViewData.Model.Captions.Count; i++)
       { %>
       
         <th colspan="4"><%= (ViewData.Model.Redirections[i] == null) ? ViewData.Model.Captions[i] : Html.ActionLink(ViewData.Model.Captions[i], ViewData.Model.Actions[i], ViewData.Model.Redirections[i], new RouteValueDictionary()) %></th>
	   <%} %>
	   </tr>    
	   <tr>
	   <% for (int i = 1; i < ViewData.Model.Captions.Count; i++)
       { %>
         <th>Vertė</th><th>Įplaukos</th><th>Skirtumas</th><th>Dalis %</th>
	   <%} %>
	   
	   </tr>
	   <% IncompleteWorkValueReportRow tempRow = new IncompleteWorkValueReportRow(); %>
	   <% foreach (IncompleteWorkValueReportRow row in ViewData.Model.Rows)
       { %>
       <tr>
       <% if (row.Period == "")
          {
              tempRow = row;
              %>
             <th></th>
             <% for (int i = 0; i < row.Cells.Count; i++)
                { %>
             <th>Suma</th>
             <th>Vidurkis</th>
             <th>Vid.Skirt.</th>
             <th>Vid.Sant.%</th>
             <%} %>
       <% }
          else if (row.Period == "Viso ")
          {%>
         <td>
           <%= row.Period%>
         </td>
         <% for (int i = 0; i < row.Cells.Count; i++)
            { %>
            <td style="text-align:right;"><%= tempRow.Cells[i].Value.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].Value.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].Difference.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].Percent%></td>
         <% } %>
          
       <% }
          else
          { %>
         <td>
           <%= row.Period%>
         </td>
         <% for (int i = 0; i < row.Cells.Count; i++)
            { %>
            <td style="text-align:right;"><%= row.Cells[i].Value.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].Income.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].Difference.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].Percent%></td>
         <% }
          } %>
       </tr>
	   <% } %>
	</table>

    </div>

</asp:Content>
