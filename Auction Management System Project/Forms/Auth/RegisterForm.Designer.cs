namespace Auction_Management_System_Project
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.registerUsernameTextBox = new System.Windows.Forms.TextBox();
            this.registerConfirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.registerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // registerUsernameTextBox
            // 
            this.registerUsernameTextBox.Location = new System.Drawing.Point(37, 31);
            this.registerUsernameTextBox.Name = "registerUsernameTextBox";
            this.registerUsernameTextBox.Size = new System.Drawing.Size(100, 22);
            this.registerUsernameTextBox.TabIndex = 0;
            this.registerUsernameTextBox.Text = "username";
            this.registerUsernameTextBox.TextChanged += new System.EventHandler(this.registerUsernameTextBox_TextChanged);
            // 
            // registerConfirmPasswordTextBox
            // 
            this.registerConfirmPasswordTextBox.Location = new System.Drawing.Point(37, 122);
            this.registerConfirmPasswordTextBox.Name = "registerConfirmPasswordTextBox";
            this.registerConfirmPasswordTextBox.Size = new System.Drawing.Size(100, 22);
            this.registerConfirmPasswordTextBox.TabIndex = 1;
            this.registerConfirmPasswordTextBox.Text = "confirm password";
            this.registerConfirmPasswordTextBox.TextChanged += new System.EventHandler(this.registerConfirmPasswordTextBox_TextChanged);
            // 
            // registerPasswordTextBox
            // 
            this.registerPasswordTextBox.Location = new System.Drawing.Point(37, 75);
            this.registerPasswordTextBox.Name = "registerPasswordTextBox";
            this.registerPasswordTextBox.Size = new System.Drawing.Size(100, 22);
            this.registerPasswordTextBox.TabIndex = 2;
            this.registerPasswordTextBox.Text = "password";
            this.registerPasswordTextBox.TextChanged += new System.EventHandler(this.registerPasswordTextBox_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "register";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.registerPasswordTextBox);
            this.Controls.Add(this.registerConfirmPasswordTextBox);
            this.Controls.Add(this.registerUsernameTextBox);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox registerUsernameTextBox;
        private System.Windows.Forms.TextBox registerConfirmPasswordTextBox;
        private System.Windows.Forms.TextBox registerPasswordTextBox;
        private System.Windows.Forms.Button button1;
    }
}