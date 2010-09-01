<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AnimeForm.ascx.cs" Inherits="AnimeForm" %>
<asp:SqlDataSource ID="animeDataSource" runat="server" 
    ConflictDetection="CompareAllValues" 
    ConnectionString="<%$ ConnectionStrings:AnimeDBConnectionString %>" 
    DeleteCommand="DELETE FROM [Anime] WHERE [ID] = @original_ID AND [Title] = @original_Title AND [Genre] = @original_Genre AND [AnimeStatus] = @original_AnimeStatus AND [FilesStatus] = @original_FilesStatus AND [LocalCopyStatus] = @original_LocalCopyStatus AND [CurrentEpisodeCount] = @original_CurrentEpisodeCount AND [LastWatchedEpisode] = @original_LastWatchedEpisode AND [LastDownloadedEpisode] = @original_LastDownloadedEpisode AND [SaveCatalog] = @original_SaveCatalog" 
    InsertCommand="INSERT INTO Anime(Title, Genre, AnimeStatus, LocalCopyStatus, CurrentEpisodeCount, LastWatchedEpisode, LastDownloadedEpisode, SaveCatalog, FilesStatus) VALUES (@Title, @Genre, @AnimeStatus, 'No local file', @CurrentEpisodeCount, 0, 0, @SaveCatalog, 'Server file')" 
     
    OldValuesParameterFormatString="original_{0}" 
    SelectCommand="SELECT [ID], [Title], [Genre], [AnimeStatus], [FilesStatus], [LocalCopyStatus], [CurrentEpisodeCount], [LastWatchedEpisode], [LastDownloadedEpisode], [SaveCatalog] FROM [Anime] WHERE id = @id" 
    UpdateCommand="UPDATE [Anime] SET [Title] = @Title, [Genre] = @Genre, [AnimeStatus] = @AnimeStatus, [FilesStatus] = @FilesStatus, [LocalCopyStatus] = @LocalCopyStatus, [CurrentEpisodeCount] = @CurrentEpisodeCount, [LastWatchedEpisode] = @LastWatchedEpisode, [LastDownloadedEpisode] = @LastDownloadedEpisode, [SaveCatalog] = @SaveCatalog WHERE [ID] = @original_ID AND [Title] = @original_Title AND [Genre] = @original_Genre AND [AnimeStatus] = @original_AnimeStatus AND [FilesStatus] = @original_FilesStatus AND [LocalCopyStatus] = @original_LocalCopyStatus AND [CurrentEpisodeCount] = @original_CurrentEpisodeCount AND [LastWatchedEpisode] = @original_LastWatchedEpisode AND [LastDownloadedEpisode] = @original_LastDownloadedEpisode AND [SaveCatalog] = @original_SaveCatalog">
    <FilterParameters>
        <asp:ControlParameter ControlID="SelectedIDHiddenField"
            Name="idParam" PropertyName="Value" Type="Int32" />    
    </FilterParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="SelectedIDHiddenField"
            Name="id" PropertyName="Value" Type="Int32" />        
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="original_ID" Type="Int32" />
        <asp:Parameter Name="original_Title" Type="String" />
        <asp:Parameter Name="original_Genre" Type="String" />
        <asp:Parameter Name="original_AnimeStatus" Type="String" />
        <asp:Parameter Name="original_FilesStatus" Type="String" />
        <asp:Parameter Name="original_LocalCopyStatus" Type="String" />
        <asp:Parameter Name="original_CurrentEpisodeCount" Type="Int32" />
        <asp:Parameter Name="original_LastWatchedEpisode" Type="Int32" />
        <asp:Parameter Name="original_LastDownloadedEpisode" Type="Int32" />
        <asp:Parameter Name="original_SaveCatalog" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="Title" Type="String" />
        <asp:Parameter Name="Genre" Type="String" />
        <asp:Parameter Name="AnimeStatus" Type="String" />
        <asp:Parameter Name="FilesStatus" Type="String" />
        <asp:Parameter Name="LocalCopyStatus" Type="String" />
        <asp:Parameter Name="CurrentEpisodeCount" Type="Int32" />
        <asp:Parameter Name="LastWatchedEpisode" Type="Int32" />
        <asp:Parameter Name="LastDownloadedEpisode" Type="Int32" />
        <asp:Parameter Name="SaveCatalog" Type="String" />
        <asp:Parameter Name="original_ID" Type="Int32" />
        <asp:Parameter Name="original_Title" Type="String" />
        <asp:Parameter Name="original_Genre" Type="String" />
        <asp:Parameter Name="original_AnimeStatus" Type="String" />
        <asp:Parameter Name="original_FilesStatus" Type="String" />
        <asp:Parameter Name="original_LocalCopyStatus" Type="String" />
        <asp:Parameter Name="original_CurrentEpisodeCount" Type="Int32" />
        <asp:Parameter Name="original_LastWatchedEpisode" Type="Int32" />
        <asp:Parameter Name="original_LastDownloadedEpisode" Type="Int32" />
        <asp:Parameter Name="original_SaveCatalog" Type="String" />
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
        <asp:Parameter Name="SaveCatalog" Type="String" />
    </InsertParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="genresDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:AnimeDBConnectionString %>" 
    SelectCommand="SELECT [Title] FROM [Genres]"></asp:SqlDataSource>
