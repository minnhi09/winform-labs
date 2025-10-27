USE [RestaurantManagement]
GO

-- Insert Categories
SET IDENTITY_INSERT [dbo].[Category] ON
INSERT INTO [dbo].[Category] ([ID], [Name], [Type]) VALUES
(1, N'Món khai vị', 0),
(2, N'Món chính', 0),
(3, N'Món tráng miệng', 0),
(4, N'Nước giải khát', 1),
(5, N'Đồ uống có cồn', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO

-- Insert Food
SET IDENTITY_INSERT [dbo].[Food] ON
INSERT INTO [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES
(1, N'Gỏi cuốn', N'Phần', 1, 35000, N'2 cuốn'),
(2, N'Chả giò', N'Phần', 1, 40000, N'4 chiếc'),
(3, N'Phở bò', N'Tô', 2, 55000, NULL),
(4, N'Cơm gà Hải Nam', N'Dĩa', 2, 60000, NULL),
(5, N'Bún chả Hà Nội', N'Phần', 2, 50000, NULL),
(6, N'Mì xào hải sản', N'Dĩa', 2, 65000, NULL),
(7, N'Chè ba màu', N'Ly', 3, 20000, NULL),
(8, N'Kem flan', N'Ly', 3, 15000, NULL),
(9, N'Nước cam ép', N'Ly', 4, 25000, NULL),
(10, N'Trà đá', N'Ly', 4, 5000, NULL),
(11, N'Cà phê sữa', N'Ly', 4, 20000, NULL),
(12, N'Bia Sài Gòn', N'Chai', 5, 18000, NULL),
(13, N'Bia Tiger', N'Chai', 5, 20000, NULL)
SET IDENTITY_INSERT [dbo].[Food] OFF
GO

-- Insert Tables
SET IDENTITY_INSERT [dbo].[Table] ON
INSERT INTO [dbo].[Table] ([ID], [Name], [Status], [Notes]) VALUES
(1, N'Bàn 1', N'Trống', NULL),
(2, N'Bàn 2', N'Trống', NULL),
(3, N'Bàn 3', N'Đang sử dụng', NULL),
(4, N'Bàn 4', N'Trống', NULL),
(5, N'Bàn 5', N'Đang sử dụng', NULL),
(6, N'Bàn VIP 1', N'Trống', N'Phòng riêng'),
(7, N'Bàn VIP 2', N'Đã đặt', N'Phòng riêng'),
(8, N'Bàn 6', N'Trống', NULL),
(9, N'Bàn 7', N'Trống', NULL),
(10, N'Bàn 8', N'Trống', NULL)
SET IDENTITY_INSERT [dbo].[Table] OFF
GO

-- Insert Roles
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT INTO [dbo].[Role] ([ID], [RoleName], [Description]) VALUES
(1, N'Admin', N'Quản trị viên hệ thống'),
(2, N'Manager', N'Quản lý nhà hàng'),
(3, N'Waiter', N'Nhân viên phục vụ'),
(4, N'Cashier', N'Thu ngân')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO

-- Insert Accounts
SET IDENTITY_INSERT [dbo].[Account] ON
INSERT INTO [dbo].[Account] ([ID], [Username], [Password], [DisplayName], [Phone], [IsActive], [Notes]) VALUES
(1, N'admin', N'admin123', N'Nguyễn Văn Admin', N'0901234567', 1, NULL),
(2, N'manager1', N'manager123', N'Trần Thị Lan', N'0912345678', 1, NULL),
(3, N'waiter1', N'waiter123', N'Lê Văn Hùng', N'0923456789', 1, NULL),
(4, N'waiter2', N'waiter123', N'Phạm Thị Mai', N'0934567890', 1, NULL),
(5, N'cashier1', N'cashier123', N'Hoàng Văn Nam', N'0945678901', 1, NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO

-- Insert AccountRole
INSERT INTO [dbo].[AccountRole] ([AccountID], [RoleID]) VALUES
(1, 1), -- Admin có role Admin
(1, 2), -- Admin có role Manager
(2, 2), -- Manager1 có role Manager
(3, 3), -- Waiter1 có role Waiter
(4, 3), -- Waiter2 có role Waiter
(5, 4)  -- Cashier1 có role Cashier
GO

-- Insert Bills
SET IDENTITY_INSERT [dbo].[Bill] ON
INSERT INTO [dbo].[Bill] ([ID], [TableID], [AccountID], [DateCheckIn], [DateCheckOut], [Status], [TotalAmount], [Discount]) VALUES
(1, 3, 3, '2025-11-26 11:30:00', '2025-11-26 12:15:00', 1, 195000, 0),
(2, 5, 4, '2025-11-26 12:00:00', NULL, 0, 0, 0),
(3, 7, 3, '2025-11-26 18:00:00', NULL, 0, 0, 5)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO

-- Insert BillDetails
SET IDENTITY_INSERT [dbo].[BillDetail] ON
INSERT INTO [dbo].[BillDetail] ([ID], [BillID], [FoodID], [Quantity], [Price], [Notes]) VALUES
-- Bill 1 (đã thanh toán)
(1, 1, 3, 2, 55000, NULL),         -- 2 tô phở bò
(2, 1, 9, 2, 25000, NULL),         -- 2 ly nước cam
(3, 1, 2, 1, 40000, NULL),         -- 1 phần chả giò
-- Bill 2 (chưa thanh toán)
(4, 2, 4, 1, 60000, NULL),         -- 1 dĩa cơm gà
(5, 2, 5, 1, 50000, NULL),         -- 1 phần bún chả
(6, 2, 11, 2, 20000, NULL),        -- 2 ly cà phê
-- Bill 3 (chưa thanh toán, có giảm giá 5%)
(7, 3, 6, 2, 65000, NULL),         -- 2 dĩa mì xào
(8, 3, 1, 1, 35000, NULL),         -- 1 phần gỏi cuốn
(9, 3, 13, 4, 20000, NULL),        -- 4 chai bia Tiger
(10, 3, 7, 2, 20000, NULL)         -- 2 ly chè ba màu
SET IDENTITY_INSERT [dbo].[BillDetail] OFF
GO

PRINT N'Dữ liệu mẫu đã được khởi tạo thành công!'
GO
