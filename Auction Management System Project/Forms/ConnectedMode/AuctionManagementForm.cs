using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Auction_Management_System_Project
{
    public partial class AuctionManagementForm : Form
    {
        private readonly string _loggedInUserId;
        private readonly OracleConnection _conn;

        public AuctionManagementForm(string loggedInUserId)
        {
            InitializeComponent();
            _loggedInUserId = loggedInUserId;
            _conn = new OracleConnection("Data Source=ORCL;User Id=scott;Password=scott");
            textBox7.Text = _loggedInUserId;
            textBox7.ReadOnly = true;

            // Make comboBox non-editable
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void AuctionManagementForm_Load(object sender, EventArgs e)
        {
            try
            {
                _conn.Open();
                LoadUserAuctions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection failed: {ex.Message}");
                this.Close();
            }
        }

        private void LoadUserAuctions()
        {
            comboBox1.Items.Clear();
            using (var cmd = new OracleCommand(
                "SELECT AUCTIONID FROM AUCTIONS WHERE CREATORID = :creatorId", _conn))
            {
                cmd.Parameters.Add("creatorId", OracleDbType.Varchar2).Value = _loggedInUserId;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr["AUCTIONID"]);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string newAuctionId = GetNextAuctionId();

            using (var cmd = new OracleCommand(
                @"INSERT INTO AUCTIONS 
                (AUCTIONID, TITLE, DESCRIPTION, STARTDATE, ENDDATE, STATUS, CREATORID) 
                VALUES (:id, :title, :descr, :sdate, :edate, :status, :creator)", _conn))
            {
                cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = newAuctionId;
                cmd.Parameters.Add("title", OracleDbType.Varchar2).Value = textBox2.Text;
                cmd.Parameters.Add("descr", OracleDbType.Varchar2).Value = textBox3.Text;
                cmd.Parameters.Add("sdate", OracleDbType.Date).Value = DateTime.Parse(textBox4.Text);
                cmd.Parameters.Add("edate", OracleDbType.Date).Value = DateTime.Parse(textBox5.Text);
                cmd.Parameters.Add("status", OracleDbType.Varchar2).Value = textBox6.Text;
                cmd.Parameters.Add("creator", OracleDbType.Varchar2).Value = _loggedInUserId;

                try
                {
                    int r = cmd.ExecuteNonQuery();
                    if (r > 0)
                    {
                        comboBox1.Items.Add(newAuctionId);
                        MessageBox.Show($"Auction added successfully! New ID: {newAuctionId}");
                        ClearForm();
                    }
                }
                catch (OracleException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                }
            }
        }

        private string GetNextAuctionId()
        {
            // Get next sequence value from database
            using (var cmd = new OracleCommand("SELECT SEQ_AUCTION_ID.NEXTVAL FROM DUAL", _conn))
            {
                object result = cmd.ExecuteScalar();
                return result.ToString();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please fill all required fields");
                return false;
            }

            if (!DateTime.TryParse(textBox4.Text, out _) ||
                !DateTime.TryParse(textBox5.Text, out _))
            {
                MessageBox.Show("Invalid date format (use YYYY-MM-DD)");
                return false;
            }

            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            using (var cmd = new OracleCommand(
                "SELECT TITLE, DESCRIPTION, STARTDATE, ENDDATE, STATUS FROM AUCTIONS WHERE AUCTIONID = :id", _conn))
            {
                cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = comboBox1.SelectedItem.ToString();

                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        textBox2.Text = rd["TITLE"].ToString();
                        textBox3.Text = rd["DESCRIPTION"].ToString();
                        textBox4.Text = Convert.ToDateTime(rd["STARTDATE"]).ToString("yyyy-MM-dd");
                        textBox5.Text = Convert.ToDateTime(rd["ENDDATE"]).ToString("yyyy-MM-dd");
                        textBox6.Text = rd["STATUS"].ToString();
                    }
                }
            }
        }

        private void ClearForm()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void AuctionManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _conn?.Dispose();
        }

        private void btnNewAuction_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}