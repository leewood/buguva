<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="NoPermissions.aspx.cs" Inherits="mvc.Views.Home.NoPermissions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h2><%= Html.Encode(ViewData["Message"]) %></h2>
</asp:Content>
