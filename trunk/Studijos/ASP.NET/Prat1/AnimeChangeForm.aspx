<%@ Page Language="C#" Title="Anime Change Form" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AnimeChangeForm.aspx.cs" Inherits="AnimeChangeForm" %>

<%@ Register src="AnimeForm.ascx" tagname="AnimeForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    
    <uc1:AnimeForm ID="AnimeForm2" runat="server" RedirectURL="AnimeList.aspx"/>
    
</asp:Content>

