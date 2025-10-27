-- =============================================
-- Script: Insert Sample Data for Chuong_5_Lab_Advanced_Command
-- Description: Sample data for Food Management System
-- Date: October 27, 2025
-- Note: Run this script AFTER running tables.sql
-- =============================================

USE Chuong_5_Lab_Advanced_Command;
GO

-- =============================================
-- Clear existing data (if any)
-- =============================================
-- Delete in correct order (child tables first)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'BillDetails')
    DELETE FROM BillDetails;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Bills')
    DELETE FROM Bills;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AccountRoles')
    DELETE FROM AccountRoles;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Foods')
    DELETE FROM Foods;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Accounts')
    DELETE FROM Accounts;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
    DELETE FROM Roles;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
    DELETE FROM Categories;

-- Reset identity columns
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'BillDetails')
    DBCC CHECKIDENT ('BillDetails', RESEED, 0);

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Bills')
    DBCC CHECKIDENT ('Bills', RESEED, 0);

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AccountRoles')
    DBCC CHECKIDENT ('AccountRoles', RESEED, 0);

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Foods')
    DBCC CHECKIDENT ('Foods', RESEED, 0);

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Accounts')
    DBCC CHECKIDENT ('Accounts', RESEED, 0);

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
    DBCC CHECKIDENT ('Roles', RESEED, 0);

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
    DBCC CHECKIDENT ('Categories', RESEED, 0);

PRINT 'Existing data cleared.';
GO

-- =============================================
-- Insert Sample Data into Categories
-- =============================================
INSERT INTO Categories (Name, Notes) VALUES
    (N'Món chính', N'Các món ăn chính như cơm, phở, bún'),
    (N'Đồ uống', N'Các loại nước uống, nước ngọt, trà, cà phê'),
    (N'Tráng miệng', N'Các món ăn tráng miệng, chè, kem'),
    (N'Khai vị', N'Các món khai vị, gỏi, salad'),
    (N'Món nướng', N'Các món nướng, BBQ');

PRINT 'Sample data inserted into Categories: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' rows.';
GO

-- =============================================
-- Insert Sample Data into Foods
-- =============================================
INSERT INTO Foods (Name, Unit, FoodCategoryID, Price, Notes) VALUES
    -- Món chính
    (N'Cơm gà xối mở', N'Đĩa', 1, 45000, N'Cơm gà Hải Nam truyền thống'),
    (N'Phở bò đặc biệt', N'Tô', 1, 50000, N'Phở bò với đầy đủ topping'),
    (N'Bún chả Hà Nội', N'Phần', 1, 40000, N'Bún chả nướng than hoa'),
    (N'Cơm tấm sườn bì chả', N'Đĩa', 1, 35000, N'Cơm tấm Sài Gòn'),
    (N'Mì Quảng', N'Tô', 1, 38000, N'Mì Quảng Đà Nẵng'),
    
    -- Đồ uống
    (N'Trà đá', N'Ly', 2, 5000, N'Trà đá miễn phí khi gọi món'),
    (N'Cà phê sữa đá', N'Ly', 2, 25000, N'Cà phê phin truyền thống'),
    (N'Nước cam ép', N'Ly', 2, 20000, N'Cam tươi vắt'),
    (N'Sinh tố bơ', N'Ly', 2, 30000, N'Sinh tố bơ sáp'),
    (N'Trà sữa trân châu', N'Ly', 2, 28000, N'Trà sữa trân châu đường đen'),
    
    -- Tráng miệng
    (N'Chè khúc bạch', N'Chén', 3, 15000, N'Chè khúc bạch hạt sen'),
    (N'Kem flan', N'Hộp', 3, 12000, N'Bánh flan caramen'),
    (N'Chè thái', N'Chén', 3, 18000, N'Chè thái trái cây tươi'),
    (N'Yaourt dâu', N'Hộp', 3, 10000, N'Yaourt nếp cẩm dâu tây'),
    
    -- Khai vị
    (N'Gỏi cuốn', N'Cuốn', 4, 8000, N'Gỏi cuốn tôm thịt (2 cuốn)'),
    (N'Salad trộn', N'Đĩa', 4, 25000, N'Salad rau củ tươi'),
    (N'Nem rán', N'Đĩa', 4, 30000, N'Nem rán giòn (5 cái)'),
    
    -- Món nướng
    (N'Sườn nướng BBQ', N'Phần', 5, 60000, N'Sườn heo nướng sốt BBQ'),
    (N'Cá lóc nướng trui', N'Con', 5, 80000, N'Cá lóc nướng ống tre'),
    (N'Gà nướng muối ớt', N'Phần', 5, 55000, N'Gà nướng muối ớt xanh');

