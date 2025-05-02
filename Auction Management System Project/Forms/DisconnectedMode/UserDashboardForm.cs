using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Auction_Management_System_Project
{
    public partial class UserDashboardForm : Form
    {
        private string _userId;
        private string connectionString = "Data Source=ORCL;User Id=scott;Password=scott;";
        private OracleConnection connection;

        public UserDashboardForm(string userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void UserDashboardForm_Load(object sender, EventArgs e)
        {
            LoadUserBids();
        }

        private void LoadUserBids()
        {
            try
            {
                using (connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT B.BidID, B.AuctionID, A.Title AS AuctionTitle, B.BidAmount, B.BidTime
                        FROM Bids B
                        JOIN Auctions A ON B.AuctionID = A.AuctionID
                        WHERE B.UserID = :userId";

                    OracleCommand cmd = new OracleCommand(query, connection);
                    cmd.Parameters.Add("userId", OracleDbType.Int32).Value = int.Parse(_userId);

                    OracleDataAdapter bidsAdapter = new OracleDataAdapter(cmd);
                    DataTable bidsTable = new DataTable();
                    bidsAdapter.Fill(bidsTable);

                    bidsGridView.DataSource = bidsTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bids: " + ex.Message);
            }
        }
    }
}
