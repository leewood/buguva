<%@ Page Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="InsertNew.aspx.cs" Inherits="LoggedIn_AdminZone_InsertNew" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
   <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
                      ContextTypeName="MainDBDataClassesDataContext" EnableInsert="True" 
                      TableName="Categories" />
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableInsert="True" 
        TableName="News">
        <InsertParameters>   
            <asp:ControlParameter ControlID="UserHiddenField" Name="Creator" 
                PropertyName="Value" />
            <asp:ControlParameter ControlID="DateHiddenField" Name="CreateDate" 
                PropertyName="Value" />
            <asp:ControlParameter ControlID="DateHiddenField" Name="LastModifiedDate" 
                PropertyName="Value" />
        </InsertParameters>
    </asp:LinqDataSource>
    <asp:HiddenField ID="DateHiddenField" runat="server" Value="<%# DateTime.Now.ToString() %>"/>
    <asp:HiddenField ID="UserHiddenField" runat="server" Value="<%# Page.User.Identity.Name %>"/>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" SkinID="form" 
        DataSourceID="LinqDataSource1" DefaultMode="Insert" 
        oniteminserted="FormView1_ItemInserted">
        <InsertItemTemplate>
           <div class="toolbar">
              <div class="extButton">
               <asp:ImageButton ID="UpdateButtonImg" runat="server" CausesValidation="true" CommandName="Insert" SkinID="SaveImageButton" CssClass="simpleImageClear" />
               <asp:Button ID="InsertButton" runat="server" CausesValidation="True" 
                 CommandName="Insert" Text="Insert"  CssClass="simpleClear"/>                
              </div>                
               <label class="separator">|</label>
              <div class="extButton">
                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="true" CommandName="Cancel" SkinID="CancelImageButton" CssClass="simpleImageClear" />
              <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="Cancel" onclick="InsertCancelButton_Click" CssClass="simpleClear" />                
              </div>
          </div>        
          <div class="form">
            <label class="title">Title:</label>
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            <label class="separator"></label>                            
            <asp:Label runat="server" ID="CategoryLabel" Text="Category:" class="title"/> 
                   <asp:DropDownList ID="CategoryDropDown" runat ="server" 
                DataSourceID="LinqDataSource2" DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Category") %>' 
                 />
            <asp:Label runat="server" ID="CategorySeparator" Text="|" class="separator"/> 
            <label class="title">Body:</label>
               <asp:TextBox ID="TextBox1" runat="server" Height="150px" 
                Text='<%# Bind("Body") %>' TextMode="MultiLine"></asp:TextBox>
            <label class="separator"></label>
        </InsertItemTemplate>

    </asp:FormView>

</asp:Content>

