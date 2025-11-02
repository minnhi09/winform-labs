-- =============================================
-- Script: Stored Procedures for Role Management
-- Description: Complete CRUD operations for Roles table
-- Database: Chuong_5_Lab_Advanced_Command
-- Date: November 2, 2025
-- =============================================

USE Chuong_5_Lab_Advanced_Command;
GO

-- =============================================
-- Procedure: GetAllRoles
-- Description: Lấy danh sách tất cả vai trò
-- =============================================
IF OBJECT_ID('GetAllRoles', 'P') IS NOT NULL
    DROP PROCEDURE GetAllRoles;
GO

CREATE PROCEDURE GetAllRoles
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ID, RoleName, Description
    FROM Roles
    ORDER BY RoleName;
END
GO

PRINT 'Procedure GetAllRoles created successfully.';
GO

-- =============================================
-- Procedure: GetRoleByID
-- Description: Lấy thông tin vai trò theo ID
-- Parameters: @ID - ID của vai trò cần lấy
-- =============================================
IF OBJECT_ID('GetRoleByID', 'P') IS NOT NULL
    DROP PROCEDURE GetRoleByID;
GO

CREATE PROCEDURE GetRoleByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy vai trò với ID = %d', 16, 1, @ID);
        RETURN -1;
    END
    
    SELECT ID, RoleName, Description
    FROM Roles
    WHERE ID = @ID;
    
    RETURN 0;
END
GO

PRINT 'Procedure GetRoleByID created successfully.';
GO

-- =============================================
-- Procedure: InsertRole
-- Description: Thêm vai trò mới
-- Parameters: 
--   @RoleName - Tên vai trò (bắt buộc, unique)
--   @Description - Mô tả vai trò (tùy chọn)
--   @ID - Output parameter trả về ID của vai trò mới
-- Returns: ID của vai trò mới được tạo
-- =============================================
IF OBJECT_ID('InsertRole', 'P') IS NOT NULL
    DROP PROCEDURE InsertRole;
GO

CREATE PROCEDURE InsertRole
    @RoleName NVARCHAR(100),
    @Description NVARCHAR(300) = NULL,
    @ID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate input
    IF @RoleName IS NULL OR LTRIM(RTRIM(@RoleName)) = ''
    BEGIN
        RAISERROR(N'Tên vai trò không được để trống!', 16, 1);
        RETURN -1;
    END
    
    IF LEN(LTRIM(RTRIM(@RoleName))) < 3
    BEGIN
        RAISERROR(N'Tên vai trò phải có ít nhất 3 ký tự!', 16, 1);
        RETURN -1;
    END
    
    -- Check if role name already exists
    IF EXISTS (SELECT 1 FROM Roles WHERE RoleName = LTRIM(RTRIM(@RoleName)))
    BEGIN
        RAISERROR(N'Tên vai trò "%s" đã tồn tại!', 16, 1, @RoleName);
        RETURN -1;
    END
    
    -- Insert new role
    BEGIN TRY
        INSERT INTO Roles (RoleName, Description)
        VALUES (LTRIM(RTRIM(@RoleName)), LTRIM(RTRIM(@Description)));
        
        SET @ID = SCOPE_IDENTITY();
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi thêm vai trò: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure InsertRole created successfully.';
GO

-- =============================================
-- Procedure: UpdateRole
-- Description: Cập nhật thông tin vai trò
-- Parameters: 
--   @ID - ID của vai trò cần cập nhật
--   @RoleName - Tên vai trò mới
--   @Description - Mô tả mới
-- =============================================
IF OBJECT_ID('UpdateRole', 'P') IS NOT NULL
    DROP PROCEDURE UpdateRole;
GO

