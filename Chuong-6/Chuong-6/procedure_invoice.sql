USE chuong_6_restaurant_management;
GO

-- =============================================
-- Procedure: Lấy tất cả Invoice
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_GetAll')
    DROP PROCEDURE sp_Invoice_GetAll;
GO

CREATE PROCEDURE sp_Invoice_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        i.ID,
        i.Name,
        i.TableID,
        t.Name AS TableName,
        i.Total,
        i.Discount,
        i.Tax,
        i.Status,
        i.AccountID,
        a.FullName AS AccountName,
        i.CheckoutDate
    FROM Invoice i
    INNER JOIN [Table] t ON i.TableID = t.ID
    INNER JOIN Account a ON i.AccountID = a.AccountName
    ORDER BY i.CheckoutDate DESC;
END
GO

-- =============================================
-- Procedure: Lấy Invoice theo ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_GetByID')
    DROP PROCEDURE sp_Invoice_GetByID;
GO

CREATE PROCEDURE sp_Invoice_GetByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        i.ID,
        i.Name,
        i.TableID,
        t.Name AS TableName,
        i.Total,
        i.Discount,
        i.Tax,
        i.Status,
        i.AccountID,
        a.FullName AS AccountName,
        i.CheckoutDate
    FROM Invoice i
    INNER JOIN [Table] t ON i.TableID = t.ID
    INNER JOIN Account a ON i.AccountID = a.AccountName
    WHERE i.ID = @ID;
END
GO

-- =============================================
-- Procedure: Lấy Invoice theo TableID (chưa thanh toán)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_GetByTableID')
    DROP PROCEDURE sp_Invoice_GetByTableID;
GO

CREATE PROCEDURE sp_Invoice_GetByTableID
    @TableID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        i.ID,
        i.Name,
        i.TableID,
        t.Name AS TableName,
        i.Total,
        i.Discount,
        i.Tax,
        i.Status,
        i.AccountID,
        a.FullName AS AccountName,
        i.CheckoutDate
    FROM Invoice i
    INNER JOIN [Table] t ON i.TableID = t.ID
    INNER JOIN Account a ON i.AccountID = a.AccountName
    WHERE i.TableID = @TableID AND i.Status = 0
    ORDER BY i.CheckoutDate DESC;
END
GO

-- =============================================
-- Procedure: Thêm mới Invoice
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_Insert')
    DROP PROCEDURE sp_Invoice_Insert;
GO

