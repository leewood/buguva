<%@ Page Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="ShowNew.aspx.cs" Inherits="ShowNew" Title="Edit new" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" TableName="News" 
        Where="id == @id">
        <WhereParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" OrderBy="CeateDate desc" 
        TableName="Comments" Where="NewID == @NewID">
        <InsertParameters>
            <asp:QueryStringParameter Name="NewID" QueryStringField="id" />
            <asp:ControlParameter ControlID="DateHiddenField" Name="CeateDate" 
                PropertyName="Value" />
            <asp:ControlParameter ControlID="UserHiddenField" Name="Creator" 
                PropertyName="Value" />
        </InsertParameters>
        <WhereParameters>
            <asp:QueryStringParameter Name="NewID" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:HiddenField ID="DateHiddenField" runat="server" />
    <asp:HiddenField ID="UserHiddenField" runat="server" />
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" 
        DataSourceID="LinqDataSource1" CssClass="maxContainer" 
        meta:resourcekey="FormView1Resource1">
        <EditItemTemplate>
            CommentsCount:
            <asp:TextBox ID="CommentsCountTextBox" runat="server" 
                Text='<%# Bind("CommentsCount") %>' 
                meta:resourcekey="CommentsCountTextBoxResource1" />
            <br />
            id:
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
            <asp:LinkButton ID="UpdateButton" runat="server" 
                CommandName="Update" Text="Update" 
                meta:resourcekey="UpdateButtonResource1" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" 
                meta:resourcekey="UpdateCancelButtonResource1" />
        </EditItemTemplate>
        <InsertItemTemplate>
            CommentsCount:
            <asp:TextBox ID="CommentsCountTextBox" runat="server" 
                Text='<%# Bind("CommentsCount") %>' 
                meta:resourcekey="CommentsCountTextBoxResource2" />
            <br />
            Title:
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' 
                meta:resourcekey="TitleTextBoxResource2" />
            <br />
            Body:
            <asp:TextBox ID="BodyTextBox" runat="server" Text='<%# Bind("Body") %>' 
                meta:resourcekey="BodyTextBoxResource2" />
            <br />
            CreateDate:
            <asp:TextBox ID="CreateDateTextBox" runat="server" 
                Text='<%# Bind("CreateDate") %>' 
                meta:resourcekey="CreateDateTextBoxResource2" />
            <br />
            LastModifiedDate:
            <asp:TextBox ID="LastModifiedDateTextBox" runat="server" 
                Text='<%# Bind("LastModifiedDate") %>' 
                meta:resourcekey="LastModifiedDateTextBoxResource2" />
            <br />
            Creator:
            <asp:TextBox ID="CreatorTextBox" runat="server" Text='<%# Bind("Creator") %>' 
                meta:resourcekey="CreatorTextBoxResource2" />
            <br />
            Category:
            <asp:TextBox ID="CategoryTextBox" runat="server" 
                Text='<%# Bind("Category") %>' 
                meta:resourcekey="CategoryTextBoxResource2" />
            <br />
            Comments:
            <asp:TextBox ID="CommentsTextBox" runat="server" 
                Text='<%# Bind("Comments") %>' 
                meta:resourcekey="CommentsTextBoxResource2" />
            <br />
            Category1:
            <asp:TextBox ID="Category1TextBox" runat="server" 
                Text='<%# Bind("Category1") %>' 
                meta:resourcekey="Category1TextBoxResource2" />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" 
                CommandName="Insert" Text="Insert" 
                meta:resourcekey="InsertButtonResource1" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" 
                meta:resourcekey="InsertCancelButtonResource1" />
        </InsertItemTemplate>
        <ItemTemplate>
            <div class="newBlock">
                <asp:Label ID="HyperLink1" runat="server" Text='<%# Eval("Title") %>' 
                    CssClass="newTitle" meta:resourcekey="HyperLink1Resource1" />                
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Body") %>' 
                    CssClass="newBody" meta:resourcekey="Label1Resource2"/>
                <span class="smallText">
                    <asp:Label ID="Label5" runat="server" Text="Posted by " 
                    meta:resourcekey="Label5Resource1"></asp:Label><asp:Label ID="Label2" runat="server" 
                    Text='<%# Eval("Creator") %>' meta:resourcekey="Label2Resource1" />
                <asp:Label ID="Label6" runat="server" Text=" on " 
                    meta:resourcekey="Label6Resource1" />
                <asp:Label ID="Label3" runat="server" 
                        Text='<%# Eval("CreateDate") %>' meta:resourcekey="Label3Resource1" />                        
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Last Modified on " 
                    meta:resourcekey="Label7Resource1" /><asp:Label ID="Label4" runat="server" 
                        Text='<%# Eval("LastModifiedDate") %>' 
                    meta:resourcekey="Label4Resource1" />
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Category: " 
                    meta:resourcekey="Label8Resource1" /><asp:Label ID="CategoryLabel" runat="server" 
                    Text='<%# Bind("Category") %>' meta:resourcekey="CategoryLabelResource1" />
                    <br />                    
                </span>      
                <br />  
                <asp:Label ID="CommentsLabel" runat="server" Text="Comments:" 
                    CssClass="newTitle" meta:resourcekey="CommentsLabelResource1"></asp:Label> 
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="id" 
                DataSourceID="LinqDataSource2" InsertItemPosition="LastItem">
                <ItemTemplate>
                   <div class="newBlock" >
                        <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' 
                            CssClass="newBody" meta:resourcekey="TextLabelResource1"></asp:Label>
                        <span class="smallText">
                            <asp:Label ID="Label2" runat="server" Text="Posted by " 
                            meta:resourcekey="Label2Resource3" /><asp:Label ID="CreatorLabel" runat="server" 
                            Text='<%# Eval("Creator") %>' meta:resourcekey="CreatorLabelResource1"></asp:Label>
                        <asp:Label ID="Label9" runat="server" Text=" on " 
                            meta:resourcekey="Label9Resource1" />
                        <asp:Label ID="CeateDateLabel" runat="server" Text='<%# Eval("CeateDate") %>' 
                            meta:resourcekey="CeateDateLabelResource1"></asp:Label>
                        </span>
                        <div class="buttonsArea" >
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" 
                            OnDataBinding="EditButton_DataBinding" CssClass="editButton" 
                                meta:resourcekey="EditButtonResource1" />
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete"  CssClass="deleteButton"
                            Text="Delete" meta:resourcekey="DeleteButtonResource1" />
                        </div>
                    </div>    
                </ItemTemplate>                
                <EmptyDataTemplate>
                     <asp:Label ID="Label2" runat="server" Text="No data was returned." 
                         meta:resourcekey="Label2Resource2" />
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextTextBox" runat="server" Height="53px" 
                        Text='<%# Bind("Text") %>' TextMode="MultiLine" Width="174px" 
                        meta:resourcekey="TextTextBoxResource2"></asp:TextBox>
                    <br />
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                        Text="Insert" CssClass="insertButton" 
                        meta:resourcekey="InsertButtonResource2"/>
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Clear" CssClass="simpleButton" 
                        meta:resourcekey="CancelButtonResource3"/>
                    <br />
                    <br />
                    <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True"  ButtonCssClass="simpleButton"
                                    ShowLastPageButton="True" 
                                    meta:resourcekey="NextPreviousPagerFieldResource1" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </InsertItemTemplate>
                <LayoutTemplate>
                    <div ID="itemPlaceholderContainer" runat="server" style="">
                        <span style=""><span ID="itemPlaceholder" 
                            runat="server"></span></span>
                    </div>
                </LayoutTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextTextBox" runat="server" Height="58px" 
                        Text='<%# Bind("Text") %>' TextMode="MultiLine" Width="183px" 
                        meta:resourcekey="TextTextBoxResource1"></asp:TextBox>
                    <br />
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" meta:resourcekey="UpdateButtonResource2" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" meta:resourcekey="CancelButtonResource2" />
                    <br />
                    <br />
                </EditItemTemplate>
                <SelectedItemTemplate>
                    <span style="">id:
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' 
                        meta:resourcekey="idLabelResource1" />
                    <br />
                    NewID:
                    <asp:Label ID="NewIDLabel" runat="server" Text='<%# Eval("NewID") %>' 
                        meta:resourcekey="NewIDLabelResource1" />
                    <br />
                    Text:
                    <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' 
                        meta:resourcekey="TextLabelResource2" />
                    <br />
                    Creator:
                    <asp:Label ID="CreatorLabel" runat="server" Text='<%# Eval("Creator") %>' 
                        meta:resourcekey="CreatorLabelResource2" />
                    <br />
                    CeateDate:
                    <asp:Label ID="CeateDateLabel" runat="server" Text='<%# Eval("CeateDate") %>' 
                        meta:resourcekey="CeateDateLabelResource2" />
                    <br />
                    New:
                    <asp:Label ID="NewLabel" runat="server" Text='<%# Eval("New") %>' 
                        meta:resourcekey="NewLabelResource1" />
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
            </div>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>

