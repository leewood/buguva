<%@ Page Title="" Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="ShowProduct.aspx.cs" Inherits="ShowProduct" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="CurrencyShow.ascx" tagname="CurrencyShow" tagprefix="uc1"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" EnableDelete="True" 
        EnableInsert="True" EnableUpdate="True" TableName="Products" Where="id == @id">
        <WhereParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="id" 
        DataSourceID="LinqDataSource1" meta:resourcekey="FormView1Resource1" 
        onitemcommand="FormView1_ItemCommand" onitemdeleted="FormView1_ItemDeleted"> 
        <ItemTemplate>
            <div class="imageContainer">
            <asp:Image ID="Image1" runat="server" 
                ImageUrl='<%# Eval("id", "GetProductImage.ashx?id={0}") %>' 
                    meta:resourcekey="Image1Resource1" />            
            </div>
            <div class="form" style="float: left;">            
           <div class="toolbar" runat="server">
             <div class="extButton" runat="server">
                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="New" 
                      SkinID="InsertImageButton" CssClass="simpleImageClear" 
                                meta:resourcekey="ImageButton2Resource1" />
            <asp:Button ID="NewButton" runat="server" CausesValidation="False"  CssClass="simpleClear" 
                CommandName="New" Text="New" onload="NewButton_Load" 
                                meta:resourcekey="NewButtonResource1" />                      
              </div>                
               <label class="separator" runat="server">|</label>           
              <div class="extButton" runat="server">
               <asp:ImageButton ID="UpdateButtonImg" runat="server" CommandName="Edit" 
                      SkinID="EditImageButton" CssClass="simpleImageClear" 
                      meta:resourcekey="UpdateButtonImgResource1" />
               <asp:Button ID="EditButton" runat="server" CausesValidation="False" CssClass="simpleClear" 
                      CommandName="Edit" Text="Edit" meta:resourcekey="EditButtonResource1" CommandArgument='<%# Bind("id") %>' />
             </div>
             <label class="separator" runat="server">|</label>
             <div class="extButton" runat="server">
               <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" 
                      SkinID="DeleteImageButton" CssClass="simpleImageClear" 
                     meta:resourcekey="ImageButton1Resource1" />             
             <asp:Button ID="DeleteButton" runat="server" CausesValidation="False"  CssClass="simpleClear" 
                CommandName="Delete" Text="Delete" meta:resourcekey="DeleteButtonResource1" />
             </div>
             <label class="separator" runat="server">|</label>
             <div class="extButton">
               <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Order" 
                      SkinID="CartAddImageButton" CssClass="simpleImageClear" 
                     meta:resourcekey="ImageButton3Resource1" />             
             <asp:Button ID="Button1" runat="server" CausesValidation="False"  CssClass="simpleClear" 
                CommandName="Order" Text="Add to backet" meta:resourcekey="Button1Resource2" CommandArgument='<%# Bind("id") %>' />
             </div>
             <label class="separator">|</label>                                      
            </div>            
            <asp:Label runat="server" ID="Label1" Text="Name:" class="title" 
                    meta:resourcekey="Label1Resource1" /> 
            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' 
                    ondatabinding="PriceLabel_DataBinding" meta:resourcekey="NameLabelResource1" />            
            <asp:Label runat="server" ID="Label7" class="separator" 
                    meta:resourcekey="Label7Resource1" />             
            <asp:Label runat="server" ID="Label2" Text="Price:" class="title" 
                    meta:resourcekey="Label2Resource1" /> 
            <uc1:CurrencyShow ID="CurrencyShow4" runat="server"/>            
            <asp:Label runat="server" ID="Label4" class="separator" 
                    meta:resourcekey="Label4Resource1" />             
            <asp:Label runat="server" ID="Label3" Text="Category:" class="title" 
                    meta:resourcekey="Label3Resource1" /> 
            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Bind("Category") %>' 
                    meta:resourcekey="CategoryLabelResource1" />            
            </div>    
        </ItemTemplate>
    </asp:FormView>
</asp:Content>

