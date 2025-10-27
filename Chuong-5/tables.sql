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
WHERE TABLE_NAME IN ('Categories', 'Foods')
ORDER BY TABLE_NAME, ORDINAL_POSITION;
GO
