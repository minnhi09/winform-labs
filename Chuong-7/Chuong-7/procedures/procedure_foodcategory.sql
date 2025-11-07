USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả FoodCategory
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_FoodCategory_GetAll')
    DROP PROCEDURE sp_FoodCategory_GetAll;
GO

CREATE PROCEDURE sp_FoodCategory_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ID, Name, Type
    FROM FoodCategory
    ORDER BY Type, Name;
END
GO

-- =============================================
-- Procedure: Lấy FoodCategory theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_FoodCategory_GetByID')
    DROP PROCEDURE sp_FoodCategory_GetByID;
GO

CREATE PROCEDURE sp_FoodCategory_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ID, Name, Type
    FROM FoodCategory
    WHERE ID = @ID;
END
GO

-- =============================================
-- Procedure: Thêm mới FoodCategory
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_FoodCategory_Insert')
    DROP PROCEDURE sp_FoodCategory_Insert;
GO

CREATE PROCEDURE sp_FoodCategory_Insert
    @Name NVARCHAR(200),
    @Type INT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO FoodCategory (Name, Type)
    VALUES (@Name, @Type);
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật FoodCategory
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_FoodCategory_Update')
    DROP PROCEDURE sp_FoodCategory_Update;
GO

CREATE PROCEDURE sp_FoodCategory_Update
    @ID INT,
    @Name NVARCHAR(200),
    @Type INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM FoodCategory WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Danh mục không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE FoodCategory
    SET Name = @Name, Type = @Type
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa FoodCategory
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_FoodCategory_Delete')
    DROP PROCEDURE sp_FoodCategory_Delete;
GO

CREATE PROCEDURE sp_FoodCategory_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM FoodCategory WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Danh mục không tồn tại!', 16, 1);
        RETURN;
    END
    
    IF EXISTS (SELECT 1 FROM Food WHERE FoodCategoryID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa danh mục đang có món ăn!', 16, 1);
        RETURN;
    END
    
    DELETE FROM FoodCategory WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

PRINT N'Tạo các stored procedures quản lý FoodCategory thành công!';
GO
