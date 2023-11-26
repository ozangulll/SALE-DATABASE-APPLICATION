using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQL_DATABASE_SALE
{
    public partial class FrmCategories : Form
    {
        public FrmCategories()
        {
            InitializeComponent();
        }

        
        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-30HS78PS\SQLEXPRESS;Initial Catalog=DbSale;Integrated Security=True");
        void List()
        {
            SqlCommand comm = new SqlCommand("Select * from TBLCategory", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            List();


        }
        private void FrmCategories_Load(object sender, EventArgs e)
        {
            List();
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand sqlCommand2 = new SqlCommand("insert into TBLCATEGORY (CategoryName) values(@p1)",connection);
            sqlCommand2.Parameters.AddWithValue("@p1",txtCategoryName.Text);
            sqlCommand2.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Category saving was completed succesfully.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCategoryName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand sqlCommand3 = new SqlCommand("Delete from TBLCATEGORY where CategoryID=@p1",connection);
            sqlCommand3.Parameters.AddWithValue("@p1", txtCategoryID.Text);
            sqlCommand3.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Category Deleting was completed succesfully");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand sqlCommand4 = new SqlCommand("update TBLCATEGORY set CategoryName=@p1 where CategoryID=@p2", connection);
            sqlCommand4.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            sqlCommand4.Parameters.AddWithValue("@p2", txtCategoryID.Text);
            sqlCommand4.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Updating was completed succesfully.");
        }


        //Data Source=LAPTOP-30HS78PS\SQLEXPRESS;Initial Catalog=DbSale;Integrated Security=True
    }
}
