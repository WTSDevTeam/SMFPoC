using PocWinForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;


using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Data;

namespace PocWinForm.Forms
{
    public partial class frmCompany : Form
    {
        public HttpClient client = new HttpClient();

        int editId { get; set; }

        public frmCompany()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:5002/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void frmCompany_Load(object sender, EventArgs e)
        {
            bindGridBrow();
        }

        private void bindGridBrow()
        {
            var Companys = GetCompany();
            VMBindingSource.DataSource = Companys.ToList();
            dataGridView2.Refresh();
        }

        private IEnumerable<Company> GetCompany(string path = "api/company")
        {
            IEnumerable<Company> Company = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                Company = response.Content.ReadAsAsync<IEnumerable<Company>>().Result;
            }
            return Company;
        }

        Uri CreateCompany(Company Company)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("api/company", Company).Result;
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        private Company UpdateCompany(Company Company)
        {
            HttpResponseMessage response = client.PutAsJsonAsync($"api/company/{Company.Id}", Company).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            Company = response.Content.ReadAsAsync<Company>().Result;
            return Company;
        }

        HttpStatusCode DeleteCompany(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/company/{id}").Result;
            return response.StatusCode;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabBrowse;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Company saveCompany = new Company()
            {
                Name = txtName.Text,
                Address = txtAddress.Text,
                Country = txtCountry.Text
            };
            if (editId > 0)
            {
                saveCompany.Id = editId;
                UpdateCompany(saveCompany);
            }
            else
                CreateCompany(saveCompany);
            bindGridBrow();
            tabMain.SelectedTab = tabBrowse;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            editId = 0;
            blankEditor();
            tabMain.SelectedTab = tabEdit;
        }

        private void blankEditor()
        {
            txtName.Clear();
            txtAddress.Clear();
            //txtTitle.Clear();
            //txtAuthor.Clear();
            //dtpPublish.Value = DateTime.Now.Date;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Company selData = dataGridView2.SelectedRows[0].DataBoundItem as Company;
            if (selData == null) { return; }
            editId = selData.Id;
            loadEditor(selData);
            tabMain.SelectedTab = tabEdit;
        }

        private void loadEditor(Company selData)
        {
            txtName.Text = selData.Name;
            txtAddress.Text = selData.Address;
            txtCountry.Text = selData.Country;
            //txtTitle.Text = selData.Title;
            //txtAuthor.Text = selData.Author;
            //dtpPublish.Value = selData.PublishedDate;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Company selData = dataGridView2.SelectedRows[0].DataBoundItem as Company;
            if (selData == null) { return; }
            DeleteCompany(selData.Id);
            bindGridBrow();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bindGridBrow();
        }

        private void btnPrint_Click(object sender, EventArgs e)
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

            DataSet inData = new DataSet();

            //rptPreviewReport.SetDataSource(inData);

            MainMenu.PreviewReport(this, false, rptPreviewReport);

        }
    }
}
