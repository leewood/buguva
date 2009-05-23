<%@ Page Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="PublicNews.aspx.cs" Inherits="PublicNews" Title="News" EnableEventValidation="false" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        CombineScripts="True">
    </cc1:ToolkitScriptManager>
    <asp:Panel ID="Panel1" runat="server" CssClass="toolbar" 
        meta:resourcekey="Panel1Resource1">
        <asp:Label ID="CreateDateFromLabel" runat="server" Text="Create date from:" 
            meta:resourcekey="CreateDateFromLabelResource1"></asp:Label>
        <asp:TextBox ID="CreateDateFromTextBox" runat="server" 
            meta:resourcekey="CreateDateFromTextBoxResource1"></asp:TextBox>
        <cc1:CalendarExtender ID="CreateDateFromTextBox_CalendarExtender" 
            runat="server" Enabled="True" PopupButtonID="CreateDateFromImageButton" 
            TargetControlID="CreateDateFromTextBox">
        </cc1:CalendarExtender>
        
        <asp:ImageButton ID="CreateDateFromImageButton" runat="server" 
            ImageAlign="TextTop" ImageUrl="~/Images/calendar.png"  CssClass="inside" 
            meta:resourcekey="CreateDateFromImageButtonResource1"/>
        <label class="separator">|</label>
        &nbsp;
        <asp:Label ID="CreateDateToLabel" runat="server" 
            meta:resourcekey="CreateDateToLabelResource1" Text="Create date to:"></asp:Label>
        <asp:TextBox ID="CreateDateToTextBox" runat="server" 
            meta:resourcekey="CreateDateToTextBoxResource1"></asp:TextBox>
        <cc1:CalendarExtender ID="CreateDateToTextBox_CalendarExtender" runat="server" 
            Enabled="True" PopupButtonID="CreateDateToImageButton" 
            TargetControlID="CreateDateToTextBox">
        </cc1:CalendarExtender>
        <asp:ImageButton ID="CreateDateToImageButton" runat="server" CssClass="inside" 
            ImageAlign="TextTop" ImageUrl="~/Images/calendar.png" 
            meta:resourcekey="CreateDateToImageButtonResource1" />
        <label class="separator">|</label>
