﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="mvc.Views.Projects.New" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["TitleWindow"]%></span>
   	</div> 
    <div class = "errors">
        <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>
    <% Page.Title = ViewData["TitleWindow"].ToString(); %>
    <% Html.BeginForm("Insert", "Projects", new {back = ViewData["back"]}, FormMethod.Post); %>
    <% { %>
          <fieldset>
        <legend>Projektas</legend>
        <% POADataModelsDataContext data = new POADataModelsDataContext(); %>
        <% 
           List<mvc.Models.Worker> workersList = data.Workers.Where(w => w.deleted.HasValue == false).ToList();
           workersList = workersList.Where(u => u.administationView()).ToList();                      
           SelectList list = new SelectList(workersList, "id", "Fullname", ViewData.Model.project_manager_id);
           UserSession userSession = new UserSession();
           User mySelf = data.Users.First(u => u.id == userSession.userId);
           %>
        <p>
            <label for="title">Projekto pavadinimas:</label><%= Html.TextBox("title") %>
        </p>         
        <p> 
            <label for="project_manager_id">Projekto vadovas:</label>
            <% if (userSession.isSimpleUser() || userSession.isAntanas())
               { %>
            <%= Html.Hidden("project_manager_id", mySelf.worker_id ?? 0) %>
            <%= (mySelf.Worker != null)?mySelf.Worker.Fullname:"" %>
            <% }
               else
               { %>
            <%= Html.DropDownList("project_manager_id", list)%>
            <% }%>
        </p>
        </fieldset>
        <input type="submit" value = "Sukurti" />                                                             
    <% } %>
</asp:Content>
