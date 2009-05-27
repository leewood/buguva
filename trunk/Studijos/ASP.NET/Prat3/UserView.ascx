<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserView.ascx.cs" Inherits="UserView" %>
<asp:LinqDataSource ID="LinqDataSource1" runat="server" 
    ContextTypeName="UserProvider" TableName="Users">
</asp:LinqDataSource>
<asp:FormView ID="FormView1" runat="server" DataSourceID="LinqDataSource1" 
    meta:resourcekey="FormView1Resource1">        
    <ItemTemplate>
       <div class="form" style="float: left;">
       <asp:Label runat="server" ID="Label1" class="title" Text="UserName:" 
               meta:resourcekey="Label1Resource1" />
        <asp:Label ID="UserNameLabel" runat="server" Text='<%# Bind("UserName") %>' 
               meta:resourcekey="UserNameLabelResource1" />
        <asp:Label runat="server" ID="Label12" class="separator" 
               meta:resourcekey="Label12Resource1" />
        <asp:Label runat="server" ID="Label2" class="title" Text="Name:" 
               meta:resourcekey="Label2Resource1" />
        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' 
               meta:resourcekey="NameLabelResource2" />
        <asp:Label runat="server" ID="Label13" class="separator" 
               meta:resourcekey="Label13Resource1" />
        <asp:Label runat="server" ID="Label3" class="title" Text="Surname:" 
               meta:resourcekey="Label3Resource1" />
        <asp:Label ID="SurnameLabel" runat="server" Text='<%# Bind("Surname") %>' 
               meta:resourcekey="SurnameLabelResource1" />
        <asp:Label runat="server" ID="Label14" class="separator" 
               meta:resourcekey="Label14Resource1" />
        <asp:Label runat="server" ID="Label4" class="title" Text="City:" 
               meta:resourcekey="Label4Resource1" />
        <asp:Label ID="CityLabel" runat="server" Text='<%# Bind("City") %>' 
               meta:resourcekey="CityLabelResource1" />
        <asp:Label runat="server" ID="Label15" class="separator" 
               meta:resourcekey="Label15Resource1" />
        <asp:Label runat="server" ID="Label5" class="title" Text="Email:" 
               meta:resourcekey="Label5Resource1" />
        <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' 
               meta:resourcekey="EmailLabelResource1" />
        <asp:Label runat="server" ID="Label16" class="separator" 
               meta:resourcekey="Label16Resource1" />
        <asp:Label runat="server" ID="Label6" class="title" Text="Language:" 
               meta:resourcekey="Label6Resource1" />
        <asp:Label ID="LanguageLabel" runat="server" Text='<%# Bind("Language") %>' 
               meta:resourcekey="LanguageLabelResource1" />
        <asp:Label runat="server" ID="Label17" class="separator" 
               meta:resourcekey="Label17Resource1" />
        <asp:Label runat="server" ID="Label7" class="title" Text="Culture:" 
               meta:resourcekey="Label7Resource1" />
        <asp:Label ID="CultureLabel" runat="server" Text='<%# Bind("Culture") %>' 
               meta:resourcekey="CultureLabelResource1" />
        <asp:Label runat="server" ID="Label18" class="separator" 
               meta:resourcekey="Label18Resource1" />
        <asp:Label runat="server" ID="Label8" class="title" Text="Theme:" 
               meta:resourcekey="Label8Resource1" />
        <asp:Label ID="ThemeLabel" runat="server" Text='<%# Bind("Theme") %>' 
               meta:resourcekey="ThemeLabelResource1" />
        <asp:Label runat="server" ID="Label19" class="separator" 
               meta:resourcekey="Label19Resource1" />
        <asp:Label runat="server" ID="Label9" class="title" Text="Creation Date:" 
               meta:resourcekey="Label9Resource1" />
        <asp:Label ID="CreationDateLabel" runat="server" 
            Text='<%# Bind("CreationDate") %>' 
               meta:resourcekey="CreationDateLabelResource1" />
        <asp:Label runat="server" ID="Label20" class="separator" 
               meta:resourcekey="Label20Resource1" />
        <asp:Label runat="server" ID="Label10" class="title" Text="Last Activity Date:" 
               meta:resourcekey="Label10Resource1" />
        <asp:Label ID="LastActivityDateLabel" runat="server" 
            Text='<%# Bind("LastActivityDate") %>' 
               meta:resourcekey="LastActivityDateLabelResource1" />
        <asp:Label runat="server" ID="Label21" class="separator" 
               meta:resourcekey="Label21Resource1" />
        <asp:Label runat="server" ID="Label11" class="title" Text="Last Login Date:" 
               meta:resourcekey="Label11Resource1" />
        <asp:Label ID="LastLoginDateLabel" runat="server" 
            Text='<%# Bind("LastLoginDate") %>' 
               meta:resourcekey="LastLoginDateLabelResource1" />
        <asp:Label runat="server" ID="Label22" class="separator" 
               meta:resourcekey="Label22Resource1" />
        </div>
    </ItemTemplate>
</asp:FormView>