<asp:FormView ID="FormView1" runat="server" DataKeyNames="ID"  CssClass="gridForm"
    DataSourceID="animeDataSource" onitemdeleted="FormView1_ItemDeleted" 
    oniteminserted="FormView1_ItemInserted" 
    onitemupdated="FormView1_ItemUpdated" onitemcommand="FormView1_ItemCommand">
    <EditItemTemplate>
    <div class="toolbar">
        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
            CommandName="Update" Text="Update" style="display:inline;float:left;" CssClass="spec_button"/>
            <span class="seperator" style="display:inline;float:left;"></span>            
        <asp:LinkButton ID="UpdateCancelButton" runat="server" 
            CausesValidation="False" CommandName="Cancel" Text="Cancel" style="display:inline;float:left;" CssClass="spec_button"/>
    <span class="seperator" style="display:inline;float:left;"></span>            
    </div>
    <p>
        <label>Title:</label>
        <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
        </p><p>
        <label>Genre:</label>
        <asp:DropDownList ID="DropDownList1" runat="server" 
            DataSourceID="genresDataSource" DataTextField="Title" DataValueField="Title" 
            SelectedValue='<%# Bind("Genre", "{0}") %>'>
        </asp:DropDownList>
        </p><p>
        <label>Anime Status:</label>
        <asp:DropDownList ID="DropDownList2" runat="server" 
            SelectedValue='<%# Bind("AnimeStatus", "{0}") %>'>
            <asp:ListItem>Ongoing</asp:ListItem>
            <asp:ListItem>Complete</asp:ListItem>
        </asp:DropDownList>
        </p>
        <p>
        <label>Files Status:</label>
        <asp:DropDownList ID="DropDownList3" runat="server" 
            SelectedValue='<%# Bind("FilesStatus", "{0}") %>'>
            <asp:ListItem>Server file</asp:ListItem>
            <asp:ListItem>Local file</asp:ListItem>
        </asp:DropDownList>
        </p>
        <p>
        <label>Local Copy Status:</label>
        <asp:DropDownList ID="DropDownList4" runat="server" 
            SelectedValue='<%# Bind("LocalCopyStatus", "{0}") %>'>
            <asp:ListItem>No local file</asp:ListItem>
            <asp:ListItem>Only downloaded</asp:ListItem>
            <asp:ListItem>Watching</asp:ListItem>
            <asp:ListItem>Watched</asp:ListItem>
        </asp:DropDownList>
        </p>
        <p>
        <label>Episode Count:</label>
        <asp:TextBox ID="CurrentEpisodeCountTextBox" runat="server" 
            Text='<%# Bind("CurrentEpisodeCount") %>' />
        </p>
        <p>
        <label>Last Watched Episode:</label>
        <asp:TextBox ID="LastWatchedEpisodeTextBox" runat="server" 
            Text='<%# Bind("LastWatchedEpisode") %>' />
        </p>
        <p>
        <label>Last Downloaded Episode:</label>
        <asp:TextBox ID="LastDownloadedEpisodeTextBox" runat="server" 
            Text='<%# Bind("LastDownloadedEpisode") %>' />
        </p>
        <p>
        <label>SaveCatalog:</label>
        <asp:TextBox ID="SaveCatalogTextBox" runat="server" 
            Text='<%# Bind("SaveCatalog") %>' />
        </p>
    </EditItemTemplate>
    <InsertItemTemplate>
