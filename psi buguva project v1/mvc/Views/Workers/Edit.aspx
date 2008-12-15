<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="mvc.Views.Workers.Edit" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["Title"]%></span>
   	</div>  
    <div class = "errors">
      <%= Html.ErrorSummary("Klaidų sąrašas:", (string[])TempData["errors"]) %>
    </div>
    
    <% Html.BeginForm("Update", "Workers", new {id = ViewData.Model.id}, FormMethod.Post); %>
    <% { %>
        <fieldset>
        <legend>Darbuotojas ID <%= ViewData.Model.id %></legend>
         <p>
            <label for="name">Vardas:</label><%= Html.TextBox("name") %>
         </p>
         <p>
            <label for="surname">Pavardė:</label><%= Html.TextBox("surname") %>
         </p>
         <% mvc.Common.UserSession userSession = new UserSession(); %>
         <p>
           <label>Skyrius:</label>
           <% POADataModelsDataContext dbConnection = new POADataModelsDataContext(); %>
         <% if (!userSession.isAdministrator())
            { %>
            <%= Html.Hidden("department_id", userSession.workerDepartment) %>
            <%= dbConnection.Departments.First(d => d.id == userSession.workerDepartment).title %>
         <% }
            else
            { %>
            
            <% SelectList departments = new SelectList(dbConnection.Departments.Where(d => d.deleted.HasValue == false), "id", "title", ViewData.Model.department_id);  %>
            <%= Html.DropDownList("department_id", departments)%>
         <% } %>
        </p>
         </fieldset>
         <input type="submit" value = "Keisti" />                         
    <% } %>
    <% Html.EndForm(); %>
</asp:Content>
