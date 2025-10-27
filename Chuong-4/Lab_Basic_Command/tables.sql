USE [RestaurantManagement]
GO

-- Drop tables in reverse order to avoid foreign key constraint errors
IF OBJECT_ID('dbo.BillDetail', 'U') IS NOT NULL DROP TABLE [dbo].[BillDetail]
GO
IF OBJECT_ID('dbo.Bill', 'U') IS NOT NULL DROP TABLE [dbo].[Bill]
GO
IF OBJECT_ID('dbo.AccountRole', 'U') IS NOT NULL DROP TABLE [dbo].[AccountRole]
GO
IF OBJECT_ID('dbo.Role', 'U') IS NOT NULL DROP TABLE [dbo].[Role]
GO
IF OBJECT_ID('dbo.Account', 'U') IS NOT NULL DROP TABLE [dbo].[Account]
GO
IF OBJECT_ID('dbo.Table', 'U') IS NOT NULL DROP TABLE [dbo].[Table]
GO
IF OBJECT_ID('dbo.Food', 'U') IS NOT NULL DROP TABLE [dbo].[Food]
GO
IF OBJECT_ID('dbo.Category', 'U') IS NOT NULL DROP TABLE [dbo].[Category]
GO

CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Type] [int] NOT NULL DEFAULT ((1))
)
GO

CREATE TABLE [dbo].[Food](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Unit] [nvarchar](50) NULL,
	[FoodCategoryID] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Notes] [nvarchar](255) NULL,
	FOREIGN KEY([FoodCategoryID]) REFERENCES [dbo].[Category] ([ID]) ON DELETE CASCADE
)
GO

CREATE TABLE [dbo].[Table](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](50) NOT NULL DEFAULT (N'Trống'),
	[Notes] [nvarchar](255) NULL
)
GO

CREATE TABLE [dbo].[Account](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Username] [nvarchar](50) NOT NULL UNIQUE,
	[Password] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[Notes] [nvarchar](255) NULL
)
GO

CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL
)
GO

CREATE TABLE [dbo].[AccountRole](
	[AccountID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	PRIMARY KEY ([AccountID], [RoleID]),
	FOREIGN KEY([AccountID]) REFERENCES [dbo].[Account] ([ID]) ON DELETE CASCADE,
	FOREIGN KEY([RoleID]) REFERENCES [dbo].[Role] ([ID]) ON DELETE CASCADE
)
GO

CREATE TABLE [dbo].[Bill](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[TableID] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[DateCheckIn] [datetime] NOT NULL DEFAULT (GETDATE()),
	[DateCheckOut] [datetime] NULL,
	[Status] [int] NOT NULL DEFAULT ((0)),
	[TotalAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Discount] [decimal](5, 2) NOT NULL DEFAULT ((0)),
	[Notes] [nvarchar](255) NULL,
	FOREIGN KEY([TableID]) REFERENCES [dbo].[Table] ([ID]),
	FOREIGN KEY([AccountID]) REFERENCES [dbo].[Account] ([ID])
)
GO

CREATE TABLE [dbo].[BillDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[BillID] [int] NOT NULL,
	[FoodID] [int] NOT NULL,
	[Quantity] [int] NOT NULL DEFAULT ((1)),
	[Price] [decimal](18, 2) NOT NULL,
	[Notes] [nvarchar](255) NULL,
	FOREIGN KEY([BillID]) REFERENCES [dbo].[Bill] ([ID]) ON DELETE CASCADE,
	FOREIGN KEY([FoodID]) REFERENCES [dbo].[Food] ([ID])
)
GO
