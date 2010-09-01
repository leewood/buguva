<%@ Page Title="Genre Form" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="GenresChangeForm.aspx.cs" Inherits="GenresChangeForm" %>

<%@ Register src="GenreForm.ascx" tagname="GenreForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

    <uc1:GenreForm ID="GenreForm1" runat="server" RedirectURL="GenresList.aspx" />

</asp:Content>

