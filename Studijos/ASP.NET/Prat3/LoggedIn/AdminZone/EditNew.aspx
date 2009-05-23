<%@ Page Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="EditNew.aspx.cs" Inherits="LoggedIn_AdminZone_EditNew" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableUpdate="True" 
        TableName="Categories">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableUpdate="True" 
        TableName="News" Where="id.ToString() == @id">
        <UpdateParameters>
            <asp:ControlParameter ControlID="DateHiddenField" Name="LastModifiedDate" 
                PropertyName="Value" />
        </UpdateParameters>
        <InsertParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" />
        </InsertParameters>
        <WhereParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id"  SkinID="form" CssClass="container"
        DataSourceID="LinqDataSource2" DefaultMode="Edit" 
        onitemupdated="FormView1_ItemUpdated">
        <EditItemTemplate>
           <div class="toolbar">
              <div class="extButton">
               <asp:ImageButton ID="UpdateButtonImg" runat="server" CausesValidation="true" CommandName="Update" SkinID="SaveImageButton" CssClass="simpleImageClear" />
               <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update"  CssClass="simpleClear"/>
              </div>                
               <label class="separator">|</label>
              <div class="extButton">
               <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="true" CommandName="Cancel" SkinID="CancelImageButton" CssClass="simpleImageClear" />
               <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" 
                CommandName="Cancel" onclick="UpdateCancelButton_Click" Text="Cancel" CssClass="simpleClear"/>
              </div>
           </div>
           <div class="form">
            <label class="title">Title:</label>
            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            <label class="separator"></label>
            <label class="title">Category:</label>
            <asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="LinqDataSource1" DataTextField="Name" DataValueField="Name" 
                SelectedValue='<%# Bind("Category") %>'>
            </asp:DropDownList>
            <label class="separator"></label>
            <label class="title">Body:</label>
            <asp:TextBox ID="BodyTextBox" runat="server" Height="150px" 
                Text='<%# Bind("Body") %>' TextMode="MultiLine" />            
            <asp:HiddenField ID="ModifiedHiddenField" runat="server" 
                Value='<%# Bind("LastModifiedDate") %>' />
            <div class="buttonsArea">
              </div>  
           </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>

