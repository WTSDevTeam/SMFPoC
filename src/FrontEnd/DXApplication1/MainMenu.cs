using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

namespace DXApplication1
{
    static class MainMenu
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMainmenu2());
            Application.Run(new frmMainMenu());

        }

        public static frmReportPreview ofrmReportPreview = null;


        #region "Report Manager"
        public static void PreviewReport(object sender, ReportDocument inReport)
        {
            pmPreviewReport(sender, true, inReport);
        }

        public static void PreviewReport(object sender, bool inShowGroupTree, ReportDocument inReport)
        {
            pmPreviewReport(sender, inShowGroupTree, inReport);
        }

        public static void PreviewReport(ReportDocument inReport)
        {
            pmPreviewReport(null, false, inReport);
        }

        private static void pmPreviewReport(object sender, bool inShowGroupTree, ReportDocument inReport)
        {
            if (ofrmReportPreview != null)
            {
                ofrmReportPreview.Dispose();
            }
            ofrmReportPreview = new frmReportPreview();
            ofrmReportPreview.DisplayGroupTree = inShowGroupTree;
            ofrmReportPreview.InitReport(sender, inReport);
            if (sender != null)
            {
                //ofrmReportPreview.TopMost = true;
                ofrmReportPreview.Show();
            }
        }
        #endregion

    }
}
