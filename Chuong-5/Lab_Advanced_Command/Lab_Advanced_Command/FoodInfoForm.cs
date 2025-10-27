using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class FoodInfoForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";

        public FoodInfoForm()
        {
            InitializeComponent();
        }

        private void FoodInfoForm_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT ID, Name FROM Categories;";

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            // 4. dong ket noi 
            sqlConnection.Close();
            sqlCommand.Dispose();

            // 5. ui logic
            cboCategory.DataSource = dataTable;
            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "ID";
        }

        private void ResetText()
        {
            txtFoodId.ResetText();
            txtFoodName.ResetText();
            txtUnit.ResetText();
            txtNotes.ResetText();
            numPrice.ResetText();

            cboCategory.ResetText();
        }

        private void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            // TODO: Open form to add new category
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // TODO: Add new food logic
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // TODO: Update food logic
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
