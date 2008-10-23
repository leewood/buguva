<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="mvc.Views.Workers.Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" 
        DataSourceID="SqlDataSource1" >
        <EditItemTemplate>
            id:
            <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
            <br />
            Departamentas
            <asp:TextBox ID="department_idTextBox" runat="server" 
                Text='<%# Bind("department_id") %>' />
            <br />
            caption:
            <asp:TextBox ID="captionTextBox" runat="server" Text='<%# Bind("caption") %>' />
            <br />
            description:
            <asp:TextBox ID="descriptionTextBox" runat="server" 
                Text='<%# Bind("description") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            id:
            <asp:TextBox ID="idTextBox" runat="server" Text='<%# Bind("id") %>' />
            <br />
            Departamentas:
            <asp:TextBox ID="department_idTextBox" runat="server" 
                Text='<%# Bind("department_id") %>' />
            <br />
            Pavadinimas
            <asp:TextBox ID="captionTextBox" runat="server" Text='<%# Bind("caption") %>' />
            <br />
            description:
            <asp:TextBox ID="descriptionTextBox" runat="server" 
                Text='<%# Bind("description") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            id:
            <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
            <br />
            department_id:
            <asp:Label ID="department_idLabel" runat="server" 
                Text='<%# Bind("department_id") %>' />
            <br />
            caption:
            <asp:Label ID="captionLabel" runat="server" Text='<%# Bind("caption") %>' />
            <br />
            description:
            <asp:Label ID="descriptionLabel" runat="server" 
                Text='<%# Bind("description") %>' />
            <br />
        </ItemTemplate>
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ProjectDatabaseConnection %>" 
        SelectCommand="SELECT * FROM [Projects]"></asp:SqlDataSource>
   </form>
</asp:Content>
