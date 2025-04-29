using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Auction_Management_System_Project
{
    public partial class UserDashboardForm : Form
    {
        private string connectionString = "Data Source = ORCL; User Id = scott; Password = scott ";
        private OracleDataAdapter dataAdapter;
        private DataSet dataSet;
        private DataGridView dataGViewUsers;
        private OracleConnection connection;




        public UserDashboardForm()
        {
            InitializeComponent();
        }

        private void UserDashboardForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Users";
                dataAdapter = new OracleDataAdapter(query, connection);
                OracleCommandBuilder builder = new OracleCommandBuilder(dataAdapter);

                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Users");

                usersGridView.DataSource = dataSet.Tables["Users"];
                usersGridView.Columns["UserID"].ReadOnly = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommandBuilder builder = new OracleCommandBuilder(dataAdapter);
                dataAdapter.UpdateCommand = builder.GetUpdateCommand();

                dataAdapter.Update(dataSet, "Users");
                MessageBox.Show("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message);
            }
        }


    }
}
