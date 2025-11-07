using System;
using System.Collections.Generic;
using System.Linq;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class FoodBL
    {
        private readonly FoodDA foodDA;

        public FoodBL()
        {
            this.foodDA = new FoodDA();
        }

        // Lấy tất cả Food
        public List<Food> GetAllFoods()
        {
            try
            {
                return foodDA.GetAllFoods();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách món ăn: {ex.Message}");
            }
        }

        // Lấy Food theo ID
        public Food GetFoodByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID món ăn không hợp lệ!");
            }

            try
            {
                return foodDA.GetFoodByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin món ăn: {ex.Message}");
            }
        }

        // Lấy Food theo CategoryID
        public List<Food> GetFoodsByCategoryID(int categoryID)
        {
            if (categoryID <= 0)
            {
                throw new ArgumentException("ID danh mục không hợp lệ!");
            }

            try
            {
                return foodDA.GetFoodsByCategoryID(categoryID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách món ăn theo danh mục: {ex.Message}");
            }
        }

        // Thêm Food mới
        public int AddFood(string name, string unit, int categoryID, int price, string notes = null)
        {
            // Validate dữ liệu
            ValidateFoodData(name, unit, categoryID, price);

            try
            {
                return foodDA.InsertFood(name, unit, categoryID, price, notes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm món ăn: {ex.Message}");
            }
        }

        // Cập nhật Food
        public int UpdateFood(int id, string name, string unit, int categoryID, int price, string notes = null)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID món ăn không hợp lệ!");
            }

            // Validate dữ liệu
            ValidateFoodData(name, unit, categoryID, price);

            try
            {
                return foodDA.UpdateFood(id, name, unit, categoryID, price, notes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật món ăn: {ex.Message}");
            }
        }

        // Xóa Food
        public int DeleteFood(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID món ăn không hợp lệ!");
            }

            try
            {
                return foodDA.DeleteFood(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa món ăn: {ex.Message}");
            }
        }

        // Validate dữ liệu Food
        private void ValidateFoodData(string name, string unit, int categoryID, int price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên món ăn không được để trống!");
            }

            if (name.Length > 200)
            {
                throw new ArgumentException("Tên món ăn không được vượt quá 200 ký tự!");
            }

            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException("Đơn vị tính không được để trống!");
            }

            if (unit.Length > 50)
            {
                throw new ArgumentException("Đơn vị tính không được vượt quá 50 ký tự!");
            }

            if (categoryID <= 0)
            {
                throw new ArgumentException("ID danh mục không hợp lệ!");
            }

            if (price < 0)
            {
                throw new ArgumentException("Giá không được âm!");
            }

            if (price > 100000000)
            {
                throw new ArgumentException("Giá không được vượt quá 100,000,000!");
            }
        }

        // Tìm kiếm Food
        public List<Food> SearchFoods(string keyword)
        {
            List<Food> foods = GetAllFoods();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return foods;
            }

            string keywordLower = keyword.ToLower();

            return foods.Where(f =>
                f.Name.ToLower().Contains(keywordLower) ||
                f.Unit.ToLower().Contains(keywordLower) ||
                (f.CategoryName != null && f.CategoryName.ToLower().Contains(keywordLower)) ||
                (f.Notes != null && f.Notes.ToLower().Contains(keywordLower))
            ).ToList();
        }

        // Đếm số lượng Food
        public int GetTotalFoods()
        {
            List<Food> foods = GetAllFoods();
            return foods.Count;
        }

        // Lấy Food object theo ID
        public Food GetFoodObject(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID món ăn không hợp lệ!");
            }

            try
            {
                return foodDA.GetFoodObject(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin món ăn: {ex.Message}");
            }
        }
    }
}
