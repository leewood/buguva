<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="mvc.Views.Users.ChangePassword" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title">Profilis</span>
   	</div> 
    <div class = "errors">
      <%= Html.ErrorSummary("Įvyko klaida:", TempData) %>
    </div>
    <div class = "messages">
      <%= Html.ErrorSummary("", (string[])TempData.getAndRemove("message")) %>
    </div>
    
    <% Html.BeginForm("UpdatePassword", "Users", new { id = ViewData.Model.id, toEditProfile = true }, FormMethod.Post); %>
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