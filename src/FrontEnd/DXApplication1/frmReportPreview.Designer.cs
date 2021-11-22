
namespace DXApplication1
{
    partial class frmReportPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
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
            this.crReportViewer.Size = new System.Drawing.Size(800, 450);
            this.crReportViewer.TabIndex = 0;
            // 
            // frmReportPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crReportViewer);
            this.Name = "frmReportPreview";
            this.Text = "frmReportPreview";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crReportViewer;
    }
}