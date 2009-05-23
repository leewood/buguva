<%@ Page Title="" Language="C#" MasterPageFile="~/NewsCategories.master" AutoEventWireup="true" CodeFile="ViewOrder.aspx.cs" Inherits="LoggedIn_ViewOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NewsContentHolder" Runat="Server">
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" 
        Select="new (Status, Person, OrderDate, ConfirmDate, Description)" 
        TableName="Orders" Where="id == @id">
        <WhereParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
        ContextTypeName="MainDBDataClassesDataContext" 
        Select="new (ProductName, ProductPrice, Count)" TableName="OrderLines" 
        Where="OrderID == @OrderID">
        <WhereParameters>
            <asp:QueryStringParameter Name="OrderID" QueryStringField="id" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:FormView ID="FormView1" runat="server" DataSourceID="LinqDataSource1">
        <ItemTemplate>
           <div class="toolbar" runat="server" id = "toolbarContainer">
              <div class="extButton" runat="server" id="SpecButton1">
               <asp:ImageButton ID="ConfirmImg" runat="server" CommandName="Confirm" 
                      SkinID="AcceptImageButton" CssClass="simpleImageClear" />
               <asp:Button ID="Button1" runat="server" 
                 CommandName="Confirm" Text="Confirm"  CssClass="simpleClear" />                
              </div>                
              <label class="separator">|</label>
              <div class="extButton" runat="server" id="SpecButton2">
               <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Reject" 
                      SkinID="CancelImageButton" CssClass="simpleImageClear" />
               <asp:Button ID="Button3" runat="server" 
                 CommandName="Reject" Text="Reject"  CssClass="simpleClear" />                
              </div>                
              <label class="separator">|</label>
              
              <div class="extButton">
                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Back" 
                      SkinID="GoBackButton" CssClass="simpleImageClear" />
              <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                CommandName="Back" Text="Back" CssClass="simpleClear" />                
              </div>           
           </div>       
           <div class="form">
            <asp:Label runat="server" ID="LLabel1" Text="Status:" class="title" />
            <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("Status") %>' />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label1" Text="Person:" class="title" />            
            <asp:Label ID="PersonLabel" runat="server" Text='<%# Bind("Person") %>' />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label2" Text="Order date:" class="title" />                        
            <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Bind("OrderDate") %>' />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label3" Text="Confirm date:" class="title" />                        
            <asp:Label ID="ConfirmDateLabel" runat="server" 
                Text='<%# Bind("ConfirmDate") %>' />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label4" Text="Description:" class="title" />                        
            <asp:Label ID="DescriptionLabel" runat="server" 
                Text='<%# Bind("Description") %>' />
            <label class="separator"></label>
            <asp:Label runat="server" ID="Label5" Text="Ordered products:" class="title" />                        
            <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource2">
                <ItemTemplate>
                    <li style="">ProductName:
                        <asp:Label ID="ProductNameLabel" runat="server" 
                            Text='<%# Eval("ProductName") %>' />
                        <br />
                        ProductPrice:
                        <asp:Label ID="ProductPriceLabel" runat="server" 
                            Text='<%# Eval("ProductPrice") %>' />
                        <br />
                        Count:
                        <asp:Label ID="CountLabel" runat="server" Text='<%# Eval("Count") %>' />
                        <br />
                    </li>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <li style="">ProductName:
                        <asp:Label ID="ProductNameLabel" runat="server" 
                            Text='<%# Eval("ProductName") %>' />
                        <br />
                        ProductPrice:
                        <asp:Label ID="ProductPriceLabel" runat="server" 
                            Text='<%# Eval("ProductPrice") %>' />
                        <br />
                        Count:
                        <asp:Label ID="CountLabel" runat="server" Text='<%# Eval("Count") %>' />
                        <br />
                    </li>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    No data was returned.
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <li style="">ProductName:
                        <asp:TextBox ID="ProductNameTextBox" runat="server" 
                            Text='<%# Bind("ProductName") %>' />
                        <br />
                        ProductPrice:
                        <asp:TextBox ID="ProductPriceTextBox" runat="server" 
                            Text='<%# Bind("ProductPrice") %>' />
                        <br />
                        Count:
                        <asp:TextBox ID="CountTextBox" runat="server" Text='<%# Bind("Count") %>' />
                        <br />
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                            Text="Insert" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                            Text="Clear" />
                    </li>
                </InsertItemTemplate>
                <LayoutTemplate>
                    <ul ID="itemPlaceholderContainer" runat="server" style="">
                        <li ID="itemPlaceholder" runat="server" />
                        </ul>
                        <div style="">
                        </div>
                    </LayoutTemplate>
                    <EditItemTemplate>
                        <li style="">ProductName:
                            <asp:TextBox ID="ProductNameTextBox" runat="server" 
                                Text='<%# Bind("ProductName") %>' />
                            <br />
                            ProductPrice:
                            <asp:TextBox ID="ProductPriceTextBox" runat="server" 
                                Text='<%# Bind("ProductPrice") %>' />
                            <br />
                            Count:
                            <asp:TextBox ID="CountTextBox" runat="server" Text='<%# Bind("Count") %>' />
                            <br />
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                Text="Cancel" />
                        </li>
                    </EditItemTemplate>
                    <ItemSeparatorTemplate>
                        <br />
                    </ItemSeparatorTemplate>
                    <SelectedItemTemplate>
                        <li style="">ProductName:
                            <asp:Label ID="ProductNameLabel" runat="server" 
                                Text='<%# Eval("ProductName") %>' />
                            <br />
                            ProductPrice:
                            <asp:Label ID="ProductPriceLabel" runat="server" 
                                Text='<%# Eval("ProductPrice") %>' />
                            <br />
                            Count:
                            <asp:Label ID="CountLabel" runat="server" Text='<%# Eval("Count") %>' />
                            <br />
                        </li>
                    </SelectedItemTemplate>
            </asp:ListView>
            </div> 
        </ItemTemplate>
    </asp:FormView>
</asp:Content>

