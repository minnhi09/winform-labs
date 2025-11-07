using System;
using System.Data;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class FoodCategoryBL
    {
        private readonly FoodCategoryDA categoryDA;

        public FoodCategoryBL()
        {
            this.categoryDA = new FoodCategoryDA();
        }

        public DataTable GetAllCategories()
        {
            try
            {
                return categoryDA.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách danh mục: {ex.Message}");
            }
        }

        public DataTable GetCategoryByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID danh mục không hợp lệ!");
            }

            try
            {
                return categoryDA.GetCategoryByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin danh mục: {ex.Message}");
            }
        }

        public int AddCategory(string name, int type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên danh mục không được để trống!");
            }

            if (type != 1 && type != 2)
            {
                throw new ArgumentException("Loại danh mục không hợp lệ! (1: Đồ ăn, 2: Thức uống)");
            }

            try
            {
                return categoryDA.InsertCategory(name, type);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm danh mục: {ex.Message}");
            }
        }

        public int UpdateCategory(int id, string name, int type)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID danh mục không hợp lệ!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên danh mục không được để trống!");
            }

            if (type != 1 && type != 2)
            {
                throw new ArgumentException("Loại danh mục không hợp lệ!");
            }

            try
            {
                return categoryDA.UpdateCategory(id, name, type);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật danh mục: {ex.Message}");
            }
        }

        public int DeleteCategory(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID danh mục không hợp lệ!");
            }

            try
            {
                return categoryDA.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa danh mục: {ex.Message}");
            }
        }
    }
}
