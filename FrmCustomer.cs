using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace SQL_DATABASE_SALE
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-30HS78PS\SQLEXPRESS;Initial Catalog=DbSale;Integrated Security=True");
        void List()
        {
          
                conn.Open();

                SqlCommand komut = new SqlCommand("Select * from TBLClient", conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(); // The SqlDataAdapter is created, but its SelectCommand is not set.

                // Set the SelectCommand property of the SqlDataAdapter
                dataAdapter.SelectCommand = komut;

                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
            conn.Close();
         
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            List();
            conn.Open();
            SqlCommand comm1 = new SqlCommand("Select * from SEHIR", conn);
            SqlDataReader dr = comm1.ExecuteReader();
            while (dr.Read())
            {
                comboCity.Items.Add(dr["sehir"]);
            }
            conn.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); // Assuming name is in the second column (index 1)
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(); // Assuming surname is in the third column (index 2)
            comboCity.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(); // Assuming city is in the fourth column (index 3)
            txtBalance.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(); // Assuming balance is in the fifth column (index 4)
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand CommandSave = new SqlCommand("INSERT INTO TBLClient (ClientName,ClientSurname,ClientCity,ClientBalance) VALUES (@p1,@p2,@p3,@p4)", conn);
            CommandSave.Parameters.AddWithValue("@p1", txtName.Text);
            CommandSave.Parameters.AddWithValue("@p2", txtSurname.Text);
            CommandSave.Parameters.AddWithValue("@p3", comboCity.Text) ;
            CommandSave.Parameters.AddWithValue("@p4",  decimal.Parse(txtBalance.Text));
            CommandSave.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Client has been added to the list.");

        }

        private void btnDelete_Click(object sender, EventArgs e)

        {
            conn.Open();
            SqlCommand CommandDelete = new SqlCommand("Delete From TBLClient Where ClientID=@p1", conn);
            CommandDelete.Parameters.AddWithValue("@p1", txtID.Text);
            CommandDelete.ExecuteNonQuery();
            MessageBox.Show("The Client has been deleted");
            conn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)

        {
            conn.Open();
            SqlCommand CommandUpdate = new SqlCommand("Update TBLClient set ClientName=@p1, ClientSurname=@p2,ClientCity=@p3,ClientBalance=@p4 where ClientID=@p5", conn);
            CommandUpdate.Parameters.AddWithValue("@p1", txtName.Text);
            CommandUpdate.Parameters.AddWithValue("@p2", txtSurname.Text);
            CommandUpdate.Parameters.AddWithValue("@p3", comboCity.Text);
            CommandUpdate.Parameters.AddWithValue("@p4", decimal.Parse(txtBalance.Text));
            CommandUpdate.Parameters.AddWithValue("@p5", txtID.Text);
            CommandUpdate.ExecuteNonQuery();
            MessageBox.Show("The Client has been updated.");
            conn.Close();
            List();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand commandSearch = new SqlCommand("Select * from TBLClient where ClientName=@p1",conn);
            commandSearch.Parameters.AddWithValue("@p1", txtName.Text);
            SqlDataAdapter da = new SqlDataAdapter(commandSearch);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
          
            
        }
    }
}
