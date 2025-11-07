using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuong_6.DataAccess
{
    public class Table
    {
        public int ID { get; set; }
        public string TableCode { get; set; } = string.Empty;
        public string Name { get; set; }
        public int Status { get; set; } // 0: Trống, 1: Đã đặt, 2: Có khách
        public int Seats { get; set; }
        public int HallID { get; set; }
        public string HallName { get; set; }
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }

        // Property để lấy text trạng thái
        public string StatusText => GetStatusText(Status);

        private static string GetStatusText(int status)
        {
            switch (status)
            {
                case 0:
                    return "Trống";
                case 1:
                    return "Đã đặt";
                case 2:
                    return "Có khách";
                default:
                    return "Không xác định";
            }
        }
    }
}
