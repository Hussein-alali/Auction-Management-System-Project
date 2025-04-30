using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Auction_Management_System_Project
{
    public partial class AuctionManagementForm: Form
    {
        string ordb = "Data Source = ORCL; User Id = scott; Password = scott ";
        OracleConnection conn;
        public AuctionManagementForm()
        {
            InitializeComponent();
        }

        private void AuctionManagementForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select AUCTIONID from AUCTIONS";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }

        // Add Auction
        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"INSERT INTO auctions 
                        (AUCTIONID, TITLE, DESCRIPTION, STARTDATE, ENDDATE, STATUS, CREATORID) 
                        VALUES (:id, :title, :descr, :sdate, :edate, :status, :creator)";

            cmd.Parameters.Add("id", comboBox1.Text);
            cmd.Parameters.Add("title", textBox2.Text);
            cmd.Parameters.Add("descr", textBox3.Text);      
            cmd.Parameters.Add("sdate", Convert.ToDateTime(textBox4.Text));
            cmd.Parameters.Add("edate", Convert.ToDateTime(textBox5.Text));
            cmd.Parameters.Add("status", textBox6.Text);
            cmd.Parameters.Add("creator", textBox7.Text);

            cmd.CommandType = CommandType.Text;

            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                comboBox1.Items.Add(comboBox1.Text);
                MessageBox.Show("Auction added successfully");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select title,description,startdate,enddate,status,creatorid from AUCTIONS where AUCTIONID =:id";
            cmd.Parameters.Add("id", comboBox1.SelectedItem.ToString()); 
            cmd.CommandType = CommandType.Text;

            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read()) 
            {
                textBox2.Text = rd[0].ToString();
                textBox3.Text = rd[1].ToString();
                textBox4.Text = ((DateTime)rd[2]).ToString("dd-MMM-yy");
                textBox5.Text = ((DateTime)rd[3]).ToString("dd-MMM-yy");
                textBox6.Text = rd[4].ToString();
                textBox7.Text = rd[5].ToString();
            }
            rd.Close();
        }
        private void AuctionManagementForm_Closed(object sender, FormClosedEventArgs e)
        {
            conn.Dispose();
        }
    }
}
