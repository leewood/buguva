<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout"  Title="Logout" Theme="Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div class="popupbox">    
        <asp:Label ID="Label2" runat="server" Text="Info" CssClass="label" 
            meta:resourcekey="Label2Resource1"></asp:Label>
        <asp:Label ID="Label1" runat="server" Text="Successfully logged out." 
            meta:resourcekey="Label1Resource1"></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" PostBackUrl="~/Login.aspx"  CssClass="simpleButton"
            Text="Login" meta:resourcekey="Button1Resource1" />    
        <asp:Button ID="Button2" runat="server" PostBackUrl="~/ProductsList.aspx" 
            CssClass="simpleButton" Text="To main page" Width="100px" 
            meta:resourcekey="Button2Resource1" />
    </div>
    </form>
</body>
</html>
