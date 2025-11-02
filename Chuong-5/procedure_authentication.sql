-- =============================================
-- Script: Stored Procedures for Authentication & Authorization
-- Description: Complete procedures for login, logout, password management
-- Database: Chuong_5_Lab_Advanced_Command
-- Date: November 2, 2025
-- =============================================

USE Chuong_5_Lab_Advanced_Command;
GO

-- =============================================
-- Procedure: CheckLogin
-- Description: Xác thực đăng nhập người dùng
-- Parameters: 
--   @Username - Tên đăng nhập
--   @Password - Mật khẩu (plain text - nên mã hóa trong production)
-- Returns: Thông tin tài khoản nếu đăng nhập thành công
-- =============================================
IF OBJECT_ID('CheckLogin', 'P') IS NOT NULL
    DROP PROCEDURE CheckLogin;
GO

CREATE PROCEDURE CheckLogin
    @Username NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate input
    IF @Username IS NULL OR LTRIM(RTRIM(@Username)) = ''
    BEGIN
        RAISERROR(N'Tên đăng nhập không được để trống!', 16, 1);
        RETURN -1;
    END
    
    IF @Password IS NULL OR LTRIM(RTRIM(@Password)) = ''
    BEGIN
        RAISERROR(N'Mật khẩu không được để trống!', 16, 1);
        RETURN -1;
    END
    
    -- Check login credentials
    DECLARE @AccountID INT;
    DECLARE @IsActive BIT;
    
    SELECT 
        @AccountID = ID,
        @IsActive = IsActive
    FROM Accounts
    WHERE Username = @Username 
        AND Password = @Password;
    
    -- Check if account exists
    IF @AccountID IS NULL
    BEGIN
        RAISERROR(N'Tên đăng nhập hoặc mật khẩu không đúng!', 16, 1);
        RETURN -1;
    END
    
    -- Check if account is active
    IF @IsActive = 0
    BEGIN
        RAISERROR(N'Tài khoản đã bị khóa! Vui lòng liên hệ quản trị viên.', 16, 1);
        RETURN -1;
    END
    
    -- Return account information with roles
    SELECT 
        a.ID,
        a.Username,
        a.FullName,
        a.IsActive,
        a.CreatedDate,
        STUFF((
            SELECT ', ' + r.RoleName
            FROM AccountRoles ar
            INNER JOIN Roles r ON ar.RoleID = r.ID
            WHERE ar.AccountID = a.ID
            FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Roles
    FROM Accounts a
    WHERE a.ID = @AccountID;
    
    RETURN 0;
END
GO

PRINT 'Procedure CheckLogin created successfully.';
GO

-- =============================================
-- Procedure: ChangePassword
-- Description: Thay đổi mật khẩu người dùng
-- Parameters: 
--   @AccountID - ID tài khoản
--   @OldPassword - Mật khẩu cũ
--   @NewPassword - Mật khẩu mới
-- =============================================
IF OBJECT_ID('ChangePassword', 'P') IS NOT NULL
    DROP PROCEDURE ChangePassword;
GO

CREATE PROCEDURE ChangePassword
    @AccountID INT,
    @OldPassword NVARCHAR(100),
    @NewPassword NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate input
    IF @OldPassword IS NULL OR LTRIM(RTRIM(@OldPassword)) = ''
    BEGIN
        RAISERROR(N'Mật khẩu cũ không được để trống!', 16, 1);
        RETURN -1;
    END
    
    IF @NewPassword IS NULL OR LTRIM(RTRIM(@NewPassword)) = ''
    BEGIN
        RAISERROR(N'Mật khẩu mới không được để trống!', 16, 1);
        RETURN -1;
    END
    
    IF LEN(LTRIM(RTRIM(@NewPassword))) < 6
    BEGIN
        RAISERROR(N'Mật khẩu mới phải có ít nhất 6 ký tự!', 16, 1);
        RETURN -1;
    END
    
    IF @OldPassword = @NewPassword
    BEGIN
        RAISERROR(N'Mật khẩu mới phải khác mật khẩu cũ!', 16, 1);
        RETURN -1;
    END
    
    -- Check if account exists
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AccountID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản!', 16, 1);
        RETURN -1;
    END
    
    -- Verify old password
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AccountID AND Password = @OldPassword)
    BEGIN
        RAISERROR(N'Mật khẩu cũ không đúng!', 16, 1);
        RETURN -1;
    END
    
    -- Update password
    BEGIN TRY
        UPDATE Accounts
        SET Password = @NewPassword
        WHERE ID = @AccountID;
        
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi thay đổi mật khẩu: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure ChangePassword created successfully.';
GO

-- =============================================
-- Procedure: ResetPassword
-- Description: Reset mật khẩu cho tài khoản (dành cho Admin)
-- Parameters: 
--   @AccountID - ID tài khoản cần reset
--   @NewPassword - Mật khẩu mới
--   @AdminID - ID của Admin thực hiện reset
-- =============================================
IF OBJECT_ID('ResetPassword', 'P') IS NOT NULL
    DROP PROCEDURE ResetPassword;
GO

CREATE PROCEDURE ResetPassword
    @AccountID INT,
    @NewPassword NVARCHAR(100),
    @AdminID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate input
    IF @NewPassword IS NULL OR LTRIM(RTRIM(@NewPassword)) = ''
    BEGIN
        RAISERROR(N'Mật khẩu mới không được để trống!', 16, 1);
        RETURN -1;
    END
    
    IF LEN(LTRIM(RTRIM(@NewPassword))) < 6
    BEGIN
        RAISERROR(N'Mật khẩu mới phải có ít nhất 6 ký tự!', 16, 1);
        RETURN -1;
    END
    
    -- Check if account exists
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AccountID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản!', 16, 1);
        RETURN -1;
    END
    
    -- Check if admin exists and is active
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AdminID AND IsActive = 1)
    BEGIN
        RAISERROR(N'Tài khoản Admin không hợp lệ!', 16, 1);
        RETURN -1;
    END
    
    -- Update password
    BEGIN TRY
        UPDATE Accounts
        SET Password = @NewPassword
        WHERE ID = @AccountID;
        
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi reset mật khẩu: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure ResetPassword created successfully.';
GO

