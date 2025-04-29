using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auction_Management_System_Project
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void auctionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AuctionManagementForm form = new AuctionManagementForm();
            form.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            BidManagementForm form = new BidManagementForm();
            form.Show();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm();
            form.Show(this);
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                userFeaturesToolStripMenuItem.Visible = true; // Make "User Features" visible after successful login
            }
        }

        private void authenticationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UserDashboardForm form = new UserDashboardForm();
            form.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            WatchlistForm form = new WatchlistForm();
            form.Show();
        }

       /* private void createAuctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createAuctioinForm form = new WatchlistForm();
            form.Show();
        }

        private void viewAuctionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }*/
    }
}
