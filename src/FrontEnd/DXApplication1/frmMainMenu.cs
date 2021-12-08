using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;

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

            string strSysMenu = Application.StartupPath + @"\sysdb.mdb";
            strConnectionStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strSysMenu;

            this.pmLoadMenu();

        }


        OleDbConnection con;
        string strConnectionStr = "";
        private DataSet dtsDataEnv = null;



        private void pmGenMenu(string sysname, string sub_sysname)
        {


            string strErrorMsg = "";

            OleDbConnection con = new OleDbConnection(this.strConnectionStr);

            try
            {

                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                //cmd.CommandText = "SELECT * FROM SYSMENU WHERE FILETYPE = 'Folder'  AND PARENT = 'M00' ORDER BY SEQNO";
                cmd.CommandText = "SELECT * FROM SYSMENU WHERE SYS_NAME = @sysname AND SUB_SYSNAME = @sub_sysname AND FILETYPE = 'Folder' and PARENT <> 'M00'  ORDER BY SEQNO";
                cmd.Parameters.AddWithValue("@sysname", sysname);
                cmd.Parameters.AddWithValue("@sub_sysname", sub_sysname);


                cmd.Connection = con;

                this.navBarControl1.Groups.Clear();

                OleDbDataReader dtrMenu = cmd.ExecuteReader();
                while (dtrMenu.Read())
                {

                    string strDesc = dtrMenu["PROMPT1"].ToString();

                    DevExpress.XtraNavBar.NavBarGroup navBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
                    navBarGroup.Caption = strDesc;
                    navBarGroup.Name = dtrMenu["SEQNO"].ToString();
                    navBarGroup.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsList;

                    pmLoopSubItem(navBarGroup, dtrMenu["SEQNO"].ToString());
                    this.navBarControl1.Groups.Add(navBarGroup);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                con.Close();
            }

        }


        private void pmLoopSubItem(DevExpress.XtraNavBar.NavBarGroup inBar, string inSeq)
        {

            string strErrorMsg = "";

            OleDbConnection con = new OleDbConnection(this.strConnectionStr);

            try
            {

                bool bllIsGroup = false;

                OleDbCommand cmd = con.CreateCommand();
                con.Open();

                cmd.CommandText = "SELECT * FROM SYSMENU WHERE PARENT = @parent ORDER BY SEQNO";

                cmd.Parameters.AddWithValue("@parent", inSeq);
                cmd.Connection = con;
                OleDbDataReader dtrMenu = cmd.ExecuteReader();

                while (dtrMenu.Read())
                {
                    if (dtrMenu["FILETYPE"].ToString().Trim() == "Folder")
                    {
                        string strDesc = dtrMenu["PROMPT1"].ToString().Trim();
                    }
                    else
                    {
                        if (dtrMenu["FILETYPE"].ToString().Trim() == "Program")
                        {
                            string strDesc = dtrMenu["PROMPT1"].ToString().Trim();
                            DevExpress.XtraNavBar.NavBarItem item1 = new DevExpress.XtraNavBar.NavBarItem();
                            item1.Caption = strDesc;
                            item1.Name = dtrMenu["SEQNO"].ToString().TrimEnd();
                            item1.Tag = dtrMenu["SEQNO"].ToString().TrimEnd();
                            inBar.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {new DevExpress.XtraNavBar.NavBarItemLink(item1)});

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                con.Close();
            }
        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }


        private void xtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            this.pmVisibleAbout();
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            this.pmVisibleAbout();
        }

        private void pmVisibleAbout()
        {
            this.pnlAbout.Visible = (this.MdiChildren.Length == 0);
        }

        private void pnlAbout_SizeChanged(object sender, EventArgs e)
        {
            //this.pnlCustomer.Location = new Point(Convert.ToInt32((this.pnlAbout.Width - this.pnlCustomer.Width) / 2), this.pnlAbout.Height - this.pnlCustomer.Height - 30);
        }



        private void ribbonControl1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MessageBox.Show(e.Item.Tag.ToString());

            string strTaskName = e.Item.Tag.ToString();
            switch (strTaskName.ToUpper())
            {
                case "ECOMPANY":
                    //Forms.frmCompany ofrmCompany = new Forms.frmCompany();
                    //ofrmCompany.MdiParent = this;
                    //ofrmCompany.Show();
                    Forms.Controller.CorpController.show(this);
                    break;
                case "ECUSTOMER":
                    frmMainmenu2 ofrmTest = new frmMainmenu2();
                    //ofrmTest.MdiParent = this;
                    ofrmTest.Show();
                    break;
            }
        }

        private void rbcMainMenu_SelectedPageChanged(object sender, EventArgs e)
        {

            //reload menu
            string strSystemName = rbcMainMenu.SelectedPage.Tag.ToString();
            string[] aSystem = strSystemName.Split(',');

            if (aSystem.Length > 1)
            {
                this.pmGenMenu(aSystem[0], aSystem[1]);
            }
            else
            {
                this.pmGenMenu(aSystem[0], aSystem[0]);
            }

        }

        private void pmLoadMenu()
        {
            //reload menu
            string strSystemName = rbcMainMenu.SelectedPage.Tag.ToString();
            string[] aSystem = strSystemName.Split(',');

            if (aSystem.Length > 1)
            {
                this.pmGenMenu(aSystem[0], aSystem[1]);
            }
            else
            {
                this.pmGenMenu(aSystem[0], aSystem[0]);
            }
        }

        private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pmRunMenuInput1(e.Link.Item.Tag.ToString(), e.Link.Item.Caption);
        }

        private void pmRunMenuInput1(string inTag, string inMenuName)
        {
            switch (inTag.ToUpper())
            {
                case "M00000101":
                    Forms.Controller.CorpController.show(this);
                    break;
                case "EXIT":
                    this.Close();
                    break;

            }
        }


    }
}
