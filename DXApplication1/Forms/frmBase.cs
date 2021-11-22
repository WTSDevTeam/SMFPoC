using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1.Forms
{
    public partial class frmBase : DevExpress.XtraEditors.XtraForm
    {
        public frmBase()
        {
            InitializeComponent();
            UIHelper.UIBase.SetDefaultChildAppreance(this);
        }

        private void frmBase_KeyDown(object sender, KeyEventArgs e)
        {

            //UIHelper.KeyControl.Send(this.ActiveControl, e);
            UIHelper.KeyControl.Send(this.ActiveControl, e);

        }
    }
}
