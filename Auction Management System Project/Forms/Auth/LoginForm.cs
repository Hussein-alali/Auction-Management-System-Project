using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Auction_Management_System_Project
{
    public partial class LoginForm : Form
    {
        private OracleConnection connection;
        private const string ConnectionString = "Data Source=ORCL;User Id=scott;Password=scott";
        public string LoggedInUserId { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            try
            {
                connection = new OracleConnection(ConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection failed: {ex.Message}");
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (connection?.State != ConnectionState.Open) return;

            try
            {
                using (OracleCommand cmd = new OracleCommand(
                    "SELECT USERID FROM USERS WHERE NAME = :name AND PASSWORD = :pwd", connection))
                {
                    cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = txtUsername.Text;
                    cmd.Parameters.Add("pwd", OracleDbType.Varchar2).Value = txtPassword.Text;

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        LoggedInUserId = result.ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new RegisterForm().Show();
            this.Hide();
        }
    }
}