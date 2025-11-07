USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả Role
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_GetAll')
    DROP PROCEDURE sp_Role_GetAll;
GO

CREATE PROCEDURE sp_Role_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ID,
        RoleName,
        Path,
        Notes,
        (SELECT COUNT(*) FROM RoleAccount WHERE RoleID = Role.ID AND Actived = 1) AS ActiveUserCount
    FROM Role
    ORDER BY RoleName;
END
GO

-- =============================================
-- Procedure: Lấy Role theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_GetByID')
    DROP PROCEDURE sp_Role_GetByID;
GO

CREATE PROCEDURE sp_Role_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ID, RoleName, Path, Notes
    FROM Role
    WHERE ID = @ID;
END
GO

-- =============================================
-- Procedure: Thêm mới Role
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_Insert')
    DROP PROCEDURE sp_Role_Insert;
GO

CREATE PROCEDURE sp_Role_Insert
    @RoleName NVARCHAR(100),
    @Path NVARCHAR(500) = NULL,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tên vai trò đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Role WHERE RoleName = @RoleName)
    BEGIN
        RAISERROR(N'Tên vai trò đã tồn tại!', 16, 1);
        RETURN;
    END
    
    INSERT INTO Role (RoleName, Path, Notes)
    VALUES (@RoleName, @Path, @Notes);
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật Role
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_Update')
    DROP PROCEDURE sp_Role_Update;
GO

CREATE PROCEDURE sp_Role_Update
    @ID INT,
    @RoleName NVARCHAR(100),
    @Path NVARCHAR(500) = NULL,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra vai trò có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Role WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Vai trò không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra tên vai trò mới có trùng với vai trò khác không
    IF EXISTS (SELECT 1 FROM Role WHERE RoleName = @RoleName AND ID != @ID)
    BEGIN
        RAISERROR(N'Tên vai trò đã tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE Role
    SET RoleName = @RoleName,
        Path = @Path,
        Notes = @Notes
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa Role
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_Delete')
    DROP PROCEDURE sp_Role_Delete;
GO

CREATE PROCEDURE sp_Role_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra vai trò có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Role WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Vai trò không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra vai trò có đang được gán cho tài khoản nào không
    IF EXISTS (SELECT 1 FROM RoleAccount WHERE RoleID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa vai trò đang được gán cho tài khoản!', 16, 1);
        RETURN;
    END
    
    DELETE FROM Role WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Tìm kiếm Role theo tên
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_SearchByName')
    DROP PROCEDURE sp_Role_SearchByName;
GO

CREATE PROCEDURE sp_Role_SearchByName
    @SearchTerm NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ID,
        RoleName,
        Path,
        Notes,
        (SELECT COUNT(*) FROM RoleAccount WHERE RoleID = Role.ID AND Actived = 1) AS ActiveUserCount
    FROM Role
    WHERE RoleName LIKE N'%' + @SearchTerm + '%'
    ORDER BY RoleName;
END
GO

-- =============================================
-- Procedure: Lấy thống kê Role
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_GetStatistics')
    DROP PROCEDURE sp_Role_GetStatistics;
GO

CREATE PROCEDURE sp_Role_GetStatistics
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        r.ID,
        r.RoleName,
        COUNT(CASE WHEN ra.Actived = 1 THEN 1 END) AS ActiveAccounts,
        COUNT(CASE WHEN ra.Actived = 0 THEN 1 END) AS InactiveAccounts,
        COUNT(ra.AccountName) AS TotalAccounts
    FROM Role r
    LEFT JOIN RoleAccount ra ON r.ID = ra.RoleID
    GROUP BY r.ID, r.RoleName
    ORDER BY r.RoleName;
END
GO

-- =============================================
-- Procedure: Lấy danh sách Account theo Role
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Role_GetAccounts')
    DROP PROCEDURE sp_Role_GetAccounts;
GO

CREATE PROCEDURE sp_Role_GetAccounts
    @RoleID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.AccountName,
        a.FullName,
        a.Email,
        a.Phone,
        ra.Actived,
        ra.Notes
    FROM Account a
    INNER JOIN RoleAccount ra ON a.AccountName = ra.AccountName
    WHERE ra.RoleID = @RoleID
    ORDER BY a.FullName;
END
GO

-- =============================================
-- Procedure: Gán Role cho Account
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_RoleAccount_Assign')
    DROP PROCEDURE sp_RoleAccount_Assign;
GO

CREATE PROCEDURE sp_RoleAccount_Assign
    @AccountName NVARCHAR(50),
    @RoleID INT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Account WHERE AccountName = @AccountName)
    BEGIN
        RAISERROR(N'Tài khoản không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra vai trò có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Role WHERE ID = @RoleID)
    BEGIN
        RAISERROR(N'Vai trò không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra đã gán vai trò này chưa
    IF EXISTS (SELECT 1 FROM RoleAccount WHERE AccountName = @AccountName AND RoleID = @RoleID)
    BEGIN
        UPDATE RoleAccount
        SET Actived = 1, Notes = @Notes
        WHERE AccountName = @AccountName AND RoleID = @RoleID;
        
        SELECT 1 AS Result, N'Kích hoạt lại vai trò thành công!' AS Message;
    END
    ELSE
    BEGIN
        INSERT INTO RoleAccount (AccountName, RoleID, Actived, Notes)
        VALUES (@AccountName, @RoleID, 1, @Notes);
        
        SELECT 1 AS Result, N'Gán vai trò thành công!' AS Message;
    END
END
GO

-- =============================================
-- Procedure: Xóa Role khỏi Account
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_RoleAccount_Remove')
    DROP PROCEDURE sp_RoleAccount_Remove;
GO

CREATE PROCEDURE sp_RoleAccount_Remove
    @AccountName NVARCHAR(50),
    @RoleID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM RoleAccount WHERE AccountName = @AccountName AND RoleID = @RoleID)
    BEGIN
        RAISERROR(N'Phân quyền không tồn tại!', 16, 1);
        RETURN;
    END
    
    DELETE FROM RoleAccount 
    WHERE AccountName = @AccountName AND RoleID = @RoleID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Cập nhật trạng thái RoleAccount
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_RoleAccount_ToggleStatus')
    DROP PROCEDURE sp_RoleAccount_ToggleStatus;
GO

CREATE PROCEDURE sp_RoleAccount_ToggleStatus
    @AccountName NVARCHAR(50),
    @RoleID INT,
    @Actived BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM RoleAccount WHERE AccountName = @AccountName AND RoleID = @RoleID)
    BEGIN
        RAISERROR(N'Phân quyền không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE RoleAccount
    SET Actived = @Actived
    WHERE AccountName = @AccountName AND RoleID = @RoleID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

PRINT N'Tạo các stored procedures quản lý Role thành công!';
GO
