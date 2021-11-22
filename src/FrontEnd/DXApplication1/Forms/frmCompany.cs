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

namespace DXApplication1.Forms
{
    public partial class frmCompany : Forms.frmBase
    {


        public HttpClient client = new HttpClient();

        int editId { get; set; }
        private CompanyVM oSource;

        public frmCompany()
        {
            InitializeComponent();

            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            client.BaseAddress = new Uri("http://localhost:5002/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            oSource = new CompanyVM(client);

            this.bindGridBrow();

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

            //gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn()
            //{
            //    Caption = "Custom Unbound Column",
            //    FieldName = "UnboundColumn3",
            //    //UnboundDataType = typeof(string),
            //    Visible = true
            //});

            //RepositoryItemButtonEdit deleteButton = new RepositoryItemButtonEdit();
            //this.grdBrowView.RepositoryItems.Add(deleteButton);
            //deleteButton.ButtonClick += DeleteButton_ButtonClick;
            //deleteButton.Buttons[0].Caption = "...";

            ////this.gridView1.Columns["UnboundColumn3"].ColumnEdit = deleteButton;
            //this.gridView1.Columns["UnboundColumn3"].ColumnEdit = grcEdit2;


            //AddHandler deleteButton.ButtonClick, AddressOf deleteButton_ButtonClick
            //GridControl1.RepositoryItems.Add(deleteButton)
            //editButton = New RepositoryItemButtonEdit
            //AddHandler editButton.ButtonClick, AddressOf editButton_ButtonClick
            //GridControl1.RepositoryItems.Add(editButton)

        }

        private void DeleteButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MessageBox.Show("delete button");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 1;
            }
            else {
                this.xtraTabControl1.SelectedTabPageIndex = 0;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.fn_GoToGrowPage();
        }

        private void fn_GoToGrowPage() {

            if (this.xtraTabControl1.SelectedTabPageIndex != 0)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 0;
            }

        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MessageBox.Show(e.Item.Tag.ToString());

            Company oData = this.gridView1.GetFocusedRow() as Company;

            switch (e.Item.Tag.ToString()) {
                case "NEW":
                    this.xtraTabControl1.SelectedTabPageIndex = 1;
                    BlankFormData();
                    break;
                case "EDIT":
                    if (oData != null)
                    {
                        this.xtraTabControl1.SelectedTabPageIndex = 1;
                        this.LoadFormData(oData.Id);
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
                    this.printData();
                    break;

            }
        }

        private void BlankFormData()
        {

            this.editId = -1;

            this.txtName.Text = "";
            this.txtAddress.Text = "";
            this.txtCountry.Text = "";
            this.startDate.DateTime = DateTime.Now;

        }

        private void LoadFormData(int Id) {

            this.BlankFormData();

            var Company = oSource.GetById(Id);

            this.editId = Id;
            this.txtName.Text = Company.Name;
            this.txtAddress.Text = Company.Address;
            this.txtCountry.Text = Company.Country;

        }

        private void deleteData(int Id)
        {

            oSource.DeleteCompany(Id);

            bindGridBrow();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Company saveCompany = new Company()
            {
                Name = txtName.Text,
                Address = txtAddress.Text,
                Country = txtCountry.Text,
                StartDate = this.startDate.DateTime
            };

            if (editId > 0)
            {
                saveCompany.Id = editId;
                oSource.UpdateCompany(saveCompany);
                //UpdateCompany(saveCompany);
            }
            else
            {
                //CreateCompany(saveCompany);
                oSource.CreateCompany(saveCompany);
            }
            bindGridBrow();
            fn_GoToGrowPage();
        }


        private void printData()
        {
            string strRPTFileName = "";

            strRPTFileName = Application.StartupPath + @"\RPT\company.rpt";

            ReportDocument rptPreviewReport = new ReportDocument();
            if (!System.IO.File.Exists(strRPTFileName))
            {
                MessageBox.Show("File not found " + strRPTFileName, "Application Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            rptPreviewReport.Load(strRPTFileName);

            //Reports.dtsAppReport ReportData = new Reports.dtsAppReport();
            DataSet ReportData = new Reports.dtsAppReport();
            DataSet PreviewData = new DataSet();

            //var Companys = GetCompany();
            var Companys = oSource.GetCompany();
            foreach (var report in Companys)
            {
                DataRow dtrNew = ReportData.Tables["Company"].NewRow();
                ReportData.Tables["Company"].Rows.Add(dtrNew);
                dtrNew["Name"] = report.Name;
                dtrNew["Address"] = report.Address;
                dtrNew["Country"] = report.Country;
            }

            DataTable dtReport = ReportData.Tables["Company"].Copy();
            PreviewData.Tables.Add(dtReport);


            rptPreviewReport.SetDataSource(ReportData);

            MainMenu.PreviewReport(this, false, rptPreviewReport);

        }

    }
}
