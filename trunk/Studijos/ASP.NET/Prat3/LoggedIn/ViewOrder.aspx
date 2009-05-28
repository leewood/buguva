<%@ Page Title="" Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="ViewOrder.aspx.cs" Inherits="LoggedIn_ViewOrder" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register src="../UserView.ascx" tagname="UserView" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" 
        Select="new (id, Status, StatusString, SpecButtonsVisible, Person, OrderDate, ConfirmDate, Description)" 
        TableName="Orders" Where="id == @id">
        <WhereParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" 
        Select="new (ProductName, ProductPrice, Count, ActivePrice)" TableName="OrderLines" 
        Where="OrderID == @OrderID">
        <WhereParameters>
            <asp:QueryStringParameter Name="OrderID" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:FormView ID="FormView1" runat="server" DataSourceID="LinqDataSource1" 
        onitemcommand="FormView1_ItemCommand" 
    meta:resourcekey="FormView1Resource1">
        <ItemTemplate>
           <div class="toolbar" runat="server" id = "toolbarContainer">
              <div class="extButton" runat="server" id="SpecButton1">
               <asp:ImageButton ID="ConfirmImg" runat="server" CommandName="Confirm"  Visible='<%# Eval("SpecButtonsVisible") %>'
                      SkinID="AcceptImageButton" CssClass="simpleImageClear" 
                      CommandArgument='<%# Eval("id") %>' meta:resourcekey="ConfirmImgResource1" />
               <asp:Button ID="Button1" runat="server"  Visible='<%# Eval("SpecButtonsVisible") %>' 
                 CommandName="Confirm" Text="Confirm"  CssClass="simpleClear" 
                      CommandArgument='<%# Eval("id") %>' meta:resourcekey="Button1Resource1" />                
              </div>                
              <asp:Label runat ="server" ID="SepLabel1" CssClass="separator"  
                   Visible='<%# Eval("SpecButtonsVisible") %>' 
                   meta:resourcekey="SepLabel1Resource1">|</asp:Label>
              <div class="extButton" runat="server" id="SpecButton2">
               <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Reject"  Visible='<%# Eval("SpecButtonsVisible") %>'
                      SkinID="CancelImageButton" CssClass="simpleImageClear" 
                      CommandArgument='<%# Eval("id") %>' meta:resourcekey="ImageButton2Resource1" />
               <asp:Button ID="Button3" runat="server"  Visible='<%# Eval("SpecButtonsVisible") %>'
                 CommandName="Reject" Text="Reject"  CssClass="simpleClear" 
                      CommandArgument='<%# Eval("id") %>' meta:resourcekey="Button3Resource1" />                
              </div>                
              <asp:Label runat ="server" ID="Label6" CssClass="separator"  
                   Visible='<%# Eval("SpecButtonsVisible") %>' meta:resourcekey="Label6Resource1">|</asp:Label>
              
              <div class="extButton">
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Back" 
                      SkinID="GoBackButton" CssClass="simpleImageClear" 
                      meta:resourcekey="ImageButton1Resource1" />
              <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                CommandName="Back" Text="Back" CssClass="simpleClear" 
                      meta:resourcekey="Button2Resource2" />                
              </div>           
           </div>       
           <div class="form">
            <asp:Label runat="server" ID="LLabel1" Text="Status:" class="title" 
                   meta:resourcekey="LLabel1Resource1" />
            <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("StatusString") %>' 
                   meta:resourcekey="StatusLabelResource1" />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label1" Text="Person:" class="title" 
                   meta:resourcekey="Label1Resource2" />            
            <asp:Label ID="PersonLabel" runat="server" Text='<%# Bind("Person") %>' 
                   meta:resourcekey="PersonLabelResource1" 
                   ondatabinding="PersonLabel_DataBinding" />
               <asp:Panel ID="Panel1" runat="server">
                   <label class="separator">
                   <uc1:UserView ID="UserView1" runat="server" />
                   </label>
               </asp:Panel>
               <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender" 
                   runat="server" CollapseControlID="PersonLabel" Collapsed="True" Enabled="True" 
                   ExpandControlID="PersonLabel" SuppressPostBack="True" TargetControlID="Panel1" ExpandedSize="400" ScrollContents="True">
               </cc1:CollapsiblePanelExtender>
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label2" Text="Order date:" class="title" 
                   meta:resourcekey="Label2Resource1" />                        
            <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Bind("OrderDate") %>' 
                   meta:resourcekey="OrderDateLabelResource1" />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label3" Text="Confirm date:" class="title" 
                   meta:resourcekey="Label3Resource1" />                        
            <asp:Label ID="ConfirmDateLabel" runat="server" 
                Text='<%# Bind("ConfirmDate") %>' 
                   meta:resourcekey="ConfirmDateLabelResource1" />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label4" Text="Description:" class="title" 
                   meta:resourcekey="Label4Resource1" />                        
            <asp:Label ID="DescriptionLabel" runat="server" 
                Text='<%# Bind("Description") %>' 
                   meta:resourcekey="DescriptionLabelResource1" />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label5" Text="Ordered products:" class="title" 
                   meta:resourcekey="Label5Resource1" />                        
            <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource2">
                <ItemTemplate>
                    <li style="">
                        <asp:Literal ID="Item1Literal" runat="server" Text="Product Name:" 
                            meta:resourcekey="Item1LiteralResource1" />
                        <asp:Label ID="ProductNameLabel" runat="server" 
                            Text='<%# Eval("ProductName") %>' 
                            meta:resourcekey="ProductNameLabelResource1" />
                        <br />
                        <asp:Literal ID="Literal1" runat="server" Text="Product Price:" 
                            meta:resourcekey="Literal1Resource1" />                        
                        <asp:Label ID="ProductPriceLabel" runat="server" 
                            
                            Text='<%# Eval("ActivePrice", "{0:0.00}") + " " + Backet.MyActiveCurrency %>' 
                            meta:resourcekey="ProductPriceLabelResource1" />
                        <br />                        
                        <asp:Literal ID="Literal2" runat="server" Text="Count:" 
                            meta:resourcekey="Literal2Resource1" />                        
                        <asp:Label ID="CountLabel" runat="server" Text='<%# Eval("Count") %>' 
                            meta:resourcekey="CountLabelResource1" />
                        <br />
                    </li>
                </ItemTemplate>                
                <EmptyDataTemplate>
                   <asp:Label id="NoDataLabel" runat="server" Text="No data was returned." 
                        meta:resourcekey="NoDataLabelResource1" />
                </EmptyDataTemplate>

                <LayoutTemplate>
                    <ul ID="itemPlaceholderContainer" runat="server" style="">
                        <li ID="itemPlaceholder" runat="server" />
                        </ul>
                        <div style="">
                        </div>
                    </LayoutTemplate>
                    <ItemSeparatorTemplate>
                        <br />
                    </ItemSeparatorTemplate>
            </asp:ListView>
            </div> 
        </ItemTemplate>
    </asp:FormView>
</asp:Content>

