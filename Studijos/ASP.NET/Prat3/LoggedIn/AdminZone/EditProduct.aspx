<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn/AdminZone/AdminZone.master" AutoEventWireup="true" CodeFile="EditProduct.aspx.cs" Inherits="LoggedIn_AdminZone_EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentPlaceHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" TableName="Products" 
        Where="id == @id">
        <WhereParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="Rates" 
        TableName="RatesList">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource3" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" TableName="Categories">
    </asp:LinqDataSource>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" 
        DataSourceID="LinqDataSource1" DefaultMode="Edit" 
        onitemupdated="FormView1_ItemUpdated">
        <EditItemTemplate>
           <div class="toolbar">
              <div class="extButton">
               <asp:ImageButton ID="UpdateButtonImg" runat="server" CommandName="Insert" 
                      SkinID="SaveImageButton" CssClass="simpleImageClear" />
               <asp:Button ID="Button1" runat="server" 
                 CommandName="Update" Text="Update"  CssClass="simpleClear" />                
              </div>                
               <label class="separator">|</label>
              <div class="extButton">
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Cancel" 
                      SkinID="CancelImageButton" CssClass="simpleImageClear" />
              <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="Cancel" onclick="InsertCancelButton_Click" CssClass="simpleClear"/>                
              </div>           
           </div>
           <div class="form">
             <asp:Label runat="server" ID="CategoryLabel" Text="Name:" class="title" />            
             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>' />
             <asp:Label runat="server" ID="Label7" class="separator" /> 
             <asp:Label runat="server" ID="Label1" Text="Price:" class="title" />             
             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Price") %>'  />
             <asp:Label runat="server" ID="Label6" class="separator" /> 
             <asp:Label runat="server" ID="Label2" Text="Currency:" class="title" />            
            <asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="LinqDataSource2" DataTextField="Description" 
                DataValueField="Code" SelectedValue='<%# Bind("Currency") %>' >
            </asp:DropDownList>
            <asp:Label runat="server" ID="Label5" class="separator" /> 
            <asp:Label runat="server" ID="Label3" Text="Picture:" class="title" />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Label runat="server" ID="CategorySeparator" class="separator" /> 
            <asp:Label runat="server" ID="Label4" Text="Category:" class="title" />            
            <asp:DropDownList ID="DropDownList2" runat="server" 
                DataSourceID="LinqDataSource3" DataTextField="Title" DataValueField="Name" 
                   SelectedValue='<%# Bind("Category") %>' >
            </asp:DropDownList>
          </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>

