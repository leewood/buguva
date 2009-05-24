<%@ Page Title="" Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="OrdersList.aspx.cs" Inherits="LoggedIn_OrdersList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" 
        Select="new (StatusString, id, OrderDate, ConfirmDate, Person)" TableName="Orders">
    </asp:LinqDataSource>
    <div class="grid">
    <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1" 
            onitemcommand="ListView1_ItemCommand">
        <ItemTemplate>
            <tr class="simpleRow" style="margin:0px;">
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                </td>            
                <td>
                   <asp:Label ID="Label2" runat="server" Text='<%# Eval("Person") %>' />
                </td>
                <td>
                  <asp:Label ID="StatusStringLabel" runat="server" Text='<%# Eval("StatusString") %>' />
                </td>
                <td>
                    <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Eval("OrderDate") %>' />
                </td>
                <td>
                    <asp:Label ID="ConfirmDateLabel" runat="server" Text='<%# Eval("ConfirmDate") %>' />
                </td>
                <td>
                    <asp:ImageButton ID="EditButton" runat="server" CommandName="Preview" CssClass="imageButton" SkinID="EditImageButton" CommandArgument='<%# Eval("id") %>' OnClick="PreviewButtonClick"/>                                        
                </td>                  
            </tr>
        </ItemTemplate>        
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td><asp:Label Text="No data was returned." runat="server" ID="NoDataLabel"></asp:Label></td>
                </tr>
            </table>
        </EmptyDataTemplate>        
        <LayoutTemplate>            
                        <table ID="itemPlaceholderContainer" runat="server" border="0" cellspacing="0" cellpadding="0" style="width:80%">
                            <tr runat="server" class="header">
                                <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel1" Text="id" /></th>
                                <th id="Th2" runat="server">
                                    <asp:Label Text="Person" runat="server" ID="Label3" /></th>                                    
                                <th id="Th1" runat="server">
                                    <asp:Label Text="Status" runat="server" ID="HeaderLabel2" /></th>                                    
                                <th runat="server">
                                    <asp:Label Text="Order Date" runat="server" ID="HeaderLabel3" /></th>
                                <th runat="server">
                                    <asp:Label Text="Confirm Date" runat="server" ID="HeaderLabel4" /></th>
                                <th runat="server">
                                    <asp:Label Text="Actions" runat="server" ID="Label1" /></th>                          
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
        </LayoutTemplate>                
    </asp:ListView>
    </div>
</asp:Content>

