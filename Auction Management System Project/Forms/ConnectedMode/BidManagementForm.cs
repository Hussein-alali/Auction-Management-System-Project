using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auction_Management_System.Entities;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace Auction_Management_System_Project
{
    public partial class BidManagementForm: Form
    {
        string ordb = "Data Source = ORCL; User Id = scott; Password = scott ";
        OracleConnection conn;
        public BidManagementForm()
        {
            InitializeComponent();
        }

        private void BidManagementForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "SELECT AUCTIONID FROM AUCTIONS";
            c.CommandType = CommandType.Text;

            OracleDataReader rd = c.ExecuteReader();
            while (rd.Read())
            {
                comboBox1.Items.Add(rd[0]);
            }
        }

        private void BidManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT BIDID FROM BIDS where AUCTIONID= :id";
            cmd.Parameters.Add("id", comboBox1.SelectedItem.ToString());
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0]);
            }
            reader.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT BIDTIME,BIDAMOUNT,USERID FROM BIDS where BIDID= :id";
            cmd.Parameters.Add("id", comboBox2.SelectedItem.ToString());
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[0].ToString();
                textBox2.Text = reader[1].ToString();
                textBox3.Text = reader[2].ToString();
            }
            reader.Close();
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
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter a Bid Time.");
                return;
            }
            if (!DateTime.TryParse(textBox1.Text, out DateTime bidTime))
            {
                MessageBox.Show("Invalid Bid Time format.");
                return;
            }

            int auctionId = Convert.ToInt32(comboBox1.Text);
            int userId = Convert.ToInt32(textBox3.Text);

           
            if (!IsAuctionExist(auctionId))
            {
                MessageBox.Show("Selected Auction ID does not exist.");
                return;
            }
            if (!IsAuctionActive(auctionId))
            {
                MessageBox.Show("Selected Auction is not active.");
                return;
            }
            if (!IsUserExist(userId))
            {
                MessageBox.Show("Entered User ID does not exist.");
                return;
            }
            decimal maxExistingBid = GetMaxBidAmount(auctionId);
            if (bidAmount <= maxExistingBid)
            {
                MessageBox.Show($"Your Bid Amount must be greater than the current maximum bid ({maxExistingBid}).");
                return;
            }
            int newBidId = GetNextBidId()+1;
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO BIDS (BIDID, AUCTIONID, USERID, BIDAMOUNT, BIDTIME) " +
                                  "VALUES (:bidID, :auctionid, :userid, :bidamount, :bidtime)";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("bidID", newBidId);
                cmd.Parameters.Add("auctionid", auctionId);
                cmd.Parameters.Add("userid", userId);
                cmd.Parameters.Add("bidamount", bidAmount);
                cmd.Parameters.Add("bidtime", bidTime);

                int r = cmd.ExecuteNonQuery();
                if (r != -1)
                {
                    comboBox2.Items.Add(newBidId);
                
                    MessageBox.Show("Bid added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while adding bid: " + ex.Message);
            }
        }
        private int GetNextBidId()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(BIDID) FROM BIDS";
            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
        private bool IsAuctionActive(int auctionId)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT STATUS FROM AUCTIONS WHERE AUCTIONID = :id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", auctionId);

            object status = cmd.ExecuteScalar();
            if (status != null && status.ToString().ToUpper() == "ACTIVE")
                return true;
            else
                return false;
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
        private bool IsAuctionExist(int auctionId)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM AUCTIONS WHERE AUCTIONID = :id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", auctionId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        private bool IsUserExist(int userId)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM USERS WHERE USERID = :id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", userId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
