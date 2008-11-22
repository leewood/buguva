<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="mvc.Views.Users.New" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class = "errors">
      <%= Html.ErrorSummary("Klaidų sąrašas:", (string[])TempData["errors"]) %>
    </div>
    <% Html.BeginForm("Insert", "Users", new {}, FormMethod.Post); %>
    <% { %>
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
         <p>
            <label for="surname">Susietas su darbuotoju:</label><%= Html.TextBox("worker_id") %>
         </p>         
         <input type="submit" value = "Sukurti" />                                                             
    <% } %>
   
</asp:Content>