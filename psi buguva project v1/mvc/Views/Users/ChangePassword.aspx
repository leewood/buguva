<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="mvc.Views.Users.ChangePassword" %>
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
    <% Html.BeginForm("UpdatePassword", "Users", new { id = ViewData.Model.id }, FormMethod.Post); %>
    <% { %>
        <fieldset>
        <legend>Slaptažodžio keitimas</legend>
         <p>
           <label for="repeated_password">Senasis slaptažodis:</label><%= Html.Password("repeated_password") %>
         </p>    
         <p>
           <label for="new_password">Naujasis slaptažodis:</label><%= Html.Password("new_password") %>
         </p>    
         <p>
           <label for="new_repeated_password">Pakartotas naujasis slaptažodis:</label><%= Html.Password("new_repeated_password") %>
         </p>    
         <br />
         </fieldset>
         <input type="submit" value = "Keisti Slaptažodį" />                                                             
    <% } %>
   
</asp:Content>