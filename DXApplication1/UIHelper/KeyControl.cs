using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1.UIHelper
{
    public class KeyControl
    {

        public KeyControl() { }

        public static void Send(Control sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.Alt | e.Control | e.Shift)
                return;

            Control oSender = sender;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (oSender is System.Windows.Forms.TextBox
                        || oSender is DevExpress.XtraEditors.ButtonEdit)
                    {

                        if (oSender.Parent is DevExpress.XtraEditors.CalcEdit
                            || oSender is DevExpress.XtraEditors.ComboBoxEdit
                            || oSender.Parent is DevExpress.XtraEditors.ComboBoxEdit
                            || oSender.Parent is DevExpress.XtraEditors.DateEdit
                            || oSender.Parent is DevExpress.XtraEditors.SpinEdit
                            || oSender.Parent is DevExpress.XtraGrid.GridControl
                            || oSender.Parent.Parent is DevExpress.XtraGrid.GridControl)
                        {
                        }
                        else
                        {
                            System.Windows.Forms.TextBox oActive = (System.Windows.Forms.TextBox)oSender;
                            if (oActive.Multiline == false)
                                System.Windows.Forms.SendKeys.Send("+{TAB}");
                        }
                    }
                    break;
                case Keys.Down:
                    if (oSender is System.Windows.Forms.TextBox
                        || oSender is DevExpress.XtraEditors.ButtonEdit)
                    {

                        if (oSender.Parent is DevExpress.XtraEditors.CalcEdit
                            || oSender is DevExpress.XtraEditors.ComboBoxEdit
                            || oSender.Parent is DevExpress.XtraEditors.ComboBoxEdit
                            || oSender.Parent is DevExpress.XtraEditors.DateEdit
                            || oSender.Parent is DevExpress.XtraEditors.SpinEdit
                            || oSender.Parent is DevExpress.XtraGrid.GridControl
                            || oSender.Parent.Parent is DevExpress.XtraGrid.GridControl)
                        {
                        }
                        else
                        {
                            System.Windows.Forms.TextBox oActive = (System.Windows.Forms.TextBox)oSender;
                            if (oActive.Multiline == false)
                                System.Windows.Forms.SendKeys.Send("{TAB}");
                        }

                    }
                    break;
            }
        }


    }
}
