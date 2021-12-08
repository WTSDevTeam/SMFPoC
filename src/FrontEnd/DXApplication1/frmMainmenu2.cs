using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class frmMainmenu2 : DevExpress.XtraEditors.XtraForm
    {
        public frmMainmenu2()
        {
            InitializeComponent();

            string strSysMenu = Application.StartupPath + @"\sysdb.mdb";
            strConnectionStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strSysMenu;

            pmGenMenu();
        }

        OleDbConnection con;
        string strConnectionStr = "";
        private DataSet dtsDataEnv = null;

        private void pmGenMenu()
        {

            this.barManager1.Items.Clear();
            DevExpress.XtraBars.BarSubItem oNode = this.pmGetMenuBar("ระบบงาน", "", true);
            this.barManager1.Items.Add(oNode);
            this.bar2.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(oNode));

            DevExpress.XtraBars.BarButtonItem oMenuItem = this.pmGetMenuItem("ออกจากระบบงาน", "EXIT");
            this.barManager1.Items.Add(oMenuItem);
            oNode.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(oMenuItem, false));

            this.barManager1.BeginInit();

            string strErrorMsg = "";

            //OleDbDataReader dtrMenu = Agents.MenuAccess.GetMainMenu();

            OleDbConnection con = new OleDbConnection(this.strConnectionStr);

            try
            {
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "SELECT * FROM SYSMENU WHERE FILETYPE = 'Folder'  AND PARENT = 'M00' ORDER BY SEQNO";
                cmd.Connection = con;
                OleDbDataReader dtrMenu = cmd.ExecuteReader();
                while (dtrMenu.Read())
                {
                    //MessageBox.Show(dtrMenu["PROMPT1"].ToString());

                    string strDesc = dtrMenu["PROMPT1"].ToString();
                    DevExpress.XtraBars.BarSubItem oBar = this.pmGetMenuBar(strDesc, dtrMenu["SEQNO"].ToString().TrimEnd(), true);
                    this.barManager1.Items.Add(oBar);
                    this.bar2.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(oBar));
                    this.pmLoopSubItem(oBar, dtrMenu["SEQNO"].ToString().TrimEnd());
                    int i = oBar.LinksPersistInfo.Count;
                    //oBar.Visibility = (oBar.LinksPersistInfo.Count > 0 ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);

                }

                this.barManager1.EndInit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                con.Close();
            }


            this.barManager1.EndInit();

        }

        private int pmCountMenuItem(DevExpress.XtraBars.BarSubItem inBar)
        {
            int intCount = 0;
            for (int i = 0; i < inBar.LinksPersistInfo.Count; i++)
            {
                DevExpress.XtraBars.LinkPersistInfo lk = inBar.LinksPersistInfo[i];

                DevExpress.XtraBars.BarSubItem oBar2 = lk.Item as DevExpress.XtraBars.BarSubItem;
                if (oBar2 != null)
                {
                    if (oBar2.Links.Count > 0) intCount++;
                }
                else
                {
                    intCount++;
                }

            }
            return intCount;
        }

        private void pmLoopSubItem(DevExpress.XtraBars.BarSubItem inBar, string inSeq)
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
                        DevExpress.XtraBars.BarSubItem oNode = this.pmGetMenuBar(strDesc, dtrMenu["SEQNO"].ToString().TrimEnd(), false);
                        this.barManager1.Items.Add(oNode);
                        inBar.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(oNode, bllIsGroup));
                        this.pmLoopSubItem(oNode, dtrMenu["SEQNO"].ToString().TrimEnd());
                    }
                    else
                    {
                        if (dtrMenu["FILETYPE"].ToString().Trim() == "Program")
                        {
                            string strDesc = dtrMenu["PROMPT1"].ToString().Trim();
                            DevExpress.XtraBars.BarButtonItem oMenuItem = this.pmGetMenuItem(strDesc, dtrMenu["SEQNO"].ToString().TrimEnd());
                            this.barManager1.Items.Add(oMenuItem);
                            inBar.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(oMenuItem, bllIsGroup));

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


        private DevExpress.XtraBars.BarSubItem pmGetMenuBar(string inText, string inTag, bool inIsBold)
        {
            DevExpress.XtraBars.BarSubItem oBar = new DevExpress.XtraBars.BarSubItem();
            oBar.Caption = inText;
            oBar.Tag = inTag;
            oBar.Appearance.Font = new System.Drawing.Font("Tahoma", 9, (inIsBold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular));
            return oBar;
        }

        private DevExpress.XtraBars.BarButtonItem pmGetMenuItem(string inText, string inTag)
        {
            DevExpress.XtraBars.BarButtonItem oBar = new DevExpress.XtraBars.BarButtonItem();
            oBar.Caption = inText;
            oBar.Tag = inTag;
            oBar.Appearance.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular);
            return oBar;
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!(e.Item is DevExpress.XtraBars.BarSubItem))
            {
            }

            if (e.Item.Tag != null)
            {

                string strMenuName = e.Item.Tag.ToString().Trim().ToUpper();
                if (!(e.Item is DevExpress.XtraBars.BarSubItem))
                {
                }

                this.pmRunMenuInput1(e.Item.Tag.ToString(), e.Item.Caption);

            }
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
