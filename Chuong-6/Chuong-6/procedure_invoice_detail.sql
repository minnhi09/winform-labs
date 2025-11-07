USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả InvoiceDetails
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_GetAll')
    DROP PROCEDURE sp_InvoiceDetail_GetAll;
GO

CREATE PROCEDURE sp_InvoiceDetail_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        id.ID,
        id.InvoiceID,
        id.FoodID,
        f.Name AS FoodName,
        f.Unit,
        f.Price,
        id.Amount,
        (id.Amount * f.Price) AS Total
    FROM InvoiceDetails id
    INNER JOIN Food f ON id.FoodID = f.ID
    ORDER BY id.InvoiceID, f.Name;
END
GO

-- =============================================
-- Procedure: Lấy InvoiceDetails theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_GetByID')
    DROP PROCEDURE sp_InvoiceDetail_GetByID;
GO

CREATE PROCEDURE sp_InvoiceDetail_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        id.ID,
        id.InvoiceID,
        id.FoodID,
        f.Name AS FoodName,
        f.Unit,
        f.Price,
        id.Amount,
        (id.Amount * f.Price) AS Total
    FROM InvoiceDetails id
    INNER JOIN Food f ON id.FoodID = f.ID
    WHERE id.ID = @ID;
END
GO

-- =============================================
-- Procedure: Lấy InvoiceDetails theo InvoiceID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_GetByInvoiceID')
    DROP PROCEDURE sp_InvoiceDetail_GetByInvoiceID;
GO

CREATE PROCEDURE sp_InvoiceDetail_GetByInvoiceID
    @InvoiceID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        id.ID,
        id.InvoiceID,
        id.FoodID,
        f.Name AS FoodName,
        f.Unit,
        f.Price,
        id.Amount,
        (id.Amount * f.Price) AS Total
    FROM InvoiceDetails id
    INNER JOIN Food f ON id.FoodID = f.ID
    WHERE id.InvoiceID = @InvoiceID
    ORDER BY f.Name;
END
GO

-- =============================================
-- Procedure: Thêm mới InvoiceDetails
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_Insert')
    DROP PROCEDURE sp_InvoiceDetail_Insert;
GO

