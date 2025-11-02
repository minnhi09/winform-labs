-- =============================================
-- Script: Stored Procedures for Accounts Management
-- Description: CRUD operations for Accounts table
-- Date: November 2, 2025
-- =============================================

USE Chuong_5_Lab_Advanced_Command;
GO

/*
Thêm stored procedure để chèn tài khoản mới vào bảng Accounts
*/
IF OBJECT_ID('InsertAccount', 'P') IS NOT NULL
    DROP PROCEDURE [InsertAccount]
GO

CREATE PROCEDURE [InsertAccount]
    @ID int output,
    @Username nvarchar(50),
    @Password nvarchar(100),
    @FullName nvarchar(200),
    @IsActive bit,
    @CreatedDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tên đăng nhập đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM Accounts WHERE Username = @Username)
    BEGIN
        RAISERROR(N'Tên đăng nhập đã tồn tại!', 16, 1);
        RETURN -1;
    END

    -- Thêm tài khoản mới
    INSERT INTO Accounts (Username, Password, FullName, IsActive, CreatedDate)
    VALUES (@Username, @Password, @FullName, @IsActive, @CreatedDate);

    -- Lấy ID của tài khoản vừa thêm
    SELECT @ID = SCOPE_IDENTITY();
    
    RETURN 1;
END
GO

/*
Thêm stored procedure để cập nhật thông tin tài khoản trong bảng Accounts
*/
IF OBJECT_ID('UpdateAccount', 'P') IS NOT NULL
    DROP PROCEDURE [UpdateAccount]
GO

CREATE PROCEDURE [UpdateAccount]
    @ID int,
    @Password nvarchar(100),
    @FullName nvarchar(200),
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản cần cập nhật!', 16, 1);
        RETURN -1;
    END

    -- Cập nhật thông tin (không cho phép sửa Username)
    UPDATE Accounts
    SET Password = @Password,
        FullName = @FullName,
        IsActive = @IsActive
    WHERE ID = @ID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để xóa tài khoản theo ID
*/
IF OBJECT_ID('DeleteAccount', 'P') IS NOT NULL
    DROP PROCEDURE [DeleteAccount]
GO

CREATE PROCEDURE [DeleteAccount]
    @ID int
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản cần xóa!', 16, 1);
        RETURN -1;
    END

    -- Kiểm tra tài khoản có hóa đơn nào không
    IF EXISTS (SELECT 1 FROM Bills WHERE AccountID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa tài khoản vì đang có hóa đơn liên quan!', 16, 1);
        RETURN -1;
    END

    -- Xóa các vai trò của tài khoản (nếu có)
    DELETE FROM AccountRoles WHERE AccountID = @ID;

    -- Xóa tài khoản
    DELETE FROM Accounts WHERE ID = @ID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để lấy thông tin tài khoản theo ID
*/
IF OBJECT_ID('GetAccountByID', 'P') IS NOT NULL
    DROP PROCEDURE [GetAccountByID]
GO

CREATE PROCEDURE [GetAccountByID]
    @ID int
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ID,
        Username,
        Password,
        FullName,
        IsActive,
        CreatedDate
    FROM Accounts
    WHERE ID = @ID;
END
GO

/*
Thêm stored procedure để lấy danh sách tất cả tài khoản
*/
IF OBJECT_ID('GetAllAccounts', 'P') IS NOT NULL
    DROP PROCEDURE [GetAllAccounts]
GO

CREATE PROCEDURE [GetAllAccounts]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ID,
        Username,
        Password,
        FullName,
        IsActive,
        CreatedDate
    FROM Accounts
    ORDER BY CreatedDate DESC;
END
GO

/*
Thêm stored procedure để tìm kiếm tài khoản theo Username hoặc FullName
*/
IF OBJECT_ID('SearchAccounts', 'P') IS NOT NULL
    DROP PROCEDURE [SearchAccounts]
GO

CREATE PROCEDURE [SearchAccounts]
    @SearchText nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ID,
        Username,
        Password,
        FullName,
        IsActive,
        CreatedDate
    FROM Accounts
    WHERE Username LIKE '%' + @SearchText + '%'
       OR FullName LIKE '%' + @SearchText + '%'
    ORDER BY CreatedDate DESC;
END
GO

/*
Thêm stored procedure để kiểm tra đăng nhập
*/
IF OBJECT_ID('CheckLogin', 'P') IS NOT NULL
    DROP PROCEDURE [CheckLogin]
GO

CREATE PROCEDURE [CheckLogin]
    @Username nvarchar(50),
    @Password nvarchar(100),
    @AccountID int output,
    @FullName nvarchar(200) output
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra username và password
    SELECT 
        @AccountID = ID,
        @FullName = FullName
    FROM Accounts
    WHERE Username = @Username 
      AND Password = @Password
      AND IsActive = 1;

    IF @AccountID IS NOT NULL
        RETURN 1  -- Đăng nhập thành công
    ELSE
        RETURN 0  -- Đăng nhập thất bại
END
GO

/*
Thêm stored procedure để thay đổi mật khẩu
*/
IF OBJECT_ID('ChangePassword', 'P') IS NOT NULL
    DROP PROCEDURE [ChangePassword]
GO

CREATE PROCEDURE [ChangePassword]
    @ID int,
    @OldPassword nvarchar(100),
    @NewPassword nvarchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra mật khẩu cũ có đúng không
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @ID AND Password = @OldPassword)
    BEGIN
        RAISERROR(N'Mật khẩu cũ không chính xác!', 16, 1);
        RETURN -1;
    END

    -- Cập nhật mật khẩu mới
    UPDATE Accounts
    SET Password = @NewPassword
    WHERE ID = @ID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để kích hoạt/vô hiệu hóa tài khoản
