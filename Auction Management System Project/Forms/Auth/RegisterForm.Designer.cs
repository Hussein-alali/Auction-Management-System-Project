namespace Auction_Management_System_Project
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // usernameTextBox
            this.usernameTextBox.Location = new System.Drawing.Point(140, 30);
            this.usernameTextBox.Size = new System.Drawing.Size(200, 26);

            // emailTextBox
            this.emailTextBox.Location = new System.Drawing.Point(140, 70);
            this.emailTextBox.Size = new System.Drawing.Size(200, 26);

            // passwordTextBox
            this.passwordTextBox.Location = new System.Drawing.Point(140, 110);
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(200, 26);

            // confirmPasswordTextBox
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(140, 150);
            this.confirmPasswordTextBox.PasswordChar = '*';
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(200, 26);

            // registerButton
            this.registerButton.Location = new System.Drawing.Point(140, 190);
            this.registerButton.Size = new System.Drawing.Size(200, 35);
            this.registerButton.Text = "Register";
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);

            // Labels
            this.label1.Text = "Username:";
            this.label1.Location = new System.Drawing.Point(40, 30);
            this.label2.Text = "Email:";
            this.label2.Location = new System.Drawing.Point(40, 70);
            this.label3.Text = "Password:";
            this.label3.Location = new System.Drawing.Point(40, 110);
            this.label4.Text = "Confirm Password:";
            this.label4.Location = new System.Drawing.Point(40, 150);

            // Form settings
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Text = "Register";
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.usernameTextBox,
                this.emailTextBox,
                this.passwordTextBox,
                this.confirmPasswordTextBox,
                this.registerButton,
                this.label1,
                this.label2,
                this.label3,
                this.label4
            });
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}