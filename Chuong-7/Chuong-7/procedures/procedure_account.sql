USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả Account
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Account_GetAll')
    DROP PROCEDURE sp_Account_GetAll;
GO

CREATE PROCEDURE sp_Account_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT AccountName, FullName, Email, Phone, DateCreated
    FROM Account
    ORDER BY DateCreated DESC;
END
GO

-- =============================================
-- Procedure: Lấy Account theo AccountName
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Account_GetByName')
    DROP PROCEDURE sp_Account_GetByName;
GO

CREATE PROCEDURE sp_Account_GetByName
    @AccountName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT AccountName, FullName, Email, Phone, DateCreated
    FROM Account
    WHERE AccountName = @AccountName;
END
GO

-- =============================================
-- Procedure: Thêm mới Account
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Account_Insert')
    DROP PROCEDURE sp_Account_Insert;
GO

CREATE PROCEDURE sp_Account_Insert
    @AccountName NVARCHAR(50),
    @Password NVARCHAR(200),
    @FullName NVARCHAR(200),
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tên tài khoản đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Account WHERE AccountName = @AccountName)
    BEGIN
        RAISERROR(N'Tên tài khoản đã tồn tại!', 16, 1);
        RETURN;
    END
    
    INSERT INTO Account (AccountName, Password, FullName, Email, Phone, DateCreated)
    VALUES (@AccountName, @Password, @FullName, @Email, @Phone, GETDATE());
    
    SELECT @AccountName AS NewAccountName;
END
GO

-- =============================================
-- Procedure: Cập nhật Account
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Account_Update')
    DROP PROCEDURE sp_Account_Update;
GO

CREATE PROCEDURE sp_Account_Update
    @AccountName NVARCHAR(50),
    @FullName NVARCHAR(200),
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Account WHERE AccountName = @AccountName)
    BEGIN
        RAISERROR(N'Tài khoản không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE Account
    SET FullName = @FullName,
        Email = @Email,
        Phone = @Phone
    WHERE AccountName = @AccountName;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa Account
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Account_Delete')
    DROP PROCEDURE sp_Account_Delete;
GO

CREATE PROCEDURE sp_Account_Delete
    @AccountName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Account WHERE AccountName = @AccountName)
    BEGIN
        RAISERROR(N'Tài khoản không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra tài khoản có đang được sử dụng không
    IF EXISTS (SELECT 1 FROM Invoice WHERE AccountID = @AccountName)
    BEGIN
        RAISERROR(N'Không thể xóa tài khoản đang có hóa đơn!', 16, 1);
        RETURN;
    END
    
    -- Xóa phân quyền trước
    DELETE FROM RoleAccount WHERE AccountName = @AccountName;
    
    -- Xóa tài khoản
    DELETE FROM Account WHERE AccountName = @AccountName;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Lấy danh sách Role của Account
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Account_GetRoles')
    DROP PROCEDURE sp_Account_GetRoles;
GO

CREATE PROCEDURE sp_Account_GetRoles
    @AccountName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT r.ID, r.RoleName, r.Path, r.Notes, ra.Actived, ra.Notes AS RoleNotes
    FROM Role r
    INNER JOIN RoleAccount ra ON r.ID = ra.RoleID
    WHERE ra.AccountName = @AccountName
    ORDER BY r.RoleName;
END
GO

-- =============================================
-- Procedure: Reset mật khẩu (dành cho admin)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Account_ResetPassword')
    DROP PROCEDURE sp_Account_ResetPassword;
GO

CREATE PROCEDURE sp_Account_ResetPassword
    @AccountName NVARCHAR(50),
    @NewPassword NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM Account WHERE AccountName = @AccountName)
    BEGIN
        RAISERROR(N'Tài khoản không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE Account
    SET Password = @NewPassword
    WHERE AccountName = @AccountName;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

PRINT N'Tạo các stored procedures quản lý Account thành công!';
GO