*/
IF OBJECT_ID('ToggleAccountStatus', 'P') IS NOT NULL
    DROP PROCEDURE [ToggleAccountStatus]
GO

CREATE PROCEDURE [ToggleAccountStatus]
    @ID int,
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản!', 16, 1);
        RETURN -1;
    END

    -- Cập nhật trạng thái
    UPDATE Accounts
    SET IsActive = @IsActive
    WHERE ID = @ID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để lấy danh sách tài khoản kèm vai trò
*/
IF OBJECT_ID('GetAccountsWithRoles', 'P') IS NOT NULL
    DROP PROCEDURE [GetAccountsWithRoles]
GO

CREATE PROCEDURE [GetAccountsWithRoles]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        A.ID,
        A.Username,
        A.FullName,
        A.IsActive,
        A.CreatedDate,
        STUFF((
            SELECT ', ' + R.RoleName
            FROM AccountRoles AR
            INNER JOIN Roles R ON AR.RoleID = R.ID
            WHERE AR.AccountID = A.ID
            FOR XML PATH('')
        ), 1, 2, '') AS Roles
    FROM Accounts A
    ORDER BY A.CreatedDate DESC;
END
GO

/*
Thêm stored procedure để gán vai trò cho tài khoản
*/
IF OBJECT_ID('AssignRoleToAccount', 'P') IS NOT NULL
    DROP PROCEDURE [AssignRoleToAccount]
GO

CREATE PROCEDURE [AssignRoleToAccount]
    @AccountID int,
    @RoleID int
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AccountID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản!', 16, 1);
        RETURN -1;
    END

    -- Kiểm tra vai trò có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE ID = @RoleID)
    BEGIN
        RAISERROR(N'Không tìm thấy vai trò!', 16, 1);
        RETURN -1;
    END

    -- Kiểm tra đã gán vai trò này chưa
    IF EXISTS (SELECT 1 FROM AccountRoles WHERE AccountID = @AccountID AND RoleID = @RoleID)
    BEGIN
        RAISERROR(N'Tài khoản đã có vai trò này!', 16, 1);
        RETURN -1;
    END

    -- Gán vai trò
    INSERT INTO AccountRoles (AccountID, RoleID)
    VALUES (@AccountID, @RoleID);

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

/*
Thêm stored procedure để xóa vai trò của tài khoản
*/
IF OBJECT_ID('RemoveRoleFromAccount', 'P') IS NOT NULL
    DROP PROCEDURE [RemoveRoleFromAccount]
GO

CREATE PROCEDURE [RemoveRoleFromAccount]
    @AccountID int,
    @RoleID int
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Xóa vai trò
    DELETE FROM AccountRoles
    WHERE AccountID = @AccountID AND RoleID = @RoleID;

    IF @@ROWCOUNT > 0
        RETURN 1
    ELSE
        RETURN 0
END
GO

PRINT '========================================';
PRINT 'Stored procedures for Accounts created successfully!';
PRINT '========================================';
