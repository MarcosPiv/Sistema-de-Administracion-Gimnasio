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

namespace gym
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mpivi\OneDrive\Documentos\GymDB.mdf;Integrated Security=True;Connect Timeout=30 ");
        private void FillName()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select MName from MemberTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("MName", typeof(string));
            dt.Load(rdr);
            NameCb.ValueMember = "MName";
            NameCb.DataSource = dt;
            con.Close();

        }
        private void filterByName()
        {
            con.Open();
            string query = "select * from PaymentTbl where PMember='"+ SearchName.Text +"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PaymentDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void populate()
        {
            con.Open();
            string query = "select * from PaymentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PaymentDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Payment_Load(object sender, EventArgs e)
        {
            FillName();
            populate();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ///NameTb.Text = "";
            AmountTb.Text = "";

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
        
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if(NameCb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Falta o faltan campos por completar");
            }
            else
            {
                string payperiode = Periode.Value.Month.ToString() + Periode.Value.Year.ToString();
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from PaymentTbl where PMember='" + NameCb.SelectedValue.ToString() + "' and PMonth='" + payperiode + "'",con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("Pago Ya Realizado Para Este Mes");
                }
                else
                {
                    string query = "insert into PaymentTbl values('" + payperiode + "' , '" + NameCb.SelectedValue.ToString() + "' , " + AmountTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pago Concretado Con Exito");
                }
                con.Close();
                populate();
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            filterByName();
            SearchName.Text = "";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
