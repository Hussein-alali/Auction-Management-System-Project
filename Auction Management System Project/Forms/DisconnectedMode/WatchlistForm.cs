using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Auction_Management_System_Project
{
    public partial class WatchlistForm : Form
    {
        private string _userId;
        private OracleDataAdapter adapter;
        private DataTable watchlistTable;
        private OracleConnection conn;
        private string connectionString = "Data Source=ORCL;User Id=scott;Password=scott;";

        public WatchlistForm(string userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void WatchlistForm_Load(object sender, EventArgs e)
        {
            txtUserID.Text = _userId;
            txtUserID.ReadOnly = true;
            LoadAuctions();
            LoadWatchlist();
        }

        private void LoadAuctions()
        {
           
                using (conn = new OracleConnection(connectionString))
                {
                    conn.Open();
                string query = "SELECT AuctionID, Title FROM AUCTIONS";
                OracleCommand cmd = new OracleCommand(query, conn);
                    OracleDataAdapter auctionAdapter = new OracleDataAdapter(cmd);
                    DataTable auctionTable = new DataTable();
                    auctionAdapter.Fill(auctionTable);

                    cmbAuction.DisplayMember = "Title";
                    cmbAuction.ValueMember = "AuctionID";
                    cmbAuction.DataSource = auctionTable;
                }
            
           
        }

        private void LoadWatchlist()
        {
            try
            {
                conn = new OracleConnection(connectionString);
                string query = "SELECT * FROM Watchlist WHERE UserID = :userId";
                adapter = new OracleDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.Add("userId", OracleDbType.Int32).Value = int.Parse(_userId);

                watchlistTable = new DataTable();
                adapter.Fill(watchlistTable);

                // Configure primary key for proper update tracking
                watchlistTable.PrimaryKey = new DataColumn[] { watchlistTable.Columns["WATCHLISTID"] };

                dataGridView1.DataSource = watchlistTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading watchlist: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    string insertQuery = @"
                INSERT INTO Watchlist (WatchlistID, UserID, AuctionID, AddedDate) 
                VALUES (WATCHLIST_SEQ.NEXTVAL, :UserID, :AuctionID, :AddedDate)";

                    OracleCommand insertCmd = new OracleCommand(insertQuery, conn);
                    insertCmd.Parameters.Add("UserID", OracleDbType.Int32).Value = int.Parse(_userId);
                    insertCmd.Parameters.Add("AuctionID", OracleDbType.Int32).Value = Convert.ToInt32(cmbAuction.SelectedValue);
                    insertCmd.Parameters.Add("AddedDate", OracleDbType.Date).Value = DateTime.Now;

                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Added to watchlist.");
                        LoadWatchlist(); // Refresh with new data
                    }
                    else
                    {
                        MessageBox.Show("Insert failed. No rows affected.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert failed: " + ex.Message);
            }
        }


        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataRow newRow = watchlistTable.NewRow();
        //        newRow["UserID"] = int.Parse(_userId);
        //        newRow["AuctionID"] = cmbAuction.SelectedValue;
        //        newRow["AddedDate"] = DateTime.Now;

        //        watchlistTable.Rows.Add(newRow);

        //        OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
        //        adapter.Update(watchlistTable);

        //        MessageBox.Show("Added to watchlist.");
        //        LoadWatchlist();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Insert failed: " + ex.Message);
        //    }
        //}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        // Mark the row for deletion in the DataTable
                        DataRowView drv = (DataRowView)row.DataBoundItem;
                        drv.Row.Delete(); // Properly mark for deletion
                    }
                }

                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                adapter.Update(watchlistTable);

                MessageBox.Show("Deleted from watchlist.");
                LoadWatchlist(); // Refresh the grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed: " + ex.Message);
            }
        }
    }
}
