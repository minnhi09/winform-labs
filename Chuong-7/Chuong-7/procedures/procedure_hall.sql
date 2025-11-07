USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả Hall
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Hall_GetAll')
    DROP PROCEDURE sp_Hall_GetAll;
GO

CREATE PROCEDURE sp_Hall_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT h.ID, h.Name, h.RestaurantID, r.Name AS RestaurantName
    FROM Hall h
    INNER JOIN Restaurant r ON h.RestaurantID = r.ID
    ORDER BY h.Name;
END
GO

-- =============================================
-- Procedure: Lấy Hall theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Hall_GetByID')
    DROP PROCEDURE sp_Hall_GetByID;
GO

CREATE PROCEDURE sp_Hall_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT h.ID, h.Name, h.RestaurantID, r.Name AS RestaurantName
    FROM Hall h
    INNER JOIN Restaurant r ON h.RestaurantID = r.ID
    WHERE h.ID = @ID;
END
GO

-- =============================================
-- Procedure: Lấy Hall theo RestaurantID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Hall_GetByRestaurantID')
    DROP PROCEDURE sp_Hall_GetByRestaurantID;
GO

CREATE PROCEDURE sp_Hall_GetByRestaurantID
    @RestaurantID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT h.ID, h.Name, h.RestaurantID, r.Name AS RestaurantName
    FROM Hall h
    INNER JOIN Restaurant r ON h.RestaurantID = r.ID
    WHERE h.RestaurantID = @RestaurantID
    ORDER BY h.Name;
END
GO

-- =============================================
-- Procedure: Thêm mới Hall
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Hall_Insert')
    DROP PROCEDURE sp_Hall_Insert;
GO

CREATE PROCEDURE sp_Hall_Insert
    @Name NVARCHAR(200),
    @RestaurantID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Restaurant có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Restaurant WHERE ID = @RestaurantID)
    BEGIN
        RAISERROR(N'Nhà hàng không tồn tại!', 16, 1);
        RETURN;
    END
    
    INSERT INTO Hall (Name, RestaurantID)
    VALUES (@Name, @RestaurantID);
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật Hall
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Hall_Update')
    DROP PROCEDURE sp_Hall_Update;
GO

CREATE PROCEDURE sp_Hall_Update
    @ID INT,
    @Name NVARCHAR(200),
    @RestaurantID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Hall có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Hall WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Sảnh không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra Restaurant có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Restaurant WHERE ID = @RestaurantID)
    BEGIN
        RAISERROR(N'Nhà hàng không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE Hall
    SET Name = @Name,
        RestaurantID = @RestaurantID
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa Hall
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Hall_Delete')
    DROP PROCEDURE sp_Hall_Delete;
GO

CREATE PROCEDURE sp_Hall_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Hall có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Hall WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Sảnh không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra Hall có đang được sử dụng không
    IF EXISTS (SELECT 1 FROM [Table] WHERE HallID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa sảnh đang có bàn!', 16, 1);
        RETURN;
    END
    
    DELETE FROM Hall WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

PRINT N'Tạo các stored procedures quản lý Hall thành công!';
GO
