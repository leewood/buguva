<%@ Page Title="" Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="TestOrderingService.aspx.cs" Inherits="TestOrderingService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <div class="form">
        <asp:Label ID="Label4" runat="server" Text="Username:" CssClass="title"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <label class="separator" ></label>
        <asp:Label ID="Label3" runat="server" Text="Password:" CssClass="title"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
        <label class="separator" ></label>
        <asp:Label ID="Label2" runat="server" Text="Ordered products:" CssClass="title"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <label class="separator" ></label> 
    </div>        
    <br />
    <asp:Button ID="Button1" runat="server" Text="Order" onclick="Button1_Click" />
</asp:Content>

