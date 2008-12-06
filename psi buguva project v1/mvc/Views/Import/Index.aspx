<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="mvc.Views.Import.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class = "path">
   	  <%= ViewData["Image"] %><span class="title"><%= ViewData["Title"]%></span>
   	</div>       
    <form id="form1" runat="server" style="height: 347px; width: 657px" >  
    <fieldset>
        <legend>Duomen&#371; importavimas</legend>
        <asp:Label ID="LabelFileUploadImport" runat="server" Text="Failas"></asp:Label>
        <br />
        <asp:FileUpload ID="FileUploadImport" runat="server"/> 
        <span id="Span1" runat="Server" />
        <%= Html.ErrorSummary("Klaid&#371; s&#261;ra&#353;as:", (string[])TempData["errors"]) %>
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
        <asp:CheckBox ID="CheckBoxDepartments" runat="server" Checked="True" CssClass="check_box"
            Text="Skyriai" />
        <br />
                            <br />
                        <span class="login_name">
        
        <asp:Button ID="ButtonImport" runat="server" onclick="ButtonImport_Click" 
            Text="Importuoti" />
        
        
	    </span>
        <br />
        
                        <span class="login_name">
        
        
	<div class="description">
	    Pasirinkite Excel fail&#261; savo lokaliame diske paspaud&#281; 'Browse'. J&#363;s galite 
        importuori *.xls ir *.xlsx failus. 
    </div> 
        </span>
        
    </fieldset>
    </form>
</asp:Content>
