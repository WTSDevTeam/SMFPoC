using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class frmMainMenu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void ribbonControl1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MessageBox.Show(e.Item.Tag.ToString());

            string strTaskName = e.Item.Tag.ToString();
            switch (strTaskName.ToUpper())
            {
                case "ECOMPANY":
                    Forms.frmCompany ofrmCompany = new Forms.frmCompany();
                    ofrmCompany.MdiParent = this;
                    ofrmCompany.Show();
                    break;
                case "ECUSTOMER":
                    frmMainmenu2 ofrmTest = new frmMainmenu2();
                    //ofrmTest.MdiParent = this;
                    ofrmTest.Show();
                    break;
            }
        }
    }
}
