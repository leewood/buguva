<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="mvc.Views.Import.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label" 
        ondatabinding="Label1_DataBinding"></asp:Label>
    <asp:FileUpload ID="FileUpload1" runat="server" 
        ondatabinding="FileUpload1_DataBinding1"/>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    </form>
</asp:Content>
