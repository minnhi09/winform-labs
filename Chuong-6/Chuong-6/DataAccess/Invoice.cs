using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuong_6.DataAccess
{
    public class Invoice
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TableID { get; set; }
        public string? TableName { get; set; } // Thêm property này để hỗ trợ InvoiceDA
        public int Total { get; set; }
        public float Discount { get; set; }
        public float Tax { get; set; }
        public bool Status { get; set; } // false: chưa thanh toán, true: đã thanh toán
        public string AccountID { get; set; } = string.Empty;
        public string? AccountName { get; set; } // Thêm property này để hỗ trợ InvoiceDA
        public DateTime CheckoutDate { get; set; }
    }
}
