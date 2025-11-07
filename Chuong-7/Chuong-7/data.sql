USE chuong_7_entity_framework;
GO

-- Xóa dữ liệu cũ nếu có (theo thứ tự để tránh lỗi foreign key)
-- Sử dụng DELETE cho các bảng có Foreign Key
DELETE FROM InvoiceDetails;
DELETE FROM Invoice;
DELETE FROM RoleAccount;
DELETE FROM Role;
DELETE FROM Account;
DELETE FROM Food;
DELETE FROM FoodCategory;
DELETE FROM [Table];
DELETE FROM Hall;
DELETE FROM Restaurant;

-- Reset IDENTITY về 0 cho tất cả các bảng (sẽ bắt đầu từ 1 khi insert)
IF EXISTS (SELECT * FROM Restaurant)
    DBCC CHECKIDENT ('Restaurant', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('Restaurant', RESEED, 0) WITH NO_INFOMSGS;

IF EXISTS (SELECT * FROM Hall)
    DBCC CHECKIDENT ('Hall', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('Hall', RESEED, 0) WITH NO_INFOMSGS;

IF EXISTS (SELECT * FROM [Table])
    DBCC CHECKIDENT ('[Table]', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('[Table]', RESEED, 0) WITH NO_INFOMSGS;

IF EXISTS (SELECT * FROM FoodCategory)
    DBCC CHECKIDENT ('FoodCategory', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('FoodCategory', RESEED, 0) WITH NO_INFOMSGS;

IF EXISTS (SELECT * FROM Food)
    DBCC CHECKIDENT ('Food', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('Food', RESEED, 0) WITH NO_INFOMSGS;

IF EXISTS (SELECT * FROM Role)
    DBCC CHECKIDENT ('Role', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('Role', RESEED, 0) WITH NO_INFOMSGS;

IF EXISTS (SELECT * FROM Invoice)
    DBCC CHECKIDENT ('Invoice', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('Invoice', RESEED, 0) WITH NO_INFOMSGS;

IF EXISTS (SELECT * FROM InvoiceDetails)
    DBCC CHECKIDENT ('InvoiceDetails', RESEED, 0);
ELSE
    DBCC CHECKIDENT ('InvoiceDetails', RESEED, 0) WITH NO_INFOMSGS;
GO

-- Insert dữ liệu mẫu cho bảng Restaurant
INSERT INTO Restaurant (Name, Address, Phone, Website)
VALUES 
    (N'Nhà hàng Thiên Hương', N'123 Nguyễn Huệ, Quận 1, TP.HCM', '02633123456', 'www.thienhương.com'),
    (N'Nhà hàng Hoa Sen', N'456 Trần Hưng Đạo, Hoàn Kiếm, Hà Nội', '02433654321', 'www.hoasen.vn'),
    (N'Nhà hàng Bình Minh', N'789 Lê Lợi, TP. Đà Nẵng', '02363111222', NULL);
GO

-- Insert dữ liệu mẫu cho bảng Hall
INSERT INTO Hall (Name, RestaurantID)
VALUES 
    (N'Tầng 1 - Sảnh chính', 1),
    (N'Tầng 2 - Phòng VIP', 1),
    (N'Tầng 3 - Sân thượng', 1),
    (N'Sảnh A', 2),
    (N'Sảnh B', 2),
    (N'Tầng trệt', 3);
GO

-- Insert dữ liệu mẫu cho bảng Table
INSERT INTO [Table] (TableCode, Name, Status, Seats, HallID)
VALUES 
    ('T01', N'Bàn 1', 0, 4, 1),
    ('T02', N'Bàn 2', 2, 6, 1),
    ('T03', N'Bàn 3', 1, 4, 1),
    ('T04', N'Bàn 4', 0, 8, 1),
    ('T05', N'Bàn 5', 0, 4, 1),
    ('VIP01', N'VIP 1', 0, 10, 2),
    ('VIP02', N'VIP 2', 2, 12, 2),
    ('VIP03', N'VIP 3', 0, 8, 2),
    ('ST01', N'Sân thượng 1', 0, 6, 3),
    ('ST02', N'Sân thượng 2', 0, 6, 3),
    ('A01', N'Bàn A1', 0, 4, 4),
    ('A02', N'Bàn A2', 0, 6, 4),
    ('B01', N'Bàn B1', 0, 4, 5),
    ('B02', N'Bàn B2', 0, 6, 5),
    ('G01', N'Bàn 1', 0, 4, 6),
    ('G02', N'Bàn 2', 0, 6, 6);
GO

-- Insert dữ liệu mẫu cho bảng FoodCategory
INSERT INTO FoodCategory (Name, Type)
VALUES 
    (N'Món khai vị', 1),
    (N'Món chính', 1),
    (N'Món tráng miệng', 1),
    (N'Món ăn vặt', 1),
    (N'Lẩu', 1),
    (N'Nước ngọt', 2),
    (N'Nước ép', 2),
    (N'Trà sữa', 2),
    (N'Cà phê', 2),
    (N'Bia rượu', 2);
GO

-- Insert dữ liệu mẫu cho bảng Food
INSERT INTO Food (Name, Unit, FoodCategoryID, Price, Notes)
VALUES 
    -- Món khai vị (Category 1)
    (N'Gỏi cuốn', N'Phần', 1, 30000, N'4 cuốn/phần'),
    (N'Chả giò', N'Phần', 1, 35000, N'6 cuốn/phần'),
    (N'Nem nướng', N'Phần', 1, 40000, N'10 xiên/phần'),
    (N'Salad trộn', N'Phần', 1, 45000, N'Salad rau củ tươi'),
    (N'Gỏi ngó sen tôm thịt', N'Phần', 1, 55000, N'Đặc sản miền Bắc'),
    
    -- Món chính (Category 2)
    (N'Cơm gà xối mỡ', N'Phần', 2, 50000, N'Gà luộc nguyên con'),
    (N'Cơm sườn nướng', N'Phần', 2, 55000, N'Sườn non nướng'),
    (N'Phở bò tái', N'Tô', 2, 60000, N'Phở bò Hà Nội'),
    (N'Bún chả Hà Nội', N'Phần', 2, 55000, N'Chả nướng than'),
    (N'Mì xào hải sản', N'Phần', 2, 65000, N'Hải sản tươi'),
    (N'Cá kho tộ', N'Phần', 2, 70000, N'Cá lóc kho'),
    (N'Gà nướng mật ong', N'Con', 2, 180000, N'Gà ta nguyên con'),
    (N'Sườn nướng BBQ', N'Kg', 2, 250000, N'Sườn non Mỹ'),
    
    -- Món tráng miệng (Category 3)
    (N'Chè thập cẩm', N'Chén', 3, 25000, N'Chè truyền thống'),
    (N'Kem chiên', N'Phần', 3, 30000, N'3 viên/phần'),
    (N'Yaourt dẻo', N'Hộp', 3, 15000, N'Vị trái cây'),
    (N'Bánh flan', N'Phần', 3, 20000, N'Flan caramel'),
    (N'Rau câu dừa', N'Phần', 3, 18000, NULL),
    
    -- Món ăn vặt (Category 4)
    (N'Khoai tây chiên', N'Phần', 4, 25000, N'Size M'),
    (N'Gà rán', N'Phần', 4, 45000, N'3 miếng/phần'),
    (N'Bánh tráng trộn', N'Phần', 4, 20000, N'Đặc sản Sài Gòn'),
    (N'Bánh xèo', N'Phần', 4, 35000, N'Bánh xèo miền Tây'),
    (N'Chân gà sả tắc', N'Phần', 4, 30000, N'20 chân/phần'),
    
    -- Lẩu (Category 5)
    (N'Lẩu thái', N'Phần', 5, 150000, N'Cho 2-3 người'),
    (N'Lẩu hải sản', N'Phần', 5, 200000, N'Cho 2-3 người'),
    (N'Lẩu bò nhúng dấm', N'Phần', 5, 180000, N'Cho 2-3 người'),
    (N'Lẩu gà lá é', N'Phần', 5, 160000, N'Cho 2-3 người'),
    
    -- Nước ngọt (Category 6)
    (N'Coca Cola', N'Lon', 6, 15000, N'Lon 330ml'),
    (N'Pepsi', N'Lon', 6, 15000, N'Lon 330ml'),
    (N'7Up', N'Lon', 6, 15000, N'Lon 330ml'),
    (N'Sting', N'Lon', 6, 18000, N'Nước tăng lực'),
    (N'Number 1', N'Chai', 6, 12000, N'Chai 500ml'),
    
    -- Nước ép (Category 7)
    (N'Nước cam ép', N'Ly', 7, 25000, N'Cam tươi 100%'),
    (N'Nước dưa hấu ép', N'Ly', 7, 22000, N'Dưa hấu tươi'),
    (N'Sinh tố bơ', N'Ly', 7, 30000, N'Bơ sáp Đà Lạt'),
    (N'Sinh tố dâu', N'Ly', 7, 28000, N'Dâu tây Đà Lạt'),
    (N'Nước chanh dây', N'Ly', 7, 20000, N'Chanh dây tươi'),
    
    -- Trà sữa (Category 8)
    (N'Trà sữa truyền thống', N'Ly', 8, 35000, N'Size M'),
    (N'Trà sữa socola', N'Ly', 8, 38000, N'Size M'),
    (N'Trà sữa matcha', N'Ly', 8, 40000, N'Size M'),
    (N'Trà sữa trân châu đường đen', N'Ly', 8, 42000, N'Size M'),
    
    -- Cà phê (Category 9)
    (N'Cà phê đen', N'Ly', 9, 20000, N'Cà phê phin'),
    (N'Cà phê sữa', N'Ly', 9, 25000, N'Cà phê phin'),
    (N'Bạc xỉu', N'Ly', 9, 28000, N'Cà phê sữa ngọt'),
    (N'Cappuccino', N'Ly', 9, 35000, N'Cà phê Ý'),
    (N'Latte', N'Ly', 9, 38000, N'Cà phê Ý'),
    
    -- Bia rượu (Category 10)
    (N'Bia Sài Gòn', N'Chai', 10, 20000, N'Chai 330ml'),
    (N'Bia Heineken', N'Chai', 10, 25000, N'Chai 330ml'),
    (N'Bia Tiger', N'Chai', 10, 22000, N'Chai 330ml'),
    (N'Rượu vang đỏ', N'Chai', 10, 350000, N'Vang Chile'),
    (N'Rượu Soju', N'Chai', 10, 80000, N'Soju Hàn Quốc');
GO

-- Insert dữ liệu mẫu cho bảng Account
INSERT INTO Account (AccountName, Password, FullName, Email, Phone, DateCreated)
VALUES 
    ('admin', '123456', N'Nguyễn Văn Admin', 'admin@restaurant.com', '0123456789', GETDATE()),
    ('1', '123456', N'Trần Thị Thu', 'thu.tt@restaurant.com', '0987654321', GETDATE()),
    ('nhanvien2', '123456', N'Lê Văn Nam', 'nam.lv@restaurant.com', '0912345678', GETDATE()),
    ('cashier1', '123456', N'Phạm Thị Lan', 'lan.pt@restaurant.com', '0909123456', GETDATE()),
    ('staff1', '123456', N'Hoàng Văn Minh', 'minh.hv@restaurant.com', '0908765432', GETDATE());
GO

-- Insert dữ liệu mẫu cho bảng Role
INSERT INTO Role (RoleName, Path, Notes)
VALUES 
    (N'Quản trị viên', '/admin', N'Toàn quyền quản lý hệ thống'),
    (N'Nhân viên phục vụ', '/staff', N'Xem và cập nhật đơn hàng'),
    (N'Thu ngân', '/cashier', N'Thanh toán hóa đơn'),
    (N'Quản lý kho', '/inventory', N'Quản lý nguyên liệu và kho');
GO

-- Insert dữ liệu mẫu cho bảng RoleAccount
INSERT INTO RoleAccount (AccountName, RoleID, Actived, Notes)
VALUES 
    ('admin', 1, 1, N'Tài khoản admin chính'),
    ('1', 2, 1, N'Nhân viên phục vụ tầng 1'),
    ('nhanvien2', 2, 1, N'Nhân viên phục vụ tầng 2'),
    ('cashier1', 3, 1, N'Thu ngân ca sáng'),
    ('staff1', 2, 1, N'Nhân viên phục vụ sân thượng');
GO

-- Insert dữ liệu mẫu cho bảng Invoice (một số hóa đơn mẫu)
INSERT INTO Invoice (Name, TableID, Total, Discount, Tax, Status, AccountID, CheckoutDate)
VALUES 
    (N'Hóa đơn bàn T02', 2, 175000, 0.00, 0.10, 0, '1', GETDATE()),
    (N'Hóa đơn bàn VIP02', 7, 490000, 0.05, 0.10, 0, '1', GETDATE());
GO

-- Insert dữ liệu mẫu cho bảng InvoiceDetails
-- Lưu ý: Price là giá tại thời điểm order (snapshot price)
INSERT INTO InvoiceDetails (InvoiceID, FoodID, Price, Amount)
VALUES 
    -- Hóa đơn 1 (Bàn T02)
    (1, 8, 60000, 2),  -- 2 tô Phở bò tái (60,000 x 2 = 120,000)
    (1, 29, 15000, 2), -- 2 lon Coca (15,000 x 2 = 30,000)
    (1, 15, 25000, 1), -- 1 chén chè thập cẩm (25,000 x 1 = 25,000)
    -- Tổng: 175,000
    
    -- Hóa đơn 2 (Bàn VIP02)
    (2, 24, 150000, 1), -- 1 phần Lẩu thái (150,000 x 1 = 150,000)
    (2, 12, 180000, 1), -- 1 con Gà nướng mật ong (180,000 x 1 = 180,000)
    (2, 47, 25000, 2),  -- 2 chai Bia Heineken (25,000 x 2 = 50,000)
    (2, 33, 25000, 2),  -- 2 ly Nước cam ép (25,000 x 2 = 50,000)
    (2, 16, 30000, 2);  -- 2 phần Kem chiên (30,000 x 2 = 60,000)
    -- Tổng: 490,000
GO

PRINT N'Thêm dữ liệu mẫu thành công!';
PRINT N'';
PRINT N'Thống kê:';
SELECT N'Restaurant' AS [Bảng], COUNT(*) AS [Số bản ghi] FROM Restaurant
UNION ALL
SELECT N'Hall', COUNT(*) FROM Hall
UNION ALL
SELECT N'Table', COUNT(*) FROM [Table]
UNION ALL
SELECT N'FoodCategory', COUNT(*) FROM FoodCategory
UNION ALL
SELECT N'Food', COUNT(*) FROM Food
UNION ALL
SELECT N'Account', COUNT(*) FROM Account
UNION ALL
SELECT N'Role', COUNT(*) FROM Role
UNION ALL
SELECT N'RoleAccount', COUNT(*) FROM RoleAccount
UNION ALL
SELECT N'Invoice', COUNT(*) FROM Invoice
UNION ALL
SELECT N'InvoiceDetails', COUNT(*) FROM InvoiceDetails;
GO
