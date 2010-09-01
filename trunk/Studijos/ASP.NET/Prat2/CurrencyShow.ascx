<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyShow.ascx.cs" Inherits="CurrencyShow" EnableViewState="True"  %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" >
    <ContentTemplate >
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
    ContextTypeName="Rates" TableName="RatesList">
        </asp:LinqDataSource>
        <asp:Literal ID="bla" runat="server"></asp:Literal>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" 
    DataSourceID="LinqDataSource1" DataTextField="Code" DataValueField="Code" 
    onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
    </ContentTemplate>
</asp:UpdatePanel>
