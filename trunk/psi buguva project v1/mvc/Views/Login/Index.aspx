<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="mvc.Views.Login.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div id="login">
    <div class="header">
        Prisijungimas
    </div>
    
    <form id="loginform" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Prisijungimo vardas"></asp:Label>
        <asp:TextBox ID="loginname" runat="server"></asp:TextBox>
        
        <br />
        
        <asp:Label ID="Label2" runat="server" Text="Slapta&#382;odis"></asp:Label>
        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        
        <br />
        
        <asp:Label ID="Label3" runat="server" Text="Prisiminti mane"></asp:Label>
        <asp:CheckBox ID="rememberme" runat="server" />        
        
        <br /><br />
        
        <center>
            <asp:Button ID="loginbutton" runat="server" Text="Prisijungti" 
                onclick="loginbutton_Click" />
        </center>
        
    </form>
    
</div>





</asp:Content>
