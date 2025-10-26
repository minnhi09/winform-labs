using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Basic_Command
{
    public class DataProvider
    {
        private static DataProvider _instance;
        private readonly string connectionSTR;

        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataProvider();
                return _instance;
            }
        }

        private DataProvider()
        {
            connectionSTR = "Server=MNHY\\SQLEXPRESS01;Initial Catalog=RestaurantManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        }

        // Dùng để chạy query bình thường hoặc "EXEC proc..."
        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Nếu muốn gọi stored procedure đúng kiểu:
                // nếu query không bắt đầu bằng "EXEC ", bạn có thể set command.CommandType = CommandType.StoredProcedure
                // Nhưng để giữ đơn giản, nếu bạn dùng phương thức riêng để gọi proc, thì ExecuteQuery giữ CommandType.Text.

                command.CommandType = CommandType.Text;

                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }

            return data;
        }

        public DataTable ExecuteStoredProcedure(string procName, params SqlParameter[] procParams)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            using (SqlCommand command = new SqlCommand(procName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (procParams != null && procParams.Length > 0)
                {
                    command.Parameters.AddRange(procParams);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }
            return data;
        }

        // ExecuteNonQuery nếu bạn muốn insert/update/delete
        public int ExecuteNonQueryStoredProcedure(string procName, params SqlParameter[] procParams)
        {
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            using (SqlCommand command = new SqlCommand(procName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (procParams != null && procParams.Length > 0)
                {
                    command.Parameters.AddRange(procParams);
                }
                connection.Open();
                int numOfRowsEffectted = command.ExecuteNonQuery();
                return numOfRowsEffectted;
            }
        }
    }
}
