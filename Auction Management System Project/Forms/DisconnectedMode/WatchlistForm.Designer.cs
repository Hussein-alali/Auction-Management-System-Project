namespace Auction_Management_System_Project
{
    partial class WatchlistForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.ComboBox cmbAuction;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblAuction;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.cmbAuction = new System.Windows.Forms.ComboBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblAuction = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeight = 34;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(797, 250);
            this.dataGridView1.TabIndex = 0;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(100, 277);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(120, 26);
            this.txtUserID.TabIndex = 1;
            // 
            // cmbAuction
            // 
            this.cmbAuction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuction.Location = new System.Drawing.Point(323, 276);
            this.cmbAuction.Name = "cmbAuction";
            this.cmbAuction.Size = new System.Drawing.Size(201, 28);
            this.cmbAuction.TabIndex = 2;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(30, 280);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(68, 20);
            this.lblUserID.TabIndex = 3;
            this.lblUserID.Text = "User ID:";
            // 
            // lblAuction
            // 
            this.lblAuction.AutoSize = true;
            this.lblAuction.Location = new System.Drawing.Point(250, 280);
            this.lblAuction.Name = "lblAuction";
            this.lblAuction.Size = new System.Drawing.Size(67, 20);
            this.lblAuction.TabIndex = 4;
            this.lblAuction.Text = "Auction:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(583, 275);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 28);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(699, 275);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 28);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // WatchlistForm
            // 
            this.ClientSize = new System.Drawing.Size(835, 325);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.cmbAuction);
            this.Controls.Add(this.lblUserID);
            this.Controls.Add(this.lblAuction);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Name = "WatchlistForm";
            this.Text = "Watchlist";
            this.Load += new System.EventHandler(this.WatchlistForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