-- =============================================
-- Procedure: GetAccountRoles
-- Description: Lấy danh sách vai trò của tài khoản
-- Parameters: @AccountID - ID tài khoản
-- =============================================
IF OBJECT_ID('GetAccountRoles', 'P') IS NOT NULL
    DROP PROCEDURE GetAccountRoles;
GO

CREATE PROCEDURE GetAccountRoles
    @AccountID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if account exists
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AccountID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản với ID = %d', 16, 1, @AccountID);
        RETURN -1;
    END
    
    -- Get roles
    SELECT 
        r.ID,
        r.RoleName,
        r.Description
    FROM AccountRoles ar
    INNER JOIN Roles r ON ar.RoleID = r.ID
    WHERE ar.AccountID = @AccountID
    ORDER BY r.RoleName;
    
    RETURN 0;
END
GO

PRINT 'Procedure GetAccountRoles created successfully.';
GO

-- =============================================
-- Procedure: CheckAccountHasRole
-- Description: Kiểm tra tài khoản có vai trò cụ thể không
-- Parameters: 
--   @AccountID - ID tài khoản
--   @RoleName - Tên vai trò cần kiểm tra
-- Returns: 1 nếu có vai trò, 0 nếu không có
-- =============================================
IF OBJECT_ID('CheckAccountHasRole', 'P') IS NOT NULL
    DROP PROCEDURE CheckAccountHasRole;
GO

