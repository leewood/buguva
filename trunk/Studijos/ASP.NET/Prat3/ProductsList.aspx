<%@ Page Title="" Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="ProductsList.aspx.cs" Inherits="ProductsList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="ProductsList" 
        Select="new (id, Name, Price, Currency, Category, ActivePrice)" 
        TableName="Products">
    </asp:LinqDataSource>
    <asp:Panel ID="Panel1" runat="server" CssClass="toolbar" 
        meta:resourcekey="Panel1Resource1" >
        <asp:Label ID="Label2" runat="server" Text="Name:" 
            meta:resourcekey="Label2Resource1"></asp:Label>
        <asp:TextBox ID="NameTextBox" runat="server" 
            meta:resourcekey="NameTextBoxResource1"></asp:TextBox>        
        <label class="separator">|</label>
        &nbsp;<asp:Label ID="Label3" runat="server" Text="Price between:" 
            meta:resourcekey="Label3Resource1"></asp:Label>
        <asp:TextBox ID="LowPrice" runat="server" meta:resourcekey="LowPriceResource1"></asp:TextBox>        
        <asp:Label ID="Label4" runat="server" Text="and" 
            meta:resourcekey="Label4Resource1"></asp:Label>
        <asp:TextBox ID="HighPrice" runat="server" 
            meta:resourcekey="HighPriceResource1"></asp:TextBox>     
        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label5Resource1"></asp:Label>
        <label class="separator">|</label>
        &nbsp;<asp:Button ID="DateFilterButton" runat="server" 
            Text="Update list" meta:resourcekey="DateFilterButtonResource1" />
    </asp:Panel>
    
    <div class="grid">
    <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1">
        <ItemTemplate>
            <tr class="simpleRow" style="margin:0px;">
                <td>
                   <asp:Image runat="server" ID="ProductImage" 
                        ImageUrl='<%# Eval("id", "GetProductImage.ashx?id={0}") %>' 
                        CssClass="tablePicture" meta:resourcekey="ProductImageResource1" />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' 
                        meta:resourcekey="NameLabelResource1" />
                </td>
                <td style="text-align:right">
                    <asp:Label ID="PriceLabel" runat="server" 
                        Text='<%# Eval("ActivePrice", "{0:0.00}") %>' 
                        meta:resourcekey="PriceLabelResource1" />
                    <asp:Label ID="CurrencyLabel" runat="server" 
                        Text='<%# Backet.MyActiveCurrency %>' 
                        meta:resourcekey="CurrencyLabelResource1" />
                </td>
                <td>
                    <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' 
                        meta:resourcekey="CategoryLabelResource1" />
                </td>
                <td>
                    <asp:ImageButton ID="EditButton" runat="server" CommandName="Preview" 
                        CssClass="imageButton" SkinID="InfoImageButton" 
                        CommandArgument='<%# Eval("id") %>' OnClick="PreviewButtonClick" 
                        meta:resourcekey="EditButtonResource1"/>   
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Order" 
                        CssClass="imageButton" SkinID="CartAddImageButton" 
                        CommandArgument='<%# Eval("id") %>' OnClick="OrderButtonClick" 
                        meta:resourcekey="ImageButton1Resource1"/>                                                            
          <asp:LoginView ID="LoginView1" runat="server">
              <RoleGroups>
                <asp:RoleGroup Roles="Admin">
                  <ContentTemplate>
                     <asp:ImageButton ID="DeleteButton" runat="server" CommandName="DeleteItem" 
                          CssClass="imageButton" SkinID="DeleteImageButton" 
                          CommandArgument='<%# Eval("id") %>' OnClick="DeleteButtonClick" 
                          meta:resourcekey="DeleteButtonResource1"/>   
                     <asp:ImageButton ID="EditButton" runat="server" CommandName="EditItem" 
                          CssClass="imageButton" SkinID="EditImageButton" 
                          CommandArgument='<%# Eval("id") %>' OnClick="EditButtonClick" 
                          meta:resourcekey="EditButtonResource2"/>   
                  </ContentTemplate>
                </asp:RoleGroup>
              </RoleGroups>
          </asp:LoginView>
                </td>
            </tr>
        </ItemTemplate>        
        <EmptyDataTemplate>          
             <table id="Table1" runat="server" style="">
                <tr runat="server">
                    <td runat="server"><asp:Label Text="No data was returned." runat="server" ID="NoDataLabel" meta:resourcekey="NoData"></asp:Label></td>
                </tr>
            </table>
        </EmptyDataTemplate>        
        <LayoutTemplate>
                        <table ID="itemPlaceholderContainer" runat="server" border="0" cellspacing="0" cellpadding="0" style="width:80%">
                            <tr runat="server" class="header">
                                <th id="Th1" runat="server">
                                    <asp:Label  runat="server" ID="Label1" Text="Picture" meta:resourcekey="Header1"/></th>                            
                                <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel1" Text="Name" meta:resourcekey="Header2"/></th>
                                <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel2" Text="Price" meta:resourcekey="Header3"/></th>
                                <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel3" Text="Category" meta:resourcekey="Header4"/></th>
                                    <th runat="server">
                                    <asp:Label  runat="server" ID="HeaderLabel4" Text="Actions" meta:resourcekey="Header5"/>
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
             <asp:LoginView ID="LoginView1" runat="server">
              <RoleGroups>
                <asp:RoleGroup Roles="Admin">
                  <ContentTemplate>
                       <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" 
                        CssClass="insertButton" OnClick="NewButtonClick" 
                           meta:resourcekey="InsertButtonResource1" />
                  </ContentTemplate>
                </asp:RoleGroup>
              </RoleGroups>
            </asp:LoginView>

    </div>
</asp:Content>

