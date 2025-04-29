using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Auction_Management_System_Project
{
    public partial class RegisterForm : Form
    {
        private string ordb = "Data Source = ORCL; User Id = scott; Password = scott";
        private OracleConnection conn;

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conn != null)
                conn.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = registerUsernameTextBox.Text;
            string password = registerPasswordTextBox.Text;
            string confirmPassword = registerConfirmPasswordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsUsernameTaken(username))
            {
                MessageBox.Show("Username is already taken. Please choose a different username.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SaveUserToDatabase(username, password))
            {
                MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration Failed, please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsUsernameTaken(string username)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM USERS WHERE USERNAME = :username";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("username", username);

            try
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking username: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Assume username taken if error occurs
            }
        }

        private bool SaveUserToDatabase(string username, string password)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO USERS (USERNAME, PASSWORD) VALUES (:username, :password)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("username", username);
            cmd.Parameters.Add("password", password);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //private void registerUsernameTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    // Optional
        //}

        //private void registerPasswordTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    // Optional
        //}

        //private void registerConfirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    // Optional
        //}
    }
}
