using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Forms;
using System;

namespace Auction_Management_System_Project
{
    public partial class BidManagementForm : Form
    {
        string ordb = "Data Source=ORCL;User Id=scott;Password=scott";
        OracleConnection conn;
        public int LoggedInUserId { get; set; }

        public BidManagementForm(int loggedInUserId)
        {
            InitializeComponent();
            LoggedInUserId = loggedInUserId;
        }

        private void BidManagementForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "SELECT AUCTIONID, TITLE FROM AUCTIONS";
            c.CommandType = CommandType.Text;

            OracleDataReader rd = c.ExecuteReader();
            while (rd.Read())
            {
                string auctionInfo = $"{rd[0]} - {rd[1]}";
                comboBox1.Items.Add(auctionInfo);
            }

            textBox3.Text = LoggedInUserId.ToString();
            rd.Close();
        }

        private void BidManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split('-');
            int auctionId = Convert.ToInt32(parts[0].Trim());
            decimal maxBidAmount = GetMaxBidAmount(auctionId);
            textBox2.Text = maxBidAmount.ToString("0.00");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select an Auction ID.");
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please enter a User ID.");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter a Bid Amount.");
                return;
            }
            if (!decimal.TryParse(textBox2.Text, out decimal bidAmount) || bidAmount <= 0)
            {
                MessageBox.Show("Bid Amount must be a positive number.");
                return;
            }

            string selectedItem = comboBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split('-');
            int auctionId = Convert.ToInt32(parts[0].Trim());
            int userId = Convert.ToInt32(textBox3.Text);

            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO BIDS (BIDID, AUCTIONID, USERID, BIDAMOUNT, BIDTIME) 
                          VALUES (SEQ_BID_ID.NEXTVAL, :auctionid, :userid, :bidamount, SYSDATE)";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("auctionid", auctionId);
                cmd.Parameters.Add("userid", userId);
                cmd.Parameters.Add("bidamount", bidAmount);

                int r = cmd.ExecuteNonQuery();
                if (r != -1)
                {
                    MessageBox.Show("Bid added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while adding bid: " + ex.Message);
            }
        }

        private decimal GetMaxBidAmount(int auctionId)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT NVL(MAX(BIDAMOUNT), 0) FROM BIDS WHERE AUCTIONID = :id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", auctionId);

            object result = cmd.ExecuteScalar();
            return Convert.ToDecimal(result);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Text changed logic if needed
        }

    }
}