using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Net.Mail;
using System.Data;

namespace Auction_Management_System_Project
{
    public partial class RegisterForm : Form
    {
        private OracleConnection connection;
        private const string ConnectionString = "Data Source=ORCL;User Id=scott;Password=scott";

        public RegisterForm()
        {
            InitializeComponent();
            InitializeDatabaseConnection(); // Initialize connection early
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
                this.Close(); // Close form if connection fails
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            // Check if connection is valid before using it
            if (connection == null || connection.State != ConnectionState.Open)
            {
                MessageBox.Show("Database connection is not available");
                return;
            }

            if (ValidateInputs())
            {
                RegisterUser();
            }
        }

        private bool ValidateInputs()
        {
            // Existing validation logic
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) ||
                string.IsNullOrWhiteSpace(emailTextBox.Text) ||
                string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Please fill all fields");
                return false;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Passwords do not match");
                return false;
            }

            try
            {
                new MailAddress(emailTextBox.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Invalid email format");
                return false;
            }
        }

        private void RegisterUser()
        {
            try
            {
                using (var cmd = new OracleCommand(
                    "INSERT INTO USERS (USERID, NAME, EMAIL, PASSWORD) " +
                    "VALUES (SEQ_USER_ID.NEXTVAL, :name, :email, :password)",
                    connection))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = usernameTextBox.Text;
                    cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = emailTextBox.Text;
                    cmd.Parameters.Add("password", OracleDbType.Varchar2).Value = passwordTextBox.Text;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!");
                        this.Close();
                    }
                }
            }
            catch (OracleException ex) when (ex.Number == 1)
            {
                MessageBox.Show("Username or email already exists");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Dispose();
                connection = null; // Clear reference
            }
        }
    }
}