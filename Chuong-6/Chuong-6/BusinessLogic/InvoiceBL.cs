using System;
using System.Collections.Generic;
using System.Linq;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class InvoiceBL
    {
        private readonly InvoiceDA invoiceDA;
        private readonly InvoiceDetailsDA invoiceDetailsDA;

        public InvoiceBL()
        {
            this.invoiceDA = new InvoiceDA();
            this.invoiceDetailsDA = new InvoiceDetailsDA();
        }

        // Lấy tất cả Invoice
        public List<Invoice> GetAllInvoices()
        {
            try
            {
                return invoiceDA.GetAllInvoices();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách hóa đơn: {ex.Message}");
            }
        }

        // Lấy Invoice theo ID
        public Invoice GetInvoiceByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDA.GetInvoiceByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin hóa đơn: {ex.Message}");
            }
        }

        // Lấy Invoice chưa thanh toán theo TableID
        public Invoice GetInvoiceByTableID(int tableID)
        {
            if (tableID <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            try
            {
                return invoiceDA.GetInvoiceByTableID(tableID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy hóa đơn theo bàn: {ex.Message}");
            }
        }

        // Thêm Invoice mới
        public int AddInvoice(string name, int tableID, string accountID,
                              int total = 0, float discount = 0, float tax = 0)
        {
            // Validate dữ liệu
            ValidateInvoiceData(name, tableID, accountID);

            if (total < 0)
            {
                throw new ArgumentException("Tổng tiền không được âm!");
            }

            if (discount < 0 || discount > 1)
            {
                throw new ArgumentException("Giảm giá phải từ 0 đến 1 (0% - 100%)!");
            }

            if (tax < 0 || tax > 1)
            {
                throw new ArgumentException("Thuế phải từ 0 đến 1 (0% - 100%)!");
            }

            try
            {
                return invoiceDA.InsertInvoice(name, tableID, accountID, total, discount, tax);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm hóa đơn: {ex.Message}");
            }
        }

        // Cập nhật Invoice
        public int UpdateInvoice(int id, string name, int total, float discount, float tax)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên hóa đơn không được để trống!");
            }

            if (total < 0)
            {
                throw new ArgumentException("Tổng tiền không được âm!");
            }

            if (discount < 0 || discount > 1)
            {
                throw new ArgumentException("Giảm giá phải từ 0 đến 1!");
            }

            if (tax < 0 || tax > 1)
            {
                throw new ArgumentException("Thuế phải từ 0 đến 1!");
            }

            try
            {
                return invoiceDA.UpdateInvoice(id, name, total, discount, tax);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật hóa đơn: {ex.Message}");
            }
        }

        // Thanh toán Invoice
        public InvoiceCheckoutResult CheckoutInvoice(int id, float discount = 0, float tax = 0)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            if (discount < 0 || discount > 1)
            {
                throw new ArgumentException("Giảm giá phải từ 0 đến 1 (0% - 100%)!");
            }

            if (tax < 0 || tax > 1)
            {
                throw new ArgumentException("Thuế phải từ 0 đến 1 (0% - 100%)!");
            }

            // Kiểm tra hóa đơn có tồn tại không
            if (!invoiceDA.CheckInvoiceExists(id))
            {
                throw new Exception("Hóa đơn không tồn tại!");
            }

            // Kiểm tra hóa đơn đã thanh toán chưa
            Invoice invoice = invoiceDA.GetInvoiceObject(id);
            if (invoice.Status)
            {
                throw new Exception("Hóa đơn đã được thanh toán!");
            }

            try
            {
                return invoiceDA.CheckoutInvoice(id, discount, tax);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thanh toán hóa đơn: {ex.Message}");
            }
        }

        // Xóa Invoice
        public int DeleteInvoice(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            // Kiểm tra hóa đơn có tồn tại không
            if (!invoiceDA.CheckInvoiceExists(id))
            {
                throw new Exception("Hóa đơn không tồn tại!");
            }

            // Kiểm tra hóa đơn đã thanh toán chưa
            Invoice invoice = invoiceDA.GetInvoiceObject(id);
            if (invoice.Status)
            {
                throw new Exception("Không thể xóa hóa đơn đã thanh toán!");
            }

            try
            {
                return invoiceDA.DeleteInvoice(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa hóa đơn: {ex.Message}");
            }
        }

        // Lấy chi tiết hóa đơn
        public List<InvoiceDetailInfo> GetInvoiceDetails(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDA.GetInvoiceDetails(invoiceID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}");
            }
        }

        // Tính tổng tiền hóa đơn
        public InvoiceTotal CalculateTotal(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDA.CalculateTotal(invoiceID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính tổng tiền: {ex.Message}");
            }
        }

        // Lấy hóa đơn theo ngày
        public List<Invoice> GetInvoicesByDate(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!");
            }

            try
            {
                return invoiceDA.GetInvoicesByDate(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy hóa đơn theo ngày: {ex.Message}");
            }
        }

        // Lấy thống kê doanh thu
        public RevenueStatistic GetRevenue(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!");
            }

            try
            {
                return invoiceDA.GetRevenue(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê doanh thu: {ex.Message}");
            }
        }

        // Thêm món vào hóa đơn
        public int AddFoodToInvoice(int invoiceID, int foodID, int amount)
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

            // Kiểm tra hóa đơn đã thanh toán chưa
            Invoice invoice = invoiceDA.GetInvoiceObject(invoiceID);
            if (invoice.Status)
            {
                throw new Exception("Không thể thêm món vào hóa đơn đã thanh toán!");
            }

            try
            {
                return invoiceDetailsDA.InsertInvoiceDetail(invoiceID, foodID, amount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm món vào hóa đơn: {ex.Message}");
            }
        }

        // Cập nhật số lượng món trong hóa đơn
        public InvoiceDetailUpdateResult UpdateFoodAmount(int detailID, int amount)
        {
            if (detailID <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.UpdateInvoiceDetail(detailID, amount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật số lượng: {ex.Message}");
            }
        }

        // Xóa món khỏi hóa đơn
        public int RemoveFoodFromInvoice(int detailID)
        {
            if (detailID <= 0)
            {
                throw new ArgumentException("ID chi tiết không hợp lệ!");
            }

            try
            {
                return invoiceDetailsDA.DeleteInvoiceDetail(detailID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa món khỏi hóa đơn: {ex.Message}");
            }
        }

        // Validate dữ liệu Invoice
        private void ValidateInvoiceData(string name, int tableID, string accountID)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tên hóa đơn không được để trống!");
            }

            if (name.Length > 200)
            {
                throw new ArgumentException("Tên hóa đơn không được vượt quá 200 ký tự!");
            }

            if (tableID <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            if (string.IsNullOrWhiteSpace(accountID))
            {
                throw new ArgumentException("Tài khoản không được để trống!");
            }
        }

        // Kiểm tra Invoice có tồn tại không
        public bool CheckInvoiceExists(int id)
        {
            return invoiceDA.CheckInvoiceExists(id);
        }

        // Lấy thông tin Invoice dưới dạng object
        public Invoice GetInvoiceObject(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID hóa đơn không hợp lệ!");
            }

            try
            {
                return invoiceDA.GetInvoiceObject(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin hóa đơn: {ex.Message}");
            }
        }

        // Đếm số lượng hóa đơn
        public int GetTotalInvoices()
        {
            List<Invoice> invoices = GetAllInvoices();
            return invoices.Count;
        }

        // Đếm số lượng hóa đơn đã thanh toán
        public int GetTotalPaidInvoices()
        {
            List<Invoice> invoices = GetAllInvoices();
            return invoices.Count(i => i.Status);
        }

        // Đếm số lượng hóa đơn chưa thanh toán
        public int GetTotalUnpaidInvoices()
        {
            List<Invoice> invoices = GetAllInvoices();
            return invoices.Count(i => !i.Status);
        }

        // Tính tổng doanh thu
        public decimal GetTotalRevenue(DateTime fromDate, DateTime toDate)
        {
            List<Invoice> invoices = GetInvoicesByDate(fromDate, toDate);
            return invoices.Where(i => i.Status).Sum(i => (decimal)i.Total);
        }

        // Validate trước khi thanh toán
        public string ValidateBeforeCheckout(int invoiceID)
        {
            if (invoiceID <= 0)
            {
                return "ID hóa đơn không hợp lệ!";
            }

            if (!CheckInvoiceExists(invoiceID))
            {
                return "Hóa đơn không tồn tại!";
            }

            Invoice invoice = GetInvoiceObject(invoiceID);
            if (invoice.Status)
            {
                return "Hóa đơn đã được thanh toán!";
            }

            // Kiểm tra hóa đơn có món nào chưa
            List<InvoiceDetailInfo> details = GetInvoiceDetails(invoiceID);
            if (details.Count == 0)
            {
                return "Hóa đơn chưa có món nào!";
            }

            return null; // Có thể thanh toán
        }

        // Lấy hóa đơn hôm nay
        public List<Invoice> GetTodayInvoices()
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            return GetInvoicesByDate(today, tomorrow);
        }

        // Lấy hóa đơn tháng này
        public List<Invoice> GetMonthInvoices()
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
            return GetInvoicesByDate(firstDay, lastDay);
        }
    }
}
