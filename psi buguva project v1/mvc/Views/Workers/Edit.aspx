<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="mvc.Views.Workers.Edit" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
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
         <p>
            <% POADataModelsDataContext dbConnection = new POADataModelsDataContext(); %>
            <% SelectList departments = new SelectList(dbConnection.Departments, "id", "title", ViewData.Model.department_id);  %>
            <label for="director">Skyrius:</label><%= Html.DropDownList("department_id", departments) %>
         </p>
         </fieldset>
         <input type="submit" value = "Keisti" />                         
    <% } %>
    <% Html.EndForm(); %>
</asp:Content>
