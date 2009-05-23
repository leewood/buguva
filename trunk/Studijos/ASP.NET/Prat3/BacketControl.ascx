<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BacketControl.ascx.cs" Inherits="BacketControl" %>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" 
    ContextTypeName="Backet" Select="new (Price, Name, Count, OnePrice, RowIndex)" 
    TableName="BacketLines">
</asp:LinqDataSource>
<asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1" 
    onitemcommand="ListView1_ItemCommand">
    <ItemTemplate>
        <tr style="">
            <td>
                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
            </td>
            <td style="white-space:nowrap; width: 50px">                                
                <asp:Label ID="OnePriceLabel" runat="server" Text='<%# Eval("OnePrice") %>' /> 
                <asp:Label ID="CurrencyLabel" runat="server" Text='<%# " " + Backet.MyActiveCurrency %>' />                
                <asp:Label ID="MulLable" runat="server" Text=" x " />
                <asp:Label ID="CountLabel" runat="server" Text='<%# Eval("Count") %>' />
                <asp:Label ID="Label1" runat="server" Text=" = " />
            </td>
            <td>
                <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' />                
                <asp:Label ID="Label2" runat="server" Text='<%# " " + Backet.MyActiveCurrency %>' />                                
            </td>
            <td>
              <asp:ImageButton ID="IncreaseButton" CommandName="Increase" runat="server" CommandArgument='<%# Eval("RowIndex") %>'  SkinID="BacketIncreaseImageButton"/>
              <asp:ImageButton ID="DecreaseButton" CommandName="Decrease" runat="server" CommandArgument='<%# Eval("RowIndex") %>'  SkinID="BacketDecreaseImageButton"/>
              <asp:ImageButton ID="DeleteButton" CommandName="DeleteItem" runat="server" CommandArgument='<%# Eval("RowIndex") %>'  SkinID="BacketDeleteImageButton"/>
            </td>
        </tr>
    </ItemTemplate>    
    <EmptyDataTemplate>
       <asp:Label runat="server" ID="NoDataLabel" Text="No products in backet" />                    
    </EmptyDataTemplate>    
    <LayoutTemplate>
        <table ID="itemPlaceholderContainer" runat="server" border="0" style="">
            <tr ID="itemPlaceholder" runat="server">
            </tr>
            <tr>
              <td colspan="2"><asp:Label ID="TotalLabel" runat="server" Text="Total:" /></td>
              <td>
              <asp:Label runat="server" ID="TotalValueLabel" Text="0 LTL" OnDataBinding="LabelDataBind" OnInit="LabelDataBind" />                    
              </td>
              <td></td>
            </tr>
            <tr>
              <td colspan="4">
                 <asp:LinkButton ID="OrderButton" runat="server" Text="Order" CommandName = "Order" />
                 <asp:LinkButton ID="ClearButton" runat="server" Text="Clear" CommandName = "Clear" />
              </td>
            </tr>
        </table>
    </LayoutTemplate>        
</asp:ListView>
