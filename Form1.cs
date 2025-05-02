using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        CrystalReport1 CR;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CR = new CrystalReport1();
            foreach(ParameterDiscreteValue v in CR.ParameterFields.DefaultValues)
            {
                comboBox1.Items.Add(v.Value);
            }
        }

        private void GenerateReport_btn_Click(object sender, EventArgs e)
        {
            CR.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer1.ReportSource = CR;
        }

     
    }
}
