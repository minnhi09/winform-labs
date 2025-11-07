using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class RestaurantBL
    {
        private readonly RestaurantDA restaurantDA;

        public RestaurantBL()
        {
            this.restaurantDA = new RestaurantDA();
        }

        // Lấy tất cả Restaurant
        public List<Restaurant> GetAllRestaurants()
        {
            try
            {
                return restaurantDA.GetAllRestaurants();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách nhà hàng: {ex.Message}");
            }
        }

        // Lấy Restaurant theo ID
        public Restaurant GetRestaurantByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID nhà hàng không hợp lệ!");
            }

            try
            {
                return restaurantDA.GetRestaurantByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin nhà hàng: {ex.Message}");
            }
        }

        // Thêm Restaurant mới
        public int AddRestaurant(string name, string address, string phone, string website = null)
        {
            // Validate dữ liệu
            ValidateRestaurantData(name, address, phone, website);

            try
            {
                return restaurantDA.InsertRestaurant(name, address, phone, website);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm nhà hàng: {ex.Message}");
            }
        }

        // Cập nhật Restaurant
        public int UpdateRestaurant(int id, string name, string address, string phone, string website = null)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID nhà hàng không hợp lệ!");
            }

            // Validate dữ liệu
            ValidateRestaurantData(name, address, phone, website);

            // Kiểm tra Restaurant có tồn tại không
            if (!restaurantDA.CheckRestaurantExists(id))
            {
                throw new Exception("Nhà hàng không tồn tại!");
            }

            try
            {
                return restaurantDA.UpdateRestaurant(id, name, address, phone, website);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật nhà hàng: {ex.Message}");
            }
        }

        // Xóa Restaurant
        public int DeleteRestaurant(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID nhà hàng không hợp lệ!");
            }

            // Kiểm tra Restaurant có tồn tại không
            if (!restaurantDA.CheckRestaurantExists(id))
            {
                throw new Exception("Nhà hàng không tồn tại!");
            }

            try
            {
                return restaurantDA.DeleteRestaurant(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa nhà hàng: {ex.Message}");
            }
        }

        // Kiểm tra Restaurant có tồn tại không
        public bool CheckRestaurantExists(int id)
        {
            return restaurantDA.CheckRestaurantExists(id);
        }

        // Lấy thông tin Restaurant dưới dạng object
        public Restaurant GetRestaurantObject(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID nhà hàng không hợp lệ!");
            }

            try
            {
                return restaurantDA.GetRestaurantObject(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin nhà hàng: {ex.Message}");
            }
        }

        // Validate dữ liệu Restaurant
        private void ValidateRestaurantData(string name, string address, string phone, string website)
        {
            // Validate name
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên nhà hàng không được để trống!");
            }

            if (name.Length > 200)
            {
                throw new ArgumentException("Tên nhà hàng không được vượt quá 200 ký tự!");
            }

            // Validate address
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Địa chỉ không được để trống!");
            }

            if (address.Length > 500)
            {
                throw new ArgumentException("Địa chỉ không được vượt quá 500 ký tự!");
            }

            // Validate phone
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Số điện thoại không được để trống!");
            }

            if (!ValidatePhone(phone))
            {
                throw new ArgumentException("Số điện thoại không hợp lệ! Phải có 10-11 chữ số.");
            }

            // Validate website (optional)
            if (!string.IsNullOrWhiteSpace(website))
            {
                if (website.Length > 200)
                {
                    throw new ArgumentException("Website không được vượt quá 200 ký tự!");
                }

                if (!ValidateWebsite(website))
                {
                    throw new ArgumentException("Website không hợp lệ!");
                }
            }
        }

        // Validate số điện thoại
        private bool ValidatePhone(string phone)
        {
            // Số điện thoại Việt Nam: 10-11 chữ số
            return Regex.IsMatch(phone, @"^[0-9]{10,11}$");
        }

        // Validate website
        private bool ValidateWebsite(string website)
        {
            // Regex đơn giản cho website
            return Regex.IsMatch(website, @"^(https?://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,}(/.*)?$");
        }

        // Tìm kiếm Restaurant
        public List<Restaurant> SearchRestaurants(string keyword)
        {
            List<Restaurant> restaurants = GetAllRestaurants();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return restaurants;
            }

            string keywordLower = keyword.ToLower();

            return restaurants.Where(r =>
                r.Name.ToLower().Contains(keywordLower) ||
                r.Address.ToLower().Contains(keywordLower) ||
                r.Phone.Contains(keyword) ||
                (r.Website != null && r.Website.ToLower().Contains(keywordLower))
            ).ToList();
        }

        // Đếm số lượng Restaurant
        public int GetTotalRestaurants()
        {
            List<Restaurant> restaurants = GetAllRestaurants();
            return restaurants.Count;
        }

        // Lấy tên Restaurant theo ID
        public string GetRestaurantName(int id)
        {
            if (id <= 0)
            {
                return string.Empty;
            }

            Restaurant restaurant = GetRestaurantObject(id);
            return restaurant?.Name ?? string.Empty;
        }

        // Kiểm tra tên Restaurant có trùng không
        public bool IsDuplicateName(string name, int excludeID = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            List<Restaurant> restaurants = GetAllRestaurants();
            string nameLower = name.Trim().ToLower();

            return restaurants.Any(r =>
                r.ID != excludeID &&
                r.Name.Trim().ToLower() == nameLower
            );
        }

        // Validate trước khi xóa
        public string ValidateBeforeDelete(int id)
        {
            if (id <= 0)
            {
                return "ID nhà hàng không hợp lệ!";
            }

            if (!CheckRestaurantExists(id))
            {
                return "Nhà hàng không tồn tại!";
            }

            // Kiểm tra có sảnh nào không (cần HallDA để kiểm tra)
            // Tạm thời bỏ qua, sẽ kiểm tra ở stored procedure

            return null; // Có thể xóa
        }

        // Format số điện thoại
        public string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return string.Empty;
            }

            // Loại bỏ các ký tự không phải số
            phone = Regex.Replace(phone, @"[^0-9]", "");

            // Format: 0xxx xxx xxx hoặc 0xxx xxx xxxx
            if (phone.Length == 10)
            {
                return $"{phone.Substring(0, 4)} {phone.Substring(4, 3)} {phone.Substring(7, 3)}";
            }
            else if (phone.Length == 11)
            {
                return $"{phone.Substring(0, 4)} {phone.Substring(4, 3)} {phone.Substring(7, 4)}";
            }

            return phone;
        }

        // Format website
        public string FormatWebsite(string website)
        {
            if (string.IsNullOrWhiteSpace(website))
            {
                return string.Empty;
            }

            // Thêm http:// nếu chưa có
            if (!website.StartsWith("http://") && !website.StartsWith("https://"))
            {
                return "http://" + website;
            }

            return website;
        }

        // Lấy thông tin đầy đủ của Restaurant để hiển thị
        public string GetRestaurantFullInfo(int id)
        {
            Restaurant restaurant = GetRestaurantObject(id);
            if (restaurant == null)
            {
                return string.Empty;
            }

            string info = $"Tên: {restaurant.Name}\n";
            info += $"Địa chỉ: {restaurant.Address}\n";
            info += $"Điện thoại: {FormatPhone(restaurant.Phone)}\n";

            if (!string.IsNullOrWhiteSpace(restaurant.Website))
            {
                info += $"Website: {restaurant.Website}";
            }

            return info;
        }
    }
}
