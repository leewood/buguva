<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BacketControl.ascx.cs" Inherits="BacketControl" EnableTheming="true" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
    
        <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1" onitemcommand="ListView1_ItemCommand">
            <ItemTemplate>     
                <dl>
                <dt>                  
                  <asp:HyperLink ID="HyperLink1" runat="server" 
                        Text='<%# Eval("Count") + " x " + Eval("Name") %>' 
                        NavigateUrl = '<%# Eval("ProductID", "~/ShowProduct.aspx?id={0}") %>' 
                        meta:resourcekey="HyperLink1Resource1" />                  
                </dt>
                <dd>
                  <asp:Literal ID="PriceTitleLiteral" runat="server" Text="Price: " 
                        meta:resourcekey="PriceTitleLiteralResource1"></asp:Literal>
                  <asp:Literal ID="PriceValueLiteral" runat="server" Text='<%# Eval("PriceLits", "{0}") %>'></asp:Literal>                  
                  <asp:Label ID="PriceValueCentsLabel" runat="server" 
                        Text='<%# Eval("PriceCents", "{0}") %>' CssClass="price_cents" 
                        meta:resourcekey="PriceValueCentsLabelResource1"></asp:Label>
                        <asp:ImageButton ID="IncreaseButton" CommandName="Increase" runat="server" 
                    CommandArgument='<%# Eval("RowIndex") %>'  SkinID="BacketIncreaseImageButton" 
                        meta:resourcekey="IncreaseButtonResource1"/>
                        <asp:ImageButton ID="DecreaseButton" CommandName="Decrease" runat="server" 
                    CommandArgument='<%# Eval("RowIndex") %>'  SkinID="BacketDecreaseImageButton" 
                        meta:resourcekey="DecreaseButtonResource1"/>
                        <asp:ImageButton ID="DeleteButton" CommandName="DeleteItem" runat="server" 
                    CommandArgument='<%# Eval("RowIndex") %>'  SkinID="BacketDeleteImageButton" 
                        meta:resourcekey="DeleteButtonResource1"/>                  
                </dd>
                </dl>          
            </ItemTemplate>
            <EmptyDataTemplate>
              <div class="catalog_basket_block" ID="itemPlaceholderContainer" runat="server">
                <h2 class="empty_basket_icon" title=""><asp:Literal runat="server" ID="BasketTitle" 
                        Text="Shopping cart" meta:resourcekey="BasketTitleResource1" /></h2>
                <ul>
                <li>
                   <asp:Label runat="server" ID="NoDataLabel" Text="Shopping cart is empty" 
                        meta:resourcekey="NoDataLabelResource1" />
                </li>
        <asp:LoginView ID="LoginView1" runat="server">
<LoggedInTemplate>                      <li>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="My orders" 
                      CommandName = "MyOrders" meta:resourcekey="MyOrders" />                      
                      </li>
                  </LoggedInTemplate>
                    </asp:LoginView>
                </ul>
              </div>
            </EmptyDataTemplate>
            <LayoutTemplate>
                 <div class="catalog_basket_block" ID="itemPlaceholderContainer" runat="server">
                    <h2 class="full_basket_icon" title=""><asp:Literal runat="server" ID="BasketTitle" 
                            Text="Shopping cart" meta:resourcekey="BasketTitleResource2" /></h2>                    
                    <dl ID="itemPlaceholder" runat="server">
                    </dl>
                    <p>
                            <asp:Label ID="TotalLabel" runat="server" Text="Total:" 
                                meta:resourcekey="TotalLabelResource1" />
                            <asp:Literal runat="server" ID="TotalValueLabel" Text="0 LTL" 
                                     OnInit="LabelDataBind" meta:resourcekey="TotalValueLabelResource1" />

                    </p>
                    
                    <ul>
                      <li>
                            <asp:LinkButton ID="OrderButton" runat="server" Text="Order" 
                      CommandName = "Order" meta:resourcekey="OrderButtonResource1" />
                      </li>
                      <li>
                            <asp:LinkButton ID="ClearButton" runat="server" Text="Clear" 
                      CommandName = "Clear" meta:resourcekey="ClearButtonResource1" />
                      </li>
        <asp:LoginView ID="LoginView1" runat="server">
              <LoggedInTemplate>
              

                      <li>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="My orders" 
                      CommandName = "MyOrders" meta:resourcekey="MyOrders" />                      
                      </li>
</LoggedInTemplate>                    
</asp:LoginView>
                    </ul>                
                   </div>
            </LayoutTemplate>
        </asp:ListView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
            ContextTypeName="Backet" Select="new (Price, Name, Count, OnePrice, RowIndex, ProductID, PriceCents, PriceLits)" 
            TableName="BacketLines">
        </asp:LinqDataSource>
    </ContentTemplate>
</asp:UpdatePanel>

