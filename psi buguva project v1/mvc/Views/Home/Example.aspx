<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Example.aspx.cs" Inherits="mvc.Views.Home.Example" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h2>About Us</h2>
    <p>
        Kad examplas veiktu:</p>
    <p>
        1. Pakeisk SqlDataSource connectionStringa i toki, koks pas tave SQL servas, nes 
        dabar nustatyta ant mano vietinio</p>
    <p>
        2. Sukurk table &quot;example1&quot; su dviem stulpeliais col1 ir col2 (abu varchar(50), 
        col1 - pirminis raktas). Tai gali padaryt siuo sakiniu jei ka: &quot;CREATE TABLE 
        [dbo].[example1]( [col1] [varchar](50) COLLATE Lithuanian_CI_AS NOT NULL, [col2] 
        [varchar](50) COLLATE Lithuanian_CI_AS NULL, CONSTRAINT [PK_example1] PRIMARY 
        KEY CLUSTERED ( [col1] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
        IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 
        ) ON [PRIMARY] GO SET ANSI_PADDING OFF&quot;</p>
<p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:HRTest5ConnectionString %>" 
            
            SelectCommand="SELECT [col1], [col2] FROM [example1]" 
            ConflictDetection="CompareAllValues" 
            DeleteCommand="DELETE FROM [example1] WHERE [col1] = @original_col1 AND [col2] = @original_col2" 
            InsertCommand="INSERT INTO [example1] ([col1], [col2]) VALUES (@col1, @col2)" 
            OldValuesParameterFormatString="original_{0}" 
            UpdateCommand="UPDATE [example1] SET [col2] = @col2 WHERE [col1] = @original_col1 AND [col2] = @original_col2">
            <DeleteParameters>
                <asp:Parameter Name="original_col1" Type="String" />
                <asp:Parameter Name="original_col2" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="col2" Type="String" />
                <asp:Parameter Name="original_col1" Type="String" />
                <asp:Parameter Name="original_col2" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="col1" Type="String" />
                <asp:Parameter Name="col2" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" 
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="COL1" HeaderText="COL1" SortExpression="COL1" />
            <asp:BoundField DataField="COL2" HeaderText="COL2" SortExpression="COL2" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                ShowEditButton="True" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
        
    </p>
<p>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1">
            <EditItemTemplate>
                COL1:
                <asp:TextBox ID="COL1TextBox" runat="server" Text='<%# Bind("COL1") %>' />
                <br />
                COL2:
                <asp:TextBox ID="COL2TextBox" runat="server" Text='<%# Bind("COL2") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
            <InsertItemTemplate>
                COL1:
                <asp:TextBox ID="COL1TextBox" runat="server" Text='<%# Bind("COL1") %>' />
                <br />
                COL2:
                <asp:TextBox ID="COL2TextBox" runat="server" Text='<%# Bind("COL2") %>' />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" />
                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                COL1:
                <asp:Label ID="COL1Label" runat="server" Text='<%# Bind("COL1") %>' />
                <br />
                COL2:
                <asp:Label ID="COL2Label" runat="server" Text='<%# Bind("COL2") %>' />
                <br />
                <asp:Button ID="Button1" runat="server" CommandName="Edit" Text="Edit" />
                <br />
            </ItemTemplate>
        </asp:FormView>
    </p>
</form>
</asp:Content>
