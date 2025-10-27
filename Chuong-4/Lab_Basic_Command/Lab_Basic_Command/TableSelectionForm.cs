using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    public partial class TableSelectionForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";
        
        public int SelectedTableID { get; private set; }

        public TableSelectionForm()
        {
            InitializeComponent();
        }

        private void TableSelectionForm_Load(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void LoadTables()
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            // Chỉ lấy các bàn đang trống hoặc có thể tạo hóa đơn mới
            cmd.CommandText = @"
                SELECT ID, Name, Status, Notes 
                FROM [Table] 
                ORDER BY Name
            ";

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                lvTables.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["ID"].ToString());
                    item.SubItems.Add(reader["Name"].ToString());
                    item.SubItems.Add(reader["Status"].ToString());
                    item.SubItems.Add(reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "");

                    lvTables.Items.Add(item);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void lvTables_DoubleClick(object sender, EventArgs e)
        {
            btnSelect.PerformClick();
        }

        private void lvTables_Click(object sender, EventArgs e)
        {
            btnSelect.Enabled = lvTables.SelectedItems.Count > 0;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lvTables.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bàn", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedTableID = int.Parse(lvTables.SelectedItems[0].SubItems[0].Text);
            string tableName = lvTables.SelectedItems[0].SubItems[1].Text;

            DialogResult result = MessageBox.Show(
                $"Tạo hóa đơn mới cho bàn '{tableName}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