CREATE PROCEDURE sp_InvoiceDetail_Insert
    @InvoiceID INT,
    @FoodID INT,
    @Amount INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra hóa đơn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Invoice WHERE ID = @InvoiceID)
    BEGIN
        RAISERROR(N'Hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra món ăn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Food WHERE ID = @FoodID)
    BEGIN
        RAISERROR(N'Món ăn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra hóa đơn đã thanh toán chưa
    IF EXISTS (SELECT 1 FROM Invoice WHERE ID = @InvoiceID AND Status = 1)
    BEGIN
        RAISERROR(N'Không thể thêm món vào hóa đơn đã thanh toán!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra số lượng hợp lệ
    IF @Amount <= 0
    BEGIN
        RAISERROR(N'Số lượng phải lớn hơn 0!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra món đã có trong hóa đơn chưa
    IF EXISTS (SELECT 1 FROM InvoiceDetails WHERE InvoiceID = @InvoiceID AND FoodID = @FoodID)
    BEGIN
        -- Nếu đã có thì cập nhật số lượng
        UPDATE InvoiceDetails
        SET Amount = Amount + @Amount
        WHERE InvoiceID = @InvoiceID AND FoodID = @FoodID;
        
        SELECT ID AS NewID FROM InvoiceDetails 
        WHERE InvoiceID = @InvoiceID AND FoodID = @FoodID;
    END
    ELSE
    BEGIN
        -- Nếu chưa có thì thêm mới
        INSERT INTO InvoiceDetails (InvoiceID, FoodID, Amount)
        VALUES (@InvoiceID, @FoodID, @Amount);
        
        SELECT SCOPE_IDENTITY() AS NewID;
    END
END
GO

-- =============================================
-- Procedure: Cập nhật InvoiceDetails
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_Update')
    DROP PROCEDURE sp_InvoiceDetail_Update;
GO

CREATE PROCEDURE sp_InvoiceDetail_Update
    @ID INT,
    @Amount INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @InvoiceID INT;
    
    -- Kiểm tra chi tiết hóa đơn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM InvoiceDetails WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Chi tiết hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Lấy InvoiceID
    SELECT @InvoiceID = InvoiceID FROM InvoiceDetails WHERE ID = @ID;
    
    -- Kiểm tra hóa đơn đã thanh toán chưa
    IF EXISTS (SELECT 1 FROM Invoice WHERE ID = @InvoiceID AND Status = 1)
    BEGIN
        RAISERROR(N'Không thể cập nhật hóa đơn đã thanh toán!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra số lượng
    IF @Amount <= 0
    BEGIN
        -- Nếu số lượng <= 0 thì xóa món
        DELETE FROM InvoiceDetails WHERE ID = @ID;
        SELECT 0 AS AffectedRows, N'Đã xóa món khỏi hóa đơn' AS Message;
    END
    ELSE
    BEGIN
        -- Cập nhật số lượng
        UPDATE InvoiceDetails
        SET Amount = @Amount
        WHERE ID = @ID;
        
        SELECT @@ROWCOUNT AS AffectedRows;
    END
END
GO

-- =============================================
-- Procedure: Xóa InvoiceDetails
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_Delete')
    DROP PROCEDURE sp_InvoiceDetail_Delete;
GO

CREATE PROCEDURE sp_InvoiceDetail_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @InvoiceID INT;
    
    -- Kiểm tra chi tiết hóa đơn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM InvoiceDetails WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Chi tiết hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Lấy InvoiceID
    SELECT @InvoiceID = InvoiceID FROM InvoiceDetails WHERE ID = @ID;
    
    -- Kiểm tra hóa đơn đã thanh toán chưa
    IF EXISTS (SELECT 1 FROM Invoice WHERE ID = @InvoiceID AND Status = 1)
    BEGIN
        RAISERROR(N'Không thể xóa món trong hóa đơn đã thanh toán!', 16, 1);
        RETURN;
    END
    
    DELETE FROM InvoiceDetails WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa tất cả InvoiceDetails của một Invoice
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_DeleteByInvoiceID')
    DROP PROCEDURE sp_InvoiceDetail_DeleteByInvoiceID;
GO

CREATE PROCEDURE sp_InvoiceDetail_DeleteByInvoiceID
    @InvoiceID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra hóa đơn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Invoice WHERE ID = @InvoiceID)
    BEGIN
        RAISERROR(N'Hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra hóa đơn đã thanh toán chưa
    IF EXISTS (SELECT 1 FROM Invoice WHERE ID = @InvoiceID AND Status = 1)
    BEGIN
        RAISERROR(N'Không thể xóa chi tiết hóa đơn đã thanh toán!', 16, 1);
        RETURN;
    END
    
    DELETE FROM InvoiceDetails WHERE InvoiceID = @InvoiceID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Cập nhật số lượng món (tăng/giảm)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_UpdateAmount')
    DROP PROCEDURE sp_InvoiceDetail_UpdateAmount;
GO

CREATE PROCEDURE sp_InvoiceDetail_UpdateAmount
    @ID INT,
    @AmountChange INT -- Số lượng thay đổi (+ để tăng, - để giảm)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @InvoiceID INT;
    DECLARE @CurrentAmount INT;
    DECLARE @NewAmount INT;
    
    -- Kiểm tra chi tiết hóa đơn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM InvoiceDetails WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Chi tiết hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Lấy thông tin hiện tại
    SELECT @InvoiceID = InvoiceID, @CurrentAmount = Amount 
    FROM InvoiceDetails WHERE ID = @ID;
    
    -- Kiểm tra hóa đơn đã thanh toán chưa
    IF EXISTS (SELECT 1 FROM Invoice WHERE ID = @InvoiceID AND Status = 1)
    BEGIN
        RAISERROR(N'Không thể cập nhật hóa đơn đã thanh toán!', 16, 1);
        RETURN;
    END
    
    -- Tính số lượng mới
    SET @NewAmount = @CurrentAmount + @AmountChange;
    
    IF @NewAmount <= 0
    BEGIN
        -- Nếu số lượng mới <= 0 thì xóa món
        DELETE FROM InvoiceDetails WHERE ID = @ID;
        SELECT 0 AS NewAmount, N'Đã xóa món khỏi hóa đơn' AS Message;
    END
    ELSE
    BEGIN
        -- Cập nhật số lượng mới
        UPDATE InvoiceDetails
        SET Amount = @NewAmount
        WHERE ID = @ID;
        
        SELECT @NewAmount AS NewAmount;
    END
END
GO

-- =============================================
-- Procedure: Lấy tổng tiền theo InvoiceID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_GetTotalByInvoiceID')
    DROP PROCEDURE sp_InvoiceDetail_GetTotalByInvoiceID;
GO

CREATE PROCEDURE sp_InvoiceDetail_GetTotalByInvoiceID
    @InvoiceID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        @InvoiceID AS InvoiceID,
        COUNT(*) AS TotalItems,
        SUM(id.Amount) AS TotalQuantity,
        SUM(id.Amount * f.Price) AS TotalAmount
    FROM InvoiceDetails id
    INNER JOIN Food f ON id.FoodID = f.ID
    WHERE id.InvoiceID = @InvoiceID;
END
GO

-- =============================================
-- Procedure: Kiểm tra món ăn đã có trong hóa đơn chưa
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetail_CheckFoodExists')
    DROP PROCEDURE sp_InvoiceDetail_CheckFoodExists;
GO

CREATE PROCEDURE sp_InvoiceDetail_CheckFoodExists
    @InvoiceID INT,
    @FoodID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CASE 
            WHEN EXISTS (SELECT 1 FROM InvoiceDetails 
                        WHERE InvoiceID = @InvoiceID AND FoodID = @FoodID)
            THEN 1
            ELSE 0
        END AS IsExists,
        ISNULL((SELECT Amount FROM InvoiceDetails 
                WHERE InvoiceID = @InvoiceID AND FoodID = @FoodID), 0) AS CurrentAmount;
END
GO

PRINT N'Tạo các stored procedures quản lý InvoiceDetails thành công!';
GO
