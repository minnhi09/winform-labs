-- =============================================
-- Script: Stored Procedures for Categories Management
-- Description: CRUD operations for Categories table
-- Date: November 2, 2025
-- =============================================

USE Chuong_5_Lab_Advanced_Command;
GO

/*
Thêm stored procedure để chèn nhóm món ăn mới vào bảng Categories
*/
IF OBJECT_ID('InsertCategory', 'P') IS NOT NULL
    DROP PROCEDURE [InsertCategory]
GO

CREATE PROCEDURE [InsertCategory]
    @ID int output,
    @Name nvarchar(200),
    @Notes nvarchar(500)
AS
BEGIN
    -- Kiểm tra tên nhóm món ăn đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Categories WHERE Name = @Name)
    BEGIN
        RAISERROR(N'Tên nhóm món ăn đã tồn tại!', 16, 1);
        RETURN -1;
    END

    -- Thêm nhóm món ăn mới
    INSERT INTO Categories (Name, Notes)
    VALUES (@Name, @Notes);

    -- Lấy ID của nhóm món ăn vừa thêm
    SELECT @ID = SCOPE_IDENTITY();
    
    RETURN 1;
END
GO

/*
Thêm stored procedure để cập nhật thông tin nhóm món ăn trong bảng Categories
*/
IF OBJECT_ID('UpdateCategory', 'P') IS NOT NULL
    DROP PROCEDURE [UpdateCategory]
GO

CREATE PROCEDURE [UpdateCategory]
    @ID int,
    @Name nvarchar(200),
    @Notes nvarchar(500)
AS
BEGIN
    -- Kiểm tra nhóm món ăn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Categories WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy nhóm món ăn cần cập nhật!', 16, 1);
        RETURN -1;
    END

    -- Kiểm tra tên nhóm món ăn đã tồn tại ở nhóm khác chưa
    IF EXISTS (SELECT 1 FROM Categories WHERE Name = @Name AND ID != @ID)
    BEGIN
        RAISERROR(N'Tên nhóm món ăn đã tồn tại!', 16, 1);
        RETURN -1;
    END

    -- Cập nhật thông tin
    UPDATE Categories
    SET Name = @Name,
        Notes = @Notes
    WHERE ID = @ID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để xóa nhóm món ăn theo ID
*/
IF OBJECT_ID('DeleteCategory', 'P') IS NOT NULL
    DROP PROCEDURE [DeleteCategory]
GO

CREATE PROCEDURE [DeleteCategory]
    @ID int
AS
BEGIN
    -- Kiểm tra nhóm món ăn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Categories WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy nhóm món ăn cần xóa!', 16, 1);
        RETURN -1;
    END

    -- Kiểm tra nhóm món ăn có món ăn nào không
    IF EXISTS (SELECT 1 FROM Foods WHERE FoodCategoryID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa nhóm món ăn vì còn món ăn thuộc nhóm này!', 16, 1);
        RETURN -1;
    END

    -- Xóa nhóm món ăn
    DELETE FROM Categories
    WHERE ID = @ID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để lấy thông tin nhóm món ăn theo ID
*/
IF OBJECT_ID('GetCategoryByID', 'P') IS NOT NULL
    DROP PROCEDURE [GetCategoryByID]
GO

CREATE PROCEDURE [GetCategoryByID]
    @ID int
AS
BEGIN
    SELECT ID, Name, Notes
    FROM Categories
    WHERE ID = @ID;
END
GO

/*
Thêm stored procedure để lấy danh sách tất cả nhóm món ăn
*/
IF OBJECT_ID('GetAllCategories', 'P') IS NOT NULL
    DROP PROCEDURE [GetAllCategories]
GO

CREATE PROCEDURE [GetAllCategories]
AS
BEGIN
    SELECT ID, Name, Notes
    FROM Categories
    ORDER BY Name;
END
GO

/*
Thêm stored procedure để lấy danh sách nhóm món ăn kèm số lượng món ăn
*/
IF OBJECT_ID('GetCategoriesWithFoodCount', 'P') IS NOT NULL
    DROP PROCEDURE [GetCategoriesWithFoodCount]
GO

CREATE PROCEDURE [GetCategoriesWithFoodCount]
AS
BEGIN
    SELECT 
        C.ID,
        C.Name,
        C.Notes,
        COUNT(F.ID) AS FoodCount
    FROM Categories C
    LEFT JOIN Foods F ON C.ID = F.FoodCategoryID
    GROUP BY C.ID, C.Name, C.Notes
    ORDER BY C.Name;
END
GO

PRINT '========================================';
PRINT 'Stored procedures for Categories created successfully!';
PRINT '========================================';
