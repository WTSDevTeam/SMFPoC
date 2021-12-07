
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

using System.Data.OleDb;

namespace DXApplication1.Agents
{
    public class MenuAccess
    {
        public static OleDbDataReader GetMainMenu()
        {

            OleDbDataReader dr = null;

            if (System.IO.File.Exists(Application.StartupPath + @"\sysdb.mdb"))
            {

                string strSysMenu = Application.StartupPath + @"\sysdb.mdb";
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strSysMenu);

                try
                {
                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "SELECT * FROM SYSMENU WHERE FILETYPE = 'Folder'  AND PARENT = 'M00' ORDER BY SEQNO";
                    cmd.Connection = con;
                    dr = cmd.ExecuteReader();
                    //while (dr.Read())
                    //{
                    //    MessageBox.Show(dr["PROMPT1"].ToString());
                    //}

                    //MessageBox.Show("Record Submitted", "Congrats");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
                finally
                {
                    con.Close();
                }

            }
            return dr;
        }
    }
}
