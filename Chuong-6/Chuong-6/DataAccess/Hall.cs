using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuong_6.DataAccess
{
    public class Hall
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; } // Thêm thuộc tính này
    }
}
