-- Chuyển về database master để có thể drop database
USE master;
GO

-- Kiểm tra và drop database nếu đã tồn tại
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'chuong_6_restaurant_management')
BEGIN
    -- Đóng tất cả connections đến database
    ALTER DATABASE chuong_6_restaurant_management SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    -- Drop database
    DROP DATABASE chuong_6_restaurant_management;
END
GO

-- Tạo cơ sở dữ liệu
CREATE DATABASE chuong_6_restaurant_management;
GO

-- Sử dụng cơ sở dữ liệu vừa tạo
USE chuong_6_restaurant_management;
GO

-- Tạo bảng Restaurant
CREATE TABLE Restaurant (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL DEFAULT N'Chưa đặt tên',
    Address NVARCHAR(500) NOT NULL DEFAULT N'Chưa có địa chỉ',
    Phone NVARCHAR(20) NOT NULL DEFAULT '02633...',
    Website NVARCHAR(200) NULL
);
GO

-- Tạo bảng Hall
CREATE TABLE Hall (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL DEFAULT N'Chưa đặt tên',
    RestaurantID INT NOT NULL,
    CONSTRAINT FK_Hall_Restaurant FOREIGN KEY (RestaurantID) 
        REFERENCES Restaurant(ID)
);
GO

-- Tạo bảng Table
CREATE TABLE [Table] (
    ID INT PRIMARY KEY IDENTITY(1,1),
    TableCode NVARCHAR(50) NOT NULL,
    Name NVARCHAR(200) NULL,
    Status INT NOT NULL DEFAULT 0 CHECK (Status IN (0, 1, 2)), -- 0: chưa đặt, 1: đã đặt, 2: có khách
    Seats INT NULL,
    HallID INT NOT NULL,
    CONSTRAINT FK_Table_Hall FOREIGN KEY (HallID) 
        REFERENCES Hall(ID)
);
GO

-- Tạo bảng FoodCategory
CREATE TABLE FoodCategory (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL DEFAULT N'Chưa đặt tên',
    Type INT NOT NULL CHECK (Type IN (1, 2)) -- 1: đồ ăn, 2: thức uống
);
GO

-- Tạo bảng Food
CREATE TABLE Food (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL DEFAULT N'Chưa đặt tên',
    Unit NVARCHAR(50) NOT NULL,
    FoodCategoryID INT NOT NULL,
    Price INT NOT NULL DEFAULT 0,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT FK_Food_FoodCategory FOREIGN KEY (FoodCategoryID) 
        REFERENCES FoodCategory(ID)
);
GO

