using System;
using System.Collections.Generic;
using System.Linq;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class HallBL
    {
        private readonly HallDA hallDA;

        public HallBL()
        {
            this.hallDA = new HallDA();
        }

        // Lấy tất cả Hall
        public List<Hall> GetAllHalls()
        {
            try
            {
                return hallDA.GetAllHalls();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách sảnh: {ex.Message}");
            }
        }

        // Lấy Hall theo ID
        public Hall GetHallByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID sảnh không hợp lệ!");
            }

            try
            {
                return hallDA.GetHallByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin sảnh: {ex.Message}");
            }
        }

        // Lấy Hall theo RestaurantID
        public List<Hall> GetHallsByRestaurantID(int restaurantID)
        {
            if (restaurantID <= 0)
            {
                throw new ArgumentException("ID nhà hàng không hợp lệ!");
            }

            try
            {
                return hallDA.GetHallsByRestaurantID(restaurantID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách sảnh theo nhà hàng: {ex.Message}");
            }
        }

        // Thêm Hall mới
        public int AddHall(string name, int restaurantID)
        {
            // Validate dữ liệu
            ValidateHallData(name, restaurantID);

            try
            {
                return hallDA.InsertHall(name, restaurantID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm sảnh: {ex.Message}");
            }
        }

        // Cập nhật Hall
        public int UpdateHall(int id, string name, int restaurantID)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID sảnh không hợp lệ!");
            }

            // Validate dữ liệu
            ValidateHallData(name, restaurantID);

            // Kiểm tra Hall có tồn tại không
            if (!hallDA.CheckHallExists(id))
            {
                throw new Exception("Sảnh không tồn tại!");
            }

            try
            {
                return hallDA.UpdateHall(id, name, restaurantID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật sảnh: {ex.Message}");
            }
        }

        // Xóa Hall
        public int DeleteHall(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID sảnh không hợp lệ!");
            }

            // Kiểm tra Hall có tồn tại không
            if (!hallDA.CheckHallExists(id))
            {
                throw new Exception("Sảnh không tồn tại!");
            }

            try
            {
                return hallDA.DeleteHall(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa sảnh: {ex.Message}");
            }
        }

        // Kiểm tra Hall có tồn tại không
        public bool CheckHallExists(int id)
        {
            return hallDA.CheckHallExists(id);
        }

        // Lấy thông tin Hall dưới dạng object
        public Hall GetHallObject(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID sảnh không hợp lệ!");
            }

            try
            {
                return hallDA.GetHallObject(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin sảnh: {ex.Message}");
            }
        }

        // Validate dữ liệu Hall
        private void ValidateHallData(string name, int restaurantID)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên sảnh không được để trống!");
            }

            if (name.Length > 200)
            {
                throw new ArgumentException("Tên sảnh không được vượt quá 200 ký tự!");
            }

            if (restaurantID <= 0)
            {
                throw new ArgumentException("ID nhà hàng không hợp lệ!");
            }
        }

        // Tìm kiếm Hall theo tên
        public List<Hall> SearchHalls(string keyword)
        {
            List<Hall> halls = GetAllHalls();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return halls;
            }

            string keywordLower = keyword.ToLower();

            return halls.Where(h =>
                h.Name.ToLower().Contains(keywordLower) ||
                (h.RestaurantName != null && h.RestaurantName.ToLower().Contains(keywordLower))
            ).ToList();
        }

        // Đếm số lượng Hall
        public int GetTotalHalls()
        {
            List<Hall> halls = GetAllHalls();
            return halls.Count;
        }

        // Đếm số lượng Hall theo Restaurant
        public int GetTotalHallsByRestaurant(int restaurantID)
        {
            if (restaurantID <= 0)
            {
                return 0;
            }

            List<Hall> halls = GetHallsByRestaurantID(restaurantID);
            return halls.Count;
        }

        // Kiểm tra Hall có bàn nào không
        public bool HasTables(int hallID)
        {
            if (hallID <= 0)
            {
                return false;
            }

            // Có thể kiểm tra qua TableDA nếu cần
            // Hiện tại return false vì chưa có logic kiểm tra
            return false;
        }

        // Validate trước khi xóa
        public string ValidateBeforeDelete(int id)
        {
            if (id <= 0)
            {
                return "ID sảnh không hợp lệ!";
            }

            if (!CheckHallExists(id))
            {
                return "Sảnh không tồn tại!";
            }

            if (HasTables(id))
            {
                return "Không thể xóa sảnh đang có bàn!";
            }

            return null; // Có thể xóa
        }

        // Lấy tên Hall theo ID
        public string GetHallName(int id)
        {
            if (id <= 0)
            {
                return string.Empty;
            }

            Hall hall = GetHallObject(id);
            return hall?.Name ?? string.Empty;
        }

        // Kiểm tra tên Hall có trùng không
        public bool IsDuplicateName(string name, int restaurantID, int excludeID = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            List<Hall> halls = GetHallsByRestaurantID(restaurantID);
            string nameLower = name.Trim().ToLower();

            return halls.Any(h =>
                h.ID != excludeID &&
                h.Name.Trim().ToLower() == nameLower
            );
        }
    }
}
