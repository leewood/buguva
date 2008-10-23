<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ViewContentPage1.aspx.cs" Inherits="mvc.Views.Uzduotys.ViewContentPage1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ProjectDatabaseConnection %>" 
            SelectCommand="SELECT * FROM [Workers]"></asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" 
            AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id" />
            <asp:BoundField DataField="login_name" HeaderText="login_name" 
                SortExpression="login_name" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:BoundField DataField="surname" HeaderText="surname" 
                SortExpression="surname" />
            <asp:BoundField DataField="department_id" HeaderText="department_id" 
                SortExpression="department_id" />
            <asp:BoundField DataField="is_leader" HeaderText="is_leader" 
                SortExpression="is_leader" />
            <asp:BoundField DataField="is_department_headmaster" 
                HeaderText="is_department_headmaster" 
                SortExpression="is_department_headmaster" />
        </Columns>
    </asp:GridView>
    </form>
    <a href="/Workers/Form/">Pridëti</a>
</asp:Content>
