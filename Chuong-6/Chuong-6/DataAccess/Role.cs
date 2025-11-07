using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuong_6.DataAccess
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Path { get; set; }
        public string? Notes { get; set; }
    }
}
