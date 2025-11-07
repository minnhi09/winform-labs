using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class TableBL
    {
        private readonly TableDA tableDA;

        public TableBL()
        {
            this.tableDA = new TableDA();
        }

        // Lấy tất cả Table
        public List<Table> GetAllTables()
        {
            try
            {
                return tableDA.GetAllTables();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách bàn: {ex.Message}");
            }
        }

        // Lấy Table theo ID
        public Table GetTableByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            try
            {
                return tableDA.GetTableByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin bàn: {ex.Message}");
            }
        }

        // Lấy Table theo HallID
        public List<Table> GetTablesByHallID(int hallID)
        {
            if (hallID <= 0)
            {
                throw new ArgumentException("ID sảnh không hợp lệ!");
            }

            try
            {
                return tableDA.GetTablesByHallID(hallID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách bàn theo sảnh: {ex.Message}");
            }
        }

        // Lấy Table theo Status
        public List<Table> GetTablesByStatus(int status)
        {
            if (status < 0 || status > 2)
            {
                throw new ArgumentException("Trạng thái bàn không hợp lệ! (0: Trống, 1: Đã đặt, 2: Có khách)");
            }

            try
            {
                return tableDA.GetTablesByStatus(status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách bàn theo trạng thái: {ex.Message}");
            }
        }

        // Thêm Table mới
        public int AddTable(string tableCode, string name, int status, int? seats, int hallID)
        {
            // Validate dữ liệu
            ValidateTableData(tableCode, name, status, seats, hallID);

            try
            {
                return tableDA.InsertTable(tableCode, name, status, seats, hallID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm bàn: {ex.Message}");
            }
        }

        // Cập nhật Table
        public int UpdateTable(int id, string tableCode, string name, int status, int? seats, int hallID)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            // Validate dữ liệu
            ValidateTableData(tableCode, name, status, seats, hallID);

            // Kiểm tra Table có tồn tại không
            if (!tableDA.CheckTableExists(id))
            {
                throw new Exception("Bàn không tồn tại!");
            }

            try
            {
                return tableDA.UpdateTable(id, tableCode, name, status, seats, hallID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật bàn: {ex.Message}");
            }
        }

        // Xóa Table
        public int DeleteTable(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            // Kiểm tra Table có tồn tại không
            if (!tableDA.CheckTableExists(id))
            {
                throw new Exception("Bàn không tồn tại!");
            }

            try
            {
                return tableDA.DeleteTable(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa bàn: {ex.Message}");
            }
        }

        // Cập nhật trạng thái Table
        public int UpdateTableStatus(int id, int status)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            if (status < 0 || status > 2)
            {
                throw new ArgumentException("Trạng thái không hợp lệ! (0: Trống, 1: Đã đặt, 2: Có khách)");
            }

            // Kiểm tra Table có tồn tại không
            if (!tableDA.CheckTableExists(id))
            {
                throw new Exception("Bàn không tồn tại!");
            }

            try
            {
                return tableDA.UpdateTableStatus(id, status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật trạng thái bàn: {ex.Message}");
            }
        }

        // Validate dữ liệu Table
        private void ValidateTableData(string tableCode, string name, int status, int? seats, int hallID)
        {
            // Validate tableCode
            if (string.IsNullOrWhiteSpace(tableCode))
            {
                throw new ArgumentException("Mã bàn không được để trống!");
            }

            if (tableCode.Length > 50)
            {
                throw new ArgumentException("Mã bàn không được vượt quá 50 ký tự!");
            }

            if (!Regex.IsMatch(tableCode, @"^[A-Za-z0-9]+$"))
            {
                throw new ArgumentException("Mã bàn chỉ được chứa chữ cái và số!");
            }

            // Validate name (optional)
            if (!string.IsNullOrWhiteSpace(name) && name.Length > 200)
            {
                throw new ArgumentException("Tên bàn không được vượt quá 200 ký tự!");
            }

            // Validate status
            if (status < 0 || status > 2)
            {
                throw new ArgumentException("Trạng thái không hợp lệ! (0: Trống, 1: Đã đặt, 2: Có khách)");
            }

            // Validate seats (optional)
            if (seats.HasValue && seats.Value <= 0)
            {
                throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
            }

            if (seats.HasValue && seats.Value > 100)
            {
                throw new ArgumentException("Số chỗ ngồi không được vượt quá 100!");
            }

            // Validate hallID
            if (hallID <= 0)
            {
                throw new ArgumentException("ID sảnh không hợp lệ!");
            }
        }

        // Kiểm tra Table có tồn tại không
        public bool CheckTableExists(int id)
        {
            return tableDA.CheckTableExists(id);
        }

        // Lấy thông tin Table dưới dạng object
        public Table GetTableObject(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            try
            {
                return tableDA.GetTableObject(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin bàn: {ex.Message}");
            }
        }

        // Tìm kiếm Table
        public List<Table> SearchTables(string keyword)
        {
            List<Table> tables = GetAllTables();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return tables;
            }

            string keywordLower = keyword.ToLower();

            return tables.Where(t =>
                t.TableCode.ToLower().Contains(keywordLower) ||
                (t.Name != null && t.Name.ToLower().Contains(keywordLower)) ||
                (t.HallName != null && t.HallName.ToLower().Contains(keywordLower)) ||
                (t.RestaurantName != null && t.RestaurantName.ToLower().Contains(keywordLower))
            ).ToList();
        }

        // Đếm số lượng Table
        public int GetTotalTables()
        {
            List<Table> tables = GetAllTables();
            return tables.Count;
        }

        // Đếm số lượng Table theo Hall
        public int GetTotalTablesByHall(int hallID)
        {
            if (hallID <= 0)
            {
                return 0;
            }

            List<Table> tables = GetTablesByHallID(hallID);
            return tables.Count;
        }

        // Đếm số lượng Table theo Status
        public int GetTotalTablesByStatus(int status)
        {
            if (status < 0 || status > 2)
            {
                return 0;
            }

            List<Table> tables = GetTablesByStatus(status);
            return tables.Count;
        }

        // Lấy danh sách bàn trống
        public List<Table> GetAvailableTables()
        {
            return GetTablesByStatus(0); // Status = 0: Trống
        }

        // Lấy danh sách bàn đang có khách
        public List<Table> GetOccupiedTables()
        {
            return GetTablesByStatus(2); // Status = 2: Có khách
        }

        // Kiểm tra mã bàn có trùng không
        public bool IsDuplicateCode(string tableCode, int hallID, int excludeID = 0)
        {
            if (string.IsNullOrWhiteSpace(tableCode))
            {
                return false;
            }

            List<Table> tables = GetTablesByHallID(hallID);
            string codeLower = tableCode.Trim().ToLower();

            return tables.Any(t =>
                t.ID != excludeID &&
                t.TableCode.Trim().ToLower() == codeLower
            );
        }

        // Validate trước khi xóa
        public string ValidateBeforeDelete(int id)
        {
            if (id <= 0)
            {
                return "ID bàn không hợp lệ!";
            }

            if (!CheckTableExists(id))
            {
                return "Bàn không tồn tại!";
            }

            // Kiểm tra bàn có đang được sử dụng không
            Table table = GetTableObject(id);
            if (table.Status != 0)
            {
                return "Không thể xóa bàn đang được sử dụng!";
            }

            return null; // Có thể xóa
        }

        // Đặt bàn
        public int ReserveTable(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            Table table = GetTableObject(id);
            if (table == null)
            {
                throw new Exception("Bàn không tồn tại!");
            }

            if (table.Status != 0)
            {
                throw new Exception("Bàn không còn trống!");
            }

            return UpdateTableStatus(id, 1); // Status = 1: Đã đặt
        }

        // Nhận khách vào bàn
        public int OccupyTable(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            Table table = GetTableObject(id);
            if (table == null)
            {
                throw new Exception("Bàn không tồn tại!");
            }

            if (table.Status == 2)
            {
                throw new Exception("Bàn đang có khách!");
            }

            return UpdateTableStatus(id, 2); // Status = 2: Có khách
        }

        // Trả bàn (checkout)
        public int ReleaseTable(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID bàn không hợp lệ!");
            }

            return UpdateTableStatus(id, 0); // Status = 0: Trống
        }

        // Lấy tên bàn hiển thị
        public string GetTableDisplayName(int id)
        {
            if (id <= 0)
            {
                return string.Empty;
            }

            Table table = GetTableObject(id);
            if (table == null)
            {
                return string.Empty;
            }

            string displayName = table.TableCode;
            if (!string.IsNullOrWhiteSpace(table.Name))
            {
                displayName += $" - {table.Name}";
            }

            return displayName;
        }
    }
}
