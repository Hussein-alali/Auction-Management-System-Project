using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Auction_Management_System_Project
{
    public partial class AuctionManagementForm: Form
    {
        string ordb = "Data Source = orcl; User Id = hr; Password = hr ";
        OracleConnection conn;
        public AuctionManagementForm()
        {
            InitializeComponent();
        }

        private void AuctionManagementForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        // Add Auction
        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO AUCTIONS (AUCTIONID, TITLE, DESCRIPTION, STARTDATE, ENDDATE, STATUS, CREATORID) " +
                              "VALUES (:id, :title, :desc, :start, :end, :status, :creator)";
            cmd.Parameters.Add("id", Convert.ToInt32(textBox1.Text));
            cmd.Parameters.Add("title", textBox2.Text);
            cmd.Parameters.Add("desc", textBox3.Text);
            cmd.Parameters.Add("start", string.IsNullOrWhiteSpace(textBox4.Text) ? (object)DBNull.Value : DateTime.Parse(textBox4.Text));
            cmd.Parameters.Add("end", string.IsNullOrWhiteSpace(textBox5.Text) ? (object)DBNull.Value : DateTime.Parse(textBox5.Text));
            cmd.Parameters.Add("status", textBox6.Text);
            cmd.Parameters.Add("creator", string.IsNullOrWhiteSpace(textBox7.Text) ? (object)DBNull.Value : Convert.ToInt32(textBox7.Text));

            try
            {
                int rows = cmd.ExecuteNonQuery();
                MessageBox.Show($"{rows} Auction added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Search by Title
        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM AUCTIONS WHERE TITLE = :title";
            cmd.Parameters.Add("title", textBox2.Text);

            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["AUCTIONID"].ToString();
                textBox3.Text = dr["DESCRIPTION"].ToString();
                textBox4.Text = dr["STARTDATE"].ToString();
                textBox5.Text = dr["ENDDATE"].ToString();
                textBox6.Text = dr["STATUS"].ToString();
                textBox7.Text = dr["CREATORID"].ToString();
            }
            else
            {
                MessageBox.Show("Auction not found.");
            }
            dr.Close();
        }
    }
}