PRINT 'Sample data inserted into Foods: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' rows.';
GO

-- =============================================
-- Insert Sample Data into Roles
-- =============================================
INSERT INTO Roles (RoleName, Description) VALUES
    (N'Admin', N'Quản trị viên hệ thống - Toàn quyền'),
    (N'Manager', N'Quản lý nhà hàng - Quản lý nhân viên và doanh thu'),
    (N'Cashier', N'Thu ngân - Thanh toán và in hóa đơn'),
    (N'Waiter', N'Phục vụ - Nhận order và phục vụ khách hàng'),
    (N'Chef', N'Đầu bếp - Quản lý món ăn và bếp');

PRINT 'Sample data inserted into Roles: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' rows.';
GO

-- =============================================
-- Insert Sample Data into Accounts
-- =============================================
INSERT INTO Accounts (Username, Password, FullName, IsActive) VALUES
    (N'admin', N'123456', N'Nguyễn Văn An', 1),
    (N'manager01', N'123456', N'Trần Thị Bình', 1),
    (N'cashier01', N'123456', N'Lê Văn Cường', 1),
    (N'waiter01', N'123456', N'Phạm Thị Dung', 1),
    (N'waiter02', N'123456', N'Hoàng Văn Em', 1),
    (N'chef01', N'123456', N'Võ Thị Phượng', 1),
    (N'staff01', N'123456', N'Đỗ Văn Giang', 0);

PRINT 'Sample data inserted into Accounts: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' rows.';
GO

-- =============================================
-- Insert Sample Data into AccountRoles (Phân quyền)
-- =============================================
INSERT INTO AccountRoles (AccountID, RoleID) VALUES
    -- Admin có tất cả quyền
    (1, 1), -- admin -> Admin
    (1, 2), -- admin -> Manager
    
    -- Manager có quyền quản lý và thu ngân
    (2, 2), -- manager01 -> Manager
    (2, 3), -- manager01 -> Cashier
    
    -- Cashier chỉ có quyền thu ngân
    (3, 3), -- cashier01 -> Cashier
    
    -- Waiter chỉ có quyền phục vụ
    (4, 4), -- waiter01 -> Waiter
    (5, 4), -- waiter02 -> Waiter
    
    -- Chef có quyền đầu bếp và có thể xem order
    (6, 5), -- chef01 -> Chef
    (6, 4), -- chef01 -> Waiter (để xem order)
    
    -- Staff không active nên không có quyền
    (7, 4); -- staff01 -> Waiter (nhưng tài khoản bị khóa)

PRINT 'Sample data inserted into AccountRoles: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' rows.';
GO

-- =============================================
-- Insert Sample Data into Bills (Hóa đơn)
-- =============================================
INSERT INTO Bills (AccountID, DateCheckIn, DateCheckOut, TotalAmount, Discount, FinalAmount, Status) VALUES
    -- Hóa đơn đã thanh toán
    (4, '2025-10-27 10:30:00', '2025-10-27 11:15:00', 95000, 0, 95000, N'Đã thanh toán'),
    (4, '2025-10-27 11:00:00', '2025-10-27 12:00:00', 180000, 10000, 170000, N'Đã thanh toán'),
    (5, '2025-10-27 12:30:00', '2025-10-27 13:45:00', 250000, 25000, 225000, N'Đã thanh toán'),
    
    -- Hóa đơn chưa thanh toán (đang phục vụ)
    (4, '2025-10-27 13:00:00', NULL, 155000, 0, 155000, N'Chưa thanh toán'),
    (5, '2025-10-27 14:00:00', NULL, 320000, 0, 320000, N'Chưa thanh toán');

PRINT 'Sample data inserted into Bills: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' rows.';
GO

-- =============================================
-- Insert Sample Data into BillDetails (Chi tiết hóa đơn)
-- =============================================
INSERT INTO BillDetails (BillID, FoodID, Quantity, Price, TotalPrice) VALUES
    -- Hóa đơn 1 (Đã thanh toán)
    (1, 1, 1, 45000, 45000),  -- Cơm gà
    (1, 2, 1, 50000, 50000),  -- Phở bò
    
    -- Hóa đơn 2 (Đã thanh toán)
    (2, 3, 2, 40000, 80000),  -- Bún chả x2
    (2, 7, 2, 25000, 50000),  -- Cà phê x2
    (2, 2, 1, 50000, 50000),  -- Phở bò
    
    -- Hóa đơn 3 (Đã thanh toán)
    (3, 4, 3, 35000, 105000), -- Cơm tấm x3
    (3, 18, 1, 60000, 60000), -- Sườn nướng
    (3, 8, 2, 20000, 40000),  -- Nước cam x2
    (3, 1, 1, 45000, 45000),  -- Cơm gà
    
    -- Hóa đơn 4 (Chưa thanh toán)
    (4, 5, 2, 38000, 76000),  -- Mì Quảng x2
    (4, 10, 3, 28000, 84000), -- Trà sữa x3
    (4, 13, 1, 18000, 18000), -- Chè thái
    
    -- Hóa đơn 5 (Chưa thanh toán)
    (5, 19, 2, 80000, 160000), -- Cá lóc nướng x2
    (5, 1, 4, 45000, 180000),  -- Cơm gà x4
    (5, 11, 2, 15000, 30000),  -- Chè khúc bạch x2
    (5, 9, 4, 30000, 120000);  -- Sinh tố bơ x4