&nbsp;&nbsp;<asp:Button ID="DateFilterButton" runat="server" Text="Update list" 
            meta:resourcekey="DateFilterButtonResource1" />
    </asp:Panel>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="id" 
        DataSourceID="LinqDataSource1"
        oniteminserting="ListView1_ItemInserting" 
         OnItemCommand="ListView1_ItemCommand"
        >
        <ItemTemplate>        
            <div class="newBlock">
                <asp:HyperLink ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' 
                    CssClass="newTitle" NavigateUrl='<%# Eval("id", "ShowNew.aspx?id={0}") %>' 
                    meta:resourcekey="TitleLabelResource1"/>                
                <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' 
                    CssClass="newBody" meta:resourcekey="BodyLabelResource1"/>
                <span class="smallText">
                    <asp:Label ID="Label1" runat="server" Text="Posted by " 
                    meta:resourcekey="Label1Resource1" /><asp:Label ID="CreatorLabel" 
                    runat="server" Text='<%# Eval("Creator") %>' 
                    meta:resourcekey="CreatorLabelResource1" /><asp:Label ID="Label2" 
                    runat="server" Text=" on " meta:resourcekey="Label2Resource1" />
                <asp:Label ID="CreateDateLabel" runat="server" 
                        Text='<%# Eval("CreateDate") %>' 
                    meta:resourcekey="CreateDateLabelResource1" />                        
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Last Modified on " 
                    meta:resourcekey="Label3Resource1" /><asp:Label ID="LastModifiedDateLabel" runat="server" 
                        Text='<%# Eval("LastModifiedDate") %>' 
                    meta:resourcekey="LastModifiedDateLabelResource1" />
                    <br />
                    <asp:HyperLink ID="CommentsLink1" runat="server"  Text="Comments: " meta:resourcekey="CommentsLinkResource2"/>
                    <asp:HyperLink ID="CommentsLink" runat="server"                  
                        Text='<%# Eval("CommentsCount", "{0}") %>' 
                    meta:resourcekey="CommentsLinkResource1"></asp:HyperLink>
                    <br />
                </span>
                  <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit"  
                    CommandArgument= '<%# Eval("id", "{0}") %>' CssClass="editButton" 
                    OnClick="EditButtonClick" meta:resourcekey="EditButtonResource1" />
                  <asp:Button ID="DeleteButton" runat="server" CommandName="Delete"  
                    CssClass="deleteButton" Text="Delete" OnClick="DeleteButtonClick" 
                    meta:resourcekey="DeleteButtonResource1" />       
            </div>
        </ItemTemplate>        
        <EmptyDataTemplate>
            <asp:Label ID="CreatorLabel" runat="server" Text="No news so far." 
                meta:resourcekey="CreatorLabelResource2" />
            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" 
                CssClass="insertButton" OnInit="InsertButtonInit" OnClick="InsertButtonClick" 
                meta:resourcekey="InsertButtonResource1" />
        </EmptyDataTemplate>
        <LayoutTemplate>
            <div ID="itemPlaceholderContainer" runat="server" 
                style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                <span ID="itemPlaceholder" runat="server" />
                <span style="background-color: #FFF8DC;">              
                <div  class="pager">                   
                   <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" 
                        CssClass="insertButton" OnInit="InsertButtonInit" OnClick="InsertButtonClick" 
                        meta:resourcekey="InsertButtonResource2" />                   
                    <asp:DataPager ID="DataPager1" runat="server">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                ShowLastPageButton="True" ButtonCssClass="simpleButton" 
                                meta:resourcekey="NextPreviousPagerFieldResource1" />
                        </Fields>
                    </asp:DataPager>
                </div>
                </span>
            </div>
        </LayoutTemplate>
        <EditItemTemplate>
            <span style="background-color: #008A8C;color: #FFFFFF;">id:
            <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' 
                meta:resourcekey="idLabel1Resource1" />
            <br />
            Title:
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' 
                meta:resourcekey="TitleTextBoxResource1" />
            <br />
            Body:
            <asp:TextBox ID="BodyTextBox" runat="server" Text='<%# Bind("Body") %>' 
                meta:resourcekey="BodyTextBoxResource1" />
            <br />
            CreateDate:
            <asp:TextBox ID="CreateDateTextBox" runat="server" 
                Text='<%# Bind("CreateDate") %>' 
                meta:resourcekey="CreateDateTextBoxResource1" />
            <br />
            LastModifiedDate:
            <asp:TextBox ID="LastModifiedDateTextBox" runat="server" 
                Text='<%# Bind("LastModifiedDate") %>' 
                meta:resourcekey="LastModifiedDateTextBoxResource1" />
            <br />
            Creator:
            <asp:TextBox ID="CreatorTextBox" runat="server" Text='<%# Bind("Creator") %>' 
                meta:resourcekey="CreatorTextBoxResource1" />
            <br />
            Category:
            <asp:TextBox ID="CategoryTextBox" runat="server" 
                Text='<%# Bind("Category") %>' 
                meta:resourcekey="CategoryTextBoxResource1" />
            <br />
            Comments:
            <asp:TextBox ID="CommentsTextBox" runat="server" 
                Text='<%# Bind("Comments") %>' 
                meta:resourcekey="CommentsTextBoxResource1" />
            <br />
            Category1:
            <asp:TextBox ID="Category1TextBox" runat="server" 
                Text='<%# Bind("Category1") %>' 
                meta:resourcekey="Category1TextBoxResource1" />
            <br />
            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                Text="Update" meta:resourcekey="UpdateButtonResource1" />
            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                Text="Cancel" meta:resourcekey="CancelButtonResource1" />
            <br />
            <br />
            </span>
        </EditItemTemplate>
        <SelectedItemTemplate>
            <span style="background-color: #008A8C;font-weight: bold;color: #FFFFFF;">id:
            <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                meta:resourcekey="idLabelResource1" />
            <br />
            Title:
            <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' 
                meta:resourcekey="TitleLabelResource2" />
            <br />
            Body:
            <asp:Label ID="BodyLabel" runat="server" Text='<%# Eval("Body") %>' 
                meta:resourcekey="BodyLabelResource2" />
            <br />
            CreateDate:
            <asp:Label ID="CreateDateLabel" runat="server" 
                Text='<%# Eval("CreateDate") %>' 
                meta:resourcekey="CreateDateLabelResource2" />
            <br />
            LastModifiedDate:
            <asp:Label ID="LastModifiedDateLabel" runat="server" 
                Text='<%# Eval("LastModifiedDate") %>' 
                meta:resourcekey="LastModifiedDateLabelResource2" />
            <br />
            Creator:
            <asp:Label ID="CreatorLabel" runat="server" Text='<%# Eval("Creator") %>' 
                meta:resourcekey="CreatorLabelResource3" />
            <br />
            Category:
            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' 
                meta:resourcekey="CategoryLabelResource1" />
            <br />
            Comments:
            <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' 
                meta:resourcekey="CommentsLabelResource1" />
            <br />
            Category1:
            <asp:Label ID="Category1Label" runat="server" Text='<%# Eval("Category1") %>' 
                meta:resourcekey="Category1LabelResource1" />
            <br />
            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" 
                meta:resourcekey="EditButtonResource2" />
            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" 
                Text="Delete" meta:resourcekey="DeleteButtonResource2" />
            <br />
            <br />
            </span>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" TableName="News">
    </asp:LinqDataSource>
</asp:Content>

