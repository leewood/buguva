<%@ Page Language="C#" MasterPageFile="~/LoggedIn/AdminZone/AdminZone.master" AutoEventWireup="true" CodeFile="ManageCategories.aspx.cs" Inherits="LoggedIn_AdminZone_ManageCategories" Title="Manage Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentPlaceHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" TableName="Categories">
    </asp:LinqDataSource>
    <div class="grid">
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Name" SkinID="grid"
        DataSourceID="LinqDataSource1" InsertItemPosition="LastItem">
        <ItemTemplate>
            <tr class="simpleRow" style="margin:0px;">
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" 
                        Text='<%# Eval("Description") %>' />
                </td>
                <td>
                    <asp:Label ID="NewsCountLabel" runat="server" Text='<%# Eval("NewsCount") %>' />
                </td>                
                <td>
                    <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" CssClass="imageButton" SkinID="EditImageButton"/>
                    <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="imageButton" SkinID="DeleteImageButton" />
                    
                </td>                
            </tr>
        </ItemTemplate>       
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>
                        No categories created.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr class="simpleRow">
                <td >
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" 
                        Text='<%# Bind("Description") %>' />
                </td>
                <td>
                </td>
                <td>
                    <asp:ImageButton ID="InsertButton" runat="server" CommandName="Insert" CssClass="imageButton" 
                         SkinID = "SaveImageButton"/>
                    <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" CssClass="imageButton" 
                        ToolTip="Clear" SkinID="CancelImageButton" />
                </td>                
            </tr>
        </InsertItemTemplate>
        <LayoutTemplate>
            <table ID="itemPlaceholderContainer" runat="server" cellspacing="0" cellpadding="0" style="width:80%">
                <tr runat="server" class="header">
                    <th runat="server" style="margin:0;">
                        <asp:LinkButton ID="NameFilterInit" runat="server" Text="Name" OnClick="FilterClick" CommandArgument="Name"></asp:LinkButton>
                        <asp:Panel runat="server" ID="NameFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="NameFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="NameFilterOK" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="Name" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "Name descending")
                           { %>
                           <asp:ImageButton SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="Name ascending"  />                                       
                        <% }
                           else if (SortMode == "Name ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton1"  SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton2"  SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="Name descending" />
                        <% } %>   
                    </th>
                    <th runat="server">
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Description" OnClick="FilterClick" CommandArgument="Description"></asp:LinkButton>
                        <asp:Panel runat="server" ID="DescriptionFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="DescriptionFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="LinkButton2" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="Description" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "Description descending")
                           { %>
                           <asp:ImageButton ID="ImageButton3"  SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="Description ascending" />                                       
                        <% }
                           else if (SortMode == "Description ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton4"  SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton5"  SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="Description descending" />
                        <% } %>   
                    </th>
                    <th id="Th2" runat="server">
                        <asp:LinkButton ID="LinkButton3" runat="server" Text="News count" OnClick="FilterClick" CommandArgument="NewsCount"></asp:LinkButton>
                        <asp:Panel runat="server" ID="NewsCountFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="NewsCountFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="LinkButton4" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="NewsCount" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "NewsCount descending")
                           { %>
                           <asp:ImageButton ID="ImageButton6"  SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="NewsCount ascending" />                                       
                        <% }
                           else if (SortMode == "NewsCount ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton7"  SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton8"  SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="NewsCount descending" />
                        <% } %>   
                                                        
                    </th>                                    
                    <th id="Th1" runat="server">
                    </th>                                    
                </tr>
                <tr ID="itemPlaceholder" runat="server" class="simpleRow">
                </tr>
                <tr id="Tr1" runat="server" class="simpleRow">
                    <td id="Td1" runat="server" style="" colspan="4">
                        <asp:DataPager ID="DataPager1" runat="server" >
                            <Fields>
                                <asp:NumericPagerField CurrentPageLabelCssClass="simpleSmallLabel" NumericButtonCssClass ="simpleSmall"  />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>                
            </table>
        </LayoutTemplate>
        <EditItemTemplate>
            <tr class="simpleRow">
                <td>
                    <asp:Label ID="NameLabel1" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" 
                        Text='<%# Bind("Description") %>' />
                </td>
                <td>
                    <asp:Label ID="NewsTextBox" runat="server" Text='<%# Eval("NewsCount") %>' />
                </td>
                <td>
                    <asp:ImageButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="imageButton" 
                        ToolTip="Update" SkinID="AcceptImageButton"/>
                    <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" CssClass="imageButton" 
                        ToolTip="Cancel" SkinID="CancelImageButton" />
                </td>                
            </tr>
        </EditItemTemplate>        
    </asp:ListView>
    </div>
</asp:Content>