PRINT 'Sample data inserted into BillDetails: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' rows.';
GO

-- =============================================
-- Display inserted data summary
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'Sample data inserted successfully!';
PRINT '========================================';
PRINT '';

-- Show row counts
SELECT 
    'Categories' as TableName, 
    COUNT(*) as RecordCount 
FROM Categories
UNION ALL
SELECT 
    'Foods' as TableName, 
    COUNT(*) as RecordCount 
FROM Foods
UNION ALL
SELECT 
    'Roles' as TableName, 
    COUNT(*) as RecordCount 
FROM Roles
UNION ALL
SELECT 
    'Accounts' as TableName, 
    COUNT(*) as RecordCount 
FROM Accounts
UNION ALL
SELECT 
    'AccountRoles' as TableName, 
    COUNT(*) as RecordCount 
FROM AccountRoles
UNION ALL
SELECT 
    'Bills' as TableName, 
    COUNT(*) as RecordCount 
FROM Bills
UNION ALL
SELECT 
    'BillDetails' as TableName, 
    COUNT(*) as RecordCount 
FROM BillDetails;
GO

-- Display sample data
PRINT '';
PRINT 'Categories:';
SELECT * FROM Categories;
GO

PRINT '';
PRINT 'Foods:';
SELECT 
    F.ID,
    F.Name,
    F.Unit,
    C.Name as Category,
    F.Price,
    F.Notes
FROM Foods F
INNER JOIN Categories C ON F.FoodCategoryID = C.ID
ORDER BY C.Name, F.Name;
GO

PRINT '';
PRINT 'Roles:';
SELECT * FROM Roles;
GO

PRINT '';
PRINT 'Accounts:';
SELECT 
    ID,
    Username,
    FullName,
    CASE WHEN IsActive = 1 THEN N'Hoạt động' ELSE N'Khóa' END as Status,
    CreatedDate
FROM Accounts;
GO

PRINT '';
PRINT 'Account Roles (Phân quyền):';
SELECT 
    A.Username,
    A.FullName,
    R.RoleName as Role,
    R.Description as RoleDescription
FROM AccountRoles AR
INNER JOIN Accounts A ON AR.AccountID = A.ID
INNER JOIN Roles R ON AR.RoleID = R.ID
ORDER BY A.Username, R.RoleName;
GO

PRINT '';
PRINT 'Bills (Hóa đơn):';
SELECT 
    B.ID,
    A.FullName as [Nhân viên],
    B.DateCheckIn as [Giờ vào],
    B.DateCheckOut as [Giờ ra],
    B.TotalAmount as [Tổng tiền],
    B.Discount as [Giảm giá],
    B.FinalAmount as [Thành tiền],
    B.Status as [Trạng thái]
FROM Bills B
INNER JOIN Accounts A ON B.AccountID = A.ID
ORDER BY B.ID;
GO

PRINT '';
PRINT 'Bill Details (Chi tiết hóa đơn):';
SELECT 
    B.ID as [Hóa đơn],
    F.Name as [Món ăn],
    BD.Quantity as [SL],
    BD.Price as [Đơn giá],
    BD.TotalPrice as [Thành tiền]
FROM BillDetails BD
INNER JOIN Bills B ON BD.BillID = B.ID
INNER JOIN Foods F ON BD.FoodID = F.ID
ORDER BY B.ID, BD.ID;
GO

PRINT '';
PRINT 'Revenue Summary (Tổng doanh thu):';
SELECT 
    COUNT(CASE WHEN Status = N'Đã thanh toán' THEN 1 END) as [Hóa đơn đã thanh toán],
    COUNT(CASE WHEN Status = N'Chưa thanh toán' THEN 1 END) as [Hóa đơn chưa thanh toán],
    SUM(CASE WHEN Status = N'Đã thanh toán' THEN FinalAmount ELSE 0 END) as [Doanh thu],
    SUM(CASE WHEN Status = N'Chưa thanh toán' THEN FinalAmount ELSE 0 END) as [Doanh thu dự kiến]
FROM Bills;
GO
