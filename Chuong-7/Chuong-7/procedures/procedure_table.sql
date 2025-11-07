USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_GetAll')
    DROP PROCEDURE sp_Table_GetAll;
GO

CREATE PROCEDURE sp_Table_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.ID,
        t.TableCode,
        t.Name,
        t.Status,
        t.Seats,
        t.HallID,
        h.Name AS HallName,
        r.ID AS RestaurantID,
        r.Name AS RestaurantName
    FROM [Table] t
    INNER JOIN Hall h ON t.HallID = h.ID
    INNER JOIN Restaurant r ON h.RestaurantID = r.ID
    ORDER BY t.TableCode;
END
GO

-- =============================================
-- Procedure: Lấy Table theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_GetByID')
    DROP PROCEDURE sp_Table_GetByID;
GO

CREATE PROCEDURE sp_Table_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.ID,
        t.TableCode,
        t.Name,
        t.Status,
        t.Seats,
        t.HallID,
        h.Name AS HallName,
        r.ID AS RestaurantID,
        r.Name AS RestaurantName
    FROM [Table] t
    INNER JOIN Hall h ON t.HallID = h.ID
    INNER JOIN Restaurant r ON h.RestaurantID = r.ID
    WHERE t.ID = @ID;
END
GO

-- =============================================
-- Procedure: Lấy Table theo HallID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_GetByHallID')
    DROP PROCEDURE sp_Table_GetByHallID;
GO

CREATE PROCEDURE sp_Table_GetByHallID
    @HallID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.ID,
        t.TableCode,
        t.Name,
        t.Status,
        t.Seats,
        t.HallID,
        h.Name AS HallName
    FROM [Table] t
    INNER JOIN Hall h ON t.HallID = h.ID
    WHERE t.HallID = @HallID
    ORDER BY t.TableCode;
END
GO

-- =============================================
-- Procedure: Lấy Table theo Status
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_GetByStatus')
    DROP PROCEDURE sp_Table_GetByStatus;
GO

CREATE PROCEDURE sp_Table_GetByStatus
    @Status INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.ID,
        t.TableCode,
        t.Name,
        t.Status,
        t.Seats,
        t.HallID,
        h.Name AS HallName
    FROM [Table] t
    INNER JOIN Hall h ON t.HallID = h.ID
    WHERE t.Status = @Status
    ORDER BY t.TableCode;
END
GO

-- =============================================
-- Procedure: Thêm mới Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_Insert')
    DROP PROCEDURE sp_Table_Insert;
GO

CREATE PROCEDURE sp_Table_Insert
    @TableCode NVARCHAR(50),
    @Name NVARCHAR(200) = NULL,
    @Status INT = 0,
    @Seats INT = NULL,
    @HallID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Hall có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Hall WHERE ID = @HallID)
    BEGIN
        RAISERROR(N'Sảnh không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra TableCode đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM [Table] WHERE TableCode = @TableCode AND HallID = @HallID)
    BEGIN
        RAISERROR(N'Mã bàn đã tồn tại trong sảnh này!', 16, 1);
        RETURN;
    END
    
    INSERT INTO [Table] (TableCode, Name, Status, Seats, HallID)
    VALUES (@TableCode, @Name, @Status, @Seats, @HallID);
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_Update')
    DROP PROCEDURE sp_Table_Update;
GO

CREATE PROCEDURE sp_Table_Update
    @ID INT,
    @TableCode NVARCHAR(50),
    @Name NVARCHAR(200) = NULL,
    @Status INT,
    @Seats INT = NULL,
    @HallID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Table có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM [Table] WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Bàn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra Hall có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Hall WHERE ID = @HallID)
    BEGIN
        RAISERROR(N'Sảnh không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra TableCode có trùng không
    IF EXISTS (SELECT 1 FROM [Table] WHERE TableCode = @TableCode AND HallID = @HallID AND ID != @ID)
    BEGIN
        RAISERROR(N'Mã bàn đã tồn tại trong sảnh này!', 16, 1);
        RETURN;
    END
    
    UPDATE [Table]
    SET TableCode = @TableCode,
        Name = @Name,
        Status = @Status,
        Seats = @Seats,
        HallID = @HallID
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_Delete')
    DROP PROCEDURE sp_Table_Delete;
GO

CREATE PROCEDURE sp_Table_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Table có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM [Table] WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Bàn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra Table có đang được sử dụng không
    IF EXISTS (SELECT 1 FROM Invoice WHERE TableID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa bàn đang có hóa đơn!', 16, 1);
        RETURN;
    END
    
    DELETE FROM [Table] WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Cập nhật trạng thái Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Table_UpdateStatus')
    DROP PROCEDURE sp_Table_UpdateStatus;
GO

CREATE PROCEDURE sp_Table_UpdateStatus
    @ID INT,
    @Status INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra Table có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM [Table] WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Bàn không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE [Table]
    SET Status = @Status
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

PRINT N'Tạo các stored procedures quản lý Table thành công!';
GO
