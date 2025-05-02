namespace Auction_Management_System_Project
{
    partial class UserDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView bidsGridView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.bidsGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.bidsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // bidsGridView
            // 
            this.bidsGridView.AllowUserToAddRows = false;
            this.bidsGridView.AllowUserToDeleteRows = false;
            this.bidsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bidsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bidsGridView.Location = new System.Drawing.Point(12, 12);
            this.bidsGridView.Name = "bidsGridView";
            this.bidsGridView.RowHeadersWidth = 51;
            this.bidsGridView.Size = new System.Drawing.Size(776, 446);
            this.bidsGridView.TabIndex = 0;
            // 
            // UserDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.bidsGridView);
            this.Name = "UserDashboardForm";
            this.Text = "User Dashboard";
            this.Load += new System.EventHandler(this.UserDashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bidsGridView)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
