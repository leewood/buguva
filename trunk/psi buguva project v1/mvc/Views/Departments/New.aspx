<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="mvc.Views.Departments.New" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><%=ViewData["Base"] %> <span class="title"><%= ViewData["TitleWindow"]%></span>
   	</div>  
    <div class = "errors">
        <%= Html.ErrorSummary("Klaidų sąrašas:", (string[])TempData["errors"]) %>
    </div>
    <% Page.Title = ViewData["TitleWindow"].ToString(); %>
    <% Html.BeginForm("Insert", "Departments", new {}, FormMethod.Post); %>
    <% { %>
        <fieldset>
        <legend>Informacija apie departamentą</legend>
    
        <% POADataModelsDataContext data = new POADataModelsDataContext(); %>
        <% SelectList list = new SelectList(data.Workers.ToList(), "id", "Fullname", ViewData.Model.headmaster_id); %>
        <p>
            <label for="title">Skyriaus pavadinimas:</label><%= Html.TextBox("title") %>
        </p>         
        <p> 
            <label for="headmaster_id">Skyriaus vadovas:</label><%= Html.DropDownList("headmaster_id", list) %>
        </p>
        </fieldset>
        <input type="submit" value = "Sukurti" />                                                             
    <% } %>
</asp:Content>
