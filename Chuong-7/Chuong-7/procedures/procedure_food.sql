USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả Food
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Food_GetAll')
    DROP PROCEDURE sp_Food_GetAll;
GO

CREATE PROCEDURE sp_Food_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        f.ID,
        f.Name,
        f.Unit,
        f.FoodCategoryID,
        fc.Name AS CategoryName,
        f.Price,
        f.Notes
    FROM Food f
    INNER JOIN FoodCategory fc ON f.FoodCategoryID = fc.ID
    ORDER BY f.Name;
END
GO

-- =============================================
-- Procedure: Lấy Food theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Food_GetByID')
    DROP PROCEDURE sp_Food_GetByID;
GO

CREATE PROCEDURE sp_Food_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        f.ID,
        f.Name,
        f.Unit,
        f.FoodCategoryID,
        fc.Name AS CategoryName,
        f.Price,
        f.Notes
    FROM Food f
    INNER JOIN FoodCategory fc ON f.FoodCategoryID = fc.ID
    WHERE f.ID = @ID;
END
GO

-- =============================================
-- Procedure: Lấy Food theo CategoryID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Food_GetByCategoryID')
    DROP PROCEDURE sp_Food_GetByCategoryID;
GO

CREATE PROCEDURE sp_Food_GetByCategoryID
    @FoodCategoryID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        f.ID,
        f.Name,
        f.Unit,
        f.FoodCategoryID,
        fc.Name AS CategoryName,
        f.Price,
        f.Notes
    FROM Food f
    INNER JOIN FoodCategory fc ON f.FoodCategoryID = fc.ID
    WHERE f.FoodCategoryID = @FoodCategoryID
    ORDER BY f.Name;
END
GO

-- =============================================
-- Procedure: Thêm mới Food
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Food_Insert')
    DROP PROCEDURE sp_Food_Insert;
GO

CREATE PROCEDURE sp_Food_Insert
    @Name NVARCHAR(200),
    @Unit NVARCHAR(50),
    @FoodCategoryID INT,
    @Price INT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra FoodCategory có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM FoodCategory WHERE ID = @FoodCategoryID)
    BEGIN
        RAISERROR(N'Danh mục món ăn không tồn tại!', 16, 1);
        RETURN;
    END
    
    INSERT INTO Food (Name, Unit, FoodCategoryID, Price, Notes)
    VALUES (@Name, @Unit, @FoodCategoryID, @Price, @Notes);
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật Food
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Food_Update')
    DROP PROCEDURE sp_Food_Update;
GO

CREATE PROCEDURE sp_Food_Update
    @ID INT,
    @Name NVARCHAR(200),
    @Unit NVARCHAR(50),
    @FoodCategoryID INT,
    @Price INT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Food có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Food WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Món ăn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra FoodCategory có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM FoodCategory WHERE ID = @FoodCategoryID)
    BEGIN
        RAISERROR(N'Danh mục món ăn không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE Food
    SET Name = @Name,
        Unit = @Unit,
        FoodCategoryID = @FoodCategoryID,
        Price = @Price,
        Notes = @Notes
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa Food
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Food_Delete')
    DROP PROCEDURE sp_Food_Delete;
GO

CREATE PROCEDURE sp_Food_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Food có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Food WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Món ăn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra Food có đang được sử dụng không
    IF EXISTS (SELECT 1 FROM InvoiceDetails WHERE FoodID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa món ăn đang có trong hóa đơn!', 16, 1);
        RETURN;
    END
    
    DELETE FROM Food WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

PRINT N'Tạo các stored procedures quản lý Food thành công!';
GO