CREATE PROCEDURE CheckAccountHasRole
    @AccountID INT,
    @RoleName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @HasRole BIT = 0;
    
    IF EXISTS (
        SELECT 1 
        FROM AccountRoles ar
        INNER JOIN Roles r ON ar.RoleID = r.ID
        WHERE ar.AccountID = @AccountID 
            AND r.RoleName = @RoleName
    )
    BEGIN
        SET @HasRole = 1;
    END
    
    SELECT @HasRole AS HasRole;
    RETURN @HasRole;
END
GO

PRINT 'Procedure CheckAccountHasRole created successfully.';
GO

-- =============================================
-- Procedure: ToggleAccountStatus
-- Description: Khóa/Mở khóa tài khoản
-- Parameters: 
--   @AccountID - ID tài khoản cần thay đổi trạng thái
--   @AdminID - ID của Admin thực hiện thao tác
-- =============================================
IF OBJECT_ID('ToggleAccountStatus', 'P') IS NOT NULL
    DROP PROCEDURE ToggleAccountStatus;
GO

CREATE PROCEDURE ToggleAccountStatus
    @AccountID INT,
    @AdminID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if account exists
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AccountID)
    BEGIN
        RAISERROR(N'Không tìm thấy tài khoản với ID = %d', 16, 1, @AccountID);
        RETURN -1;
    END
    
    -- Check if admin exists and is active
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE ID = @AdminID AND IsActive = 1)
    BEGIN
        RAISERROR(N'Tài khoản Admin không hợp lệ!', 16, 1);
        RETURN -1;
    END
    
    -- Prevent admin from locking themselves
    IF @AccountID = @AdminID
    BEGIN
        RAISERROR(N'Bạn không thể khóa tài khoản của chính mình!', 16, 1);
        RETURN -1;
    END
    
    -- Toggle status
    BEGIN TRY
        UPDATE Accounts
        SET IsActive = CASE WHEN IsActive = 1 THEN 0 ELSE 1 END
        WHERE ID = @AccountID;
        
        DECLARE @NewStatus BIT;
        SELECT @NewStatus = IsActive FROM Accounts WHERE ID = @AccountID;
        
        IF @NewStatus = 1
            PRINT N'Đã mở khóa tài khoản thành công!';
        ELSE
            PRINT N'Đã khóa tài khoản thành công!';
        
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi thay đổi trạng thái tài khoản: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure ToggleAccountStatus created successfully.';
GO

-- =============================================
-- Procedure: GetLoginHistory
-- Description: Lấy lịch sử đăng nhập (cần tạo bảng LoginHistory trước)
-- Note: Đây là procedure mẫu, cần tạo bảng LoginHistory để sử dụng
-- =============================================
-- Tạo bảng LoginHistory (nếu cần)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LoginHistory')
BEGIN
    CREATE TABLE LoginHistory
    (
        ID INT PRIMARY KEY IDENTITY(1,1),
        AccountID INT NOT NULL,
        LoginTime DATETIME NOT NULL DEFAULT GETDATE(),
        LogoutTime DATETIME NULL,
        IPAddress NVARCHAR(50) NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT N'Success',
        Message NVARCHAR(500) NULL,
        CONSTRAINT FK_LoginHistory_Accounts FOREIGN KEY (AccountID) 
            REFERENCES Accounts(ID) ON DELETE CASCADE
    );
    PRINT 'Table LoginHistory created successfully.';
END
GO

-- =============================================
-- Procedure: LogLogin
-- Description: Ghi nhận thời điểm đăng nhập
-- Parameters: 
--   @AccountID - ID tài khoản
--   @IPAddress - Địa chỉ IP (optional)
--   @Status - Trạng thái đăng nhập (Success/Failed)
--   @Message - Thông điệp (optional)
-- =============================================
IF OBJECT_ID('LogLogin', 'P') IS NOT NULL
    DROP PROCEDURE LogLogin;
GO

