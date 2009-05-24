<%@ Page Title="" Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="ProductsList.aspx.cs" Inherits="ProductsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="ProductsList" 
        Select="new (id, Name, Price, Currency, Category, ActivePrice)" 
        TableName="Products">
    </asp:LinqDataSource>
    <asp:Panel ID="Panel1" runat="server" CssClass="toolbar" >
        <asp:Label ID="Label2" runat="server" Text="Name:"></asp:Label>
        <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>        
        <label class="separator">|</label>
        &nbsp;<asp:Label ID="Label3" runat="server" Text="Price between:"></asp:Label>
        <asp:TextBox ID="LowPrice" runat="server"></asp:TextBox>        
        <asp:Label ID="Label4" runat="server" Text="and"></asp:Label>
        <asp:TextBox ID="HighPrice" runat="server"></asp:TextBox>     
        <asp:Label ID="Label5" runat="server" Text='<%# Backet.MyActiveCurrency %>'></asp:Label>
        <label class="separator">|</label>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="DateFilterButton" runat="server" Text="Update list" />
    </asp:Panel>
    
    <div class="grid">
    <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1">
        <ItemTemplate>
            <tr class="simpleRow" style="margin:0px;">
                <td>
                   <asp:Image runat="server" ID="ProductImage" ImageUrl='<%# Eval("id", "GetProductImage.ashx?id={0}") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' />
                    <asp:Label ID="CurrencyLabel" runat="server" Text='<%# Eval("Currency") %>' />
                </td>
                <td>
                    <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
                </td>
                <td>
                    <asp:ImageButton ID="EditButton" runat="server" CommandName="Preview" CssClass="imageButton" SkinID="EditImageButton" CommandArgument='<%# Eval("id") %>' OnClick="PreviewButtonClick"/>                                        
                </td>
            </tr>
        </ItemTemplate>        
        <EmptyDataTemplate>          
             <table id="Table1" runat="server" style="">
                <tr>
                    <td><asp:Label Text="No data was returned." runat="server" ID="NoDataLabel"></asp:Label></td>
                </tr>
            </table>
        </EmptyDataTemplate>        
        <LayoutTemplate>
                        <table ID="itemPlaceholderContainer" runat="server" border="0" cellspacing="0" cellpadding="0" style="width:80%">
                            <tr runat="server" class="header">
                                <th id="Th1" runat="server">
                                    <asp:Label  runat="server" ID="Label1" Text="Picture" /></th>                            
                                <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel1" Text="Name" /></th>
                                <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel2" Text="Price" /></th>
                                <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel3" Text="Category" /></th>
                                    <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel4" Text="Actions" />
                                    </th>
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                <tr id="Tr1" runat="server" class="simpleRow">
                    <td id="Td1" runat="server" style="" colspan="5">
                        <asp:DataPager ID="DataPager1" runat="server" >
                            <Fields>
                                <asp:NumericPagerField CurrentPageLabelCssClass="simpleSmallLabel" NumericButtonCssClass ="simpleSmall"  />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>                             
                        </table>
        </LayoutTemplate>                
    </asp:ListView>
    </div>
</asp:Content>

