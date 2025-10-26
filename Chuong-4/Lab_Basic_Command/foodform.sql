USE RestaurantManagement;
GO
-- Bảng món ăn
IF OBJECT_ID('dbo.Food') IS NULL
CREATE TABLE dbo.Food (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(50) NULL,
    FoodCategoryID INT NOT NULL REFERENCES dbo.Category(ID),
    Price DECIMAL(18,2) NOT NULL DEFAULT(0),
    Notes NVARCHAR(255) NULL
);

-- mẫu thử
INSERT INTO Food(Name,Unit,FoodCategoryID,Price,Notes) VALUES
(N'Cá thu rim', N'Phần', 2, 70000, N''),
(N'Ếch xào sả ớt', N'Đĩa', 2, 200000, N'Cay'),
(N'Càng cua hấp', N'Phần', 2, 100000, N'');
