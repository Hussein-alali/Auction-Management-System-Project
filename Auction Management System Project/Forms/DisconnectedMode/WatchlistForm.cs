
using System;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

// For Oracle DB (ensure you reference the correct Oracle library)
using System.Windows.Forms;

namespace Auction_Management_System_Project
{
    public partial class WatchlistForm : Form
    {

        private string connectionString = "Data Source = ORCL; User Id = scott; Password = scott ";
        private OracleDataAdapter adapter;
        private DataTable watchlistTable;
        private OracleConnection conn;

        public WatchlistForm()
        {
            InitializeComponent();
        }

        private void WatchlistForm_Load(object sender, EventArgs e)
        {
            LoadWatchlist();
        }

        private void LoadWatchlist()
        {
            try
            {
                conn = new OracleConnection(connectionString);
                string query = "SELECT * FROM Watchlist";
                adapter = new OracleDataAdapter(query, conn);
                watchlistTable = new DataTable();

                adapter.Fill(watchlistTable);

                dataGridView1.DataSource = watchlistTable;


                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
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
                decimal newID = 1;

                if (watchlistTable.Rows.Count > 0)
                {
                    newID = watchlistTable.AsEnumerable()
                                          .Max(row => row.Field<decimal>("WatchlistID")) + 1;
                }

                DataRow newRow = watchlistTable.NewRow();
                newRow["WatchlistID"] = newID;
                newRow["UserID"] = decimal.Parse(txtUserID.Text);
                newRow["AuctionID"] = decimal.Parse(txtAuctionID.Text);
                newRow["AddedDate"] = DateTime.Now;

                watchlistTable.Rows.Add(newRow);

                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                adapter.Update(watchlistTable);

                MessageBox.Show("Added to watchlist.");
                LoadWatchlist(); // Refresh the data
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert failed: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dataGridView1.Rows.Remove(row);
                }

                adapter.Update(watchlistTable);
                MessageBox.Show("Deleted from watchlist.");
                LoadWatchlist();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
