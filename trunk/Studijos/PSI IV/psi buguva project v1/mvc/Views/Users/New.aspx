<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="mvc.Views.Users.New" %>
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
    <% Html.BeginForm("Insert", "Users", new {}, FormMethod.Post); %>
    <% { %>
         <fieldset>
        <legend>Vartotojas</legend>
         <p>
            <label for="name">Prisijungimo vardas:</label><%= Html.TextBox("login_name") %>
         </p>
         <p>
            <label for="surname">Slaptažodis:</label><%= Html.Password("password") %>
         </p>
         <p>
            <label for="surname">Pakartotas slaptažodis:</label><%= Html.Password("repeated_password") %>
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
         <input type="submit" value = "Sukurti" />                                                             
    <% } %>
   
</asp:Content>