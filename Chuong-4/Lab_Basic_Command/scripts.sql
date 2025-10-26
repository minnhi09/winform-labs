/*
INSERT INTO Category ([Name], [Type]) VALUES
(N'Cơm phần', 0),
(N'Phở & Bún', 0),
(N'Lẩu', 0),
(N'Nước giải khát', 1),
(N'Trà sữa & Cafe', 1),
(N'Tráng miệng', 0);
*/

/*
INSERT INTO Food ([Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES
(N'Cơm gà xối mỡ', N'Phần', 1, 35000, N'Cơm vàng giòn, gà chiên giòn rụm'),
(N'Cơm sườn bì chả', N'Phần', 1, 40000, N'Sườn nướng, bì, chả trứng'),
(N'Phở bò tái', N'Tô', 2, 45000, N'Nước dùng đậm đà, thịt bò tái mềm'),
(N'Bún chả Hà Nội', N'Tô', 2, 40000, N'Ăn kèm rau sống và nước chấm chua ngọt'),
(N'Lẩu thái hải sản', N'Nồi', 3, 180000, N'Tôm, mực, cá viên, vị cay chua đặc trưng'),
(N'Lẩu nấm chay', N'Nồi', 3, 150000, N'Dành cho người ăn chay, nước dùng thanh ngọt'),
(N'Coca-Cola', N'Lon', 4, 12000, N'Uống lạnh ngon hơn'),
(N'Trà đào cam sả', N'Ly', 4, 25000, N'Vị chua nhẹ, thơm sả'),
(N'Trà sữa trân châu đường đen', N'Ly', 5, 35000, N'Ngọt béo, topping dẻo dai'),
(N'Cà phê sữa đá', N'Ly', 5, 20000, N'Cà phê nguyên chất, sữa đặc thơm'),
(N'Bánh flan caramel', N'Cái', 6, 15000, N'Mềm mịn, béo nhẹ'),
(N'Kem dừa Thái', N'Ly', 6, 25000, N'Có topping đậu phộng, dừa khô');
*/


/*
SELECT * FROM Category;
*/

/*
INSERT INTO Category(Name, Type)
VALUES (N'Test3', 1);
*/

/*
UPDATE Category
SET
	Name = N'Updated test',
	Type = 0
WHERE
	ID = 15;
*/

/*
DELETE FROM Category
WHERE ID = 14;
*/

SELECT * FROM Food;

SELECT 
	Food.ID AS ID,
	Food.Name,
	Food.Unit,
	Food.FoodCategoryID,
	Category.Name AS CategoryName,
	Food.Price,
	Food.Notes,
	Category.Type AS CategoryType
FROM Food
JOIN Category ON Food.FoodCategoryID = Category.ID;


SELECT ID, Name, Unit, FoodCategoryID, Price, Notes FROM Food
WHERE FoodCategoryID = 2;


SELECT Name FROM Category
WHERE ID = 2;
