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
DELETE FROM Foods;
DELETE FROM Categories;

-- Reset identity columns
DBCC CHECKIDENT ('Foods', RESEED, 0);
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
    COUNT(*) as RowCount 
FROM Categories
UNION ALL
SELECT 
    'Foods' as TableName, 
    COUNT(*) as RowCount 
FROM Foods;
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
