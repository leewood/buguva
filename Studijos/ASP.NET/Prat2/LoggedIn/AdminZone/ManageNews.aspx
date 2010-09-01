<%@ Page Language="C#" MasterPageFile="~/LoggedIn/AdminZone/AdminZone.master" AutoEventWireup="true" CodeFile="ManageNews.aspx.cs" Inherits="LoggedIn_AdminZone_ManageNews" Title="Manage News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentPlaceHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" TableName="News">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" TableName="Categories">
    </asp:LinqDataSource>
    <div class="grid">
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="id" 
        DataSourceID="LinqDataSource1" InsertItemPosition="LastItem">
        <ItemTemplate>
            <tr class="simpleRow">
                <td>
                    <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                </td>
                <td>
                    <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' />
                </td>
                <td>
                    <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
                </td>
                <td>
                    <asp:Label ID="CommentsCountLabel" runat="server" 
                        Text='<%# Eval("CommentsCount") %>' />
                </td>                
                <td>
                    <asp:Label ID="CreatorLabel" runat="server" Text='<%# Eval("Creator") %>' />
                </td>                
                <td>
                    <asp:Label ID="CreateDateLabel" runat="server" 
                        Text='<%# Eval("CreateDate") %>' />
                </td>
                <td>
                    <asp:Label ID="LastModifiedDateLabel" runat="server" 
                        Text='<%# Eval("LastModifiedDate") %>' />
                </td>
                
                <td>
                    <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="imageButton" 
                        SkinID="DeleteImageButton" />
                    <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit" SkinID="EditImageButton" CssClass="imageButton" />
                </td>                
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>
                        No news.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr id = "InsertPlace" class="simpleRow">
                <td>
                    <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                </td>
                <td>
                    <asp:TextBox ID="BodyTextBox" runat="server" Text='<%# Bind("Body") %>' />
                </td>
                <td>
                    <asp:TextBox ID="CategoryTextBox" runat="server" Text='<%# Bind("Category") %>'  Visible='false'/>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" DataSourceID="LinqDataSource2" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged = "CategoryChanged" OnInit = "CategoryChanged" />                                          
                </td>
                <td>
                </td>   
                <td>
                    <asp:Label ID="InsertCreator" runat="server" Text='<%# Bind("Creator") %>' OnInit="InitCreator" />
                </td>                                             
                <td>
                    <asp:Label ID="InsertCreateDate" runat="server" 
                        Text='<%# Bind("CreateDate") %>' OnInit="InitDate" />
                </td>
                <td>
                    <asp:Label ID="InsertLastModifiedDate" runat="server" 
                        Text='<%# Bind("LastModifiedDate") %>' OnInit="InitDate" />
                </td>
                <td>
                    <asp:ImageButton ID="InsertButton" runat="server" CommandName="Insert" CssClass="imageButton" 
                        ToolTip="Insert" SkinID="SaveImageButton" />
                    <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" CssClass="imageButton" 
                        ToolTip="Clear" SkinID="CancelImageButton"/>
                </td>                
            </tr>
        </InsertItemTemplate>
        <LayoutTemplate>
            <table ID="itemPlaceholderContainer" runat="server" border="0" style="width:auto" cellpadding="0" cellspacing="0">
                <tr runat="server" class="header">
                    <th runat="server">                                    
                        <asp:LinkButton ID="TitleFilterInit" runat="server" Text="Title" OnClick="FilterClick" CommandArgument="Title"></asp:LinkButton>
                        <asp:Panel runat="server" ID="TitleFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="TitleFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="TitleFilterOK" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="Title" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "Title descending")
                           { %>
                           <asp:ImageButton ID="ImageButton1" SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="Title ascending" />                                       
                        <% }
                           else if (SortMode == "Title ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton2" SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton3" SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="Title descending" />
                        <% } %>                                      
                    </th>
                    <th runat="server">                                    
                        <asp:LinkButton ID="BodyFilterInit" runat="server" Text="Body" OnClick="FilterClick" CommandArgument="Body"></asp:LinkButton>
                        <asp:Panel runat="server" ID="BodyFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="BodyFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="BodyFilterOK" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="Body" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "Body descending")
                           { %>
                           <asp:ImageButton ID="ImageButton4" SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="Body ascending" />                                       
                        <% }
                           else if (SortMode == "Body ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton5" SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton6" SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="Body descending" />
                        <% } %>                                      
                    </th>
                    <th id="Th1" runat="server">                                    
                        <asp:LinkButton ID="CategoryFilterInit" runat="server" Text="Category" OnClick="FilterClick" CommandArgument="Category"></asp:LinkButton>
                        <asp:Panel runat="server" ID="CategoryFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="CategoryFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="CategoryFilterOK" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="Category" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "Category descending")
                           { %>
                           <asp:ImageButton ID="ImageButton7" SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="Category ascending" />                                       
                        <% }
                           else if (SortMode == "Category ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton8" SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton9" SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="Category descending" />
                        <% } %>                                      
                    </th>  
                    <th id="Th3" runat="server">                                    
                        CommentsCount                                    
                    </th>                                     
                    <th id="Th2" runat="server">                                    
                        <asp:LinkButton ID="CreatorFilterInit" runat="server" Text="Creator" OnClick="FilterClick" CommandArgument="Creator"></asp:LinkButton>
                        <asp:Panel runat="server" ID="CreatorFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="CreatorFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="CreatorFilterOK" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="Creator" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "Creator descending")
                           { %>
                           <asp:ImageButton ID="ImageButton13" SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="Creator ascending" />                                       
                        <% }
                           else if (SortMode == "Creator ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton14" SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton15" SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="Creator descending" />
                        <% } %>                                      
                    </th>                                                                      
                    <th runat="server">                                    
                        <asp:LinkButton ID="CreateDateFilterInit" runat="server" Text="CreateDate" OnClick="FilterClick" CommandArgument="CreateDate"></asp:LinkButton>
                        <asp:Panel runat="server" ID="CreateDateFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="CreateDateFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="CreateDateFilterOK" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="CreateDate" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "CreateDate descending")
                           { %>
                           <asp:ImageButton ID="ImageButton16" SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="CreateDate ascending" />                                       
                        <% }
                           else if (SortMode == "CreateDate ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton17" SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton18" SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="CreateDate descending" />
                        <% } %>                                      
                    </th>
                    <th runat="server">                                    
                        <asp:LinkButton ID="LastModifiedDateFilterInit" runat="server" Text="LastModifiedDate" OnClick="FilterClick" CommandArgument="LastModifiedDate"></asp:LinkButton>
                        <asp:Panel runat="server" ID="LastModifiedDateFilterPanel" Visible="false">
                           =
                           <asp:TextBox ID="LastModifiedDateFilter" runat="server"></asp:TextBox>
                           <asp:LinkButton ID="LastModifiedDateFilterOK" runat="server" Text="OK" OnClick="FilterOK" CommandArgument="LastModifiedDate" CssClass="okButton"></asp:LinkButton>
                        </asp:Panel>
                        <% if (SortMode == "LastModifiedDate descending")
                           { %>
                           <asp:ImageButton ID="ImageButton19" SkinID="DescendingButton" OnClick="Sort" runat="server" CommandArgument="LastModifiedDate ascending" />                                       
                        <% }
                           else if (SortMode == "LastModifiedDate ascending")
                           { %>
                           <asp:ImageButton ID="ImageButton20" SkinID="AscendingButton" OnClick="Sort" runat="server" CommandArgument="" />
                        <% }
                           else
                           { %>
                           <asp:ImageButton ID="ImageButton21" SkinID="NoSortButton" OnClick="Sort" runat="server" CommandArgument="LastModifiedDate descending" />
                        <% } %>                                      
                    </th>
                    <th id="Th4" runat="server">
                    </th>                                    
                </tr>
                <tr ID="itemPlaceholder" runat="server">
                </tr>
                <tr id="Tr1" runat="server" class="simpleRow">
                    <td id="Td1" runat="server" style="" colspan="8">
                        <asp:DataPager ID="DataPager1" runat="server">
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
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Title") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Body") %>' />
                </td>
                <td>
                    <asp:TextBox ID="CategoryTextBox" runat="server" Text='<%# Bind("Category") %>'  Visible='false'/>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" DataSourceID="LinqDataSource2" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged = "CategoryChanged" OnInit="CategoryInit" />                                          
                </td>
                <td>
                  <asp:Label ID="Label1" runat="server" Text='<%# Bind("CommentsCount") %>' />
                </td>   
                <td>
                    <asp:Label ID="InsertCreator" runat="server" Text='<%# Bind("Creator") %>' />
                </td>                                             
                <td>
                    <asp:Label ID="InsertCreateDate" runat="server" 
                        Text='<%# Bind("CreateDate") %>' />
                </td>
                <td>
                    <asp:Label ID="InsertLastModifiedDate" runat="server" 
                        Text='<%# Bind("LastModifiedDate") %>' OnInit="InitDate" />
                </td>            
                <td>
                    <asp:ImageButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="imageButton" 
                        ToolTip="Update"  SkinID="AcceptImageButton"/>
                    <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" CssClass="imageButton" 
                        ToolTip="Cancel"  SkinID="CancelImageButton"/>
                </td>        
            </tr>
        </EditItemTemplate>
    </asp:ListView>
    </div>
</asp:Content>