CREATE PROCEDURE LogLogin
    @AccountID INT,
    @IPAddress NVARCHAR(50) = NULL,
    @Status NVARCHAR(50) = N'Success',
    @Message NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO LoginHistory (AccountID, LoginTime, IPAddress, Status, Message)
        VALUES (@AccountID, GETDATE(), @IPAddress, @Status, @Message);
        
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi ghi nhận lịch sử đăng nhập: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure LogLogin created successfully.';
GO

-- =============================================
-- Procedure: LogLogout
-- Description: Ghi nhận thời điểm đăng xuất
-- Parameters: 
--   @AccountID - ID tài khoản
-- =============================================
IF OBJECT_ID('LogLogout', 'P') IS NOT NULL
    DROP PROCEDURE LogLogout;
GO

CREATE PROCEDURE LogLogout
    @AccountID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Update the latest login record with logout time
        UPDATE TOP(1) LoginHistory
        SET LogoutTime = GETDATE()
        WHERE AccountID = @AccountID 
            AND LogoutTime IS NULL
        ORDER BY LoginTime DESC;
        
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi ghi nhận đăng xuất: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure LogLogout created successfully.';
GO

-- =============================================
-- Procedure: GetLoginHistory
-- Description: Lấy lịch sử đăng nhập của tài khoản
-- Parameters: 
--   @AccountID - ID tài khoản (NULL = tất cả)
--   @FromDate - Từ ngày (NULL = không giới hạn)
--   @ToDate - Đến ngày (NULL = không giới hạn)
--   @Top - Số bản ghi tối đa (NULL = tất cả)
-- =============================================
IF OBJECT_ID('GetLoginHistory', 'P') IS NOT NULL
    DROP PROCEDURE GetLoginHistory;
GO

CREATE PROCEDURE GetLoginHistory
    @AccountID INT = NULL,
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL,
    @Top INT = 100
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP (ISNULL(@Top, 100))
        lh.ID,
        lh.AccountID,
        a.Username,
        a.FullName,
        lh.LoginTime,
        lh.LogoutTime,
        lh.IPAddress,
        lh.Status,
        lh.Message,
        DATEDIFF(MINUTE, lh.LoginTime, ISNULL(lh.LogoutTime, GETDATE())) AS SessionDuration
    FROM LoginHistory lh
    INNER JOIN Accounts a ON lh.AccountID = a.ID
    WHERE (@AccountID IS NULL OR lh.AccountID = @AccountID)
        AND (@FromDate IS NULL OR lh.LoginTime >= @FromDate)
        AND (@ToDate IS NULL OR lh.LoginTime <= @ToDate)
    ORDER BY lh.LoginTime DESC;
    
    RETURN 0;
END
GO

PRINT 'Procedure GetLoginHistory created successfully.';
GO

-- =============================================
-- Display all created procedures
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'All Authentication Procedures Created Successfully!';
PRINT '========================================';
PRINT '';

SELECT 
    ROUTINE_NAME AS 'Procedure Name',
    CREATED AS 'Created Date',
    LAST_ALTERED AS 'Last Modified'
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE'
    AND ROUTINE_NAME IN (
        'CheckLogin',
        'ChangePassword',
        'ResetPassword',
        'GetAccountRoles',
        'CheckAccountHasRole',
        'ToggleAccountStatus',
        'LogLogin',
        'LogLogout',
        'GetLoginHistory'
    )
ORDER BY ROUTINE_NAME;
GO

-- =============================================
-- Test data: Insert sample login history
-- =============================================
PRINT '';
PRINT 'Inserting sample login history...';

-- Log some sample login records
EXEC LogLogin @AccountID = 1, @Status = N'Success', @Message = N'Đăng nhập thành công';
WAITFOR DELAY '00:00:02';
EXEC LogLogout @AccountID = 1;

EXEC LogLogin @AccountID = 2, @Status = N'Success', @Message = N'Đăng nhập thành công';
EXEC LogLogin @AccountID = 3, @Status = N'Failed', @Message = N'Mật khẩu không đúng';

PRINT 'Sample login history inserted successfully.';
GO
