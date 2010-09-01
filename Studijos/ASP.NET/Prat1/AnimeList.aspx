<%@ Page Title="My Anime List" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AnimeList.aspx.cs" Inherits="AnimeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>List of my Animes</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:SqlDataSource ID="mainSqlDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:AnimeDBConnectionString %>" 
    DeleteCommand="DELETE FROM [Anime] WHERE [ID] = @ID" 
    InsertCommand="INSERT INTO [Anime] ([Title], [Genre], [AnimeStatus], [FilesStatus], [LocalCopyStatus], [CurrentEpisodeCount], [LastWatchedEpisode], [LastDownloadedEpisode]) VALUES (@Title, @Genre, @AnimeStatus, @FilesStatus, @LocalCopyStatus, @CurrentEpisodeCount, @LastWatchedEpisode, @LastDownloadedEpisode)" 
    SelectCommand="SELECT [ID], [Title], [AnimeStatus], [FilesStatus], [LocalCopyStatus], [CurrentEpisodeCount], [LastWatchedEpisode], [LastDownloadedEpisode] FROM [Anime] WHERE [Genre] = @genre" 
    
        
    
        UpdateCommand="UPDATE [Anime] SET [Title] = @Title, [Genre] = @Genre, [AnimeStatus] = @AnimeStatus, [FilesStatus] = @FilesStatus, [LocalCopyStatus] = @LocalCopyStatus, [CurrentEpisodeCount] = @CurrentEpisodeCount, [LastWatchedEpisode] = @LastWatchedEpisode, [LastDownloadedEpisode] = @LastDownloadedEpisode WHERE [ID] = @ID" 
        FilterExpression="[Title] like '%{0}%'">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="genresDropDownList" Name="genre" 
                PropertyName="SelectedValue" />           
        </SelectParameters>
        <FilterParameters>
            <asp:ControlParameter ControlID="titleTextBox" Name="filter" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="genresDropDownList" Name="genresFilter" 
                PropertyName="SelectedValue" />
        </FilterParameters>
        <UpdateParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Genre" Type="String" />
            <asp:Parameter Name="AnimeStatus" Type="String" />
            <asp:Parameter Name="FilesStatus" Type="String" />
            <asp:Parameter Name="LocalCopyStatus" Type="String" />
            <asp:Parameter Name="CurrentEpisodeCount" Type="Int32" />
            <asp:Parameter Name="LastWatchedEpisode" Type="Int32" />
            <asp:Parameter Name="LastDownloadedEpisode" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Genre" Type="String" />
            <asp:Parameter Name="AnimeStatus" Type="String" />
            <asp:Parameter Name="FilesStatus" Type="String" />
            <asp:Parameter Name="LocalCopyStatus" Type="String" />
            <asp:Parameter Name="CurrentEpisodeCount" Type="Int32" />
            <asp:Parameter Name="LastWatchedEpisode" Type="Int32" />
            <asp:Parameter Name="LastDownloadedEpisode" Type="Int32" />
        </InsertParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="genresDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AnimeDBConnectionString %>" 
        SelectCommand="SELECT [Title] FROM [Genres]"></asp:SqlDataSource>
        <div class="toolbar">
    <asp:Label ID="Label1" runat="server" Text="Items per page:"></asp:Label>
    <asp:TextBox ID="pageSizeTextBox" runat="server">10</asp:TextBox>       
        <span class="seperator"></span>    
    <asp:Label ID="Label2" runat="server" Text="Genre:"></asp:Label>
    <asp:DropDownList ID="genresDropDownList" runat="server" AutoPostBack="True" 
        DataSourceID="genresDataSource" DataTextField="Title" DataValueField="Title">
    </asp:DropDownList>
        <span class="seperator"></span>    
        <asp:Label ID="Label3" runat="server" Text="Title filter:"></asp:Label>
    <asp:TextBox ID="titleTextBox" runat="server" 
    ontextchanged="TextBox1_TextChanged"></asp:TextBox>   
    <span class="seperator"></span>    
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Update" />
    
    </div>
  

<div class="grid_new">
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="dataGrid" style="width:100%"
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
    DataSourceID="mainSqlDataSource" onrowcommand="GridView1_RowCommand">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
            ReadOnly="True" SortExpression="ID" Visible="False" />
        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
        <asp:BoundField DataField="AnimeStatus" HeaderText="Status" 
            SortExpression="AnimeStatus" />
        <asp:BoundField DataField="CurrentEpisodeCount" HeaderText="Episode Count" 
            SortExpression="CurrentEpisodeCount" />
        <asp:BoundField DataField="LastWatchedEpisode" 
            HeaderText="Last Watched Episode" SortExpression="LastWatchedEpisode" />
        <asp:BoundField DataField="LastDownloadedEpisode" 
            HeaderText="Last Downloaded Episode" SortExpression="LastDownloadedEpisode" />
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
            ShowSelectButton="True" ButtonType="Image" 
            DeleteImageUrl="~/View/media/delete.png" 
            EditImageUrl="~/View/media/script_edit.png" 
            SelectImageUrl="~/View/media/select.png" />
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

