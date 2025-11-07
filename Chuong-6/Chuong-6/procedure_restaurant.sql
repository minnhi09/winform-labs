USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả Restaurant
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Restaurant_GetAll')
    DROP PROCEDURE sp_Restaurant_GetAll;
GO

CREATE PROCEDURE sp_Restaurant_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ID, Name, Address, Phone, Website
    FROM Restaurant
    ORDER BY Name;
END
GO

-- =============================================
-- Procedure: Lấy Restaurant theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Restaurant_GetByID')
    DROP PROCEDURE sp_Restaurant_GetByID;
GO

CREATE PROCEDURE sp_Restaurant_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ID, Name, Address, Phone, Website
    FROM Restaurant
    WHERE ID = @ID;
END
GO

-- =============================================
-- Procedure: Thêm mới Restaurant
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Restaurant_Insert')
    DROP PROCEDURE sp_Restaurant_Insert;
GO

CREATE PROCEDURE sp_Restaurant_Insert
    @Name NVARCHAR(200),
    @Address NVARCHAR(500),
    @Phone NVARCHAR(20),
    @Website NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Restaurant (Name, Address, Phone, Website)
    VALUES (@Name, @Address, @Phone, @Website);
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật Restaurant
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Restaurant_Update')
    DROP PROCEDURE sp_Restaurant_Update;
GO

CREATE PROCEDURE sp_Restaurant_Update
    @ID INT,
    @Name NVARCHAR(200),
    @Address NVARCHAR(500),
    @Phone NVARCHAR(20),
    @Website NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Restaurant có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Restaurant WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Nhà hàng không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE Restaurant
    SET Name = @Name,
        Address = @Address,
        Phone = @Phone,
        Website = @Website
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa Restaurant
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Restaurant_Delete')
    DROP PROCEDURE sp_Restaurant_Delete;
GO

CREATE PROCEDURE sp_Restaurant_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Restaurant có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Restaurant WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Nhà hàng không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra Restaurant có đang được sử dụng không
    IF EXISTS (SELECT 1 FROM Hall WHERE RestaurantID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa nhà hàng đang có sảnh!', 16, 1);
        RETURN;
    END
    
    DELETE FROM Restaurant WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

PRINT N'Tạo các stored procedures quản lý Restaurant thành công!';
GO
