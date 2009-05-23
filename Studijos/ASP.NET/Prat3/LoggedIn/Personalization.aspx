<%@ Page Language="C#" MasterPageFile="~/LoggedIn/PreferencesMaster.master" AutoEventWireup="true" CodeFile="Personalization.aspx.cs" Inherits="LoggedIn_Personalization" Title="Personalization" Theme="Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrefPlaceHolder" Runat="Server">
<h3><asp:Literal ID="headerLabel" runat="server" 
        Text="User preferences personalization" 
        meta:resourcekey="headerLabelResource1" /></h3>
<div class="toolbar">
  <div class="extButton">
     <asp:ImageButton ID="UpdateButtonImg" runat="server" SkinID="SaveImageButton" 
          CssClass="simpleImageClear" OnClick="Button1_Click" 
          meta:resourcekey="UpdateButtonImgResource1" />
     <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click"  
          CssClass="simpleClear" meta:resourcekey="Button1Resource1"/>
      <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
          ContextTypeName="ThemesModel" Select="new (Name)" TableName="Themes">
      </asp:LinqDataSource>
  </div>
</div>
    <asp:Label ID="Label2" runat="server" Text="Language" CssClass="title" 
        meta:resourcekey="Label2Resource1"></asp:Label>  
<asp:TextBox ID="TextBox1" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
<label class="separator"></label>
<asp:Label ID="Label3" runat="server" Text="Culture" CssClass="title" 
        meta:resourcekey="Label3Resource1"></asp:Label>
<asp:TextBox ID="TextBox2" runat="server" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
<label class="separator"></label>
<asp:Label ID="Label4" runat="server" Text="Theme" CssClass="title" 
        meta:resourcekey="Label4Resource1"></asp:Label>
<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="LinqDataSource1" 
        DataTextField="Name" DataValueField="Name" 
        meta:resourcekey="DropDownList1Resource1">
    <asp:ListItem meta:resourcekey="ListItemResource1">Default</asp:ListItem>
</asp:DropDownList>

</asp:Content>

