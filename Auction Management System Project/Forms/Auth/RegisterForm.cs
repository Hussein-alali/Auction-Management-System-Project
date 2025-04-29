using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Auction_Management_System_Project
{
    public partial class RegisterForm : Form
    {
        // Connection string to your database
        private string connectionString = "YourConnectionStringHere";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Initialize any logic for form load if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = registerUsernameTextBox.Text;
            string password = registerPasswordTextBox.Text;
            string confirmPassword = registerConfirmPasswordTextBox.Text;

            // Validation
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

            // Check if the username already exists in the database
            if (IsUsernameTaken(username))
            {
                MessageBox.Show("Username is already taken. Please choose a different username.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save the new user to the database
            if (SaveUserToDatabase(username, password))
            {
                MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close the registration form after success
            }
            else
            {
                MessageBox.Show("Registration Failed, please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsUsernameTaken(string username)
        {
            // Check if the username exists in the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();

                return count > 0; // Return true if username exists
            }
        }

        private bool SaveUserToDatabase(string username, string password)
        {
            // Insert new user into the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Optional: Handle text change events for validation (if necessary)
        private void registerUsernameTextBox_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void registerPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void registerConfirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
