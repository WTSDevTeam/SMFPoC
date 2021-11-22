
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DXApplication1.UIHelper
{
    class UIBase
    {

        private static bool stop = false;
        private static Thread thread;

        private static DevExpress.Utils.WaitDialogForm dlgWaitWind = null;
        private static void pmCreateWaitDialog()
        {
            dlgWaitWind = new DevExpress.Utils.WaitDialogForm("Loading Components...");
        }

        public static void WaitWind(string inText)
        {
            if (dlgWaitWind == null)
            {
                pmCreateWaitDialog();
            }

            //stop = false;
            //thread = new Thread(new ThreadStart(ptStartProcess));
            //thread.Start();

            dlgWaitWind.Caption = inText;

        }

        public static void WaitClear()
        {
            //if (dlgWaitWind != null) 
            //   dlgWaitWind.Close();

            //ptEndProcess();
            dlgWaitWind.Hide();
        }

        private static void ptStartProcess()
        {
            Thread.Sleep(400);
            if (stop)
                return;

            dlgWaitWind.Show();

            try
            {
                while (!stop)
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                }
            }
            catch
            {
            }
        }

        private static void ptEndProcess()
        {
            stop = true;
            thread.Join();
        }

        public static string GetThaiMonth(int inMth)
        {
            string[] aMth1 = new string[] { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
            return aMth1[inMth - 1];
        }

        public static string GetThaiMonthLong(int inMth)
        {
            string[] aMth1 = new string[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
            return aMth1[inMth - 1];
        }

        public static string GetThaiDate(DateTime inDate, string inFormat)
        {
            string strDate = inFormat;
            //string[] aMth1 = new string[] { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
            //return aMth1[inMth - 1];
            strDate = strDate.Replace("yyyy", (inDate.Year + 543).ToString("0000"));
            strDate = strDate.Replace("yy", (inDate.Year + 543).ToString("0000").Substring(2, 2));
            strDate = strDate.Replace("MMMM", GetThaiMonthLong(inDate.Month));
            strDate = strDate.Replace("MM", GetThaiMonth(inDate.Month));
            strDate = strDate.Replace("dd", inDate.Day.ToString("00"));
            strDate = strDate.Replace("d", inDate.Day.ToString());
            return strDate;
        }

        public static void SetDefaultGridViewAppreance(DevExpress.XtraGrid.Views.Grid.GridView inSender)
        {

            inSender.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            inSender.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            inSender.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));

            inSender.Appearance.FocusedCell.Options.UseFont = true;
            inSender.Appearance.FocusedRow.Options.UseFont = true;
            inSender.Appearance.HeaderPanel.Options.UseFont = true;
            inSender.Appearance.HeaderPanel.Options.UseTextOptions = true;

            if (inSender.OptionsBehavior.Editable == true)
            {
                inSender.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(255, 238, 153);
                inSender.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(255, 238, 153);
                inSender.Appearance.FocusedCell.Options.UseBackColor = true;
            }

        }

        public static void SetDefaultChildAppreance(Control inSender)
        {

            foreach (Control oSender in inSender.Controls)
            {

                if (oSender.Controls.Count > 1)
                {
                    SetDefaultChildAppreance(oSender);
                    if (oSender is DevExpress.XtraGrid.GridControl)
                    {
                        DevExpress.XtraGrid.GridControl oGrid = oSender as DevExpress.XtraGrid.GridControl;
                        if (oGrid.Views.Count > 0)
                        {
                            for (int i = 0; i < oGrid.Views.Count; i++)
                            {
                                DevExpress.XtraGrid.Views.Grid.GridView gv = oGrid.Views[i] as DevExpress.XtraGrid.Views.Grid.GridView;
                                if (gv != null)
                                {
                                    SetDefaultGridViewAppreance(gv);
                                }
                            }
                        }
                    }
                }
                else
                {

                    if (oSender.Parent is DevExpress.XtraEditors.CalcEdit
                        || oSender is DevExpress.XtraEditors.ButtonEdit
                        || oSender is DevExpress.XtraEditors.ComboBoxEdit
                        || oSender.Parent is DevExpress.XtraEditors.DateEdit
                        || oSender.Parent is DevExpress.XtraEditors.SpinEdit
                        || oSender.Parent.Parent is DevExpress.XtraGrid.GridControl)
                    {

                        DevExpress.XtraEditors.BaseEdit oSetCtrl = oSender as DevExpress.XtraEditors.BaseEdit;
                        if (oSetCtrl != null)
                        {
                            //oSetCtrl.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
                            oSetCtrl.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                            //oSetCtrl.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Regular);
                            oSetCtrl.Properties.Appearance.BackColor = System.Drawing.Color.White;
                            //oSetCtrl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Moccasin;
                            //oSetCtrl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(197, 217, 241);  //Dark Blue
                            //oSetCtrl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LemonChiffon;
                            oSetCtrl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(255, 238, 153);  //เขียวแบบ SAP

                            oSetCtrl.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.ActiveBorder;
                            oSetCtrl.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;

                        }

                        if (oSender.Parent is DevExpress.XtraEditors.CalcEdit
                            || oSender.Parent is DevExpress.XtraEditors.DateEdit
                            || oSender.Parent is DevExpress.XtraEditors.SpinEdit
                            || oSender is DevExpress.XtraEditors.ComboBoxEdit
                            || oSender is DevExpress.XtraEditors.CalcEdit
                            || oSender is DevExpress.XtraEditors.DateEdit
                            || oSender is DevExpress.XtraEditors.SpinEdit)
                        {
                            oSetCtrl.KeyDown += new KeyEventHandler(txtSpin_KeyDown);
                        }

                    }
                    else if (oSender is System.Windows.Forms.Label)
                    {
                        System.Windows.Forms.Label oSetCtrl = oSender as System.Windows.Forms.Label;
                        if (oSetCtrl != null)
                        {
                            bool bllIsUnderLine = oSetCtrl.Font.Underline;
                            bool bllIsItalic = oSetCtrl.Font.Italic;
                            bool bllIsStrikeout = oSetCtrl.Font.Strikeout;

                            System.Drawing.FontStyle fs = System.Drawing.FontStyle.Bold;
                            if (oSetCtrl.Font.Underline)
                            {
                                fs = fs | System.Drawing.FontStyle.Underline;
                            }
                            if (oSetCtrl.Font.Italic)
                            {
                                fs = fs | System.Drawing.FontStyle.Italic;
                            }
                            if (oSetCtrl.Font.Strikeout)
                            {
                                fs = fs | System.Drawing.FontStyle.Strikeout;
                            }

                            //oSetCtrl.Font = new System.Drawing.Font("Tahoma", 10, fs);
                            //oSetCtrl.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Regular);
                            oSetCtrl.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                        }
                    }


                }

            }
        }

        private static void txtSpin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (sender is DevExpress.XtraEditors.ComboBoxEdit)
                    {
                        DevExpress.XtraEditors.ComboBoxEdit oSender = sender as DevExpress.XtraEditors.ComboBoxEdit;
                        if (oSender != null && oSender.IsPopupOpen == false)
                        {
                            System.Windows.Forms.SendKeys.Send("+{TAB}");
                            e.Handled = true;
                        }
                    }
                    else if (sender is DevExpress.XtraEditors.DateEdit)
                    {
                        DevExpress.XtraEditors.DateEdit oSender = sender as DevExpress.XtraEditors.DateEdit;
                        if (oSender != null && oSender.IsPopupOpen == false)
                        {
                            System.Windows.Forms.SendKeys.Send("+{TAB}");
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.SendKeys.Send("+{TAB}");
                        e.Handled = true;
                    }
                    break;
                case Keys.Down:
                    if (sender is DevExpress.XtraEditors.ComboBoxEdit)
                    {
                        DevExpress.XtraEditors.ComboBoxEdit oSender = sender as DevExpress.XtraEditors.ComboBoxEdit;
                        if (oSender != null && oSender.IsPopupOpen == false)
                        {
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                            e.Handled = true;
                        }
                    }
                    else if (sender is DevExpress.XtraEditors.DateEdit)
                    {
                        DevExpress.XtraEditors.DateEdit oSender = sender as DevExpress.XtraEditors.DateEdit;
                        if (oSender != null && oSender.IsPopupOpen == false)
                        {
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.SendKeys.Send("{TAB}");
                        e.Handled = true;
                    }
                    break;
                case Keys.Delete:
                    if (sender is DevExpress.XtraEditors.DateEdit)
                    {
                        DevExpress.XtraEditors.DateEdit oSender = sender as DevExpress.XtraEditors.DateEdit;
                        if (oSender != null && oSender.Properties.AllowNullInput == DevExpress.Utils.DefaultBoolean.True)
                        {
                            oSender.EditValue = null;
                            e.Handled = true;
                        }
                    }
                    break;
            }

            //if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            //{
            //    switch (e.KeyCode)
            //    {
            //        case Keys.Up:
            //            System.Windows.Forms.SendKeys.Send("+{TAB}");
            //            break;
            //        case Keys.Down:
            //            System.Windows.Forms.SendKeys.Send("{TAB}");
            //            break;
            //    }
            //    e.Handled = true;
            //}

        }

        public static string GetCostText(string inType)
        {
            string strRetStr = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\Support\\Costing.txt"))
            {
                string strFileName = Application.StartupPath + "\\Support\\Costing.txt";

                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(strFileName, System.Text.Encoding.Default);
                    string strLine = "";
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        string[] staText = strLine.Split("=".ToCharArray());
                        if (staText.Length > 0 && staText[0].TrimEnd() == inType)
                        {
                            strRetStr = staText[1].TrimEnd();
                            break;
                        }

                    }
                    sr.Close();
                }
                catch { }

            }
            return strRetStr;
        }

    }
}
