<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn/AdminZone/AdminZone.master" AutoEventWireup="true" CodeFile="EditProduct.aspx.cs" Inherits="LoggedIn_AdminZone_EditProduct" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
        onitemupdated="FormView1_ItemUpdated" 
        meta:resourcekey="FormView1Resource1">
        <EditItemTemplate>
           <div class="toolbar">
              <div class="extButton">
               <asp:ImageButton ID="UpdateButtonImg" runat="server" CommandName="Insert" 
                      SkinID="SaveImageButton" CssClass="simpleImageClear" 
                      meta:resourcekey="UpdateButtonImgResource1" />
               <asp:Button ID="Button1" runat="server" 
                 CommandName="Update" Text="Update"  CssClass="simpleClear" 
                      meta:resourcekey="Button1Resource1" />                
              </div>                
               <label class="separator">|</label>
              <div class="extButton">
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Cancel" 
                      SkinID="CancelImageButton" CssClass="simpleImageClear" 
                      meta:resourcekey="ImageButton1Resource1" />
              <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                CommandName="Cancel" Text="Cancel" onclick="InsertCancelButton_Click" 
                      CssClass="simpleClear" meta:resourcekey="Button2Resource1"/>                
              </div>           
           </div>
           <div class="form">
             <asp:Label runat="server" ID="CategoryLabel" Text="Name:" class="title" 
                   meta:resourcekey="CategoryLabelResource1" />            
             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>' 
                   meta:resourcekey="TextBox1Resource1" />
             <asp:Label runat="server" ID="Label7" class="separator" 
                   meta:resourcekey="Label7Resource1" /> 
             <asp:Label runat="server" ID="Label1" Text="Price:" class="title" 
                   meta:resourcekey="Label1Resource1" />             
             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Price") %>' 
                   meta:resourcekey="TextBox2Resource1"  />
             <asp:Label runat="server" ID="Label6" class="separator" 
                   meta:resourcekey="Label6Resource1" /> 
             <asp:Label runat="server" ID="Label2" Text="Currency:" class="title" 
                   meta:resourcekey="Label2Resource1" />            
            <asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="LinqDataSource2" DataTextField="Description" 
                DataValueField="Code" SelectedValue='<%# Bind("Currency") %>' 
                   meta:resourcekey="DropDownList1Resource1" >
            </asp:DropDownList>
            <asp:Label runat="server" ID="Label5" class="separator" 
                   meta:resourcekey="Label5Resource1" /> 
            <asp:Label runat="server" ID="Label3" Text="Picture:" class="title" 
                   meta:resourcekey="Label3Resource1" />
            <asp:FileUpload ID="FileUpload1" runat="server" 
                   meta:resourcekey="FileUpload1Resource1" />
            <asp:Label runat="server" ID="CategorySeparator" class="separator" 
                   meta:resourcekey="CategorySeparatorResource1" /> 
            <asp:Label runat="server" ID="Label4" Text="Category:" class="title" 
                   meta:resourcekey="Label4Resource1" />            
            <asp:DropDownList ID="DropDownList2" runat="server" 
                DataSourceID="LinqDataSource3" DataTextField="Title" DataValueField="Name" 
                   SelectedValue='<%# Bind("Category") %>' 
                   meta:resourcekey="DropDownList2Resource1" >
            </asp:DropDownList>
          </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>