<div class="toolbar">
        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CssClass="spec_button" style="display:inline;float:left;"
            CommandName="Insert" Text="Insert" />
<span class="seperator" style="display:inline;float:left;"></span>            
<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CssClass="spec_button" style="display:inline;float:left;"
            CommandName="Cancel" Text="Cancel" />
<span class="seperator" style="display:inline;float:left;"></span>
</div>    
<p>
        <label>Title:</label>
        <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
        </p>
        <p>
        <label>Genre:</label>
        <asp:DropDownList ID="DropDownList5" runat="server" 
            DataSourceID="genresDataSource" DataTextField="Title" DataValueField="Title" 
            SelectedValue='<%# Bind("Genre", "{0}") %>'>
        </asp:DropDownList>
        </p>
        <p>
        <label>AnimeStatus:</label>
        <asp:DropDownList ID="DropDownList2" runat="server" 
            SelectedValue='<%# Bind("AnimeStatus", "{0}") %>'>
            <asp:ListItem>Ongoing</asp:ListItem>
            <asp:ListItem>Complete</asp:ListItem>
        </asp:DropDownList>
        </p>
        <p>
        <label>Episode Count:</label>
        <asp:TextBox ID="CurrentEpisodeCountTextBox" runat="server" 
            Text='<%# Bind("CurrentEpisodeCount") %>' />
        </p>
        <p>
        <label>Save Catalog:</label>
        
        <asp:TextBox ID="SaveCatalogTextBox" runat="server" 
            Text='<%# Bind("SaveCatalog") %>' />
        </p>
    </InsertItemTemplate>
    <ItemTemplate>
    <div class="toolbar">    
            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False"  CssClass="spec_button" style="display:inline;float:left;"
            CommandName="New" Text="New" />
            <span class="seperator" style="display:inline;float:left;"></span>
        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CssClass="spec_button"
            CommandName="Edit" Text="Edit" style="display:inline;float:left;"/>
            <span class="seperator" style="display:inline;float:left"></span>
<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False"  CssClass="spec_button" style="display:inline;float:left;"
            CommandName="Delete" Text="Delete" />
            <span class="seperator" style="display:inline;float:left;"></span>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"  CssClass="spec_button" style="display:inline;float:left;">Back</asp:LinkButton>
    <span class="seperator" style="display:inline;float:left;"></span>
    </div>
     <p>
        <label>Title:</label>
        <asp:Label ID="TitleLabel" runat="server" Text='<%# Bind("Title") %>' />
        </p>
        <p>
        <label>Genre:</label>
        <asp:Label ID="GenreLabel" runat="server" Text='<%# Bind("Genre") %>' />
        </p>
        <p>
        <label>Anime Status:</label>
        <asp:Label ID="AnimeStatusLabel" runat="server" 
            Text='<%# Bind("AnimeStatus") %>' />
        </p>
        <p>
        <label>Files Status:</label>
        <asp:Label ID="FilesStatusLabel" runat="server" 
            Text='<%# Bind("FilesStatus") %>' />
        </p>
        <p>
        <label>Local Copy Status:</label>
        <asp:Label ID="LocalCopyStatusLabel" runat="server" 
            Text='<%# Bind("LocalCopyStatus") %>' />
        </p>
        <p>
        <label>Current Episode Count:</label>
        <asp:Label ID="CurrentEpisodeCountLabel" runat="server" 
            Text='<%# Bind("CurrentEpisodeCount") %>' />
        </p>
        <p>
        <label>Last Watched Episode:</label>
        <asp:Label ID="LastWatchedEpisodeLabel" runat="server" 
            Text='<%# Bind("LastWatchedEpisode") %>' />
        </p>
        <p>
        <label>Last Downloaded Episode:</label>
        <asp:Label ID="LastDownloadedEpisodeLabel" runat="server" 
            Text='<%# Bind("LastDownloadedEpisode") %>' />
        </p>
        <p>
        <label>Save Catalog:</label>
        <asp:Label ID="SaveCatalogLabel" runat="server" 
            Text='<%# Bind("SaveCatalog") %>' />
        </p>
    </ItemTemplate>
</asp:FormView>
<asp:HiddenField ID="SelectedIDHiddenField" runat="server" />

