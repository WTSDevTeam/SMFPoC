using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

using DXApplication1.Models;
using DXApplication1.ViewModels;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DXApplication1.Forms.Controller;

namespace DXApplication1.Forms.frmCorp
{
    public partial class index : DevExpress.XtraEditors.XtraForm //Forms.frmBase
    {

        public HttpClient client = new HttpClient();

        int editId { get; set; }
        private CompanyVM oSource;

        public index()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:5002/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            oSource = new CompanyVM(client);

            this.bindGridBrow();

        }

        public void RefreshData()
        {
            bindGridBrow();
        }

        private void bindGridBrow()
        {
            var Companys = oSource.GetCompany();
            this.grdBrowView.DataSource = Companys.ToList();
            for (int intCnt = 0; intCnt < this.gridView1.Columns.Count; intCnt++)
            {
                this.gridView1.Columns[intCnt].Visible = false;
                this.gridView1.Columns[intCnt].OptionsColumn.ReadOnly = true;
            }

            int i = 0;
            this.gridView1.Columns["Name"].VisibleIndex = i++;
            this.gridView1.Columns["Address"].VisibleIndex = i++;
            this.gridView1.Columns["StartDate"].VisibleIndex = i++;

        }

        private void DeleteButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MessageBox.Show("delete button");
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MessageBox.Show(e.Item.Tag.ToString());

            Company oData = this.gridView1.GetFocusedRow() as Company;

            switch (e.Item.Tag.ToString())
            {
                case "NEW":
                    //this.xtraTabControl1.SelectedTabPageIndex = 1;
                    //BlankFormData();

                    Forms.Controller.CorpController.BlankFormData(this);
                    CorpController.showEdit();

                    break;
                case "EDIT":
                    if (oData != null)
                    {
                        //this.xtraTabControl1.SelectedTabPageIndex = 1;
                        //this.LoadFormData(oData.Id);
                        Forms.Controller.CorpController.LoadFormData(this, oData.Id);
                        CorpController.showEdit();

                    }
                    break;
                case "DEL":
                    if (MessageBox.Show("ยืนยันการลบข้อมูล ?", "Application confirm message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        if (oData != null)
                        {
                            deleteData(oData.Id);
                        }

                    }
                    break;

                case "PRINT":
                    Forms.Controller.CorpController.printData(this);

                    break;

            }
        }

        private void deleteData(int Id)
        {

            oSource.DeleteCompany(Id);

            bindGridBrow();
        }


    }
}
