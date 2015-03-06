USE [master]
GO
/****** Object:  Database [MovieFinder]    Script Date: 04/07/2014 17:14:45 ******/
CREATE DATABASE [MovieFinder] ON  PRIMARY 
( NAME = N'MovieFinder', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\MovieFinder.mdf' , SIZE = 51200KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MovieFinder_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\MovieFinder_log.ldf' , SIZE = 12288KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MovieFinder] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MovieFinder].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MovieFinder] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MovieFinder] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MovieFinder] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MovieFinder] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MovieFinder] SET ARITHABORT OFF
GO
ALTER DATABASE [MovieFinder] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MovieFinder] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [MovieFinder] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MovieFinder] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MovieFinder] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MovieFinder] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MovieFinder] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MovieFinder] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MovieFinder] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MovieFinder] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MovieFinder] SET  DISABLE_BROKER
GO
ALTER DATABASE [MovieFinder] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MovieFinder] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MovieFinder] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MovieFinder] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MovieFinder] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MovieFinder] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MovieFinder] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [MovieFinder] SET  READ_WRITE
GO
ALTER DATABASE [MovieFinder] SET RECOVERY FULL
GO
ALTER DATABASE [MovieFinder] SET  MULTI_USER
GO
ALTER DATABASE [MovieFinder] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MovieFinder] SET DB_CHAINING OFF
GO
USE [MovieFinder]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 04/07/2014 17:14:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Movie](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[ImageUrl] [varchar](500) NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[LanguageCode] [char](2) NOT NULL,
	[Description] [varchar](1000) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[Version] [int] NOT NULL,
	[VersionChange] [int] NOT NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MovieLink]    Script Date: 04/07/2014 17:14:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MovieLink](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MovieID] [int] NOT NULL,
	[MovieTitle] [varchar](100) NOT NULL,
	[SiteTitle] [varchar](50) NOT NULL,
	[Url] [varchar](500) NOT NULL,
	[SiteID] [varchar](10) NOT NULL,
 CONSTRAINT [PK_MovieLink] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MovieLink_Url] ON [dbo].[MovieLink] 
(
	[Url] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Default [DF_Movie_CreateDate]    Script Date: 04/07/2014 17:14:47 ******/
ALTER TABLE [dbo].[Movie] ADD  CONSTRAINT [DF_Movie_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_Movie_Version]    Script Date: 04/07/2014 17:14:47 ******/
ALTER TABLE [dbo].[Movie] ADD  CONSTRAINT [DF_Movie_Version]  DEFAULT ((1)) FOR [Version]
GO
/****** Object:  Default [DF_Movie_VersionChange]    Script Date: 04/07/2014 17:14:47 ******/
ALTER TABLE [dbo].[Movie] ADD  CONSTRAINT [DF_Movie_VersionChange]  DEFAULT ((0)) FOR [VersionChange]
GO
/****** Object:  ForeignKey [FK_MovieLink_Movie]    Script Date: 04/07/2014 17:14:47 ******/
ALTER TABLE [dbo].[MovieLink]  WITH CHECK ADD  CONSTRAINT [FK_MovieLink_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieLink] CHECK CONSTRAINT [FK_MovieLink_Movie]
GO
