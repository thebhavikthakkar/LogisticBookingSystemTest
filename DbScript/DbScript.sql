USE [master]
GO
/****** Object:  Database [LogisticsBookingSystem]    Script Date: 15-07-2023 18:56:20 ******/
CREATE DATABASE [LogisticsBookingSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LogisticsBookingSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LogisticsBookingSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LogisticsBookingSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LogisticsBookingSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LogisticsBookingSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LogisticsBookingSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LogisticsBookingSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LogisticsBookingSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LogisticsBookingSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LogisticsBookingSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LogisticsBookingSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LogisticsBookingSystem] SET  MULTI_USER 
GO
ALTER DATABASE [LogisticsBookingSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LogisticsBookingSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LogisticsBookingSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LogisticsBookingSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LogisticsBookingSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LogisticsBookingSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LogisticsBookingSystem] SET QUERY_STORE = OFF
GO
USE [LogisticsBookingSystem]
GO
/****** Object:  User [Phoenix]    Script Date: 15-07-2023 18:56:22 ******/
CREATE USER [Phoenix] FOR LOGIN [Phoenix] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15-07-2023 18:56:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 15-07-2023 18:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Goods] [nvarchar](max) NOT NULL,
	[LocationId] [uniqueidentifier] NOT NULL,
	[Carrier] [nvarchar](max) NOT NULL,
	[State] [int] NOT NULL,
	[Time] [time](7) NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 15-07-2023 18:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Capacity] [int] NOT NULL,
	[StartDate] [time](7) NULL,
	[EndDate] [time](7) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230714042016_InitialCreate', N'7.0.9')
GO
INSERT [dbo].[Booking] ([Id], [Date], [Goods], [LocationId], [Carrier], [State], [Time]) VALUES (N'79018f46-e641-4a01-b9b7-08db84eb16ce', CAST(N'2023-07-15T00:00:00.0000000' AS DateTime2), N'updTE', N'0295f7a3-a760-452e-bacb-eb0b1ba5f2a2', N'string', 4, CAST(N'00:00:00' AS Time))
GO
INSERT [dbo].[Booking] ([Id], [Date], [Goods], [LocationId], [Carrier], [State], [Time]) VALUES (N'08cdb17d-d600-4d21-22d5-08db84f299e9', CAST(N'2023-07-15T00:00:00.0000000' AS DateTime2), N'New BTS', N'0295f7a3-a760-452e-bacb-eb0b1ba5f2a2', N'SEVEN', 0, CAST(N'00:00:00' AS Time))
GO
INSERT [dbo].[Location] ([Id], [Name], [Address], [Capacity], [StartDate], [EndDate]) VALUES (N'0295f7a3-a760-452e-bacb-eb0b1ba5f2a2', N'Changed name', N'changed address', 10, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time))
GO
/****** Object:  Index [IX_Booking_LocationId]    Script Date: 15-07-2023 18:56:25 ******/
CREATE NONCLUSTERED INDEX [IX_Booking_LocationId] ON [dbo].[Booking]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Location_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Location_LocationId]
GO
USE [master]
GO
ALTER DATABASE [LogisticsBookingSystem] SET  READ_WRITE 
GO
