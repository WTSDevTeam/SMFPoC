
namespace PocWinForm
{
    partial class frmReportPreview
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.crReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReport11 = new PocWinForm.CrystalReport1();
            this.SuspendLayout();
            // 
            // crReportViewer
            // 
            this.crReportViewer.ActiveViewIndex = -1;
            this.crReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crReportViewer.Location = new System.Drawing.Point(0, 0);
            this.crReportViewer.Name = "crReportViewer";
            this.crReportViewer.Size = new System.Drawing.Size(799, 566);
            this.crReportViewer.TabIndex = 0;
            // 
            // frmReportPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 566);
            this.Controls.Add(this.crReportViewer);
            this.Name = "frmReportPreview";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crReportViewer;
        private CrystalReport1 CrystalReport11;
    }
}

