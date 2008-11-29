<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="mvc.Views.Import.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
   	</div>       
    <form id="form1" runat="server" style="height: 265px; width: 657px">  
    <fieldset>
        <legend>Duomenų importavimas</legend>
        <asp:Label ID="LabelFileUploadImport" runat="server" Text="Failas"></asp:Label>
        <br />
        <asp:FileUpload ID="FileUploadImport" runat="server" />
        <br />
        <br />
        <asp:Label ID="LabelCheckBoxes" runat="server" Text="Importuoti"></asp:Label>
        <br /><br />
        <asp:CheckBox ID="CheckBoxTasks" runat="server" Checked="True" CssClass="check_box"
            Text="U&#382;duotys" />
        <br />
        <asp:CheckBox ID="CheckBoxEmployees" runat="server" Checked="True" CssClass="check_box"
            Text="Darbuotojai" />
        <br />
        <asp:CheckBox ID="CheckBoxProjects" runat="server" Checked="True" CssClass="check_box"
            Text="Projektai" />
        <br />
        
        </fieldset>
        
        <asp:Button ID="ButtonImport" runat="server" onclick="ButtonImport_Click" 
            Text="Importuoti" />
        <span id="Span1" runat="Server" />
        <%= Html.ErrorSummary("Klaidų sąrašas:", (string[])TempData["errors"]) %>
        <br />
        
        <br />
    
    </form>
    
</asp:Content>
