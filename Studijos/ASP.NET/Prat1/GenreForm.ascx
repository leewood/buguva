<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GenreForm.ascx.cs" Inherits="GenreForm"%>
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
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConflictDetection="CompareAllValues" 
    ConnectionString="<%$ ConnectionStrings:AnimeDBConnectionString %>" 
    DeleteCommand="DELETE FROM [Genres] WHERE [Title] = @original_Title AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([SpecificSaveCatalog] = @original_SpecificSaveCatalog) OR ([SpecificSaveCatalog] IS NULL AND @original_SpecificSaveCatalog IS NULL))" 
    InsertCommand="INSERT INTO [Genres] ([Title], [Description], [SpecificSaveCatalog]) VALUES (@Title, @Description, @SpecificSaveCatalog)" 
    OldValuesParameterFormatString="original_{0}" 
    SelectCommand="SELECT [Title], [Description], [SpecificSaveCatalog] FROM [Genres] WHERE Title = @id" 
    UpdateCommand="UPDATE [Genres] SET [Description] = @Description, [SpecificSaveCatalog] = @SpecificSaveCatalog WHERE [Title] = @original_Title AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([SpecificSaveCatalog] = @original_SpecificSaveCatalog) OR ([SpecificSaveCatalog] IS NULL AND @original_SpecificSaveCatalog IS NULL))">
    <DeleteParameters>
        <asp:Parameter Name="original_Title" Type="String" />
        <asp:Parameter Name="original_Description" Type="String" />
        <asp:Parameter Name="original_SpecificSaveCatalog" Type="String" />
    </DeleteParameters>
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
        <SelectParameters>
        <asp:ControlParameter ControlID="SelectedIDHiddenField"
            Name="id" PropertyName="Value" Type="String" />        
    </SelectParameters>

</asp:SqlDataSource>
<asp:FormView ID="FormView1" runat="server" DataKeyNames="Title"  CssClass="gridForm"
    DataSourceID="SqlDataSource1" onitemdeleted="FormView1_ItemDeleted" 
    oniteminserted="FormView1_ItemInserted" 
    onitemupdated="FormView1_ItemUpdated" 
    onitemcommand="FormView1_ItemCommand">
    <EditItemTemplate>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  CssClass="ErrorsBar"/>
    <div class="toolbar">
        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
            CommandName="Update" Text="Update" style="display:inline;float:left;" CssClass="spec_button"/>
            <span class="seperator" style="display:inline;float:left">
        
        </span>
            <span class="seperator" style="display:inline;float:left;"></span>            
        <asp:LinkButton ID="UpdateCancelButton" runat="server" 
            CausesValidation="False" CommandName="Cancel" Text="Cancel" style="display:inline;float:left;" CssClass="spec_button"/>
    </div>
    <p>
        <label>Title:</label>
        <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TitleTextBox" ErrorMessage="Title is a must" Visible="False"></asp:RequiredFieldValidator>
        </p><p>
        <label>Description:</label>
        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Description") %>' />
        </p><p>
        <label>Save Catalog:</label>
        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SpecificSaveCatalog") %>' />
        </p>
    </EditItemTemplate>
    <InsertItemTemplate>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"  CssClass="ErrorsBar"/>
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TitleTextBox" ErrorMessage="Title is a must" Visible="False"></asp:RequiredFieldValidator>

        </p>
        <p>
        <label>Descrption:</label>
        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Description") %>' />
        </p>
        <p>
        <label>Save Catalog:</label>
        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("SpecificSaveCatalog") %>' />
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
        <label>Description:</label>
        <asp:Label ID="GenreLabel" runat="server" Text='<%# Bind("Description") %>' />
        </p>
        <p>
        <label>Save Catalog:</label>
        <asp:Label ID="AnimeStatusLabel" runat="server" 
            Text='<%# Bind("SpecificSaveCatalog") %>' />
        </p>
    </ItemTemplate>
</asp:FormView>
<asp:HiddenField ID="SelectedIDHiddenField" runat="server" />

