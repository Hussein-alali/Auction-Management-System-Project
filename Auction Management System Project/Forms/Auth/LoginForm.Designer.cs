using System.Windows.Forms;

namespace Auction_Management_System_Project
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private LinkLabel lnkRegister;
        private Label lblUsername;
        private Label lblPassword;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Username Label
            this.lblUsername = new Label();
            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new System.Drawing.Point(20, 20);
            this.lblUsername.AutoSize = true;

            // Username TextBox
            this.txtUsername = new TextBox();
            this.txtUsername.Location = new System.Drawing.Point(20, 40);
            this.txtUsername.Size = new System.Drawing.Size(200, 20);

            // Password Label
            this.lblPassword = new Label();
            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new System.Drawing.Point(20, 70);
            this.lblPassword.AutoSize = true;

            // Password TextBox
            this.txtPassword = new TextBox();
            this.txtPassword.Location = new System.Drawing.Point(20, 90);
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.PasswordChar = '*';

            // Login Button
            this.btnLogin = new Button();
            this.btnLogin.Text = "Login";
            this.btnLogin.Location = new System.Drawing.Point(20, 120);
            this.btnLogin.Size = new System.Drawing.Size(200, 30);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Register Link
            this.lnkRegister = new LinkLabel();
            this.lnkRegister.Text = "Create new account";
            this.lnkRegister.Location = new System.Drawing.Point(20, 160);
            this.lnkRegister.AutoSize = true;
            this.lnkRegister.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkRegister_LinkClicked);

            // Form Settings
            this.ClientSize = new System.Drawing.Size(250, 200);
            this.Text = "Login";
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lnkRegister);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}