<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="mvc.Views.Import.Index" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
      <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
    </div>       
    <form id="form2" runat="server" style="height: 265px; width: 657px">  
    <fieldset>
        <legend>Duomenų importavimas</legend>
        <asp:Label ID="Label1" runat="server" Text="Failas"></asp:Label>
        <br />
        <asp:FileUpload ID="FileUploadImport" runat="server" />
                <span id="Span1" runat="Server" />
                <%= Html.ErrorSummary("Klaidų sąrašas:", (string[])TempData["errors"]) %>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Importuoti"></asp:Label>
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
        <asp:CheckBox ID="CheckBoxDepartments" runat="server" Checked="True" CssClass="check_box"
            Text="Skyriai" />
        <br />
        
        </fieldset>
        
        <asp:Button ID="Button1" runat="server" onclick="ButtonImport_Click" 
            Text="Importuoti" />
        
 <div class="description">
     Pasirinkite Excel failą savo lokaliame diske paspaudę 'Browse'.
     Jūs galite importuori *.xls ir *.xlsx failus. 
 </div>
 
    </form>
    

    
</asp:Content>