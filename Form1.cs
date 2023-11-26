using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_DATABASE_SALE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                FrmCustomer customer = new FrmCustomer();
            customer.Show();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-30HS78PS\SQLEXPRESS;Initial Catalog=DbSale;Integrated Security=True");
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCategory(object sender, EventArgs e)
        {
            FrmCategories frm = new FrmCategories();
            frm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand sql = new SqlCommand("execute test4", connection);
            SqlDataAdapter da = new SqlDataAdapter(sql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //data graphic
            SqlCommand comm = new SqlCommand("select CategoryName,Count(*) from TBLCategory INNER JOIN TBLProduct on TBLCategory.CategoryID=TBLProduct.ProductCategory Group By CategoryName", connection);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Categories"].Points.AddXY(dr[0], dr[1]);
                
            }
            connection.Close();
            connection.Open();
            SqlCommand comm2 = new SqlCommand("select ClientCity,COUNT(*) from TBLClient group by ClientCity", connection);
            SqlDataReader dr2 = comm2.ExecuteReader();
            while (dr2.Read())
            {
                chart2.Series["Cities"].Points.AddXY(dr2[0], dr2[1]);

            }
            connection.Close();

        }

        private void dataGridView1_AutoSizeColumnModeChanged(object sender, DataGridViewAutoSizeColumnModeEventArgs e)
        {
                
        }
    }
}
