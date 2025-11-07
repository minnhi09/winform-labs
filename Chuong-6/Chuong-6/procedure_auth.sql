USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Đăng nhập (Xác thực người dùng)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Login')
    DROP PROCEDURE sp_Login;
GO

CREATE PROCEDURE sp_Login
    @AccountName NVARCHAR(50),
    @Password NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản và mật khẩu
    SELECT 
        AccountName,
        FullName,
        Email,
        Phone,
        DateCreated
    FROM Account
    WHERE AccountName = @AccountName 
        AND Password = @Password;
    
    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR(N'Tên đăng nhập hoặc mật khẩu không đúng!', 16, 1);
    END
END
GO

-- =============================================
-- Procedure: Đổi mật khẩu
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ChangePassword')
    DROP PROCEDURE sp_ChangePassword;
GO

CREATE PROCEDURE sp_ChangePassword
    @AccountName NVARCHAR(50),
    @OldPassword NVARCHAR(200),
    @NewPassword NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra mật khẩu cũ
    IF NOT EXISTS (SELECT 1 FROM Account 
                   WHERE AccountName = @AccountName 
                   AND Password = @OldPassword)
    BEGIN
        RAISERROR(N'Mật khẩu cũ không đúng!', 16, 1);
        RETURN;
    END
    
    -- Cập nhật mật khẩu mới
    UPDATE Account
    SET Password = @NewPassword
    WHERE AccountName = @AccountName;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Lấy thông tin chi tiết người dùng
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetUserInfo')
    DROP PROCEDURE sp_GetUserInfo;
GO

CREATE PROCEDURE sp_GetUserInfo
    @AccountName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        AccountName,
        FullName,
        Email,
        Phone,
        DateCreated
    FROM Account
    WHERE AccountName = @AccountName;
END
GO

-- =============================================
-- Procedure: Lấy thông tin vai trò của người dùng
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetUserRoles')
    DROP PROCEDURE sp_GetUserRoles;
GO

CREATE PROCEDURE sp_GetUserRoles
    @AccountName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Lấy danh sách vai trò đang hoạt động của người dùng
    SELECT 
        r.ID,
        r.RoleName,
        r.Path,
        r.Notes,
        ra.Actived,
        ra.Notes AS RoleNotes
    FROM RoleAccount ra
    INNER JOIN Role r ON ra.RoleID = r.ID
    WHERE ra.AccountName = @AccountName 
        AND ra.Actived = 1
    ORDER BY r.RoleName;
END
GO

-- =============================================
-- Procedure: Kiểm tra quyền truy cập của người dùng
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CheckUserPermission')
    DROP PROCEDURE sp_CheckUserPermission;
GO

CREATE PROCEDURE sp_CheckUserPermission
    @AccountName NVARCHAR(50),
    @RoleID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra người dùng có vai trò cụ thể không
    SELECT COUNT(*) AS HasPermission
    FROM RoleAccount
    WHERE AccountName = @AccountName 
        AND RoleID = @RoleID 
        AND Actived = 1;
END
GO

-- =============================================
-- Procedure: Kiểm tra quyền theo tên vai trò
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CheckUserPermissionByName')
    DROP PROCEDURE sp_CheckUserPermissionByName;
GO

CREATE PROCEDURE sp_CheckUserPermissionByName
    @AccountName NVARCHAR(50),
    @RoleName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra người dùng có vai trò theo tên
    SELECT COUNT(*) AS HasPermission
    FROM RoleAccount ra
    INNER JOIN Role r ON ra.RoleID = r.ID
    WHERE ra.AccountName = @AccountName 
        AND r.RoleName = @RoleName
        AND ra.Actived = 1;
END
GO

-- =============================================
-- Procedure: Kiểm tra quyền truy cập theo đường dẫn
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CheckUserAccessByPath')
    DROP PROCEDURE sp_CheckUserAccessByPath;
GO

CREATE PROCEDURE sp_CheckUserAccessByPath
    @AccountName NVARCHAR(50),
    @Path NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra người dùng có quyền truy cập đường dẫn không
    SELECT COUNT(*) AS HasAccess
    FROM RoleAccount ra
    INNER JOIN Role r ON ra.RoleID = r.ID
    WHERE ra.AccountName = @AccountName 
        AND r.Path = @Path
        AND ra.Actived = 1;
END
GO

-- =============================================
-- Procedure: Xác thực và lấy đầy đủ thông tin người dùng
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ValidateAndGetUserInfo')
    DROP PROCEDURE sp_ValidateAndGetUserInfo;
GO

CREATE PROCEDURE sp_ValidateAndGetUserInfo
    @AccountName NVARCHAR(50),
    @Password NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra và lấy thông tin tài khoản
    SELECT 
        a.AccountName,
        a.FullName,
        a.Email,
        a.Phone,
        a.DateCreated,
        (
            SELECT STRING_AGG(r.RoleName, ', ') 
            FROM RoleAccount ra
            INNER JOIN Role r ON ra.RoleID = r.ID
            WHERE ra.AccountName = a.AccountName AND ra.Actived = 1
        ) AS Roles
    FROM Account a
    WHERE a.AccountName = @AccountName 
        AND a.Password = @Password;
    
    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR(N'Tên đăng nhập hoặc mật khẩu không đúng!', 16, 1);
    END
END
GO

-- =============================================
-- Procedure: Kiểm tra tài khoản có tồn tại không
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CheckAccountExists')
    DROP PROCEDURE sp_CheckAccountExists;
GO

CREATE PROCEDURE sp_CheckAccountExists
    @AccountName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CASE 
            WHEN EXISTS (SELECT 1 FROM Account WHERE AccountName = @AccountName)
            THEN 1
            ELSE 0
        END AS AccountExists;
END
GO

PRINT N'Tạo các stored procedures xác thực thành công!';
GO
