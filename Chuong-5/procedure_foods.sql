-- =============================================
-- Script: Stored Procedures for Foods Management
-- Description: CRUD operations for Foods table
-- Date: October 27, 2025
-- =============================================

USE Chuong_5_Lab_Advanced_Command;
GO

/*
Thêm stored procedure để chèn món ăn mới vào bảng Foods
*/
IF OBJECT_ID('InsertFood', 'P') IS NOT NULL
    DROP PROCEDURE [InsertFood]
GO

CREATE PROCEDURE [InsertFood]
    @ID int output,
    @Name nvarchar(1000),
    @Unit nvarchar(100),
    @FoodCategoryID int,
    @Price int,
    @Notes nvarchar(3000)
AS
BEGIN
    INSERT INTO Foods (Name, Unit, FoodCategoryID, Price, Notes)
    VALUES (@Name, @Unit, @FoodCategoryID, @Price, @Notes);

    SELECT @ID = SCOPE_IDENTITY();
END
GO

/*
Thêm stored procedure để cập nhật thông tin món ăn trong bảng Foods
*/
IF OBJECT_ID('UpdateFood', 'P') IS NOT NULL
    DROP PROCEDURE [UpdateFood]
GO

CREATE PROCEDURE [UpdateFood]
    @ID int,
    @Name nvarchar(1000),
    @Unit nvarchar(100),
    @FoodCategoryID int,
    @Price int,
    @Notes nvarchar(3000)
AS
BEGIN
    UPDATE Foods
    SET Name = @Name,
        Unit = @Unit,
        FoodCategoryID = @FoodCategoryID,
        Price = @Price,
        Notes = @Notes
    WHERE ID = @ID;

    -- Sử dụng @@ROWCOUNT thay vì @@ERROR để kiểm tra chính xác hơn
    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để xóa món ăn theo ID
*/
IF OBJECT_ID('DeleteFood', 'P') IS NOT NULL
    DROP PROCEDURE [DeleteFood]
GO

CREATE PROCEDURE [DeleteFood]
    @ID int
AS
BEGIN
    DELETE FROM Foods
    WHERE ID = @ID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để lấy thông tin món ăn theo ID
*/
IF OBJECT_ID('GetFoodByID', 'P') IS NOT NULL
    DROP PROCEDURE [GetFoodByID]
GO

CREATE PROCEDURE [GetFoodByID]
    @ID int
AS
BEGIN
    SELECT ID, Name, Unit, FoodCategoryID, Price, Notes
    FROM Foods
    WHERE ID = @ID;
END
GO

/*
Thêm stored procedure để lấy danh sách tất cả món ăn
*/
IF OBJECT_ID('GetAllFoods', 'P') IS NOT NULL
    DROP PROCEDURE [GetAllFoods]
GO

CREATE PROCEDURE [GetAllFoods]
AS
BEGIN
    SELECT ID, Name, Unit, FoodCategoryID, Price, Notes
    FROM Foods
    ORDER BY Name;
END
GO