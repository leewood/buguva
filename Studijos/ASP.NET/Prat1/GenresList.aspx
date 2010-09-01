<%@ Page  Title="Genres list" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="GenresList.aspx.cs" Inherits="GenresList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>List of my Animes</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:SqlDataSource ID="mainSqlDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:AnimeDBConnectionString %>" 
    DeleteCommand="DELETE FROM [Genres] WHERE [Title] = @original_Title AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([SpecificSaveCatalog] = @original_SpecificSaveCatalog) OR ([SpecificSaveCatalog] IS NULL AND @original_SpecificSaveCatalog IS NULL))" 
    InsertCommand="INSERT INTO [Genres] ([Title], [Description], [SpecificSaveCatalog]) VALUES (@Title, @Description, @SpecificSaveCatalog)" 
    SelectCommand="SELECT [Title], [Description], [SpecificSaveCatalog] FROM [Genres]" 
    
        
    
        UpdateCommand="UPDATE [Genres] SET [Description] = @Description, [SpecificSaveCatalog] = @SpecificSaveCatalog WHERE [Title] = @original_Title AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([SpecificSaveCatalog] = @original_SpecificSaveCatalog) OR ([SpecificSaveCatalog] IS NULL AND @original_SpecificSaveCatalog IS NULL))" 
        FilterExpression="[Title] like '%{0}%'" 
        ConflictDetection="CompareAllValues" 
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="original_Title" Type="String" />
            <asp:Parameter Name="original_Description" Type="String" />
            <asp:Parameter Name="original_SpecificSaveCatalog" Type="String" />
        </DeleteParameters>
        <FilterParameters>
            <asp:ControlParameter ControlID="titleTextBox" Name="filter" 
                PropertyName="Text" />
        </FilterParameters>
        <UpdateParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="SpecificSaveCatalog" Type="String" />
            <asp:Parameter Name="original_Title" Type="String" />
            <asp:Parameter Name="original_Description" Type="String" />
            <asp:Parameter Name="original_SpecificSaveCatalog" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="SpecificSaveCatalog" Type="String" />
        </InsertParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="genresDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AnimeDBConnectionString %>" 
        SelectCommand="SELECT [Title] FROM [Genres]"></asp:SqlDataSource>
        <div class="toolbar">
    <asp:Label ID="Label1" runat="server" Text="Items per page:"></asp:Label>
    <asp:TextBox ID="pageSizeTextBox" runat="server">10</asp:TextBox>       
        <span class="seperator"></span>    
        <asp:Label ID="Label3" runat="server" Text="Title filter:"></asp:Label>
    <asp:TextBox ID="titleTextBox" runat="server" 
    ontextchanged="TextBox1_TextChanged"></asp:TextBox>   
    <span class="seperator"></span>    
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Update" />
    
    </div>
  

<div class="grid_new">
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="dataGrid" style="width:100%"
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Title" 
    DataSourceID="mainSqlDataSource" onrowcommand="GridView1_RowCommand">
    <Columns>
        <asp:BoundField DataField="Title" HeaderText="Title" 
            ReadOnly="True" SortExpression="Title" />
        <asp:BoundField DataField="Description" HeaderText="Description" 
            SortExpression="Description" />
        <asp:BoundField DataField="SpecificSaveCatalog" HeaderText="SpecificSaveCatalog" 
            SortExpression="SpecificSaveCatalog" />
        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/View/media/delete.png" 
            EditImageUrl="~/View/media/script_edit.png" 
            SelectImageUrl="~/View/media/select.png" ShowDeleteButton="True" 
            ShowEditButton="True" ShowSelectButton="True" />
    </Columns>
    <EmptyDataTemplate>
        No data found
    </EmptyDataTemplate>
</asp:GridView>
</div>
    <asp:ImageButton ID="ImageButton1" runat="server" 
        ImageUrl="~/View/media/bullet.png" onclick="ImageButton1_Click" />
<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" style="text-decoration:none" CssClass="spec_button">Add</asp:LinkButton>
<br />

</asp:Content>