CREATE PROCEDURE UpdateRole
    @ID INT,
    @RoleName NVARCHAR(100),
    @Description NVARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if role exists
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy vai trò với ID = %d', 16, 1, @ID);
        RETURN -1;
    END
    
    -- Validate input
    IF @RoleName IS NULL OR LTRIM(RTRIM(@RoleName)) = ''
    BEGIN
        RAISERROR(N'Tên vai trò không được để trống!', 16, 1);
        RETURN -1;
    END
    
    IF LEN(LTRIM(RTRIM(@RoleName))) < 3
    BEGIN
        RAISERROR(N'Tên vai trò phải có ít nhất 3 ký tự!', 16, 1);
        RETURN -1;
    END
    
    -- Check if new role name already exists (exclude current role)
    IF EXISTS (SELECT 1 FROM Roles WHERE RoleName = LTRIM(RTRIM(@RoleName)) AND ID != @ID)
    BEGIN
        RAISERROR(N'Tên vai trò "%s" đã tồn tại!', 16, 1, @RoleName);
        RETURN -1;
    END
    
    -- Update role
    BEGIN TRY
        UPDATE Roles
        SET RoleName = LTRIM(RTRIM(@RoleName)),
            Description = LTRIM(RTRIM(@Description))
        WHERE ID = @ID;
        
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi cập nhật vai trò: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure UpdateRole created successfully.';
GO

-- =============================================
-- Procedure: DeleteRole
-- Description: Xóa vai trò
-- Parameters: @ID - ID của vai trò cần xóa
-- Note: Sẽ kiểm tra xem vai trò có đang được sử dụng không
-- =============================================
IF OBJECT_ID('DeleteRole', 'P') IS NOT NULL
    DROP PROCEDURE DeleteRole;
GO

CREATE PROCEDURE DeleteRole
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if role exists
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Không tìm thấy vai trò với ID = %d', 16, 1, @ID);
        RETURN -1;
    END
    
    -- Check if role is being used by any account
    IF EXISTS (SELECT 1 FROM AccountRoles WHERE RoleID = @ID)
    BEGIN
        RAISERROR(N'Không thể xóa vai trò này vì đang được sử dụng bởi các tài khoản!', 16, 1);
        RETURN -1;
    END
    
    -- Delete role
    BEGIN TRY
        DELETE FROM Roles WHERE ID = @ID;
        RETURN 0;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi xóa vai trò: %s', 16, 1, @ErrorMessage);
        RETURN -1;
    END CATCH
END
GO

PRINT 'Procedure DeleteRole created successfully.';
GO

-- =============================================
-- Procedure: SearchRoles
-- Description: Tìm kiếm vai trò theo tên hoặc mô tả
-- Parameters: @SearchText - Từ khóa tìm kiếm
-- =============================================
IF OBJECT_ID('SearchRoles', 'P') IS NOT NULL
    DROP PROCEDURE SearchRoles;
GO

CREATE PROCEDURE SearchRoles
    @SearchText NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @SearchText IS NULL OR LTRIM(RTRIM(@SearchText)) = ''
    BEGIN
        -- If search text is empty, return all roles
        EXEC GetAllRoles;
    END
    ELSE
    BEGIN
        SELECT ID, RoleName, Description
        FROM Roles
        WHERE RoleName LIKE '%' + @SearchText + '%'
           OR Description LIKE '%' + @SearchText + '%'
        ORDER BY RoleName;
    END
    
    RETURN 0;
END
GO

PRINT 'Procedure SearchRoles created successfully.';
GO

-- =============================================
-- Procedure: GetRolesWithAccountCount
-- Description: Lấy danh sách vai trò kèm số lượng tài khoản
-- =============================================
IF OBJECT_ID('GetRolesWithAccountCount', 'P') IS NOT NULL
    DROP PROCEDURE GetRolesWithAccountCount;
GO

CREATE PROCEDURE GetRolesWithAccountCount
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        r.ID,
        r.RoleName,
        r.Description,
        COUNT(ar.AccountID) AS AccountCount
    FROM Roles r
    LEFT JOIN AccountRoles ar ON r.ID = ar.RoleID
    GROUP BY r.ID, r.RoleName, r.Description
    ORDER BY r.RoleName;
    
    RETURN 0;
END
GO

PRINT 'Procedure GetRolesWithAccountCount created successfully.';
GO

-- =============================================
-- Display all created procedures
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'All Role Management Procedures Created Successfully!';
PRINT '========================================';
PRINT '';

SELECT 
    ROUTINE_NAME AS 'Procedure Name',
    CREATED AS 'Created Date',
    LAST_ALTERED AS 'Last Modified'
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE'
    AND ROUTINE_NAME IN (
        'GetAllRoles',
        'GetRoleByID', 
        'InsertRole',
        'UpdateRole',
        'DeleteRole',
        'SearchRoles',
        'GetRolesWithAccountCount'
    )
ORDER BY ROUTINE_NAME;
GO
