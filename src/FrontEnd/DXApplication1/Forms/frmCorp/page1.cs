using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DXApplication1.Forms.Controller;

namespace DXApplication1.Forms.frmCorp
{
    public partial class page1 : Form
    {
        public page1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CorpController.SaveData();
        }

    }
}
