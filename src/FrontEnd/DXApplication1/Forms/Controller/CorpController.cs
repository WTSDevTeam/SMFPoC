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

namespace DXApplication1.Forms.Controller
{
    public class CorpController
    {
        public static void show(DevExpress.XtraEditors.XtraForm parent)
        {
            if (CorpController.ofrmCompany == null) {
                CorpController.ofrmCompany = new Forms.frmCorp.index();

                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5002/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                oSource = new CompanyVM(client);

            }
            CorpController.ofrmCompany.MdiParent = parent;
            CorpController.ofrmCompany.Show();
        }

        public static void showEdit()
        {
            CorpController.ofrmEdit1.ShowDialog();
        }

        private static Forms.frmCorp.index ofrmCompany;
        private static Forms.frmCorp.page1 ofrmEdit1;

        public static HttpClient client = null;

        public static int editId { get; set; }
        private static CompanyVM oSource;

        public static void ClearInstanse()
        {
            if (CorpController.ofrmCompany != null)
            {
                CorpController.ofrmCompany = null;
                CorpController.client.Dispose();
                CorpController.client = null;
            }
        }

        public static void BlankFormData(DevExpress.XtraEditors.XtraForm parent)
        {
            if (CorpController.ofrmEdit1 == null)
            {
                CorpController.ofrmEdit1 = new Forms.frmCorp.page1();
            }

            CorpController.editId = -1;
            CorpController.ofrmEdit1.txtName.Text = "";
            CorpController.ofrmEdit1.txtAddress.Text = "";
            CorpController.ofrmEdit1.txtCountry.Text = "";
            CorpController.ofrmEdit1.startDate.EditValue = null;
        }


        public static void LoadFormData(DevExpress.XtraEditors.XtraForm parent,int Id)
        {

            Forms.Controller.CorpController.BlankFormData(parent);

            var Company = oSource.GetById(Id);

            CorpController.editId = Id;
            CorpController.ofrmEdit1.txtName.Text = Company.Name;
            CorpController.ofrmEdit1.txtAddress.Text = Company.Address;
            CorpController.ofrmEdit1.txtCountry.Text = Company.Country;

        }

        public static void SaveData()
        {
            Company saveCompany = new Company()
            {
                Name = CorpController.ofrmEdit1.txtName.Text,
                Address = CorpController.ofrmEdit1.txtAddress.Text,
                Country = CorpController.ofrmEdit1.txtCountry.Text,
                StartDate = CorpController.ofrmEdit1.startDate.DateTime
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
            
            fn_GoToGrowPage();
        }


        private static void fn_GoToGrowPage()
        {
            CorpController.ofrmEdit1.Hide();
            CorpController.ofrmCompany.RefreshData();
        }


        public static void printData(DevExpress.XtraEditors.XtraForm parent)
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

            MainMenu.PreviewReport(parent, false, rptPreviewReport);

        }

    }
}
