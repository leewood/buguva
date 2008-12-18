<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="OvertimeReport.aspx.cs" Inherits="mvc.Views.Projects.OvertimeReport" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class = "path">
   	  <img src="../../Content/Images/Icons/scheduledtasks_big.png" alt="logo" /><span class="title"><%= ViewData["Title"]%></span>
 <img src="../../Content/Images/Icons/Print30.png" onclick="printLandscapeTable()" class="print_image" alt="logo" style="float: right;" />  
   	 
   	</div>   
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>   	
   	<div class="pager">   	
	  <%= Html.Pager((int)ViewData["size"], (int)ViewData["page"], (int)ViewData["total"], new { type = (int)ViewData["type"]})%>
	</div>   
    <% Html.BeginForm("OvertimeReport", "Projects", FormMethod.Get); %>  
       <% List<KeyValuePair<string, int>> types = new List<KeyValuePair<string, int>>(); %>
       <% types.Add(new KeyValuePair<string,int>("Metai", 1));%>
       <% types.Add(new KeyValuePair<string,int>("Pusmečiai", 2));%>
       <% types.Add(new KeyValuePair<string,int>("Ketvirčiai", 3));%>
       <% types.Add(new KeyValuePair<string,int>("Mėnesiai", 4));%>
    <fieldset class="years">
     <legend>Ataskaitos laikotarpis</legend>   
   <label>Periodo tipas:</label><%= Html.DropDownList("type", new SelectList(types, "Value", "Key", ViewData["type"]), new { onChange = "javascript: form.submit();"})%>
 <% Html.EndForm(); %>
</fieldset>
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
 <label style="width: 100px;">Paslinkti sąrašą: </label>
 <a href="javascript:scrool(-10);">< į kairę</a>
 <a href="javascript:scrool(10);">į dešinę ></a>
 </div>
	<div style="display: run-in; overflow : auto;" id="scrool_container">
	
	<table class = "grid">
	   <tr>
	   <th rowspan="2"><%= ViewData.Model.Captions[0] %></th>
	   <% for (int i = 1; i < ViewData.Model.Captions.Count; i++)
       { %>
       
         <th colspan="5"><%= (ViewData.Model.Redirections[i] == null) ? ViewData.Model.Captions[i] : Html.ActionLink(ViewData.Model.Captions[i], ViewData.Model.Actions[i], ViewData.Model.Redirections[i], new RouteValueDictionary()) %></th>
	   <%} %>
	   </tr>    
	   <tr>
	   <% for (int i = 1; i < ViewData.Model.Captions.Count; i++)
       { %>
         <th>Viso dirbta</th><th>Galėjo dirbti</th><th>Viršvalandžiai</th><th>Nedirbta %</th><th>Viršvalandžiai %</th>
	   <%} %>
	   
	   </tr>
	   <% OvertimeReportRow tempRow = new OvertimeReportRow(); %>
	   <% foreach (OvertimeReportRow row in ViewData.Model.Rows)
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
            <td style="text-align:right;"><%= tempRow.Cells[i].TimeSum.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].TimeSum.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].TimeNormal.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].TimeOvertime.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].PercentUndertime%></td>
            <td style="text-align:right;"><%= row.Cells[i].PercentOvertime%></td>
         <% } %>
          
       <% }
          else
          { %>
         <td>
           <%= row.Period%>
         </td>
         <% for (int i = 0; i < row.Cells.Count; i++)
            { %>
            <td style="text-align:right;"><%= row.Cells[i].TimeSum.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].TimeNormal.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].TimeOvertime.ToString("0.00")%></td>
            <td style="text-align:right;"><%= row.Cells[i].PercentUndertime%></td>
            <td style="text-align:right;"><%= row.Cells[i].PercentOvertime%></td>
         <% }
          } %>
       </tr>
	   <% } %>
	</table>

    </div>

</asp:Content>
