<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="mvc.Views.Departments.New" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["TitleWindow"]%></span>
   	</div>  
    <div class = "errors">
        <%= Html.ErrorSummary("Įvyko klaida:", TempData)%>
    </div>
    <% Page.Title = ViewData["TitleWindow"].ToString(); %>
    <% Html.BeginForm("Insert", "Departments", new {}, FormMethod.Post); %>
    <% { %>
        <fieldset>
        <legend>Informacija apie departamentą</legend>
    
        <% POADataModelsDataContext data = new POADataModelsDataContext(); %>
        <% 
           List<mvc.Models.Worker> workersList = data.Workers.Where(w => w.deleted.HasValue == false).ToList();
           workersList = workersList.Where(u => u.administationView()).ToList();           
           SelectList list = new SelectList(workersList, "id", "Fullname", ViewData.Model.headmaster_id); 
           UserSession userSession = new UserSession();
           User mySelf = data.Users.First(u => u.id == userSession.userId);
           %>           
        <p>
            <label for="title">Skyriaus pavadinimas:</label><%= Html.TextBox("title") %>
        </p>         
        <p> 
            <label for="headmaster_id">Skyriaus vadovas:</label>
            <% if (userSession.isSimpleUser() || userSession.isAntanas())
               { %>
            <%= Html.Hidden("headmaster_id", mySelf.worker_id ?? 0) %>
            <%= (mySelf.Worker != null)?mySelf.Worker.Fullname %>
            <% }
               else
               { %>
            <%= Html.DropDownList("headmaster_id", list) %>
            <% }%>
            

        </p>
        </fieldset>
        <input type="submit" value = "Sukurti" />                                                             
    <% } %>
</asp:Content>