CREATE PROCEDURE sp_Invoice_Insert
    @Name NVARCHAR(200) = N'Hóa đơn chưa thanh toán',
    @TableID INT,
    @AccountID NVARCHAR(50),
    @Total INT = 0,
    @Discount FLOAT = 0,
    @Tax FLOAT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra bàn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM [Table] WHERE ID = @TableID)
    BEGIN
        RAISERROR(N'Bàn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra tài khoản có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Account WHERE AccountName = @AccountID)
    BEGIN
        RAISERROR(N'Tài khoản không tồn tại!', 16, 1);
        RETURN;
    END
    
    INSERT INTO Invoice (Name, TableID, Total, Discount, Tax, Status, AccountID, CheckoutDate)
    VALUES (@Name, @TableID, @Total, @Discount, @Tax, 0, @AccountID, GETDATE());
    
    -- Cập nhật trạng thái bàn thành có khách
    UPDATE [Table] SET Status = 2 WHERE ID = @TableID;
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật Invoice
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_Update')
    DROP PROCEDURE sp_Invoice_Update;
GO

CREATE PROCEDURE sp_Invoice_Update
    @ID INT,
    @Name NVARCHAR(200),
    @Total INT,
    @Discount FLOAT,
    @Tax FLOAT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM Invoice WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    UPDATE Invoice
    SET Name = @Name,
        Total = @Total,
        Discount = @Discount,
        Tax = @Tax
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Thanh toán Invoice
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_Checkout')
    DROP PROCEDURE sp_Invoice_Checkout;
GO

CREATE PROCEDURE sp_Invoice_Checkout
    @ID INT,
    @Discount FLOAT = 0,
    @Tax FLOAT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @TableID INT;
    DECLARE @Total INT;
    
    -- Kiểm tra hóa đơn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Invoice WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra hóa đơn đã thanh toán chưa
    IF EXISTS (SELECT 1 FROM Invoice WHERE ID = @ID AND Status = 1)
    BEGIN
        RAISERROR(N'Hóa đơn đã được thanh toán!', 16, 1);
        RETURN;
    END
    
    -- Lấy TableID và tính tổng tiền
    SELECT @TableID = TableID FROM Invoice WHERE ID = @ID;
    
    SELECT @Total = SUM(id.Amount * f.Price)
    FROM InvoiceDetails id
    INNER JOIN Food f ON id.FoodID = f.ID
    WHERE id.InvoiceID = @ID;
    
    SET @Total = ISNULL(@Total, 0);
    
    -- Áp dụng giảm giá và thuế
    SET @Total = @Total * (1 - @Discount) * (1 + @Tax);
    
    -- Cập nhật hóa đơn
    UPDATE Invoice
    SET Status = 1,
        Total = @Total,
        Discount = @Discount,
        Tax = @Tax,
        CheckoutDate = GETDATE()
    WHERE ID = @ID;
    
    -- Cập nhật trạng thái bàn về trống
    UPDATE [Table] SET Status = 0 WHERE ID = @TableID;
    
    SELECT @Total AS TotalAmount, @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa Invoice
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_Delete')
    DROP PROCEDURE sp_Invoice_Delete;
GO

CREATE PROCEDURE sp_Invoice_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @TableID INT;
    
    IF NOT EXISTS (SELECT 1 FROM Invoice WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra hóa đơn đã thanh toán chưa
    IF EXISTS (SELECT 1 FROM Invoice WHERE ID = @ID AND Status = 1)
    BEGIN
        RAISERROR(N'Không thể xóa hóa đơn đã thanh toán!', 16, 1);
        RETURN;
    END
    
    -- Lấy TableID
    SELECT @TableID = TableID FROM Invoice WHERE ID = @ID;
    
    -- Xóa chi tiết hóa đơn trước
    DELETE FROM InvoiceDetails WHERE InvoiceID = @ID;
    
    -- Xóa hóa đơn
    DELETE FROM Invoice WHERE ID = @ID;
    
    -- Cập nhật trạng thái bàn về trống
    UPDATE [Table] SET Status = 0 WHERE ID = @TableID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Lấy chi tiết hóa đơn
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_GetDetails')
    DROP PROCEDURE sp_Invoice_GetDetails;
GO

CREATE PROCEDURE sp_Invoice_GetDetails
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
-- Procedure: Thêm món vào hóa đơn
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetails_Insert')
    DROP PROCEDURE sp_InvoiceDetails_Insert;
GO

CREATE PROCEDURE sp_InvoiceDetails_Insert
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
    
    -- Kiểm tra món đã có trong hóa đơn chưa
    IF EXISTS (SELECT 1 FROM InvoiceDetails WHERE InvoiceID = @InvoiceID AND FoodID = @FoodID)
    BEGIN
        -- Nếu đã có thì cập nhật số lượng
        UPDATE InvoiceDetails
        SET Amount = Amount + @Amount
        WHERE InvoiceID = @InvoiceID AND FoodID = @FoodID;
    END
    ELSE
    BEGIN
        -- Nếu chưa có thì thêm mới
        INSERT INTO InvoiceDetails (InvoiceID, FoodID, Amount)
        VALUES (@InvoiceID, @FoodID, @Amount);
    END
    
    SELECT SCOPE_IDENTITY() AS NewID;
END
GO

-- =============================================
-- Procedure: Cập nhật số lượng món trong hóa đơn
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetails_Update')
    DROP PROCEDURE sp_InvoiceDetails_Update;
GO

CREATE PROCEDURE sp_InvoiceDetails_Update
    @ID INT,
    @Amount INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM InvoiceDetails WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Chi tiết hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END
    
    IF @Amount <= 0
    BEGIN
        -- Nếu số lượng <= 0 thì xóa món
        DELETE FROM InvoiceDetails WHERE ID = @ID;
    END
    ELSE
    BEGIN
        -- Cập nhật số lượng
        UPDATE InvoiceDetails
        SET Amount = @Amount
        WHERE ID = @ID;
    END
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Xóa món khỏi hóa đơn
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InvoiceDetails_Delete')
    DROP PROCEDURE sp_InvoiceDetails_Delete;
GO

CREATE PROCEDURE sp_InvoiceDetails_Delete
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM InvoiceDetails WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- =============================================
-- Procedure: Tính tổng tiền hóa đơn
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_CalculateTotal')
    DROP PROCEDURE sp_Invoice_CalculateTotal;
GO

CREATE PROCEDURE sp_Invoice_CalculateTotal
    @InvoiceID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Total INT;
    DECLARE @Discount FLOAT;
    DECLARE @Tax FLOAT;
    DECLARE @FinalTotal INT;
    
    -- Lấy thông tin giảm giá và thuế
    SELECT @Discount = Discount, @Tax = Tax
    FROM Invoice
    WHERE ID = @InvoiceID;
    
    -- Tính tổng tiền trước giảm giá và thuế
    SELECT @Total = SUM(id.Amount * f.Price)
    FROM InvoiceDetails id
    INNER JOIN Food f ON id.FoodID = f.ID
    WHERE id.InvoiceID = @InvoiceID;
    
    SET @Total = ISNULL(@Total, 0);
    
    -- Áp dụng giảm giá và thuế
    SET @FinalTotal = @Total * (1 - ISNULL(@Discount, 0)) * (1 + ISNULL(@Tax, 0));
    
    SELECT 
        @Total AS SubTotal,
        @Discount AS Discount,
        @Tax AS Tax,
        @FinalTotal AS Total;
END
GO

-- =============================================
-- Procedure: Lấy danh sách hóa đơn theo ngày
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_GetByDate')
    DROP PROCEDURE sp_Invoice_GetByDate;
GO

CREATE PROCEDURE sp_Invoice_GetByDate
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        i.ID,
        i.Name,
        i.TableID,
        t.Name AS TableName,
        i.Total,
        i.Discount,
        i.Tax,
        i.Status,
        i.AccountID,
        a.FullName AS AccountName,
        i.CheckoutDate
    FROM Invoice i
    INNER JOIN [Table] t ON i.TableID = t.ID
    INNER JOIN Account a ON i.AccountID = a.AccountName
    WHERE i.CheckoutDate BETWEEN @FromDate AND @ToDate
    ORDER BY i.CheckoutDate DESC;
END
GO

-- =============================================
-- Procedure: Thống kê doanh thu theo ngày
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Invoice_GetRevenue')
    DROP PROCEDURE sp_Invoice_GetRevenue;
GO

CREATE PROCEDURE sp_Invoice_GetRevenue
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CAST(CheckoutDate AS DATE) AS Date,
        COUNT(*) AS TotalInvoices,
        SUM(Total) AS TotalRevenue,
        AVG(Total) AS AverageRevenue
    FROM Invoice
    WHERE Status = 1 
        AND CheckoutDate BETWEEN @FromDate AND @ToDate
    GROUP BY CAST(CheckoutDate AS DATE)
    ORDER BY Date DESC;
END
GO

PRINT N'Tạo các stored procedures quản lý Invoice thành công!';
GO
