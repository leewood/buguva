﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="mvc.Views.Workers.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">   
    
    <h1>Method1: </h1>
    <table class="grid">
      <tr>
        <th>id</th>      
        <th>Name</th>
        <th>Surname</th>      
        <th>Department</th>
      </tr>    
      <% foreach (mvc.Models.Worker worker in ViewData.Model) %>
      <% { %>
      <tr>
        <td>
          
          <%= Html.ActionLink(worker.id.ToString(), "View", new {id = worker.id } )%>         
        </td>
        <td>
          <%= worker.name  %>
        </td>
        <td>
          <%= worker.surname  %>
        </td>        
        <td>
          <% if (worker.department_id != null)
             { %>
          <%= Html.ActionLink(worker.department_id.ToString(), "View", "Departments", new { id = worker.department_id })%>
          <% } %>
        </td>
      </tr>
    <% } %>    
    </table>
    
    <h1>Method2:</h1>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetWorkers" TypeName="mvc.Models.POADataModelsDataContext">
    </asp:ObjectDataSource>
    
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" 
            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" >
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:BoundField DataField="surname" HeaderText="surname" 
                SortExpression="surname" />
        </Columns>
    </asp:GridView>
    
    <h1>Method3:</h1>
    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataSourceID="LinqDataSource1">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" 
                ReadOnly="True" />
            <asp:BoundField DataField="surname" HeaderText="surname" 
                SortExpression="surname" ReadOnly="True" />            
        </Columns>
    </asp:GridView>

    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="mvc.Models.POADataModelsDataContext" TableName="Workers" 
        Select="new (id, name, surname)">
    </asp:LinqDataSource>
    </form>
    <a href="/Workers/Form/">Prid&#279;ti</a>
</asp:Content>