<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="mvc.Views.Workers.Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h1>Method1:</h1>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" 
        DataSourceID="LinqDataSource1" DefaultMode="Insert" 
        oniteminserted="FormView1_ItemInserted" >
        <InsertItemTemplate>
            login_name:
            <asp:TextBox ID="login_nameTextBox" runat="server" 
                Text='<%# Bind("login_name") %>' />
            <br />
            name:
            <asp:TextBox ID="nameTextBox" runat="server" 
                Text='<%# Bind("name") %>' />
            <br />
            surname:
            <asp:TextBox ID="surnameTextBox" runat="server" Text='<%# Bind("surname") %>' />
            <br />
            department_id:
            <asp:TextBox ID="department_idTextBox" runat="server" 
                Text='<%# Bind("department_id") %>' />
            <br />
            is_leader:
            <asp:TextBox ID="is_leaderTextBox" runat="server" 
                Text='<%# Bind("is_leader") %>' />
            <br />
            is_department_headmaster:
            <asp:TextBox ID="is_department_headmasterTextBox" runat="server" 
                Text='<%# Bind("is_department_headmaster") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
    </asp:FormView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="mvc.Models.POADataModelsDataContext" EnableInsert="True" 
        TableName="Workers">
    </asp:LinqDataSource>
    
    <h1>Method2:</h1>
    <asp:FormView ID="FormView2" runat="server" DataSourceID="ObjectDataSource1" 
        DefaultMode="Insert">
        <InsertItemTemplate>
            login_name:
            <asp:TextBox ID="login_nameTextBox" runat="server" 
                Text='<%# Bind("login_name") %>' />
            <br />
            name:
            <asp:TextBox ID="nameTextBox" runat="server" Text='<%# Bind("name") %>' />
            <br />
            surname:
            <asp:TextBox ID="surnameTextBox" runat="server" Text='<%# Bind("surname") %>' />
            <br />
            department_id:
            <asp:TextBox ID="department_idTextBox" runat="server" 
                Text='<%# Bind("department_id") %>' />
            <br />
            is_leader:
            <asp:TextBox ID="is_leaderTextBox" runat="server" 
                Text='<%# Bind("is_leader") %>' />
            <br />
            is_department_headmaster:
            <asp:TextBox ID="is_department_headmasterTextBox" runat="server" 
                Text='<%# Bind("is_department_headmaster") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="mvc.Models.Worker" InsertMethod="AddWorker" 
        TypeName="mvc.Models.POADataModelsDataContext">
    </asp:ObjectDataSource>
    <br />
    
       
   </form>
   
    <form method="post" action="/Workers/Insert">

    <label for="title">Name:</label>
    <br />
      <%= Html.TextBox("name") %>
    
    <br /><br />
    
    <label for="director">Surname:</label>
    <br />
    <%= Html.TextBox("surname") %>
    
    <br /><br />
    
    <label for="dateReleased">Login:</label>
    <br />
    <%= Html.TextBox("login_name") %>
    
    <br /><br />
    
    <input type="submit" value="Insert" />
    
    </form>
   
</asp:Content>
