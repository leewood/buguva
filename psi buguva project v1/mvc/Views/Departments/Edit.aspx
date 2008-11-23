<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="mvc.Views.Departments.Edit" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "errors">
      <%= Html.ErrorSummary("Klaidų sąrašas:", (string[])TempData["errors"]) %>
    </div>
    
    <% Html.BeginForm("Update", "Departments", new {id = ViewData.Model.id}, FormMethod.Post); %>
    <% { %>
        <% POADataModelsDataContext data = new POADataModelsDataContext(); %>
        <% SelectList list = new SelectList(data.Workers.ToList(), "Fullname", "id", ViewData.Model.headmaster_id); %>
         <p>
           <label for="id">ID:</label><span><%= ViewData.Model.id %></span>
         </p>
         <p>
            <label for="title">Skyriaus pavadinimas:</label><%= Html.TextBox("title") %>
         </p>
         <p> 
            <label for="project_manager_id">Skyriaus vadovas:</label><%= Html.DropDownList("project_manager_id", list) %>
        </p>
         <input type="submit" value = "Keisti" />                         
    <% } %>
    <% Html.EndForm(); %>
</asp:Content>
