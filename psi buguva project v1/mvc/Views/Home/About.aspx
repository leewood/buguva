<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="mvc.Views.Home.About" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h2>About Us</h2>
    <p>
        TODO: Put <em>about</em> content here.
        
    </p>
<p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
        </asp:ListView>
        
    </p>
<p>
        &nbsp;</p>
</form>
</asp:Content>