-- Tạo bảng Account
CREATE TABLE Account (
    AccountName NVARCHAR(50) PRIMARY KEY,
    Password NVARCHAR(200) NOT NULL,
    FullName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NOT NULL,
    DateCreated SMALLDATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Tạo bảng Invoice
CREATE TABLE Invoice (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL DEFAULT N'Hóa đơn chưa thanh toán',
    TableID INT NOT NULL,
    Total INT NOT NULL DEFAULT 0,
    Discount FLOAT NULL DEFAULT 0,
    Tax FLOAT NULL DEFAULT 0,
    Status BIT NOT NULL DEFAULT 0, -- 0: chưa thanh toán, 1: đã thanh toán
    AccountID NVARCHAR(50) NOT NULL DEFAULT '1',
    CheckoutDate SMALLDATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Invoice_Table FOREIGN KEY (TableID) 
        REFERENCES [Table](ID),
    CONSTRAINT FK_Invoice_Account FOREIGN KEY (AccountID) 
        REFERENCES Account(AccountName)
);
GO

-- Tạo bảng InvoiceDetails
CREATE TABLE InvoiceDetails (
    ID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT NOT NULL,
    FoodID INT NOT NULL,
    Amount INT NOT NULL DEFAULT 0,
    CONSTRAINT FK_InvoiceDetails_Invoice FOREIGN KEY (InvoiceID) 
        REFERENCES Invoice(ID),
    CONSTRAINT FK_InvoiceDetails_Food FOREIGN KEY (FoodID) 
        REFERENCES Food(ID)
);
GO

-- Tạo bảng Role
CREATE TABLE Role (
    ID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(100) NOT NULL,
    Path NVARCHAR(500) NULL,
    Notes NVARCHAR(500) NULL
);
GO

-- Tạo bảng RoleAccount
CREATE TABLE RoleAccount (
    AccountName NVARCHAR(50) NOT NULL,
    RoleID INT NOT NULL,
    Actived BIT NOT NULL DEFAULT 1,
    Notes NVARCHAR(500) NULL,
    PRIMARY KEY (AccountName, RoleID),
    CONSTRAINT FK_RoleAccount_Account FOREIGN KEY (AccountName) 
        REFERENCES Account(AccountName),
    CONSTRAINT FK_RoleAccount_Role FOREIGN KEY (RoleID) 
        REFERENCES Role(ID)
);
GO

-- Insert dữ liệu mẫu cho bảng Restaurant
INSERT INTO Restaurant (Name, Address, Phone, Website)
VALUES 
    (N'Nhà hàng ABC', N'123 Đường ABC, TP.HCM', '02633123456', 'www.nhahangabc.com'),
    (N'Nhà hàng XYZ', N'456 Đường XYZ, Hà Nội', '02633654321', NULL);
GO

-- Insert dữ liệu mẫu cho bảng Hall
INSERT INTO Hall (Name, RestaurantID)
VALUES 
    (N'Tầng 1', 1),
    (N'Tầng 2', 1),
    (N'Sảnh chính', 2);
GO

-- Insert dữ liệu mẫu cho bảng Table
INSERT INTO [Table] (TableCode, Name, Status, Seats, HallID)
VALUES 
    ('T01', N'Bàn 1', 0, 4, 1),
    ('T02', N'Bàn 2', 0, 6, 1),
    ('T03', N'Bàn 3', 0, 8, 2),
    ('T04', N'Bàn 4', 0, 4, 3);
GO

-- Insert dữ liệu mẫu cho bảng FoodCategory
INSERT INTO FoodCategory (Name, Type)
VALUES 
    (N'Món khai vị', 1),
    (N'Món chính', 1),
    (N'Tráng miệng', 1),
    (N'Nước ngọt', 2),
    (N'Bia', 2);
GO

-- Insert dữ liệu mẫu cho bảng Food
INSERT INTO Food (Name, Unit, FoodCategoryID, Price, Notes)
VALUES 
    (N'Gỏi cuốn', N'Đĩa', 1, 30000, N'2 cuốn'),
    (N'Nem rán', N'Đĩa', 1, 35000, N'3 nem'),
    (N'Phở bò', N'Tô', 2, 50000, NULL),
    (N'Cơm gà', N'Đĩa', 2, 45000, NULL),
    (N'Chè khúc bạch', N'Tô', 3, 25000, NULL),
    (N'Coca Cola', N'Lon', 4, 15000, NULL),
    (N'Bia Sài Gòn', N'Chai', 5, 20000, NULL);
GO

-- Insert dữ liệu mẫu cho bảng Account
INSERT INTO Account (AccountName, Password, FullName, Email, Phone, DateCreated)
VALUES 
    ('admin', '123456', N'Quản trị viên', 'admin@restaurant.com', '0123456789', GETDATE()),
    ('nhanvien1', '123456', N'Nhân viên 1', 'nv1@restaurant.com', '0987654321', GETDATE()),
    ('nhanvien2', '123456', N'Nhân viên 2', NULL, '0912345678', GETDATE());
GO

-- Insert dữ liệu mẫu cho bảng Role
INSERT INTO Role (RoleName, Path, Notes)
VALUES 
    (N'Quản trị viên', '/admin', N'Toàn quyền'),
    (N'Nhân viên phục vụ', '/staff', N'Xem và cập nhật đơn hàng'),
    (N'Thu ngân', '/cashier', N'Thanh toán hóa đơn');
GO

-- Insert dữ liệu mẫu cho bảng RoleAccount
INSERT INTO RoleAccount (AccountName, RoleID, Actived, Notes)
VALUES 
    ('admin', 1, 1, N'Tài khoản admin chính'),
    ('nhanvien1', 2, 1, NULL),
    ('nhanvien2', 3, 1, NULL);
GO

PRINT N'tạo cơ sở dữ liệu thành công!';
GO
