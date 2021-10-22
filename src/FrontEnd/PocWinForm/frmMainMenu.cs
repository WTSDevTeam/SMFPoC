using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PocWinForm
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReportPreview oPreviewReport = new frmReportPreview();
            oPreviewReport.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Forms.frmCompany ofrm = new Forms.frmCompany();
            ofrm.ShowDialog();
        }
    }
}
