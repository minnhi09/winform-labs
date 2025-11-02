-- =============================================
-- Stored Procedures for Orders Management
-- =============================================

-- 1. Get all bills with account information
GO
CREATE OR ALTER PROCEDURE GetAllBills
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    ORDER BY b.DateCheckIn DESC;
END
GO

-- 2. Get bill by ID with account information
GO
CREATE OR ALTER PROCEDURE GetBillByID
    @BillID INT
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE b.ID = @BillID;
END
GO

-- 3. Get bill details by BillID
GO
CREATE OR ALTER PROCEDURE GetBillDetails
    @BillID INT
AS
BEGIN
    SELECT 
        bd.ID,
        bd.BillID,
        bd.FoodID,
        f.Name AS FoodName,
        f.Unit,
        bd.Quantity,
        bd.Price,
        bd.TotalPrice
    FROM BillDetails bd
    INNER JOIN Foods f ON bd.FoodID = f.ID
    WHERE bd.BillID = @BillID
    ORDER BY f.Name;
END
GO

-- 4. Get bill details with category information (for OrderDetailsForm)
GO
CREATE OR ALTER PROCEDURE GetBillDetailsWithCategory
    @BillID INT
AS
BEGIN
    SELECT 
        bd.ID,
        f.Name AS FoodName,
        c.Name AS CategoryName,
        f.Unit,
        bd.Quantity,
        bd.Price,
        bd.TotalPrice
    FROM BillDetails bd
    INNER JOIN Foods f ON bd.FoodID = f.ID
    INNER JOIN Categories c ON f.FoodCategoryID = c.ID
    WHERE bd.BillID = @BillID
    ORDER BY c.Name, f.Name;
END
GO

-- 5. Update bill information
GO
CREATE OR ALTER PROCEDURE UpdateBill
    @ID INT,
    @DateCheckOut DATETIME,
    @Discount INT,
    @FinalAmount INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE Bills
    SET DateCheckOut = @DateCheckOut,
        Discount = @Discount,
        FinalAmount = @FinalAmount,
        Status = @Status
    WHERE ID = @ID;
    
    -- Return number of rows affected
    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- 6. Delete bill (will cascade delete bill details if foreign key is set)
GO
CREATE OR ALTER PROCEDURE DeleteBill
    @ID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        
        -- Delete bill details first
        DELETE FROM BillDetails WHERE BillID = @ID;
        
        -- Delete bill
        DELETE FROM Bills WHERE ID = @ID;
        
        COMMIT TRANSACTION
        
        -- Return success
        SELECT 1 AS Success, 'Xóa hóa đơn thành công' AS Message;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        
        -- Return error
        SELECT 0 AS Success, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO

-- 7. Insert new bill
GO
CREATE OR ALTER PROCEDURE InsertBill
    @AccountID INT,
    @DateCheckIn DATETIME,
    @DateCheckOut DATETIME = NULL,
    @TotalAmount INT = 0,
    @Discount INT = 0,
    @FinalAmount INT = 0,
    @Status NVARCHAR(50) = N'Chưa thanh toán'
AS
BEGIN
    INSERT INTO Bills (AccountID, DateCheckIn, DateCheckOut, TotalAmount, Discount, FinalAmount, Status)
    VALUES (@AccountID, @DateCheckIn, @DateCheckOut, @TotalAmount, @Discount, @FinalAmount, @Status);
    
    -- Return the newly created bill ID
    SELECT SCOPE_IDENTITY() AS NewBillID;
END
GO

-- 8. Insert bill detail
GO
CREATE OR ALTER PROCEDURE InsertBillDetail
    @BillID INT,
    @FoodID INT,
    @Quantity INT,
    @Price INT,
    @TotalPrice INT
AS
BEGIN
    INSERT INTO BillDetails (BillID, FoodID, Quantity, Price, TotalPrice)
    VALUES (@BillID, @FoodID, @Quantity, @Price, @TotalPrice);
    
    -- Return the newly created detail ID
    SELECT SCOPE_IDENTITY() AS NewDetailID;
END
GO

-- 9. Update bill detail
GO
CREATE OR ALTER PROCEDURE UpdateBillDetail
    @ID INT,
    @Quantity INT,
    @Price INT,
    @TotalPrice INT
AS
BEGIN
    UPDATE BillDetails
    SET Quantity = @Quantity,
        Price = @Price,
        TotalPrice = @TotalPrice
    WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- 10. Delete bill detail
GO
CREATE OR ALTER PROCEDURE DeleteBillDetail
    @ID INT
AS
BEGIN
    DELETE FROM BillDetails WHERE ID = @ID;
    
    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- 11. Calculate and update bill totals
GO
CREATE OR ALTER PROCEDURE CalculateBillTotals
    @BillID INT
AS
BEGIN
    DECLARE @TotalAmount INT;
    
    -- Calculate total from bill details
    SELECT @TotalAmount = ISNULL(SUM(TotalPrice), 0)
    FROM BillDetails
    WHERE BillID = @BillID;
    
    -- Update bill with calculated total
    UPDATE Bills
    SET TotalAmount = @TotalAmount,
        FinalAmount = @TotalAmount - Discount
    WHERE ID = @BillID;
    
    -- Return updated bill information
    SELECT 
        ID,
        TotalAmount,
        Discount,
        FinalAmount
    FROM Bills
    WHERE ID = @BillID;
