-- =============================================
-- Script: Create Database and Tables for Chuong_5_Lab_Advanced_Command
-- Description: Database structure for Food Management System
-- Date: October 27, 2025
-- =============================================

-- =============================================
-- DROP existing database if exists (WARNING: This will delete all data!)
-- =============================================
USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Chuong_5_Lab_Advanced_Command')
BEGIN
    -- Disconnect all users from the database
    ALTER DATABASE Chuong_5_Lab_Advanced_Command SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Chuong_5_Lab_Advanced_Command;
    PRINT 'Existing database Chuong_5_Lab_Advanced_Command has been dropped.';
END
GO

-- =============================================
-- Create new database
-- =============================================
CREATE DATABASE Chuong_5_Lab_Advanced_Command;
PRINT 'Database Chuong_5_Lab_Advanced_Command created successfully.';
GO

-- Use the database
USE Chuong_5_Lab_Advanced_Command;
GO

-- =============================================
-- Create Categories Table
-- =============================================
CREATE TABLE Categories
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Notes NVARCHAR(500)
);
PRINT 'Table Categories created successfully.';
GO

-- =============================================
-- Create Foods Table
-- =============================================
CREATE TABLE Foods
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Unit NVARCHAR(50) NOT NULL,
    FoodCategoryID INT NOT NULL,
    Price INT NOT NULL DEFAULT 0,
    Notes NVARCHAR(500),
    CONSTRAINT FK_Foods_Categories FOREIGN KEY (FoodCategoryID) 
        REFERENCES Categories(ID) ON DELETE CASCADE
);
PRINT 'Table Foods created successfully.';
GO

-- =============================================
-- Create Roles Table (Bảng Vai trò)
-- =============================================
CREATE TABLE Roles
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(300)
);
PRINT 'Table Roles created successfully.';
GO

-- =============================================
-- Create Accounts Table (Bảng Tài khoản)
-- =============================================
CREATE TABLE Accounts
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(200) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
PRINT 'Table Accounts created successfully.';
GO

-- =============================================
-- Create AccountRoles Table (Bảng trung gian - Phân quyền)
-- Mối quan hệ Many-to-Many giữa Accounts và Roles
-- =============================================
CREATE TABLE AccountRoles
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    AccountID INT NOT NULL,
    RoleID INT NOT NULL,
    CONSTRAINT FK_AccountRoles_Accounts FOREIGN KEY (AccountID) 
        REFERENCES Accounts(ID) ON DELETE CASCADE,
    CONSTRAINT FK_AccountRoles_Roles FOREIGN KEY (RoleID) 
        REFERENCES Roles(ID) ON DELETE CASCADE,
    CONSTRAINT UQ_AccountRoles UNIQUE(AccountID, RoleID)
);
PRINT 'Table AccountRoles created successfully.';
GO

-- =============================================
-- Create Bills Table (Bảng hóa đơn)
-- =============================================
CREATE TABLE Bills
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    AccountID INT NOT NULL,
    DateCheckIn DATETIME NOT NULL DEFAULT GETDATE(),
    DateCheckOut DATETIME,
    TotalAmount INT NOT NULL DEFAULT 0,
    Discount INT NOT NULL DEFAULT 0,
    FinalAmount INT NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Chưa thanh toán',
    CONSTRAINT FK_Bills_Accounts FOREIGN KEY (AccountID) 
        REFERENCES Accounts(ID)
);
PRINT 'Table Bills created successfully.';
GO

-- =============================================
-- Create BillDetails Table (Bảng chi tiết hóa đơn)
-- =============================================
CREATE TABLE BillDetails
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    BillID INT NOT NULL,
    FoodID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    Price INT NOT NULL,
    TotalPrice INT NOT NULL,
    CONSTRAINT FK_BillDetails_Bills FOREIGN KEY (BillID) 
        REFERENCES Bills(ID) ON DELETE CASCADE,
    CONSTRAINT FK_BillDetails_Foods FOREIGN KEY (FoodID) 
        REFERENCES Foods(ID)
);
PRINT 'Table BillDetails created successfully.';
GO

-- =============================================
-- Display table structures
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'Database and tables created successfully!';
PRINT '========================================';
PRINT '';

SELECT 
    TABLE_NAME as 'Table',
    COLUMN_NAME as 'Column',
    DATA_TYPE as 'Type',
    CHARACTER_MAXIMUM_LENGTH as 'Max Length',
    IS_NULLABLE as 'Nullable'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME IN ('Categories', 'Foods', 'Roles', 'Accounts', 'AccountRoles', 'Bills', 'BillDetails')
ORDER BY TABLE_NAME, ORDINAL_POSITION;
GO
