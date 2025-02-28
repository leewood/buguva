SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Genres](
	[Title] [varchar](250) NOT NULL,
	[Description] [varchar](max) NULL,
	[SpecificSaveCatalog] [varchar](max) NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[Title] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Anime]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Anime](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Genre] [varchar](250) NOT NULL,
	[AnimeStatus] [varchar](250) NOT NULL,
	[FilesStatus] [varchar](250) NOT NULL,
	[LocalCopyStatus] [varchar](250) NOT NULL,
	[CurrentEpisodeCount] [int] NOT NULL,
	[LastWatchedEpisode] [int] NOT NULL,
	[LastDownloadedEpisode] [int] NOT NULL,
	[SaveCatalog] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Anime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Anime_Genres]') AND parent_object_id = OBJECT_ID(N'[dbo].[Anime]'))
ALTER TABLE [dbo].[Anime]  WITH CHECK ADD  CONSTRAINT [FK_Anime_Genres] FOREIGN KEY([Genre])
REFERENCES [dbo].[Genres] ([Title])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Anime] CHECK CONSTRAINT [FK_Anime_Genres]
