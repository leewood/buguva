<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="mvc.Views.Users.Edit" %>
<%@ Import Namespace="mvc.Common"%>
<%@ Import Namespace="mvc.Models"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class = "errors">
      <%= Html.ErrorSummary("Klaidų sąrašas:", (string[])TempData["errors"]) %>
    </div>
    <% Html.BeginForm("Update", "Users", new { id = ViewData.Model.id }, FormMethod.Post); %>
    <% { %>
         <p>
           <label for="id">ID:</label><span><%= ViewData.Model.id %></span>
         </p>    
         <p>
            <label for="name">Prisijungimo vardas:</label><span><%=ViewData.Model.login_name %></span><%= Html.Hidden("login_name") %><%= Html.Hidden("password") %>
         </p>        
         <p>
            <label for="director">Lygis:</label><%= Html.DropDownList("level", ViewData.Model.LevelsList) %>
         </p>
        <% POADataModelsDataContext data = new POADataModelsDataContext(); %>
        <% List<Worker> dataList = data.Workers.Where(w => (w.deleted.HasValue == false)).ToList(); %>        
        <% SelectList list = new SelectList(dataList, "id", "Fullname", ViewData.Model.worker_id); %>         
        
         <p>
            <label for="surname">Susietas su darbuotoju:</label><%= Html.DropDownList("worker_id", list) %>
         </p>         
         <input type="submit" value = "Keisti" />                                                             
    <% } %>
   
</asp:Content>