END
GO

-- 12. Search bills by account name or username
GO
CREATE OR ALTER PROCEDURE SearchBills
    @SearchText NVARCHAR(100)
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE a.FullName LIKE N'%' + @SearchText + '%'
       OR a.Username LIKE N'%' + @SearchText + '%'
    ORDER BY b.DateCheckIn DESC;
END
GO

-- 13. Get bills by status
GO
CREATE OR ALTER PROCEDURE GetBillsByStatus
    @Status NVARCHAR(50)
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE b.Status = @Status
    ORDER BY b.DateCheckIn DESC;
END
GO

-- 14. Get bills statistics
GO
CREATE OR ALTER PROCEDURE GetBillsStatistics
AS
BEGIN
    SELECT 
        COUNT(*) AS TotalBills,
        ISNULL(SUM(TotalAmount), 0) AS TotalAmount,
        ISNULL(SUM(Discount), 0) AS TotalDiscount,
        ISNULL(SUM(FinalAmount), 0) AS TotalFinalAmount,
        COUNT(CASE WHEN Status = N'Đã thanh toán' THEN 1 END) AS PaidBills,
        COUNT(CASE WHEN Status = N'Chưa thanh toán' THEN 1 END) AS UnpaidBills
    FROM Bills;
END
GO

-- 15. Get bills statistics by status filter
GO
CREATE OR ALTER PROCEDURE GetBillsStatisticsByStatus
    @Status NVARCHAR(50) = NULL
AS
BEGIN
    IF @Status IS NULL OR @Status = N'Tất cả'
    BEGIN
        -- Get all bills statistics
        SELECT 
            COUNT(*) AS TotalBills,
            ISNULL(SUM(TotalAmount), 0) AS TotalAmount,
            ISNULL(SUM(Discount), 0) AS TotalDiscount,
            ISNULL(SUM(FinalAmount), 0) AS TotalFinalAmount
        FROM Bills;
    END
    ELSE
    BEGIN
        -- Get statistics for specific status
        SELECT 
            COUNT(*) AS TotalBills,
            ISNULL(SUM(TotalAmount), 0) AS TotalAmount,
            ISNULL(SUM(Discount), 0) AS TotalDiscount,
            ISNULL(SUM(FinalAmount), 0) AS TotalFinalAmount
        FROM Bills
        WHERE Status = @Status;
    END
END
GO

-- 16. Get bills by account ID
GO
CREATE OR ALTER PROCEDURE GetBillsByAccountID
    @AccountID INT
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE b.AccountID = @AccountID
    ORDER BY b.DateCheckIn DESC;
END
GO

-- 17. Get bills by date range
GO
CREATE OR ALTER PROCEDURE GetBillsByDateRange
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE b.DateCheckIn BETWEEN @StartDate AND @EndDate
    ORDER BY b.DateCheckIn DESC;
END
GO

-- 18. Update bill status
GO
CREATE OR ALTER PROCEDURE UpdateBillStatus
    @BillID INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE Bills
    SET Status = @Status
    WHERE ID = @BillID;
    
    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- 19. Get unpaid bills
GO
CREATE OR ALTER PROCEDURE GetUnpaidBills
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE b.Status = N'Chưa thanh toán'
    ORDER BY b.DateCheckIn DESC;
END
GO

-- 20. Get paid bills
GO
CREATE OR ALTER PROCEDURE GetPaidBills
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE b.Status = N'Đã thanh toán'
    ORDER BY b.DateCheckIn DESC;
END
GO

PRINT 'Created 20 stored procedures for Orders Management successfully!';

-- 21. Advanced filter: Get bills by date range, status, and search text
GO
CREATE OR ALTER PROCEDURE GetBillsAdvancedFilter
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @Status NVARCHAR(50) = NULL,
    @SearchText NVARCHAR(100) = NULL
AS
BEGIN
    SELECT 
        b.ID,
        b.AccountID,
        a.Username,
        a.FullName AS AccountName,
        b.DateCheckIn,
        b.DateCheckOut,
        b.TotalAmount,
        b.Discount,
        b.FinalAmount,
        b.Status
    FROM Bills b
    INNER JOIN Accounts a ON b.AccountID = a.ID
    WHERE 
        -- Date range filter
        (@StartDate IS NULL OR b.DateCheckIn >= @StartDate)
        AND (@EndDate IS NULL OR b.DateCheckIn <= @EndDate)
        -- Status filter
        AND (@Status IS NULL OR @Status = N'Tất cả' OR b.Status = @Status)
        -- Search filter
        AND (
            @SearchText IS NULL 
            OR @SearchText = '' 
            OR a.FullName LIKE N'%' + @SearchText + '%'
            OR a.Username LIKE N'%' + @SearchText + '%'
        )
    ORDER BY b.DateCheckIn DESC;
END
GO

PRINT 'Created 21 stored procedures for Orders Management successfully!';
