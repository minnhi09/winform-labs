USE [RestaurantManagement]
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
