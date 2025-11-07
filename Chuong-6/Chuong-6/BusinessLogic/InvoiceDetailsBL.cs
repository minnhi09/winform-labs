using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class InvoiceDetailsBL
    {
        private readonly InvoiceDetailsDA invoiceDetailsDA;

        public InvoiceDetailsBL()
        {
            this.invoiceDetailsDA = new InvoiceDetailsDA();
        }

        // Lấy tất cả InvoiceDetails
        public List<InvoiceDetail> GetAllInvoiceDetails()
        {
            try
            {
                return invoiceDetailsDA.GetAllInvoiceDetails();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách chi tiết hóa đơn: {ex.Message}");
            }
        }

        // Lấy InvoiceDetail theo ID
        public InvoiceDetail GetInvoiceDetailByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID chi tiết hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.GetInvoiceDetailByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}");
            }
        }

        // Lấy InvoiceDetails theo InvoiceID
        public List<InvoiceDetail> GetInvoiceDetailsByInvoiceID(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.GetInvoiceDetailsByInvoiceID(invoiceID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}");
            }
        }

        // Thêm món vào hóa đơn
        public int AddInvoiceDetail(int invoiceID, int foodID, int amount)
        {
            // Validate dữ liệu
            ValidateInvoiceDetailData(invoiceID, foodID, amount);

            try
            {
                return invoiceDetailsDA.InsertInvoiceDetail(invoiceID, foodID, amount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm món vào hóa đơn: {ex.Message}");
            }
        }

        // Cập nhật số lượng món
        public InvoiceDetailUpdateResult UpdateInvoiceDetail(int id, int amount)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            if (amount < 0)
            {
                throw new ArgumentException("Số lượng không được âm!");
            }

            try
            {
                return invoiceDetailsDA.UpdateInvoiceDetail(id, amount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật số lượng: {ex.Message}");
            }
        }

        // Xóa món khỏi hóa đơn
        public int DeleteInvoiceDetail(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            // Kiểm tra chi tiết có tồn tại không
            if (!invoiceDetailsDA.CheckInvoiceDetailExists(id))
            {
                throw new Exception("Chi tiết hóa đơn không tồn tại!");
            }

            try
            {
                return invoiceDetailsDA.DeleteInvoiceDetail(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa món khỏi hóa đơn: {ex.Message}");
            }
        }

        // Xóa tất cả món trong hóa đơn
        public int DeleteInvoiceDetailsByInvoiceID(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.DeleteInvoiceDetailsByInvoiceID(invoiceID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa chi tiết hóa đơn: {ex.Message}");
            }
        }

        // Tăng số lượng món
        public InvoiceDetailAmountResult IncreaseAmount(int id, int increment = 1)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            if (increment <= 0)
            {
                throw new ArgumentException("Số lượng tăng phải lớn hơn 0!");
            }

            try
            {
                return invoiceDetailsDA.UpdateAmount(id, increment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tăng số lượng: {ex.Message}");
            }
        }

        // Giảm số lượng món
        public InvoiceDetailAmountResult DecreaseAmount(int id, int decrement = 1)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            if (decrement <= 0)
            {
                throw new ArgumentException("Số lượng giảm phải lớn hơn 0!");
            }

            try
            {
                return invoiceDetailsDA.UpdateAmount(id, -decrement);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi giảm số lượng: {ex.Message}");
            }
        }

        // Lấy tổng tiền theo InvoiceID
        public InvoiceDetailTotal GetTotalByInvoiceID(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.GetTotalByInvoiceID(invoiceID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính tổng tiền: {ex.Message}");
            }
        }

        // Kiểm tra món ăn đã có trong hóa đơn chưa
        public FoodExistsResult CheckFoodExists(int invoiceID, int foodID)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            if (foodID <= 0)
            {
                throw new ArgumentException("ID món ăn không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.CheckFoodExists(invoiceID, foodID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra món ăn: {ex.Message}");
            }
        }

        // Validate dữ liệu InvoiceDetail
        private void ValidateInvoiceDetailData(int invoiceID, int foodID, int amount)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            if (foodID <= 0)
            {
                throw new ArgumentException("ID món ăn không hợp lệ!");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Số lượng phải lớn hơn 0!");
            }

            if (amount > 100)
            {
                throw new ArgumentException("Số lượng không được vượt quá 100!");
            }
        }

        // Kiểm tra chi tiết có tồn tại không
        public bool CheckInvoiceDetailExists(int id)
        {
            return invoiceDetailsDA.CheckInvoiceDetailExists(id);
        }

        // Lấy thông tin chi tiết dưới dạng object
        public InvoiceDetail GetInvoiceDetailObject(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.GetInvoiceDetailObject(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin chi tiết: {ex.Message}");
            }
        }

        // Đếm số lượng món trong hóa đơn
        public int GetTotalItems(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                return 0;
            }

            List<InvoiceDetail> details = GetInvoiceDetailsByInvoiceID(invoiceID);
            return details.Count;
        }

        // Tính tổng số lượng món trong hóa đơn
        public int GetTotalQuantity(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                return 0;
            }

            InvoiceDetailTotal total = GetTotalByInvoiceID(invoiceID);
            return total?.TotalQuantity ?? 0;
        }

        // Tính tổng tiền của hóa đơn
        public int GetTotalAmount(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                return 0;
            }

            InvoiceDetailTotal total = GetTotalByInvoiceID(invoiceID);
            return total?.TotalAmount ?? 0;
        }

        // Tìm món ăn trong hóa đơn
        public List<InvoiceDetail> SearchFoodInInvoice(int invoiceID, string keyword)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            List<InvoiceDetail> details = GetInvoiceDetailsByInvoiceID(invoiceID);

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return details;
            }

            string keywordLower = keyword.ToLower();

            return details.Where(d =>
                d.FoodName.ToLower().Contains(keywordLower) ||
                d.Unit.ToLower().Contains(keywordLower)
            ).ToList();
        }

        // Validate trước khi xóa
        public string ValidateBeforeDelete(int id)
        {
            if (id <= 0)
            {
                return "ID chi tiết không hợp lệ!";
            }

            if (!CheckInvoiceDetailExists(id))
            {
                return "Chi tiết hóa đơn không tồn tại!";
            }

            return null; // Có thể xóa
        }

        // Cập nhật số lượng với validation
        public InvoiceDetailUpdateResult UpdateAmountSafely(int id, int newAmount)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            if (newAmount < 0)
            {
                throw new ArgumentException("Số lượng không được âm!");
            }

            if (newAmount > 100)
            {
                throw new ArgumentException("Số lượng không được vượt quá 100!");
            }

            if (newAmount == 0)
            {
                // Nếu số lượng = 0, xóa món
                DeleteInvoiceDetail(id);
                return new InvoiceDetailUpdateResult
                {
                    AffectedRows = 1,
                    Message = "Đã xóa món khỏi hóa đơn"
                };
            }

            return UpdateInvoiceDetail(id, newAmount);
        }

        // Kiểm tra hóa đơn có món nào chưa
        public bool HasItems(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                return false;
            }

            return GetTotalItems(invoiceID) > 0;
        }

        // Lấy món đắt nhất trong hóa đơn
        public InvoiceDetail GetMostExpensiveItem(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                return null;
            }

            List<InvoiceDetail> details = GetInvoiceDetailsByInvoiceID(invoiceID);
            if (details.Count == 0)
            {
                return null;
            }

            return details.OrderByDescending(d => d.Total).FirstOrDefault();
        }
    }
}
