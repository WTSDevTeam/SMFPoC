using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PocWinForm
{
    public partial class frmReportPreview : Form
    {

        public frmReportPreview()
        {
            InitializeComponent();
        }


        private System.Windows.Forms.Form mParentForm = null;

        /// <summary>
        /// แสดง Group Tree
        /// </summary>
        public bool DisplayGroupTree
        {
            get { return this.crReportViewer.ToolPanelView != CrystalDecisions.Windows.Forms.ToolPanelViewType.None; }
            set
            {
                if (value)
                {
                    this.crReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.GroupTree;
                }
                else
                {
                    this.crReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
                }
            }
        }

        public void ClearReportParent()
        {
            if (this.mParentForm != null)
                this.mParentForm = null;
        }

        public void InitReport(object sender, ReportDocument inReport)
        {
            this.mParentForm = (System.Windows.Forms.Form)sender;
            this.crReportViewer.ReportSource = inReport;
        }

        public void InitReport(object sender, ReportDocument inReport, bool inPrint)
        {
            this.mParentForm = (System.Windows.Forms.Form)sender;
            this.crReportViewer.ReportSource = inReport;
            if (inPrint)
            {
                this.crReportViewer.PrintReport();
            }
        }

        private void frmReportPreview_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                this.mParentForm.Activate();
                //App.SetForegroundWindow(App.mSave_hWnd);
            }
        }

    }
}
