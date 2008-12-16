<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="mvc.Views.Users.Edit" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["Title"]%></span>
   	</div> 
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData)%>
    </div>
    <% Html.BeginForm("Update", "Users", new { id = ViewData.Model.id }, FormMethod.Post); %>
    <% { %>
             <fieldset>
        <legend>Vartotojas ID <%= ViewData.Model.id %></legend>
         <p>
            <label for="name">Prisijungimo vardas:</label><span><%=ViewData.Model.login_name %></span><%= Html.Hidden("login_name") %><%= Html.Hidden("password") %>
         </p>        
         <p>
            <label for="director">Lygis:</label><%= Html.DropDownList("level", ViewData.Model.LevelsList) %>
         </p>
        <% POADataModelsDataContext data = new POADataModelsDataContext(); %>
        <% List<Worker> dataList = data.Workers.Where(w => (w.deleted.HasValue == false)).ToList(); %>                
        <% List<KeyValuePair<string, object>> additionalValues = new List<KeyValuePair<string, object>>();
           additionalValues.Add(new KeyValuePair<string,object>("Nesusietas", null));
           
           foreach (Worker worker in dataList)
           {
               additionalValues.Add(new KeyValuePair<string, object>(worker.Fullname, worker.id));
           }
            %>
        <% SelectList list = new SelectList(additionalValues, "Value", "Key", ViewData.Model.worker_id); %>                     
         <p>
            <label for="surname">Susietas su darbuotoju:</label><%= Html.DropDownList("worker_id", list)%>
         </p>  
         </fieldset>      
         <input type="submit" value = "Keisti" />                                                             
    <% } %>
   
</asp:Content>